using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Interfaces
{
    /// <summary>
    /// Прототип пакета
    /// </summary>
    interface IPackage
    {
        /// <summary>
        /// Требование (адрес, направление)
        /// </summary>
        IClaim Claim { get; set; }

        /// <summary>
        /// Тип предмента
        /// </summary>
        Type ItemType { get; set; }
        
        /// <summary>
        /// Предмет
        /// </summary>
        object Item { get; set; }
    }
}
