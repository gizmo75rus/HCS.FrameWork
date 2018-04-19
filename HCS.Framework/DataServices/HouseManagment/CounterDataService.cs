using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.DataServices.HouseManagment
{
    public class CounterDataService : IDataService<CounterData>
    {
        public IEnumerable<CounterData> Load(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
