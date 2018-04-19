using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.DataServices.HouseManagment
{
    public class HouseDataService : IDataService<HouseData>
    {
        public IEnumerable<HouseData> Load(params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
