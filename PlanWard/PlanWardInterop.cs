using CefSharp;
using CefSharp.WinForms;
using Rhino.Geometry;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PlanWard
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

        public void UpdateParkingCount(double count)
        {
           string data = JsonConvert.SerializeObject(count);
            NotifyFrame("UpdateParkingCount", data);
        }

        public void UpdateTotalSquareFootage(double area)
        {
            string data = JsonConvert.SerializeObject(area);
            NotifyFrame("UpdateTotalSquareFootage", data);
        }

        #endregion

        #region From UI

        public void RefreshInformation()
        {
            double count = Parking.ParkingManager.CountParkingStalls();
            UpdateParkingCount(count);

            double area = Buildings.BuildingsManager.CountSquareFootage();
            UpdateTotalSquareFootage(area);
        }

        #endregion
    }
}
