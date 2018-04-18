using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Interfaces;
using HCS.Framework.Enums;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Helpers;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    public class ImportMeteringDevice : BaseBuilder, IRequestBuilder<importMeteringDeviceDataRequest1, CounterDto>
    {
        public importMeteringDeviceDataRequest1 Build(BuilderOption option, IEnumerable<CounterDto> data)
        {
            if (data.Count() > LIMIT_BY_REQUEST)
                throw new ArgumentOutOfRangeException("Превышено максимальное кол-во CounterDto для запроса");

            return new importMeteringDeviceDataRequest1 {
                RequestHeader = Create<RequestHeader>(option.IsOperator,option.Get(ParametrType.OrgPPAGUID)),
                importMeteringDeviceDataRequest = new importMeteringDeviceDataRequest {
                    Id = RequestID,
                    FIASHouseGuid = option.Get(ParametrType.FIASHouseGUID).ToGisString(),
                    MeteringDevice = MapMeteringDevices(option.Command, data)
                }
            };
        }

        importMeteringDeviceDataRequestMeteringDevice[] MapMeteringDevices(RequestCMD cmd,IEnumerable<CounterDto> data)
        {
            switch (cmd) {
                case RequestCMD.Create:
                    return data.Select(dto => new importMeteringDeviceDataRequestMeteringDevice {
                        TransportGUID = dto.TransportGuid,
                        Item = DeviceDataToCreate(dto)
                    }).ToArray();
                case RequestCMD.Update:
                    return data.Select(dto => new importMeteringDeviceDataRequestMeteringDevice {
                        TransportGUID = dto.TransportGuid,
                        Item = DeviceDataToCreate(dto)
                    }).ToArray();
                default:
                    throw new NotImplementedException();
            }

        }
        MeteringDeviceFullInformationType DeviceDataToCreate(CounterDto dto) {
            var value = new MeteringDeviceFullInformationType {
                //Объем реусурса определяетс несколькими ПУ
                Item = false,
                BasicChatacteristicts = GetBasicCharacteristic(dto)
            };

            switch (dto.ResourceType) {
                case ResourceType.Electrical:
                    value.Items = new object[] {
                        GetElectricType(dto)
                    };
                    break;
                case ResourceType.Municipal:
                    value.Items = new object[] {
                        GetNotElectricType(dto)
                    };
                    break;
            }
            return value;
        }
        importMeteringDeviceDataRequestMeteringDeviceDeviceDataToUpdate DeviceDataToUpdate(CounterDto dto)
        {
            var value = new importMeteringDeviceDataRequestMeteringDeviceDeviceDataToUpdate {
                MeteringDeviceVersionGUID = dto.MeteringDeviceVersionGUID,
                Item = new MeteringDeviceFullInformationType {
                    Item = false,
                    Items = dto.ResourceType == ResourceType.Electrical ? 
                        new object[] { GetElectricType(dto) } : new object[] {  GetNotElectricType(dto) },
                    BasicChatacteristicts = GetBasicCharacteristic(dto)
                }
            };

            return value;
        }

        MunicipalResourceElectricType GetElectricType(CounterDto dto){
            return new MunicipalResourceElectricType {
                TransformationRatioSpecified = false,
                MeteringValueT1 = dto.Value.T1,
                MeteringValueT2Specified = dto.Value.T2.HasValue,
                MeteringValueT2 = dto.Value.T2.HasValue ? dto.Value.T2.Value : 0,
                MeteringValueT3Specified = dto.Value.T3.HasValue,
                MeteringValueT3 = dto.Value.T3.HasValue ? dto.Value.T3.Value : 0
            };
        }

        MunicipalResourceNotElectricType GetNotElectricType(CounterDto dto)
        {
            return new MunicipalResourceNotElectricType {
                MeteringValue = dto.Value.T1,
                MunicipalResource = new nsiRef {
                    Code = dto.Resource.Code,
                    GUID = dto.Resource.Guid
                }
            };
        }

        MeteringDeviceBasicCharacteristicsType GetBasicCharacteristic(CounterDto dto)
        {
            var value = new MeteringDeviceBasicCharacteristicsType {
               CommissioningDateSpecified = true,
               CommissioningDate = dto.CommissioningDate,
               FactorySealDateSpecified = dto.FactorySealDate.HasValue,
               FactorySealDate = dto.FactorySealDate.HasValue? dto.FactorySealDate.Value : new DateTime(),
               FirstVerificationDateSpecified = dto.FirstVerificationDate.HasValue,
               FirstVerificationDate = dto.FirstVerificationDate.HasValue ? dto.FirstVerificationDate.Value : new DateTime(),
               InstallationDateSpecified = dto.InstallationDate.HasValue,
               InstallationDate = dto.InstallationDate.HasValue ? dto.InstallationDate.Value : new DateTime(),
               MeteringDeviceModel = dto.Model,
               MeteringDeviceStamp = dto.Stamp,
               MeteringDeviceNumber = dto.Number,
               PressureSensor = false,
               RemoteMeteringMode = false,
               TemperatureSensor = false,
            };

            switch (dto.Type) {
                case CounterType.ApartmentHouseDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeApartmentHouseDevice {
                        AccountGUID = new string[] { dto.AccountGUID }
                    };
                    break;
                case CounterType.CollectiveApartmentDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeCollectiveApartmentDevice {
                        AccountGUID = new string[] { dto.AccountGUID},
                        PremiseGUID = dto.PremisesGUID
                    };
                    break;
                case CounterType.CollectiveDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeCollectiveDevice {
                        PressureSensingElementInfo = "Н/А",
                        TemperatureSensingElementInfo = "Н/А"
                    };
                    break;
                case CounterType.LivingRoomDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeLivingRoomDevice {
                        AccountGUID = new string[] { dto.AccountGUID },
                        LivingRoomGUID = new string[] { dto.PremisesGUID }
                    };
                    break;
                case CounterType.NonResidentialPremiseDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeNonResidentialPremiseDevice {
                        AccountGUID = new string[] { dto.AccountGUID },
                        PremiseGUID = dto.PremisesGUID 
                    };
                    break;
                case CounterType.ResidentialPremiseDevice:
                    value.Item = new MeteringDeviceBasicCharacteristicsTypeResidentialPremiseDevice {
                        AccountGUID = new string[] { dto.AccountGUID },
                        PremiseGUID = dto.PremisesGUID
                    };
                    break;
                default:
                    throw new NotImplementedException();
            }

            return value;
        }

    }
}
