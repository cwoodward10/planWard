using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanWard.DataAccounting.Models.Core
{
    internal interface IAccountingObject
    {
        /// <summary>
        /// Name of the Region Name that this object belongs to within a Design Option.
        /// ex: The "A" in building "A2"
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// An Identifier to differentiate itself from other objects within the Region.
        /// ex: The "2" in building "A2"
        /// </summary>
        string RegionIdentifier { get; }
    }
}
