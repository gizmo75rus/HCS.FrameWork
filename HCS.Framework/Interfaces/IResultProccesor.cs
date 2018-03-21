using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Interfaces;

namespace HCS.Framework.Interfaces
{
    interface IResultProccesor<T> where T : class
    {
        bool TryGet(IGetStateResult result, out IEnumerable<T> value);
    }
}
