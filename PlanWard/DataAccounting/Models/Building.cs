using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using PlanWard.Interop;
using PlanWard.Interop.Models;
using PlanWard.DataAccounting.Models.Core;

using Rhino.DocObjects;
using Rhino;
using Rhino.Collections;
using PlanWard.DataAccounting.Utilities;
using Rhino.Geometry;

namespace PlanWard.DataAccounting.Models
{
    internal class Building : PlanWardObject, IAccountingObject
    {
        public string RegionName { get; private set; }
        public string RegionIdentifier { get; private set; }

        /// <summary>
        /// Program Type for this particular building.
        /// ex: Residential, Retail, Hospitality, etc...
        /// </summary>
        [JsonProperty(PropertyName = "ProgramType")]
        public string ProgramType { get; private set; }

        /// <summary>
        /// Area within the bounds of the curve that define this building
        /// </summary>
        [JsonProperty(PropertyName = "Area")]
        public double Area { get; private set; }

        public Building() : base() { }

        public Building(RhinoObject rhinoObject) : base(rhinoObject)
        {
            PlanWardType = PlanWardTypes.Building;

            // code should check that RhinoObject is CurveObject
            // and is closed before even passing the object into the constructor
            // if this turns out not to be case in future, may want to program more defensively here
            CurveObject curveObject = (CurveObject)rhinoObject;
            Area = AreaMassProperties.Compute(curveObject.CurveGeometry).Area;
        }

        public override void PullPropertiesFromRhinoObject(RhinoObject rhObj)
        {
            ArchivableDictionary rhDict = rhObj.UserDictionary;
            if (rhDict.ContainsKey(InteropConstants.DICT_DESIGN_OPTION_NAME))
            {
                string designOption = rhDict.GetString(InteropConstants.DICT_DESIGN_OPTION_NAME);
                DesignOption = designOption;
            }

            if (rhDict.ContainsKey(InteropConstants.DICT_REGION_NAME))
            {
                string rName = rhObj.UserDictionary.GetString(InteropConstants.DICT_REGION_NAME);
                RegionName = rName;
            }

            if (rhDict.ContainsKey(InteropConstants.DICT_REGION_IDENTIFIER))
            {
                string rIdentifier = rhObj.UserDictionary.GetString(InteropConstants.DICT_REGION_IDENTIFIER);
                RegionIdentifier = rIdentifier;
            }

            if (rhDict.ContainsKey(InteropConstants.DICT_BLDG_PROGRAM))
            {
                string rProgram = rhObj.UserDictionary.GetString(InteropConstants.DICT_BLDG_PROGRAM);
                ProgramType = rProgram;
            }
        }

        public override bool TrySetTrackedInfo(RhinoDoc doc)
        {
            RhinoObject rhObj = doc.Objects.FindId(RhinoId);
            if (rhObj == null)
            {
                return false;
            }

            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_DESIGN_OPTION_NAME, DesignOption);
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_REGION_NAME, RegionName);
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_REGION_IDENTIFIER, RegionIdentifier);
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_BLDG_PROGRAM, ProgramType);

            return true;
        }
    }
}
