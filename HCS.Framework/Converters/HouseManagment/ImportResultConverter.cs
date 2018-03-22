using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;
using HCS.Framework.Dto;
using HCS.Framework.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Framework.Converters.HouseManagment
{
    public class ImportResultConverter : IConverter<ImportResult, ImportResultDto> 
    {
        public event EventHandler<ErrorEventArgs> ErrorEvent;

        public bool TryConvert(ImportResult value, out IEnumerable<ImportResultDto> result)
        {
            var dto =  new List<ImportResultDto>();
            if (value.Items.OfType<ErrorMessageType>().Any()) {
                var fault = value.Items.OfType<ErrorMessageType>().FirstOrDefault();
                ErrorEvent?.Invoke(this, new ErrorEventArgs(new Base.Fault(fault.ErrorCode, fault.Description)));
                result = null;
                return false;
            }

            value.Items.OfType<ImportResultCommonResult>().ToList().ForEach(x => {
                var fault = x.Items.OfType<CommonResultTypeError>();

                dto.Add(new ImportResultDto {
                    TransportGuid = x.TransportGUID,
                    Guid = x.GUID,
                    IsBroken = fault.Any(),
                    UniqueNumber = fault.Any()?string.Empty:x.Items.OfType<string>().First(),
                    ErrorCode = fault.Any()?fault.First().ErrorCode:string.Empty,
                    ErrorMessage = fault.Any()?fault.First().Description:string.Empty,
                    Name = Enum.GetName(typeof(ItemChoiceType1),x.ItemElementName),
                    Package = x.Item
                });
            });
            result = dto;
            return true;
        }
    }
}
