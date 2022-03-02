using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;

namespace PlanWard.DataAccounting.Parking
{
    internal static class ParkingManager
    {
        const string defaultParkingPrefix = "Parking Stall -";

        public static double CountParkingStalls()
        {
            string parkingPrefixSettingName  = "ParkingBlockPrefix";
            string prefix = PlanWardPlugIn.Instance.Settings.Keys.Any(k => k == parkingPrefixSettingName) ? 
                PlanWardPlugIn.Instance.Settings.GetString(parkingPrefixSettingName) : defaultParkingPrefix;
            
            RhinoDoc doc = RhinoDoc.ActiveDoc;
            IEnumerable<InstanceDefinition> parkingDefs = doc.InstanceDefinitions.Where(d => d.HasName && d.Name.StartsWith(defaultParkingPrefix));
            return parkingDefs.Select(p => p.GetReferences(1).Length).Sum();
        }
    }
}
