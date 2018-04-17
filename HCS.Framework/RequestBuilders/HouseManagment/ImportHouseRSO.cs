using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.HouseManagment;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImportHouseRSO : BaseBuilder, IRequestBuilder<importHouseRSODataRequest, HouseDto>
    {
        public importHouseRSODataRequest Build(BuilderOption option, IEnumerable<HouseDto> data)
        {
            if(data.Count() > 1)
                throw new ArgumentOutOfRangeException("Превышено макисмальное кол-во объектов типа HouseDto, ожидался 1");

            var dto = data.FirstOrDefault();
            switch (dto.Type) {
                case HouseTypes.Apartment:
                    return new importHouseRSODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseRSORequest = new importHouseRSORequest {
                            Id = RequestID,
                            Item = dto.MapToApartmentHouseRSO(option.Command)
                        }
                    };
                case HouseTypes.Living:
                    return new importHouseRSODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseRSORequest = new importHouseRSORequest {
                            Id = RequestID,
                            Item = dto.MapToLivingHouseRSO(option.Command)
                        }
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
