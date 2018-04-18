using System;
using HCS.Framework.Base;

namespace HCS.Framework.Dto.HouseManagment
{
    public class CounterDto : BaseDto
    {
        /// <summary>
        /// Идентификатор версии ПУ
        /// </summary>
        public string MeteringDeviceVersionGUID { get; set; }
        public string AccountGUID { get; set; }
        public string PremisesGUID { get; set; }
        public string RoomGUID { get; set; }



        /// <summary>
        /// Тип ИПУ
        /// </summary>
        public CounterType Type { get; set; }

        /// <summary>
        /// Тип измеряемого ресурса
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Ссылка на справочник ком. реусурсов
        /// </summary>
        public NsiRef Resource { get; set; }

        /// <summary>
        /// Дата установки
        /// </summary>
        public DateTime? InstallationDate { get; set; }

        /// <summary>
        /// Дата ввода в эксплуатацию
        /// </summary>
        public DateTime CommissioningDate { get; set; }

        /// <summary>
        /// Дата опломбировки заводом изготовителем
        /// </summary>
        public DateTime? FactorySealDate { get; set; }

        /// <summary>
        /// Дата первой певерки
        /// </summary>
        public DateTime? FirstVerificationDate { get; set; }

        /// <summary>
        /// Поверочный интервал
        /// </summary>
        public NsiRef VerificationInterval { get; set; }

        /// <summary>
        /// Марка ПУ
        /// </summary>
        public string Stamp { get; set; }

        /// <summary>
        /// Модель
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Серийный номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Первоначальные показания
        /// </summary>
        public CounterValueDto Value { get; set; }
    }

    public class CounterValueDto: BaseDto
    {
        public decimal T1 { get; set; }
        public decimal? T2 { get; set; }
        public decimal? T3 { get; set; }
    }

    public enum CounterType
    {
        /// <summary>
        /// ИПУ Жилого дома, (Вид ПУ = индивидуальный, Тип дома = жилой)
        /// </summary>
        ApartmentHouseDevice,
       
        /// <summary>
        /// Общеквартирый ПУ для квартир коммунального заселения (Вид ПУ = общеквартирный)
        /// </summary>
        CollectiveApartmentDevice,
       
        /// <summary>
        /// Общедомовой ПУ (Вид ПУ = коллективный)
        /// </summary>
        CollectiveDevice,
        
        /// <summary>
        /// Комнатный ПУ (Вид ИПУ = комнатный)
        /// </summary>
        LivingRoomDevice,

        /// <summary>
        /// ИПУ нежилого помещения (Вид ИПУ = индивидуальный)
        /// </summary>
        NonResidentialPremiseDevice,

        /// <summary>
        /// ИПУ жилого помещения (Вид ИПУ = индивидуальный, Тип дома = МКД)
        /// </summary>
        ResidentialPremiseDevice
    }
    public enum ResourceType
    {
        Electrical,
        Municipal
    }
}
