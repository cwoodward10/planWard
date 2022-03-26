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
    public interface IRhinoInteroperable
    {
        #region properties

        /// <summary>
        /// The Id of the Rhino object within the RhinoDoc
        /// </summary>
        Guid RhinoId { get; }

        #endregion properties

        /// <summary>
        /// Creates an Interoperable Object from a RhinoObject's UserDictionary.
        /// RhinoId is always set. Other values are blank by default unless they already exist in the UserDictionary.
        /// </summary>
        /// <param name="rhObj"></param>
        /// <returns></returns>
        void PullPropertiesFromRhinoObject(RhinoObject rhObj);

        /// <summary>
        /// Tries to assign the properties of the InteroperableObject to a RhinoObject's UserDictionary.
        /// Will not create a key/value pair in the dictionary if the Property is null or empty string.
        /// Returns false if RhinoDoc does not contain an object corresponding with this object
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        bool TrySetTrackedInfo(Rhino.RhinoDoc doc);
    }
}
