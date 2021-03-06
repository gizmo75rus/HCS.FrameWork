﻿using System.Collections.Generic;
using HCS.Framework.Base;
using HCS.Framework.Dto.Payment;

namespace HCS.Framework.DataServices.Payments
{
    public class OrderData : DtoData
    {
        public IEnumerable<OrderExecutionDto> Values { get; set; }
    }
}
