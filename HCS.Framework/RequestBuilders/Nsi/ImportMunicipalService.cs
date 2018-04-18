using System.Linq;
using HCS.Service.Async.Nsi.v11_10_0_13;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Framework.DataServices.Nsi;

namespace HCS.Framework.RequestBuilders.Nsi
{
    public class ImportMunicipalService : BaseBuilder, IRequestBuilder<importMunicipalServicesRequest1, MunicipalServiceData>
    {
        public importMunicipalServicesRequest1 Build(BuilderOption option, MunicipalServiceData dto)
        {

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
