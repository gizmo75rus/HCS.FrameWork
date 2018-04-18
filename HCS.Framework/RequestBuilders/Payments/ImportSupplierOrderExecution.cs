using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.Payment.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.Payment;

namespace HCS.Framework.RequestBuilders.Payments
{
    public class ImportSupplierOrderExecution : BaseBuilder, IRequestBuilder<importSupplierNotificationsOfOrderExecutionRequest1, OrderDataDto>
    {
        public importSupplierNotificationsOfOrderExecutionRequest1 Build(BuilderOption option, IEnumerable<OrderDataDto> data)
        {
            var dto = data.FirstOrDefault();
            return new importSupplierNotificationsOfOrderExecutionRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator,option.Get(ParametrType.OrgPPAGUID)),
                importSupplierNotificationsOfOrderExecutionRequest = new importSupplierNotificationsOfOrderExecutionRequest {
                    Id = RequestID,
                    SupplierNotificationOfOrderExecution = getOrders(dto.Orders)
                }
            };
        }

        importSupplierNotificationsOfOrderExecutionRequestSupplierNotificationOfOrderExecution[] getOrders(IEnumerable<OrderExecutionDto> data)
        {
            var value = new List<importSupplierNotificationsOfOrderExecutionRequestSupplierNotificationOfOrderExecution>();
            data.ToList().ForEach(dto => {
                value.Add(new importSupplierNotificationsOfOrderExecutionRequestSupplierNotificationOfOrderExecution {
                    TransportGUID = dto.TransportGuid,
                    Amount = dto.Amount,
                    ItemElementName = ItemChoiceType1.ServiceID,
                    Item = dto.ServiceID,
                    OrderDate = dto.OrderDate,
                });
            });

            return value.ToArray();
        }
    }
}
