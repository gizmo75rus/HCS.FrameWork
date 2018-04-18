using System;
using System.Collections.Generic;
using HCS.Framework.Interfaces;

namespace HCS.Framework.DataServices.HouseManagment
{
    public class AccountDataService : IDataService<AccountData>
    {
        public IEnumerable<AccountData> Load(params object[] param)
        {
            throw new NotImplementedException();
        }
        
    }
}
