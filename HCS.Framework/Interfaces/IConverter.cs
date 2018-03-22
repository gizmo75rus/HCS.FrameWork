using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;

namespace HCS.Framework.Interfaces
{
    internal interface IConverter<in T,V> where T:class where V: class
    {
        event EventHandler<ErrorEventArgs> ErrorEvent;
        bool TryConvert(T value, out IEnumerable<V> result);
    }
}
