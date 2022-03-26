using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.DocObjects;
using Rhino.Collections;

using PlanWard.DataAccounting.Models;
using PlanWard.Interop;

namespace PlanWard.DataAccounting.Utilities
{
    internal static class DataAccountingExtensions
    {
        internal static IEnumerable<RhinoObject> WithDefinedDesignOption(this IEnumerable<RhinoObject> rhinoObjects)
        {
            return rhinoObjects.Where(rhObj =>
            {
                ArchivableDictionary rhDict = rhObj.UserDictionary;

                // check if we need to return true
                if (rhDict.ContainsKey(InteropConstants.DICT_DESIGN_OPTION_NAME))
                {
                    string designOption = rhDict.GetString(InteropConstants.DICT_DESIGN_OPTION_NAME);
                    if (designOption != null && designOption.Length > 0)
                    {
                        return true;
                    }
                }
                // else we return false b/c if statements were not met
                return false;
            });
        }
        
        internal static IEnumerable<DesignOptionContainer> ToDesignOptionContainers(this IEnumerable<RhinoObject> rhinoObjects, Layer containerLayer)
        {
            IEnumerable<CurveObject> crvsOnDocLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == containerLayer.Index).Where(obj => obj.ObjectType == ObjectType.Curve).Cast<CurveObject>();
            IEnumerable<DesignOptionContainer> docObjects = crvsOnDocLayer.Where(crv => crv.CurveGeometry.IsClosed).Select(crv =>
            {
                return new DesignOptionContainer(crv);
            });

            return docObjects;
        }

        internal static IEnumerable<Building> ToBuildingObjects(this IEnumerable<RhinoObject> rhinoObjects, Layer bldgLayer)
        {
            IEnumerable<CurveObject> crvsOnDocLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == bldgLayer.Index).Where(obj => obj.ObjectType == ObjectType.Curve).Cast<CurveObject>();
            IEnumerable<Building> buildingObjects = crvsOnDocLayer.Where(crv => crv.CurveGeometry.IsClosed).Select(crv =>
            {
                return new Building(crv);
            });

            return buildingObjects;
        }

        internal static IEnumerable<Parking> ToParkingObjects(this IEnumerable<RhinoObject> rhinoObjects, Layer parkingLayer)
        {
            IEnumerable<InstanceObject> blocksOnLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == parkingLayer.Index).Where(obj => obj.ObjectType == ObjectType.InstanceReference).Cast<InstanceObject>();
            IEnumerable<Parking> parkingObjects = blocksOnLayer.Where(b => b.InstanceDefinition.Name.Contains("Parking Stall -")).Select(b =>
            {
                return new Parking(b);
            });

            return parkingObjects;
        }
    }
}
