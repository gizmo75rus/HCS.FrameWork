using System.Linq;
using System.Collections.Generic;
using System;
using HCS.Service.Async.Nsi.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.Nsi;
using HCS.Framework.SourceData.Nsi;

namespace HCS.Framework.RequestBuilders.Nsi
{
    public class ImportAdditionalServices : BaseBuilder, IRequestBuilder<importAdditionalServicesRequest1, AdditionalServiceData>
    {
        public importAdditionalServicesRequest1 Build(BuilderOption option, AdditionalServiceData dto)
        {
            return new importAdditionalServicesRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgGUID)),
                importAdditionalServicesRequest = new importAdditionalServicesRequest {
                    Id = RequestID,
                    ImportAdditionalServiceType = dto.Values.Select(x => new importAdditionalServicesRequestImportAdditionalServiceType {
                        AdditionalServiceTypeName = x.Name,
                        TransportGUID = x.TransportGuid,
                        ItemElementName = ItemChoiceType.OKEI,
                        Item = x.Unit.OKEI
                    }).ToArray()
                }
            };
        }
    }
}
