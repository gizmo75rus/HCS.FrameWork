using System.Collections.Generic;
using HCS.Framework.Base;

namespace HCS.Framework.Interfaces
{
    interface IDataService<T> where T : DtoData
    {
        IEnumerable<T> Load(params object[] param);
    }
}
