using System;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Dto.HouseManagment;
using HCS.Framework.Enums;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    internal static class HouseDataMapper
    {
        internal static importHouseUORequestApartmentHouse MapToApartmentHouseUO(this HouseDto dto, RequestCMD cmd)
        {
            var value = new importHouseUORequestApartmentHouse();
            switch (cmd) {
                case RequestCMD.Create:
                    value.Item = ApartmentHouseCreate(dto);
                    break;
                case RequestCMD.Update:
                    value.Item = ApartmentHouseUpdate(dto);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return value;
        }

        internal static importHouseRSORequestApartmentHouse MapToApartmentHouseRSO(this HouseDto dto,RequestCMD cmd)
        {
            var value = new importHouseRSORequestApartmentHouse();
            switch (cmd) {
                default:
                    throw new NotImplementedException();
            }
        }

        internal static importHouseUORequestLivingHouse MapToLivingHouseUO(this HouseDto dto, RequestCMD cmd)
        {
            var value = new importHouseUORequestLivingHouse();

            switch (cmd) {
                case RequestCMD.Create:
                    value.Item = LivingHouseToCreate(dto);
                    break;
                case RequestCMD.Update:
                    value.Item = LivingHouseToUpdate(dto);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return value;
        }

        internal static importHouseRSORequestLivingHouse MapToLivingHouseRSO(this HouseDto dto,RequestCMD cmd)
        {
            throw new NotImplementedException();
        }

        #region МКД
        internal static importHouseUORequestApartmentHouseApartmentHouseToCreate ApartmentHouseCreate(HouseDto dto)
        {
            var value = new importHouseUORequestApartmentHouseApartmentHouseToCreate {
                MinFloorCountSpecified = true,
                MinFloorCount = (sbyte)dto.MinFloorCount,
                UndergroundFloorCount = dto.UndergroundFloorCount,
                BasicCharacteristicts = (ApartmentHouseUOTypeBasicCharacteristicts)dto.BasicCharacteristicts(),
                TransportGUID = dto.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(dto.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { dto.CadastrNumber };
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }
            return value;
        }

        private static HouseBasicUOType BasicCharacteristicts(this HouseDto dto)
        {
            return new HouseBasicUOType {
                FIASHouseGuid = dto.FiasGuid,
                CulturalHeritageSpecified = true,
                CulturalHeritage = dto.CulturalHeritage,

                UsedYearSpecified = true,
                UsedYear = (short)dto.UsedYear,

                TotalSquareSpecified = true,
                TotalSquare = dto.TotalSquare,

                FloorCount = dto.FloorCount,

                /// ОКТОМ
                OKTMO = new OKTMORefType { code = dto.OKTMO },
                OlsonTZ = new nsiRef {
                    Code = dto.OlsonTZ.Code,
                    GUID = dto.OlsonTZ.Guid
                },
                State = new nsiRef {
                    Code = dto.State.Code,
                    GUID = dto.State.Guid
                }

            };
        }

        internal static importHouseUORequestApartmentHouseApartmentHouseToUpdate ApartmentHouseUpdate(HouseDto dto)
        {
            var value = new importHouseUORequestApartmentHouseApartmentHouseToUpdate {
                MinFloorCountSpecified = true,
                MinFloorCount = (sbyte)dto.MinFloorCount,

                UndergroundFloorCount = dto.UndergroundFloorCount,
                BasicCharacteristicts = new HouseBasicUpdateUOType {
                    FIASHouseGuid = dto.FiasGuid,
                    CulturalHeritageSpecified = true,
                    CulturalHeritage = dto.CulturalHeritage,

                    UsedYearSpecified = true,
                    UsedYear = (short)dto.UsedYear,

                    TotalSquareSpecified = true,
                    TotalSquare = dto.TotalSquare,

                    FloorCount = dto.FloorCount,

                    /// ОКТОМ
                    OKTMO = new OKTMORefType { code = dto.OKTMO },
                    OlsonTZ = new nsiRef {
                        Code = dto.OlsonTZ.Code,
                        GUID = dto.OlsonTZ.Guid
                    },
                    State = new nsiRef {
                        Code = dto.State.Code,
                        GUID = dto.State.Guid
                    }
                },
                TransportGUID = dto.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(dto.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { dto.CadastrNumber};
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }
            return value;
        }

        internal static importHouseUORequestApartmentHouseResidentialPremisesResidentialPremisesToCreate ResPremiseCreate(PremisesDto premise)
        {
            if (premise.PremiseCharacteristic == null)
                throw new ArgumentException("Не указана характеристика помещения");

            var value = new importHouseUORequestApartmentHouseResidentialPremisesResidentialPremisesToCreate {
                Item = (string.IsNullOrEmpty(premise.EntranceNum) ? (object)true : premise.EntranceNum),
                Item1 = (premise.GrossArea == null ? (object)true : (decimal)premise.GrossArea),
                PremisesNum = premise.PremiseNumber,
 
                TotalArea = premise.TotalSquare.Value,
                PremisesCharacteristic = new nsiRef {
                    Code = premise.PremiseCharacteristic.Code,
                    GUID = premise.PremiseCharacteristic.Guid
                },
                TransportGUID = premise.TransportGuid
            };

            value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.Items = new object[] { true };

            if(premise.HasNotRelation == false) {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.Items = new object[] { premise.CadastrNumber };
            }
            else {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.Items = new object[] { true };
            }
            return value;
        }

        internal static importHouseUORequestApartmentHouseResidentialPremisesResidentialPremisesToUpdate ResPremiseUpdate(PremisesDto premise)
        {
            if (premise.PremiseCharacteristic == null)
                throw new ArgumentException("Не указана характеристика помещения");

            var value = new importHouseUORequestApartmentHouseResidentialPremisesResidentialPremisesToUpdate {
                Item = (string.IsNullOrEmpty(premise.EntranceNum) ? (object)true : premise.EntranceNum),
                Item1 = (premise.GrossArea == null ? (object)true : (decimal)premise.GrossArea),
                PremisesNum = premise.PremiseNumber,

                PremisesGUID = premise.Guid,

                TotalAreaSpecified = (premise.TotalSquare == null ? false : true),
                TotalArea = (premise.TotalSquare == null ? 0 : (decimal)premise.TotalSquare),

                PremisesCharacteristic = new nsiRef {
                    Code = premise.PremiseCharacteristic.Code,
                    GUID = premise.PremiseCharacteristic.Guid
                },

                TerminationDateSpecified = false,
                TransportGUID = premise.TransportGuid
            };


            value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.Items = new object[] { true };

            if (premise.HasNotRelation == false) {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.Items = new object[] { premise.CadastrNumber };
            }
            else {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.Items = new object[] { true };
            }
            return value;
        }

        internal static importHouseUORequestApartmentHouseNonResidentialPremiseToCreate NonResPremiseCreate(PremisesDto premise)
        {
            var value = new importHouseUORequestApartmentHouseNonResidentialPremiseToCreate {
                PremisesNum = premise.PremiseNumber,
                TotalArea = !premise.TotalSquare.HasValue ? 0 : (decimal)premise.TotalSquare,
                IsCommonProperty = !premise.IsCommonProperty.HasValue? false: premise.IsCommonProperty.Value,
                TransportGUID = premise.TransportGuid
            };

            value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.Items = new object[] { true };

            if (premise.HasNotRelation == false) {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.Items = new object[] { premise.CadastrNumber };
            }
            else {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.Items = new object[] { true };
            }
            return value;
        }

        internal static importHouseUORequestApartmentHouseNonResidentialPremiseToUpdate NonResPremiseUpdate(PremisesDto premise)
        {
            var value = new importHouseUORequestApartmentHouseNonResidentialPremiseToUpdate {
                PremisesGUID = premise.Guid,
                PremisesNum = premise.PremiseNumber,

                TotalAreaSpecified = premise.TotalSquare == null ? false : true,
                TotalArea = premise.TotalSquare == null ? 0 : (decimal)premise.TotalSquare,

                IsCommonPropertySpecified = premise.IsCommonProperty == null ? false : true,
                IsCommonProperty = premise.IsCommonProperty == null ? false : (bool)premise.IsCommonProperty,

                TerminationDateSpecified = false,

                TransportGUID = premise.TransportGuid
            };

            value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.Items = new object[] { true };

            if (premise.HasNotRelation == false) {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.Items = new object[] { premise.CadastrNumber };
            }
            else {
                value.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.Items = new object[] { true };
            }
            return value;
        }

        internal static importHouseUORequestApartmentHouseEntranceToCreate EntranceCreate(EntranceDto entrance)
        {
            return new importHouseUORequestApartmentHouseEntranceToCreate {
                CreationYearSpecified = true,
                CreationYear = (short)entrance.CreationYear,

                EntranceNum = entrance.EntranceNum,

                StoreysCountSpecified = true,
                StoreysCount = (sbyte)entrance.StoreysCount,

                TransportGUID = entrance.TransportGuid
            };
        }

        internal static importHouseUORequestApartmentHouseEntranceToUpdate EntranceUpdate(EntranceDto entrance)
        {
            return new importHouseUORequestApartmentHouseEntranceToUpdate {
                CreationYearSpecified = true,
                CreationYear = (short)entrance.CreationYear,

                StoreysCountSpecified = true,
                StoreysCount = entrance.StoreysCount,

                EntranceGUID = entrance.Guid,
                EntranceNum = entrance.EntranceNum,

                TerminationDateSpecified = false,

                TransportGUID = entrance.TransportGuid
            };
        }
        #endregion

        #region Жилой дом
        internal static importHouseUORequestLivingHouseLivingHouseToCreate LivingHouseToCreate(HouseDto dto)
        {
            var value = new importHouseUORequestLivingHouseLivingHouseToCreate {
                BasicCharacteristicts = dto.BasicCharacteristicts(),
                TransportGUID = dto.TransportGuid,
                HasBlocksSpecified = false,
                HasMultipleHousesWithSameAddressSpecified = false
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(dto.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { dto.CadastrNumber };
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }

            return value;
        }

        internal static importHouseUORequestLivingHouseLivingHouseToUpdate LivingHouseToUpdate(HouseDto dto)
        {
            var value = new importHouseUORequestLivingHouseLivingHouseToUpdate {
                BasicCharacteristicts = new HouseBasicUpdateUOType {
                    FIASHouseGuid = dto.FiasGuid,
                    CulturalHeritageSpecified = true,
                    CulturalHeritage = dto.CulturalHeritage,

                    UsedYearSpecified = true,
                    UsedYear = dto.UsedYear,

                    TotalSquareSpecified = true,
                    TotalSquare = dto.TotalSquare,

                    FloorCount = dto.FloorCount,

                    /// ОКТОМ
                    OKTMO = new OKTMORefType { code = dto.OKTMO },
                    OlsonTZ = new nsiRef {
                        Code = dto.OlsonTZ.Code,
                        GUID = dto.OlsonTZ.Guid
                    },
                    State = new nsiRef {
                        Code = dto.State.Code,
                        GUID = dto.State.Guid
                    }
                },
                TransportGUID = dto.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(dto.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { dto.CadastrNumber };
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }

            return value;
        }

        internal static importHouseUORequestLivingHouseBlocks LivingHouseBlocks()
        {
            var value = new importHouseUORequestLivingHouseBlocks {


            };
            return value;
        }

        internal static importHouseUORequestLivingHouseLivingRoomToCreate LivingRoomToCreate()
        {
            var value = new importHouseUORequestLivingHouseLivingRoomToCreate();
            return value;
        }

        internal static importHouseUORequestLivingHouseLivingRoomToUpdate LivingRoomToUpdate()
        {
            var value = new importHouseUORequestLivingHouseLivingRoomToUpdate();
            return value;
        }
        #endregion
    }
}
