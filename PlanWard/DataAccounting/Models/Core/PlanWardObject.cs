using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanWard.Interop.Models;
using Rhino;
using Rhino.DocObjects;

namespace PlanWard.DataAccounting.Models.Core
{
    internal abstract class PlanWardObject : IRhinoInteroperable
    {
        public Guid RhinoId { get; private set; }

        /// <summary>
        /// Name of the Design Option that this Object belongs to
        /// </summary>
        public String DesignOption { get; protected set; }

        /// <summary>
        /// Type of Object that this corresponds to. Is based on which layer it exists on
        /// as well as if it is a valid version of that object
        /// </summary>
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
