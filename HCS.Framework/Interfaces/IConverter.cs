using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Interfaces
{
    internal interface IConverter<in T,out V> where T:class where V: class
    {
        V Conevrt(T inputObject);
    }
}
