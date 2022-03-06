using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlanWard.Interop;
using PlanWard.Interop.Models;
using PlanWard.DataAccounting.Models.Core;

using Rhino.DocObjects;
using Rhino;
using Rhino.Collections;
using PlanWard.DataAccounting.Utilities;

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
        public string ProgramType { get; private set; }

        public Building() : base() { }

        public Building(RhinoObject rhinoObject) : base(rhinoObject)
        {
            PlanWardType = PlanWardTypes.Building;
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
