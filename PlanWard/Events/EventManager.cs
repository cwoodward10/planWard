using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;
using PlanWard.Interop;
using PlanWard.Interop.Models;

namespace PlanWard.Events
{
    internal class EventManager
    {
        public static void SetupPlanwardEventHandlers()
        {
            RhinoDoc.SelectObjects += OnRhinoObjectsSelected;
            RhinoDoc.DeselectObjects += OnRhinoObjectsSelected;
            RhinoDoc.DeselectAllObjects += OnRhinoObjectsDeselected;

            RhinoDoc.AddRhinoObject += OnRhinoObjectAdded;
            RhinoDoc.DeleteRhinoObject += OnRhinoObjectDeleted;
            RhinoDoc.UndeleteRhinoObject += OnRhinoObjectAdded;
        }   
        
        #region event handlers

        private static void OnRhinoObjectAdded(Object sender, RhinoObjectEventArgs e)
        {
            PlanWardPlugIn.Interop.RefreshInformation();
        }

        private static void OnRhinoObjectDeleted(Object sender, RhinoObjectEventArgs e)
        {
            PlanWardPlugIn.Interop.RefreshInformation();
        }

        private static void OnRhinoObjectsSelected(Object sender, RhinoObjectSelectionEventArgs e)
        {
            PlanWardPlugIn.Interop.SendSelectedObjects(e.RhinoObjects);
        }

        private static void OnRhinoObjectsDeselected(object sender, RhinoDeselectAllObjectsEventArgs e)
        {
            PlanWardPlugIn.Interop.SendSelectedObjects(new List<RhinoObject>());
        }

        #endregion event handlers
    }
}
