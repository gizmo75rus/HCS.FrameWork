using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Implement
{
    internal class Package : IPackage
    {
        public IClaim Claim { get; set; }
        public Type ItemType { get; set; }
        public object Item { get; set; }
    }
}
