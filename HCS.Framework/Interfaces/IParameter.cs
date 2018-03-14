using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Interfaces
{
    /// <summary>
    /// Прототип парамета
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IParameter
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Тип
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Значение
        /// </summary>
        object Value { get; }
    }

    
}
