using System;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImporHouseUORequestBuilder : BaseBuilder, IRequestBuilder<importHouseUODataRequest, HouseDto>
    {
        public importHouseUODataRequest[] Build(BuilderOption option, HouseDto data)
        {
            bool IsOperator = (bool)option.Params[ParametrType.IsOperator];
            string ogrPPAGuid = option.Params[ParametrType.OrgGUID].ToString().ToLower();

            var value = new importHouseUODataRequest[]{
                new importHouseUODataRequest {
                    RequestHeader = Create<RequestHeader>(IsOperator, ogrPPAGuid),
                    importHouseUORequest = new importHouseUORequest {
                        Id = RequestID,
                        Item = data.Type == HouseTypes.Apartment? apartmentHouse(data) : livingHouse(data)
                    }
                }
            };

            return value;
        }

        object apartmentHouse(HouseDto data)
        {
            return new importHouseUORequestApartmentHouse { };
        }

        object livingHouse(HouseDto data)
        {
            return new importHouseUORequestLivingHouse { };
        }

    }
}
