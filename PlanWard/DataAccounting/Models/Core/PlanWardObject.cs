using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlanWard.Interop.Models;
using PlanWard.Interop.JSON.Converters;
using Rhino;
using Rhino.DocObjects;

namespace PlanWard.DataAccounting.Models.Core
{
    internal abstract class PlanWardObject : IRhinoInteroperable
    {
        [JsonConverter(typeof(GuidConverter))]
        [JsonProperty(PropertyName = "RhinoId")]
        public Guid RhinoId { get; protected set; }

        /// <summary>
        /// Name of the Design Option that this Object belongs to
        /// </summary>
        [JsonProperty(PropertyName = "DesignOption")]
        public string DesignOption { get; set; }

        /// <summary>
        /// Type of Object that this corresponds to. Is based on which layer it exists on
        /// as well as if it is a valid version of that object
        /// </summary>
        [JsonProperty(PropertyName = "PlanWardType")]
        public PlanWardTypes PlanWardType { get; protected set; }

        public PlanWardObject() { }

        public PlanWardObject(RhinoObject rhinoObject)
        {
            RhinoId = rhinoObject.Id;
            PullPropertiesFromRhinoObject(rhinoObject);
        }

        public abstract void PullPropertiesFromRhinoObject(RhinoObject rhObj);

        public abstract bool TrySetTrackedInfo(RhinoDoc doc);
    }
}
