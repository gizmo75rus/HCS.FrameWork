using System.Linq;
using System.Collections.Generic;
using System;
using HCS.Service.Async.Nsi.v11_10_0_13;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Framework.Dto.Nsi;

namespace HCS.Framework.RequestBuilders.Nsi
{
    public class ImportMunicipalService : BaseBuilder, IRequestBuilder<importMunicipalServicesRequest1, MunicipalServiceDto>
    {
        public importMunicipalServicesRequest1 Build(BuilderOption option, IEnumerable<MunicipalServiceDto> data)
        {
            if (data.Count() > 1)
                throw new ArgumentOutOfRangeException("Превышено макисмальное кол-во объектов типа AdditionalServiceDto, ожидался 1");
            var dto = data.FirstOrDefault();

            return new importMunicipalServicesRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator, option.Get(ParametrType.OrgPPAGUID)),
                importMunicipalServicesRequest = new importMunicipalServicesRequest {
                    Id = RequestID,
                    ImportMainMunicipalService = dto.Values.Select(x => new importMunicipalServicesRequestImportMainMunicipalService {
                        TransportGUID = x.TransportGuid,
                        MainMunicipalServiceName = x.Name,
                        MunicipalResourceRef = new nsiRef {
                            Code = x.ResourceRef.Code,
                            GUID = x.ResourceRef.Guid
                        },
                        MunicipalServiceRef = new nsiRef {
                            Code = x.ServiceRef.Code,
                            GUID = x.ServiceRef.Guid
                        },
                        OKEI = x.Unit.OKEI
                    }).ToArray()
                }
            };
        }
    }
}
