using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlanWard.Interop;
using PlanWard.Interop.Models;
using PlanWard.DataAccounting.Models.Core;

using Rhino;
using Rhino.DocObjects;
using Rhino.Collections;
using PlanWard.DataAccounting.Utilities;

namespace PlanWard.DataAccounting.Models
{
    internal class Parking : PlanWardObject, IAccountingObject
    {
        public string RegionName { get; private set; }
        public string RegionIdentifier { get; private set; }

        /// <summary>
        /// True if is a Handicapped Stall
        /// </summary>
        public bool? IsHandicap { get; private set; }

        /// <summary>
        /// True if is a Compact Stall
        /// </summary>
        public bool? IsCompact { get; private set; }

        /// <summary>
        /// True if is an Angled Stall
        /// </summary>
        public bool? IsAngled { get; private set; }

        public Parking(): base(){}

        public Parking(RhinoObject rhinoObject): base(rhinoObject)
        {
            PlanWardType = PlanWardTypes.ParkingSpace;
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

            if (rhDict.ContainsKey(InteropConstants.DICT_PARK_ISCOMPACT))
            {
                bool rCompact = rhObj.UserDictionary.GetBool(InteropConstants.DICT_PARK_ISCOMPACT);
                IsCompact = rCompact;
            }

            if (rhDict.ContainsKey(InteropConstants.DICT_PARK_ISHANDICAP ))
            {
                bool rHandicap = rhObj.UserDictionary.GetBool(InteropConstants.DICT_PARK_ISHANDICAP);
                IsHandicap = rHandicap;
            }

            if (rhDict.ContainsKey(InteropConstants.DICT_PARK_ISANGLED))
            {
                bool rAngle = rhObj.UserDictionary.GetBool(InteropConstants.DICT_PARK_ISANGLED);
                IsAngled = rAngle;
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
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_PARK_ISANGLED, IsAngled);
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_PARK_ISCOMPACT, IsCompact);
            ModelUtilities.SetIfExists(rhObj, InteropConstants.DICT_PARK_ISHANDICAP, IsHandicap);

            return true;
        }
    }
}
