using System;
using HCS.Framework.Base;

namespace HCS.Framework.Dto.HouseManagment
{
    public class AccountDto : BaseDto
    {
        public string AccountName { get; set; }
        public string ServiceID { get; set; }
        public AccountTypes Type { get; set; }
        public string HouseGuid { get; set; }
        public string PremiseGuid { get; set; }
        public string RoomGuid { get; set; }
        public decimal TotalSquare { get; set; }
        public decimal ResidentialSquare { get; set; }
        public decimal GrossSquare { get; set; }
        public AccountReason Reason { get; set; }
        public string ReasonGuid { get; set; }
        public PayerInfo Payer { get; set; }
        public AccountClose Closed { get; set; }

    }

    public class PayerInfo
    {
        public PayerType Type { get; set; }
        public string OrgVersionGuid { get; set; }
        public string SNILS { get; set; }
    }

    public class AccountClose {
        public NsiRef Reason { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }

    public enum AccountTypes
    {
        UK,
        RSO,
        CRA,
        RC
    }

    public enum AccountReason
    {
        SupplyContract,
        SocialContract,
        Contract,
        Charter,
        CRProtocol,
        OMSCR
    }

    public enum PayerType
    {
        Indv,
        Legal
    }
}
