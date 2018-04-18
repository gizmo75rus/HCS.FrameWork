using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.DataServices.HouseManagment;

namespace HCS.Framework.Core
{
    public class DataServiceCore
    {
        Dictionary<Type, Type> _dataServiceLocator = new Dictionary<Type, Type>();

        public DataServiceCore()
        {
            _dataServiceLocator.Add(typeof(AccountData), typeof(AccountDataService));

        }
    }
}
