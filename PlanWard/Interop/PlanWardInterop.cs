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

            foreach (PlanWardObject plo in pwObjects)
            {
                plo.TrySetTrackedInfo(RhinoDoc.ActiveDoc);
            }

            RefreshInformation();
        }

        #endregion
    }
}
