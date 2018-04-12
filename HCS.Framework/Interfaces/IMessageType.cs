using System;
using HCS.Framework.Enums;
using HCS.Globals;

namespace HCS.Framework.Interfaces
{
    public interface IMessageType
    {
        /// <summary>
        /// Конечная точка (адресат)
        /// </summary>
        EndPoints EndPoint { get; set; }

        /// <summary>
        /// Идентификатор поставщика информации
        /// </summary>
        Guid OrgPPAGUID { get; set; }

        /// <summary>
        /// Тип запроса
        /// </summary>
        Type RequestType { get; set; }

        /// <summary>
        /// Запрос
        /// </summary>
        object Request { get; set; }

        /// <summary>
        /// Ответ
        /// </summary>
        object Result { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        Guid MessageGUID { get; set; }

        /// <summary>
        /// Идентификатор ответа ГИС
        /// </summary>
        Guid ResponceGUID { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        DateTime SendDate { get; set; }

        /// <summary>
        /// Дата завершения обработки и возврата ответа от ГИС
        /// </summary>
        DateTime CompliteDate { get; set; }

        /// <summary>
        /// Статус сообщения
        /// </summary>
        MessageStatuses Status { get; set; }

    }
}
