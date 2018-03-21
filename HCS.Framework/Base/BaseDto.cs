using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Base
{
    public abstract class BaseDto : IDto
    {
        public int ID { get; set; }
        public string TransportGuid { get; set; }
        public string Guid { get; set; }
        public string UniqueNumber { get; set; }
    }
}
