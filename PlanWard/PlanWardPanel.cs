using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanWard
{
    /// <summary>
    /// This is the user control that is buried in the tabbed, docking panel.
    /// </summary>
    [System.Runtime.InteropServices.Guid("2910f431-4cfc-4252-8812-91def7d94d62")]
    public partial class PlanWardPanel : UserControl
    {
        /// <summary>
        /// Returns the ID of this panel.
        /// </summary>
        public static System.Guid PanelId => typeof(PlanWardPanel).GUID;

        public PlanWardPanel()
        {
            InitializeComponent();

            this.Controls.Add(PlanWardPlugIn.Browser);

            // Set the user control property on our plug-in
            PlanWardPlugIn.Instance.PanelUserControl = this;
        }
    }
}
