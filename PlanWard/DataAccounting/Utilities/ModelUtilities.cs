using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Rhino.DocObjects;
using PlanWard.DataAccounting.Models;
using PlanWard.DataAccounting.Models.Core;
using Newtonsoft.Json;

namespace PlanWard.DataAccounting.Utilities
{
    internal static class ModelUtilities
    {
        const string PW_TYPE_KEY = "PlanWardType";
        const string RHINO_ID_KEY = "RhinoId";

        #region rh user dictionary utilities

        /// <summary>
        /// Sets a property on the RhinoObject UserDictionary if the string value is not null or empty
        /// </summary>
        /// <param name="rhObj"></param>
        /// <param name="key"></param>
        public static void SetIfExists(RhinoObject rhObj, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
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

        #region extensions

        public static PlanWardObject ToPlanWardObject(this JObject obj)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                NullValueHandling = NullValueHandling.Include,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            if (obj == null || !obj.ContainsKey(PW_TYPE_KEY)) {
                return null;
            }
            PlanWardTypes type = (PlanWardTypes)Enum.Parse( typeof(PlanWardTypes), obj.GetValue(PW_TYPE_KEY).ToString());

            string data = JsonConvert.SerializeObject(obj);
            switch (type)
            {
                case PlanWardTypes.DesignOptionContainer:
                    return JsonConvert.DeserializeObject<DesignOptionContainer>(data, jss);
                case PlanWardTypes.Building:
                    return JsonConvert.DeserializeObject<Building>(data, jss);
                case PlanWardTypes.ParkingSpace:
                    return JsonConvert.DeserializeObject<Parking>(data, jss);
                default:
                    return null;
            }
        }

        #endregion
    }
}
