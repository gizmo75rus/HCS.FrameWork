using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;
using HCS.Framework.Dto.HouseManagment;

namespace HCS.Framework.DataServices.HouseManagment
{
    public class CounterData : DtoData
    {
        public IEnumerable<CounterDto> Values { get; set; }
    }
}
