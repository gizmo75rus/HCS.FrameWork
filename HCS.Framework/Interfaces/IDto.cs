﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Interfaces
{
    public interface IDto
    {
        int ID { get; set; }
        string TransportGuid { get; set; }
        string Guid { get; set; }
        string UniqueNumber { get; set; }

    }
}
