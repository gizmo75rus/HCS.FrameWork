using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Service.Async.DeviceMetering.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Helpers;

namespace HCS.Framework.RequestBuilders.DeviceMetering
{
    public class ExportMeteringDeviceHistory : BaseBuilder, IRequestBuilder<exportMeteringDeviceHistoryRequest1>
    {
        public exportMeteringDeviceHistoryRequest1 Build(BuilderOption option)
        {
            return new exportMeteringDeviceHistoryRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator,option.Get(ParametrType.OrgPPAGUID)),
                exportMeteringDeviceHistoryRequest = new exportMeteringDeviceHistoryRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Get(ParametrType.FIASHouseGUID).ToGisString()
                }
            };
        }
    }
}
