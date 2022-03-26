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
            RhinoDoc.ActiveDocumentChanged += OnActiveDocChanged;
            
            RhinoDoc.SelectObjects += OnRhinoObjectsSelected;
            RhinoDoc.DeselectObjects += OnRhinoObjectsSelected;
            RhinoDoc.DeselectAllObjects += OnRhinoObjectsAllDeselected;

            RhinoDoc.AddRhinoObject += OnRhinoObjectAdded;
            RhinoDoc.DeleteRhinoObject += OnRhinoObjectDeleted;
            RhinoDoc.UndeleteRhinoObject += OnRhinoObjectAdded;
        }

        public static void UnsubscribeEventHandlers()
        {
            RhinoDoc.ActiveDocumentChanged -= OnActiveDocChanged;

            RhinoDoc.SelectObjects -= OnRhinoObjectsSelected;
            RhinoDoc.DeselectObjects -= OnRhinoObjectsSelected;
            RhinoDoc.DeselectAllObjects -= OnRhinoObjectsAllDeselected;

            RhinoDoc.AddRhinoObject -= OnRhinoObjectAdded;
            RhinoDoc.DeleteRhinoObject -= OnRhinoObjectDeleted;
            RhinoDoc.UndeleteRhinoObject -= OnRhinoObjectAdded;
        }
        
        #region event handlers

        private static void OnActiveDocChanged(Object sender, DocumentEventArgs e)
        {
            PlanWardPlugIn.Interop.RefreshInformation();
        }

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
            PlanWardPlugIn.Interop.SendSelectedObjects(CheckSelection(e));
        }

        private static void OnRhinoObjectsAllDeselected(object sender, RhinoDeselectAllObjectsEventArgs e)
        {
            PlanWardPlugIn.Interop.SendSelectedObjects(new List<RhinoObject>());
        }

        #endregion event handlers

        #region helper methods
        private static IEnumerable<RhinoObject> CheckSelection(RhinoObjectSelectionEventArgs e)
        {
            return RhinoDoc.ActiveDoc.Objects.Where(obj => obj.IsSelected(false) != 0);
        }
        #endregion
    }
}
