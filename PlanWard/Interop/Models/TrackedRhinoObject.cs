using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;
using Rhino.Collections;
using Newtonsoft.Json;
using PlanWard.Interop;
using Newtonsoft.Json.Linq;

namespace PlanWard.Interop.Models
{
    internal class TrackedRhinoObject
    {
        #region properties

        /// <summary>
        /// The Id of the Rhino object within the RhinoDoc
        /// </summary>
        public Guid RhinoId { get; set; }
        
        /// <summary>
        /// Name of the Design Option that this Rhino Object belongs to.
        /// </summary>
        public string DesignOptionName { get; set; }

        /// <summary>
        /// Name of the Region Name that this object belongs to within a Design Option.
        /// ex: The "A" in building "A2"
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// An Identifier to differentiate itself from other objects within the Region.
        /// ex: The "2" in building "A2"
        /// </summary>
        public string RegionIdentifier { get; set; }

        #endregion properties


        public TrackedRhinoObject() { }

        /// <summary>
        /// Creates a TrackedRhinoObject from a RhinoObject's UserDictionary.
        /// RhinoId is always set. Other values are blank by default unless they already exist in the UserDictionary.
        /// </summary>
        /// <param name="rhObj"></param>
        /// <returns></returns>
        public static TrackedRhinoObject FromRhinoObject(RhinoObject rhObj)
        {
            string designOptionName = string.Empty;
            string regionName = string.Empty;
            string regionIdentifier = string.Empty;

            ArchivableDictionary info = rhObj.UserDictionary;
            info.TryGetString(InteropConstants.DICTIONARY_KEY_DESIGN_OPTION_NAME, out designOptionName);
            info.TryGetString(InteropConstants.DICTIONARY_KEY_REGION_NAME, out regionName);
            info.TryGetString(InteropConstants.DICTIONARY_KEY_REGION_IDENTIFIER, out regionIdentifier);

            return new TrackedRhinoObject()
            {
                RhinoId = rhObj.Id,
                DesignOptionName = designOptionName,
                RegionName = regionName,
                RegionIdentifier = regionIdentifier
            };
        }

        /// <summary>
        /// Parses a json string into a TrackedRhinoObject. Returns null on failure.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="jss"></param>
        /// <returns></returns>
        public static TrackedRhinoObject SerializeFromJson(string jsonString, JsonSerializerSettings jss = null)
        {
            if (jsonString == null && jsonString == string.Empty)
            {
                return null;
            }

            if (jss == null)
            {
                jss = new JsonSerializerSettings()
                {
                    //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
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
            }

            JObject parsedJsonObject = JObject.Parse(jsonString);
            if (parsedJsonObject == null)
            {
                return null;
            }
            return parsedJsonObject.ToObject<TrackedRhinoObject>();
        }
    }

    internal static class TrackedRhinoObjectExtensions
    {
        /// <summary>
        /// Tries to assign the properties of a TrackedRhinoObject to a RhinoObject's UserDictionary.
        /// Will not create a key/value pair in the dictionary if the Property is null or empty string.
        /// Returns false if fails (will fail if no rhino object in doc with id is found)
        /// </summary>
        /// <param name="tracked"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static bool TrySetTrackedInfo(this TrackedRhinoObject tracked, Rhino.RhinoDoc doc)
        {
            RhinoObject rhObj = doc.Objects.FindId(tracked.RhinoId);
            if (rhObj == null)
            {
                return false;
            }

            ArchivableDictionary info = rhObj.UserDictionary;
            if (tracked.DesignOptionName != null && tracked.DesignOptionName != string.Empty)
            {
                info.Set(InteropConstants.DICTIONARY_KEY_DESIGN_OPTION_NAME, tracked.DesignOptionName);
            }
            if (tracked.RegionName != null && tracked.RegionName != string.Empty)
            {
                info.Set(InteropConstants.DICTIONARY_KEY_REGION_NAME, tracked.RegionName);
            }
            if (tracked.RegionIdentifier != null && tracked.RegionIdentifier != string.Empty)
            {
                info.Set(InteropConstants.DICTIONARY_KEY_REGION_IDENTIFIER, tracked.RegionIdentifier);
            }
            return true;
        }
    }
}
