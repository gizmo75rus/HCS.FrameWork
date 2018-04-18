using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;

namespace HCS.Framework.Dto.Bills
{
    public class PaymentDataDto : BaseDto {
        public int Year { get; set; }
        public int Month { get; set; }
        public bool HasSpecialKP { get; set; }
        public PaymentRequisites Requisites { get; set; }
        public List<PaymentDocumentDto> Documents { get; set; }
        public PaymentDataDto()
        {
            Documents = new List<PaymentDocumentDto>();
        }
    }

    public class PaymentDocumentDto: BaseDto
    {
        /// <summary>
        /// Требуется отзвать ПД
        /// </summary>
        public bool Revoke { get; set; }

        /// <summary>
        /// Идентификатор ЛС
        /// </summary>
        public string AccountGUID { get; set; }

        /// <summary>
        /// Номер ПД
        /// </summary>
        public string PaymentDocumentNumber { get; set; }

        /// <summary>
        /// Общая площадь помещения
        /// </summary>
        public decimal TotalSquare { get; set; }

        /// <summary>
        /// Начисления по услугам
        /// </summary>
        public List<ChargeInfo> ChargesInfos { get; set; }

        /// <summary>
        /// Кап. ремонт
        /// </summary>
        public List<CapitalRepair> CapitalRepairs { get; set; }

        /// <summary>
        /// Пени
        /// </summary>
        public List<Peni> Penies { get; set; }

        /// <summary>
        /// Задолженности по услугам
        /// </summary>
        public List<ChargeDept> ChargesDept { get; set; }


        #region  Справочная информация

        /// <summary>
        /// аванс на начало расчетного периода, руб. 	PrePayment
        /// </summary>
        public decimal PrePaymentPeriod { get; set; }

        /// <summary>
        /// Задолженность за предыдущие периоды
        /// </summary>
        public decimal DebtPreviousPeriods { get; set; }

        /// <summary>
        /// Сумма последнего платежа
        /// </summary>
        public decimal Pay { get; set; }

        /// <summary>
        /// Дата последей поступившей оплаты
        /// </summary>
        public DateTime? DateOfLastPay { get; set; }

        #endregion

        public PaymentDocumentDto()
        {
            ChargesInfos = new List<ChargeInfo>();
            CapitalRepairs = new List<CapitalRepair>();
            Penies = new List<Peni>();
            ChargesDept = new List<ChargeDept>();
        }


    }

    /// <summary>
    /// Платежные реквизиты
    /// </summary>
    public class PaymentRequisites
    {
        public PaymentRequisites()
        {
            _RequsiteKEY = Guid.NewGuid().ToString().ToLower();
        }

        /// <summary>
        /// БИК Банка
        /// </summary>
        public string BIK { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Ключь ПД реквизита
        /// </summary>
        public string RequsiteKEY { get { return _RequsiteKEY; } }

        private string _RequsiteKEY;


    }


    /// <summary>
    /// Начисление по услуге
    /// </summary>
    public class ChargeInfo : BaseCharge
    {
        public int SubListID { get; set; }

        /// <summary>
        /// Способ определения объема потребления услуги
        /// </summary>
        public DeterminingMethod DeterminingMethod { get; set; }

        /// <summary>
        /// Тип объема 
        /// </summary>
        public VolumeType VolumeType { get; set; }

        /// <summary>
        /// Объем потребленной услуги
        /// </summary>
        public decimal Value { get; set; }


        /// <summary>
        /// Тариф
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Перерасчеты
        /// </summary>
        public decimal ReCalc { get; set; }

        /// <summary>
        /// Итого к оплате
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Итого к оплате за период
        /// </summary>
        public decimal PeriodTotal { get; set; }

        /// <summary>
        /// Под услуги для ком.ресурсов на ОДН
        /// </summary>
        public List<ChargeInfo> SubCharges { get; set; }
    }

    /// <summary>
    /// Начисления за кап.ремонт
    /// </summary>
    public class CapitalRepair : BaseCharge
    {
        /// <summary>
        /// Размер взноса за кв.м.
        /// </summary>
        public decimal Contribution { get; set; }

        /// <summary>
        /// Начислено за период (без. перерасчетов)
        /// </summary>
        public decimal AccountingPeriodTotal { get; set; }

        /// <summary>
        /// Перерасчет, корректировка
        /// </summary>
        public decimal Recalcultion { get; set; }

        /// <summary>
        /// Льготы, субсидии, скидки
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Итого к оплате
        /// </summary>
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Пеня
    /// </summary>
    public class Peni : BaseCharge
    {
        /// <summary>
        /// Основание
        /// </summary>
        public string Clause { get; set; }

        /// <summary>
        /// Итого начислено
        /// </summary>
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Задолженность по услуге
    /// </summary>
    public class ChargeDept : BaseCharge
    {

        /// <summary>
        /// Месяц
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Итого к оплате за ресчетный период
        /// </summary>
        public decimal TotalDebt { get; set; }
    }

    #region Base

    public class BaseCharge :NsiRef
    {
        public ServiceType ServiceType { get; set; }
    }

    /// <summary>
    /// Тип услуг
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// Жилищная
        /// </summary>
        HousingService = 0,

        /// <summary>
        /// Муниципальная
        /// </summary>
        MunicipalSerive = 1,

        /// <summary>
        /// Дополнительная
        /// </summary>
        AdditionalService = 2,

        /// <summary>
        /// Ком ресурсы на ОДН
        /// </summary>
        HouseResource = 4,

        /// <summary>
        /// Пени
        /// </summary>
        Peni = 5,


        /// <summary>
        /// кап. ремонт
        /// </summary>
        CapitalRepair = 6



    }

    /// <summary>
    /// Тип объема
    /// </summary>
    public enum VolumeType
    {
        /// <summary>
        /// Индивидуальная
        /// </summary>
        Individual,

        /// <summary>
        /// ОДН
        /// </summary>
        Сollective
    }

    /// <summary>
    /// Способ определения объема потребления услуги
    /// </summary>
    public enum DeterminingMethod
    {

        /// <summary>
        /// Норматив
        /// </summary>
        Norm = 0,
        /// <summary>
        /// Прибор учета
        /// </summary>
        Metering = 1,

        /// <summary>
        /// Другое
        /// </summary>
        Other = 2
    }
    #endregion

}
