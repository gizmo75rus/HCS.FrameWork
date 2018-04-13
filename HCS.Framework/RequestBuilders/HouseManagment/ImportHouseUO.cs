using System;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImporHouseUO : BaseBuilder, IRequestBuilder<importHouseUODataRequest, HouseDto>
    {
        public importHouseUODataRequest[] Build(BuilderOption option, HouseDto data)
        {
            bool IsOperator = (bool)option.Params[ParametrType.IsOperator];
            string ogrPPAGuid = option.Params[ParametrType.OrgGUID].ToString().ToLower();

            var value = new importHouseUODataRequest[]{
                new importHouseUODataRequest {
                    RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                    importHouseUORequest = new importHouseUORequest {
                        Id = RequestID,
                        Item = data.Type == HouseTypes.Apartment? apartmentHouse(option.Command,data) : livingHouse(option.Command,data)
                    }
                }
            };

            return value;
        }

        object apartmentHouse(RequestCMD command, HouseDto data)
        {
            return new importHouseUORequestApartmentHouse {
                
            };
        }

        object livingHouse(RequestCMD command,  HouseDto data)
        {
            return new importHouseUORequestLivingHouse { };
        }

    }
}
