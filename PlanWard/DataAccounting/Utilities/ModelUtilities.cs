using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Collections;
using Rhino.DocObjects;
using PlanWard.DataAccounting.Models.Core;
using PlanWard.Interop;

namespace PlanWard.DataAccounting.Utilities
{
    internal static class ModelUtilities
    {
        #region rh user dictionary utilities

        /// <summary>
        /// Sets a property on the RhinoObject UserDictionary if the string value is not null or empty
        /// </summary>
        /// <param name="rhObj"></param>
        /// <param name="key"></param>
        public static void SetIfExists(RhinoObject rhObj, string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                rhObj.UserDictionary.Set(key, value);
            }
        }

        public static void SetIfExists(RhinoObject rhObj, string key, bool? value)
        {
            if (value != null)
            {
                rhObj.UserDictionary.Set(key, (bool)value);
            }
        }

        #endregion rh user dictionary utilities
    }
}
