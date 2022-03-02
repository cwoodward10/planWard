using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace PlanWard.DataAccounting.Buildings
{
    internal class BuildingsManager
    {
        const string defaultBuildingOutlineLayerName = "Parking Stall -";

        public static double CountSquareFootage()
        {
            string buildingLayerName = "Building Outlines";
            string prefix = PlanWardPlugIn.Instance.Settings.Keys.Any(k => k == buildingLayerName) ?
                PlanWardPlugIn.Instance.Settings.GetString(buildingLayerName) : defaultBuildingOutlineLayerName;

            RhinoDoc doc = RhinoDoc.ActiveDoc;
            Layer buildingLayer = doc.Layers.FirstOrDefault(l => l.Name == buildingLayerName);
            if (buildingLayer == null)
            {
                return 0;
            }

           IEnumerable<Curve> closedCurves = doc.Objects.FindByLayer(buildingLayer)
                .Where(o => o.ObjectType == ObjectType.Curve && (o as CurveObject).CurveGeometry != null)
                .Select(co => (co as CurveObject).CurveGeometry)
                .Where(crv => crv.IsClosed);

            return closedCurves.Select(crv => AreaMassProperties.Compute(crv).Area).Sum();
        }
    }
}
