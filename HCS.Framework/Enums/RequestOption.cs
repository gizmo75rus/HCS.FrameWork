using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Globals;

namespace HCS.Framework.Enums
{
    public enum CriteriaType
    {
        OrgGUID,
        OrgPPAGUID,
        UOGUID,
        ORGN,
        INN,
        SNILS,
        FIASHouseGUID,
        LivingRoomGUID,
        PremisesGUID,
        AccountGUID,
        ContractGuid,
        ContractRootGuid,
        ConractVersionGuid,
        CharterGuid,
        CharterVersionGuid,
        LastVersionOnly,
        SigningDate,
        DateTo,
        DateFrom
    }

    public class RequestOption
    {
        public OrganizationRole Role { get; private set; }
        public Dictionary<CriteriaType, object> Criteries;
        public RequestOption(OrganizationRole role)
        {
            Role = role;
            Criteries = new Dictionary<CriteriaType, object>();
        }
    }
}
