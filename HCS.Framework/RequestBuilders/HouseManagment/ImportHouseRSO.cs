using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.SourceData.HouseManagment;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImportHouseRSO : BaseBuilder, IRequestBuilder<importHouseRSODataRequest, HouseData>
    {
        public importHouseRSODataRequest Build(BuilderOption option, HouseData dto)
        {
            switch (dto.Value.Type) {
                case HouseTypes.Apartment:
                    return new importHouseRSODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseRSORequest = new importHouseRSORequest {
                            Id = RequestID,
                            Item = dto.Value.MapToApartmentHouseRSO(option.Command)
                        }
                    };
                case HouseTypes.Living:
                    return new importHouseRSODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseRSORequest = new importHouseRSORequest {
                            Id = RequestID,
                            Item = dto.Value.MapToLivingHouseRSO(option.Command)
                        }
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
