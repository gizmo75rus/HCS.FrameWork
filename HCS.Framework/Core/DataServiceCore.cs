using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;
using HCS.Framework.DataServices.HouseManagment;
using HCS.Framework.DataServices.Bills;
using HCS.Framework.Base;

namespace HCS.Framework.Core
{
    public class DataServiceCore
    {
        Dictionary<Type, Type> _dataServiceLocator = new Dictionary<Type, Type>();

        public DataServiceCore()
        {
            _dataServiceLocator.Add(typeof(AccountData), typeof(AccountDataService));
            _dataServiceLocator.Add(typeof(CounterData), typeof(CounterDataService));
            _dataServiceLocator.Add(typeof(HouseData), typeof(HouseDataService));
            _dataServiceLocator.Add(typeof(PaymentDocumentData), typeof(PaymentDocumentDataService));
        }

        public IDataService<T> GetService<T>(params object[] param) where T : DtoData
        {
            if (!_dataServiceLocator.Any(x => x.Key == typeof(T)))
                throw new ArgumentNullException("");
            var instance = Activator.CreateInstance(_dataServiceLocator[typeof(T)],param);
            return (IDataService<T>)instance;
        }
    }
}
