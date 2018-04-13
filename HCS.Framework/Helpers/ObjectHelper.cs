using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Helpers
{
    public static class ObjectHelper
    {
        public static string ToGisString(this object obj)
        {
            return obj.ToString().ToLower();
        }

        public static string ToGisString(this Guid guid)
        {
            return guid.ToString().ToLower();
        }
    }
}
