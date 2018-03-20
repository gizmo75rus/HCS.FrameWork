using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Interfaces;

namespace HCS.Framework.Base
{
    public class Ack : IAck
    {
        public string MessageGUID { get; set; }
        public string RequesterMessageGUID { get; set; }
    }
}
