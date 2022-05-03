using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanWard.DataAccounting.Models.Core;
using PlanWard.DataAccounting.Utilities;
using PlanWard.Interop;
using Rhino;
using Rhino.DocObjects;

namespace PlanWard.DataAccounting.Models
{
    internal class DesignOptionContainer : PlanWardObject
    {
        public DesignOptionContainer() : base() { }
        
        public DesignOptionContainer(RhinoObject rhinoObject): base(rhinoObject)
        {
            PlanWardType = PlanWardTypes.DesignOptionContainer;
        }

        public override void PullPropertiesFromRhinoObject(RhinoObject rhObj)
        {
            if (rhObj.UserDictionary.ContainsKey(InteropConstants.DICT_DESIGN_OPTION_NAME))
            {
                string designOption = rhObj.UserDictionary.GetString(InteropConstants.DICT_DESIGN_OPTION_NAME);
                DesignOption = designOption;
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

            return true;
        }

    }
}
