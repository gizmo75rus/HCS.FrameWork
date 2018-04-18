using System;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Framework.SourceData.HouseManagment;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImporHouseUO : BaseBuilder, IRequestBuilder<importHouseUODataRequest, HouseData>
    {
        public importHouseUODataRequest Build(BuilderOption option, HouseData dto)
        {

            switch (dto.Value.Type) {
                case HouseTypes.Apartment:
                    return new importHouseUODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseUORequest = new importHouseUORequest {
                            Id = RequestID,
                            Item = dto.Value.MapToApartmentHouseUO(option.Command)
                        }
                    };
                case HouseTypes.Living:
                    return new importHouseUODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseUORequest = new importHouseUORequest {
                            Id = RequestID,
                            Item = dto.Value.MapToLivingHouseUO(option.Command)
                        }
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
