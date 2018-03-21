using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;
using HCS.Framework.Interfaces;
using HCS.Interfaces;

namespace HCS.Framework.Implement
{
    public class ResultProccesor<T> : IResultProccesor<T> where T : class
    {
        public bool TryGet(IGetStateResult result, out IEnumerable<T> value)
        {
            value = null;
            if (!result.Items.OfType<T>().Any())
                return false;

            value = result.Items.OfType<T>();
            return true;
        }
    }
}
