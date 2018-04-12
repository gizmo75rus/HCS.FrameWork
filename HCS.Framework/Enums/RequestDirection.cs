using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Enums
{
    /// <summary>
    /// Направление запроса (импорт/экспорт)
    /// </summary>
    public enum RequestDirection
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        Export,

        /// <summary>
        /// Импортирование данных
        /// </summary>
        Import
    }
}
