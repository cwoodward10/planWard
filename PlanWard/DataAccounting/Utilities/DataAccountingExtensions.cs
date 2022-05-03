using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.DocObjects;
using Rhino.Collections;
using Rhino;
using Rhino.DocObjects.Tables;

using PlanWard.DataAccounting.Models;
using PlanWard.Interop;
using Rhino.Geometry;

namespace PlanWard.DataAccounting.Utilities
{
    internal static class DataAccountingExtensions
    {
        /// <summary>
        /// Returns all Objects from the given Collection that is eligible for PlanWard meaning
        /// it is of the correct layer and object type.
        /// If it is a parking object, it also contains the correct naming prefix.
        /// </summary>
        /// <param name="rhinoObjects"></param>
        /// <returns></returns>
        internal static IEnumerable<RhinoObject> WhereIsPlanwardEligible(this IEnumerable<RhinoObject> rhinoObjects)
        {
            if (rhinoObjects == null)
            {
                return new List<RhinoObject>();
            }
            IEnumerable<RhinoObject> validObjects = rhinoObjects.Where(rObj => rObj != null && rObj.Document != null);
            if (!validObjects.Any())
            {
                return new List<RhinoObject>();
            }

            // get the layers in the document
            RhinoDoc doc = validObjects.First().Document;
            LayerTable layerTable = doc.Layers;
            Layer docLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_DESIGN_OPTION_CONTAINER);
            Layer bldgLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_BUILDING_OUTLINES);
            Layer parkLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_PARKING);

            // return the rhino objects
            return rhinoObjects.Where(rhObj =>
            {
                int objLayerIndex = rhObj.Attributes.LayerIndex;

                if (objLayerIndex == docLayer.Index || objLayerIndex == bldgLayer.Index)
                {
                    if (
                        rhObj.ObjectType == ObjectType.Curve && 
                        ((CurveObject)rhObj).CurveGeometry.IsClosed
                    )
                    {
                        return true;
                    }
                }

                if (objLayerIndex == parkLayer.Index)
                {
                    if (
                        rhObj.ObjectType == ObjectType.InstanceReference && 
                        ((InstanceObject)rhObj).InstanceDefinition.Name.Contains("Parking Stall -")
                    )
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        /// <summary>
        /// Returns all objects within a set of objects that has a "Design Option Name" attribute in its user dictionary
        /// </summary>
        /// <param name="rhinoObjects"></param>
        /// <returns></returns>
        internal static IEnumerable<RhinoObject> WithDefinedDesignOption(this IEnumerable<RhinoObject> rhinoObjects)
        {
            return rhinoObjects.Where(rhObj =>
            {
                ArchivableDictionary rhDict = rhObj.UserDictionary;

                // check if we need to return true
                if (
                    rhDict != null &&
                    rhDict.Any() && 
                    rhDict.ContainsKey(InteropConstants.DICT_DESIGN_OPTION_NAME)
                )
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
            IEnumerable<CurveObject> crvsOnBldgLayer = rhinoObjects.Where(ro => ro.Attributes.LayerIndex == bldgLayer.Index).Where(obj => obj.ObjectType == ObjectType.Curve).Cast<CurveObject>();
            IEnumerable<Building> buildingObjects = crvsOnBldgLayer.Where(crv => crv.CurveGeometry.IsClosed).Select(crv =>
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

        internal static IEnumerable<RhinoObject> WhereIsContainedByDesignOption(this IEnumerable<RhinoObject> rhinoObjects, DesignOptionContainer container)
        {
            // account for list that is empty
            if (rhinoObjects == null || !rhinoObjects.Any())
            {
                return new List<RhinoObject>();
            }

            // get the boundary curve of the container
            RhinoDoc doc = rhinoObjects.First().Document;
            CurveObject boundaryObject = doc.Objects.FindId(container.RhinoId) as CurveObject;
            Curve boundaryCurve = boundaryObject.Geometry as Curve;

            // skip if lacking boundary curve for some reason
            if (boundaryCurve == null)
            {
                return new List<RhinoObject>(); ;
            }

            // return based on containment
            return rhinoObjects.Where(rhObj =>
            {
                GeometryBase geo = rhObj.Geometry;
                if (geo == null)
                {
                    return false;
                }

                Point3d location = geo.GetBoundingBox(true).Center;
                return boundaryCurve.Contains(location, Plane.WorldXY, doc.ModelAbsoluteTolerance) == PointContainment.Inside;
            });
        }
    }
}
