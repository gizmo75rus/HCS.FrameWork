using System;
using System.Collections.Generic;
using HCS.Globals;
using HCS.Framework.Enums;

namespace HCS.Framework.Interfaces
{
    /// <summary>
    /// Прототип запроса (требования) для пакета
    /// </summary>
    internal interface IClaim
    {
        /// <summary>
        /// Конченая точка получателя
        /// </summary>
        EndPoints GetRecipient { get; }

        /// <summary>
        /// Направление (импорт/экспорт)
        /// </summary>
        ClaimDirection GetDirection { get; }

        IEnumerable<IParameter> GetParameters();
        IEnumerable<IParameter> GetParameters(Type Type);
        IParameter GetParameter(string name);
        void AddParameter(IParameter parameter);
    }

    

}
