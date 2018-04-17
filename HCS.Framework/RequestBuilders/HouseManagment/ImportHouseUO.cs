﻿using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImporHouseUO : BaseBuilder, IRequestBuilder<importHouseUODataRequest, HouseDto>
    {
        public importHouseUODataRequest Build(BuilderOption option, IEnumerable<HouseDto> data)
        {
            if (data.Count() > 1)
                throw new ArgumentOutOfRangeException("Превышено макисмальное кол-во объектов типа HouseDto, ожидался 1");

            var dto = data.FirstOrDefault();

            switch (dto.Type) {
                case HouseTypes.Apartment:
                    return new importHouseUODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseUORequest = new importHouseUORequest {
                            Id = RequestID,
                            Item = dto.MapToApartmentHouseUO(option.Command)
                        }
                    };
                case HouseTypes.Living:
                    return new importHouseUODataRequest {
                        RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                        importHouseUORequest = new importHouseUORequest {
                            Id = RequestID,
                            Item = dto.MapToLivingHouseUO(option.Command)
                        }
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
