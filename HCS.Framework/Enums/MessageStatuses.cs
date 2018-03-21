using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Enums
{

    /// <summary>
    /// Статусы сообщения
    /// </summary>
    public enum MessageStatuses
    {
        Ready,
        SendOk,
        SendError,
        SendCriticalError,
        SendTimeout,
        GetResultOk,
        GetResultError,
        GetResultCriticalError,
        GetResultTimeout,
        EndLive,
    }
}
