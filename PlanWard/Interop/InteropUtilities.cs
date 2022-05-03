using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using PlanWard.Interop.Models;
using PlanWard.DataAccounting.Models;
using PlanWard.DataAccounting.Utilities;

using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;
using Rhino.Geometry;
using PlanWard.DataAccounting.Models.Core;

namespace PlanWard.Interop
{
    internal static class InteropUtilities
    {
        /// <summary>
        /// Converts a given list of RhinoObjects to RhinoInteroperable Objects
        /// </summary>
        /// <param name="rhinoObjects"></param>
        /// <returns></returns>
        internal static IEnumerable<IRhinoInteroperable> ConvertRhinoObjectsToInteropable(IEnumerable<RhinoObject> rhinoObjects)
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
                    IEnumerable<DesignOptionContainer> docObjects = rhinoObjects.ToDesignOptionContainers(docLayer);
                    interopObjects = interopObjects.Concat(docObjects);
                }

                // retrieve building objects
                Layer bldgLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_BUILDING_OUTLINES);
                if (bldgLayer != null)
                {
                    IEnumerable<Building> buildingObjects = rhinoObjects.ToBuildingObjects(bldgLayer);
                    interopObjects = interopObjects.Concat(buildingObjects);
                }

                // retrieve parking objects
                Layer parkLayer = layerTable.FirstOrDefault(l => l.Name == InteropConstants.LAYER_PARKING);
                if (parkLayer != null)
                {
                    IEnumerable<Parking> parkingObjects = rhinoObjects.ToParkingObjects(parkLayer);
                    interopObjects = interopObjects.Concat(parkingObjects);
                }

            }

            return interopObjects;
        }
        
        /// <summary>
        /// Returns all PlanWard Objects in the Rhino doc that are eligible to be tracked by the UI.
        /// Converts the RhinoObjects in the Doc to these objects. 
        /// Eligible objects to be tracked are objects that are on valid layers, of valid types, and have assigned Design Options.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        internal static IEnumerable<IRhinoInteroperable> GetActivePlanWardObjects(RhinoDoc doc)
        {
            IEnumerable<RhinoObject> eligibleObjects = doc.Objects.WithDefinedDesignOption();
            return ConvertRhinoObjectsToInteropable(eligibleObjects);
        }

        internal static IEnumerable<RhinoObject> GetEligiblePlanwardObjects(RhinoDoc doc)
        {
            return doc.Objects.WhereIsPlanwardEligible();
        }

        internal static void SyncWithDesignOptionNameChange(RhinoDoc doc, IEnumerable<DesignOptionContainer> containers)
        {
            // get all eligible objects
            IEnumerable<RhinoObject> eligibleObjects = doc.Objects.WhereIsPlanwardEligible();

            // filter out the ones that are containers themselves
            Layer containerLayer = doc.Layers.FirstOrDefault(l => l.Name == InteropConstants.LAYER_DESIGN_OPTION_CONTAINER);
            if (containerLayer == null)
            {
                // if null, return b/c something is wrong
                return;
            }
            eligibleObjects = eligibleObjects.Where(obj => obj.Attributes.LayerIndex != containerLayer.Index);

            Layer bldgLayer = doc.Layers.FirstOrDefault(l => l.Name == InteropConstants.LAYER_BUILDING_OUTLINES);
            Layer parkLayer = doc.Layers.FirstOrDefault(l => l.Name == InteropConstants.LAYER_PARKING);
            if (bldgLayer == null || parkLayer == null)
            {
                // if null, return b/c something is wrong
                return;
            }
            // iterate through the containers to find eligible objects within them
            // sync naming changes within each container
            foreach (DesignOptionContainer container in containers)
            {
                IEnumerable<RhinoObject> containedObjects = eligibleObjects.WhereIsContainedByDesignOption(container);
                IEnumerable<PlanWardObject> pwObjects = containedObjects.ToBuildingObjects(bldgLayer).Cast<PlanWardObject>().Concat(containedObjects.ToParkingObjects(parkLayer));
                // change d.o then set in doc
                foreach (PlanWardObject pwObject in pwObjects)
                {
                    pwObject.DesignOption = container.DesignOption;
                    pwObject.TrySetTrackedInfo(doc);
                }
            }
        }
    }
}
