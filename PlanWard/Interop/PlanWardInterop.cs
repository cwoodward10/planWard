using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CefSharp;
using CefSharp.WinForms;

using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

using PlanWard.Interop.Models;
using PlanWard.DataAccounting;
using PlanWard.DataAccounting.Models.Core;
using PlanWard.DataAccounting.Utilities;
using PlanWard.DataAccounting.Models;

namespace PlanWard.Interop
{
    public class PlanWardInterop
    {
        public ChromiumWebBrowser Browser { get; private set; }
        public PlanWardInterop(ChromiumWebBrowser browser)
        {
            Browser = browser;

        }

        #region To UI (Generic)
        /// <summary>
        /// Sends a JSON string as args to the given function name in the EventBus attached to the browser
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="jsonString"></param>
        public void NotifyFrame(string functionName, string jsonString)
        {

            string script = $"if (window.EventBus) {{ window.EventBus.{functionName}('{jsonString}'); }}";
            try
            {
                Browser.GetMainFrame().EvaluateScriptAsync(script);
            }
            catch
            {
                Debug.WriteLine("For some reason, this browser was not initialised.");
            }
        }

        /// <summary>
        /// Sends a Message to the Frontend
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type">Should be "Success", "Warning", or "Error". Will send "Warning" if none of those.</param>
        /// <param name="timeout"></param>
        public void SendMessageToFrontend(string message, string type, double timeout)
        {
            string protectedType = type;
            if (type != "Success" || type != "Warning" || type != "Error")
            {
                protectedType = "Warning";
            }

            string script = $"window.EventBus.SendApplicationMessage('{message}', '{type}', {timeout})";
            try
            {
                Browser.GetMainFrame().EvaluateScriptAsync(script);
            }
            catch
            {
                Debug.WriteLine("For some reason, this browser was not initialised.");
            }
        }
        #endregion

        #region To UI

        /// <summary>
        /// Calls the UI Event Bus method for setting the selected Planward Objects
        /// </summary>
        /// <param name="objects"></param>
        public void SendSelectedObjects(IEnumerable<RhinoObject> objects)
        {
            IEnumerable<IRhinoInteroperable> interopObjects = InteropUtilities.ConvertRhinoObjectsToInteropable(objects);
            string data = JsonConvert.SerializeObject(interopObjects);
            NotifyFrame("SetSelectedObjects", data);
        }

        /// <summary>
        /// Calls the UI Event Bus method for setting Planward Object information
        /// </summary>
        /// <param name="objects"></param>
        public void UpdatePlanWardDataAccounting(IEnumerable<IRhinoInteroperable> objects)
        {
           string data = JsonConvert.SerializeObject(objects);
            NotifyFrame("UpdatePlanWardDataAccounting", data);
        }

        #endregion

        #region From UI

        /// <summary>
        /// Gathers all active Planward Objects and sends them to the UI.
        /// </summary>
        public void RefreshInformation()
        {
            IEnumerable<IRhinoInteroperable> planwardObjects = InteropUtilities.GetActivePlanWardObjects(RhinoDoc.ActiveDoc);
            UpdatePlanWardDataAccounting(planwardObjects);
        }

        /// <summary>
        /// Gets updated information from the UI. Tries to apply that information to the Rhino Objects. 
        /// Then refreshes the information in the UI and sends a message of succss or failure.
        /// </summary>
        /// <param name="jsonData"></param>
        public void UpdateObjectUserDictionary(string jsonData)
        {
            IEnumerable<JObject> parsedObjects = JsonConvert.DeserializeObject<IEnumerable<JObject>>(jsonData);
            IEnumerable<PlanWardObject> pwObjects = parsedObjects.Select(po => po.ToPlanWardObject()).Where(p => p != null);

            bool triggerAutoUpdate = false;
            int failureCount = 0;
            foreach (PlanWardObject pwo in pwObjects)
            {
                bool success = pwo.TrySetTrackedInfo(RhinoDoc.ActiveDoc);
                if (!success)
                {
                    failureCount++;
                }
                // if planward type if design option container, assume the name changed and we need to update
                // todo add another && here to check settings if should auto update
                else if (success && pwo.PlanWardType == PlanWardTypes.DesignOptionContainer)
                {
                    triggerAutoUpdate = true;
                }
            }

            if (triggerAutoUpdate)
            {
                InteropUtilities.SyncWithDesignOptionNameChange(RhinoDoc.ActiveDoc, pwObjects.Where(obj => obj.PlanWardType == PlanWardTypes.DesignOptionContainer).Cast<DesignOptionContainer>());
            }

            // unless all failed, refresh information
            if (failureCount != pwObjects.Count())
            {
                RefreshInformation();
            }

            // send messages regarding success or failure
            if (failureCount == 0)
            {
                SendMessageToFrontend("Objects Updated!", "Success", 2000);
            } else if (failureCount < pwObjects.Count())
            {
                SendMessageToFrontend($"Warning: {failureCount} Objects Failed to Update", "Warning", 3000);
            } else
            {
                SendMessageToFrontend($"Error Updating Objects", "Error", 3000);
            }
        }

        #endregion
    }
}
