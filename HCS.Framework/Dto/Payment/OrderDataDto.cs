using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;

namespace HCS.Framework.Dto.Payment
{
    //DTO для «Извещение о принятии к исполнению распоряжения», размещаемых исполнителем
    public class OrderExecutionDto:BaseDto
    {
        /// <summary>
        /// Идентификатор оплаты от ГИС
        /// </summary>
        public string OrderGuid { get; set; }

        /// <summary>
        /// Тип оплаты
        /// </summary>
        public PaymentType Type { get; set; }

        /// <summary>
        /// Дата внесения
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Оплата за период
        /// </summary>
        public OrderPeriod Period { get; set; }

        /// <summary>
        /// Идентификатор Жку
        /// </summary>
        public string ServiceID { get; set; }

        /// <summary>
        /// Номер л/с
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// ЕЛС
        /// </summary>
        public string AccountUniqueNumber { get; set; }

        /// <summary>
        /// Идентификатор ПД
        /// </summary>
        public string PaymentDocumentID { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Amount { get; set; }
    }

    public class OrderPeriod
    {
        public int Month { get; set; }
        public short Year { get; set; }
    }

    /// <summary>
    /// Тип оплаты
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Банк
        /// </summary>
        Bank = 0,

        /// <summary>
        /// Касса
        /// </summary>
        CashBox = 1
    }
}
