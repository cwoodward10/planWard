using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using PlanWard.Interop.Models;
using PlanWard.DataAccounting.Models;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace PlanWard.Interop
{
    internal static class InteropUtilities
    {
        public static IEnumerable<IRhinoInteroperable> ConvertRhinoObjectsToInteropable(IEnumerable<RhinoObject> rhinoObjects)
        {
            IEnumerable<IRhinoInteroperable> interopObjects = new List<IRhinoInteroperable>();

            // return empty list if no rhino objects
            if (rhinoObjects != null && rhinoObjects.Count() > 0)
            {
                // get the layers in the document
                RhinoDoc doc = rhinoObjects.First().Document;
                LayerTable layerTable = doc.Layers;

                // retrieve Design Option Containers
                Layer docLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_DESIGN_OPTION_CONTAINER);
                if (docLayer != null)
                {
                    IEnumerable<CurveObject> crvsOnDocLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == docLayer.Index).Where(obj => obj.ObjectType == ObjectType.Curve).Cast<CurveObject>();
                    IEnumerable<DesignOptionContainer> docObjects = crvsOnDocLayer.Where(crv => crv.CurveGeometry.IsClosed).Select(crv =>
                    {
                        return new DesignOptionContainer(crv);
                    });
                    interopObjects = interopObjects.Concat(docObjects);
                }

                // retrieve building objects
                Layer bldgLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_BUILDING_OUTLINES);
                if (bldgLayer != null)
                {
                    IEnumerable<CurveObject> crvsOnDocLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == bldgLayer.Index).Where(obj => obj.ObjectType == ObjectType.Curve).Cast<CurveObject>();
                    IEnumerable<Building> buildingObjects = crvsOnDocLayer.Where(crv => crv.CurveGeometry.IsClosed).Select(crv =>
                    {
                        return new Building(crv);
                    });
                    interopObjects = interopObjects.Concat(buildingObjects);
                }

                // retrieve parking objects
                Layer parkLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_PARKING);
                if (parkLayer != null)
                {
                    IEnumerable<InstanceObject> blocksOnLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == parkLayer.Index).Where(obj => obj.ObjectType == ObjectType.InstanceReference).Cast<InstanceObject>();
                    IEnumerable<Parking> parkingObjects = blocksOnLayer.Where(b => b.InstanceDefinition.Name.Contains("Parking Stall -")).Select(b =>
                    {
                        return new Parking(b);
                    });
                    interopObjects = interopObjects.Concat(parkingObjects);
                }

            }

            return interopObjects;
        }
    }
}
