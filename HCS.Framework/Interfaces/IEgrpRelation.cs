using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Interfaces
{
    interface IEgrpRelation
    {
        string CadastrNumber { get; set; }
        bool HasNotRelation { get; set; }
    }
}
