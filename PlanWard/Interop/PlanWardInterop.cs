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

            string script = $"window.EventBus.{functionName}('{jsonString}')";
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

        public void SendSelectedObjects(IEnumerable<RhinoObject> objects)
        {
            IEnumerable<IRhinoInteroperable> interopObjects = InteropUtilities.ConvertRhinoObjectsToInteropable(objects);
            string data = JsonConvert.SerializeObject(interopObjects);
            NotifyFrame("SetSelectedObjects", data);
        }

        public void UpdatePlanWardDataAccounting(IEnumerable<IRhinoInteroperable> objects)
        {
           string data = JsonConvert.SerializeObject(objects);
            NotifyFrame("UpdatePlanWardDataAccounting", data);
        }

        #endregion

        #region From UI

        public void RefreshInformation()
        {
            IEnumerable<IRhinoInteroperable> planwardObjects = InteropUtilities.GetActivePlanWardObjects(RhinoDoc.ActiveDoc);
            UpdatePlanWardDataAccounting(planwardObjects);
        }

        public void UpdateObjectUserDictionary(string jsonData)
        {
            IEnumerable<JObject> parsedObjects = JsonConvert.DeserializeObject<IEnumerable<JObject>>(jsonData);
            IEnumerable<PlanWardObject> pwObjects = parsedObjects.Select(po => po.ToPlanWardObject()).Where(p => p != null);

            int failureCount = 0;
            foreach (PlanWardObject plo in pwObjects)
            {
                bool success = plo.TrySetTrackedInfo(RhinoDoc.ActiveDoc);
                if (!success)
                {
                    failureCount++;
                }
            }

            RefreshInformation();
            if (failureCount == 0)
            {
                SendMessageToFrontend("Objects Updated!", "Success", 500);
            } else if (failureCount < pwObjects.Count())
            {
                SendMessageToFrontend($"Failed to Update {failureCount} Objects", "Warning", 1000);
            } else
            {
                SendMessageToFrontend($"Error Update Objects", "Error", 1000);
            }
        }

        #endregion
    }
}
