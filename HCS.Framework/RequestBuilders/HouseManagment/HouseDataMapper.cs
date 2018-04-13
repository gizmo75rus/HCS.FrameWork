using System;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Dto.HouseManagment;

namespace HCS.Framework.RequestBuilders.HouseManagment
{
    internal static class HouseDataMapper
    {
        internal static importHouseUORequestApartmentHouse ApartmentHouse(bool isCreate, HouseDto house, Guid transportGuid)
        {
            var apartmentHouse = new importHouseUORequestApartmentHouse {
                Item = isCreate ? ApartmentHouseCreate(house, transportGuid) : (object)ApartmentHouseUpdate(house, transportGuid)
            };
            return apartmentHouse;
        }

        #region МКД
        internal static importHouseUORequestApartmentHouseApartmentHouseToCreate ApartmentHouseCreate(HouseDto house, Guid transportGuid)
        {
            var value = new importHouseUORequestApartmentHouseApartmentHouseToCreate {
                MinFloorCountSpecified = true,
                MinFloorCount = (sbyte)house.MinFloorCount,
                UndergroundFloorCount = house.UndergroundFloorCount,
                BasicCharacteristicts = (ApartmentHouseUOTypeBasicCharacteristicts)house.BasicCharacteristicts(),
                TransportGUID = house.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(house.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { house.CadastrNumber };
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }
            return value;
        }

        private static HouseBasicUOType BasicCharacteristicts(this HouseDto house)
        {
            return new HouseBasicUOType {
                FIASHouseGuid = house.FiasGuid,
                CulturalHeritageSpecified = true,
                CulturalHeritage = house.CulturalHeritage,

                UsedYearSpecified = true,
                UsedYear = (short)house.UsedYear,

                TotalSquareSpecified = true,
                TotalSquare = house.TotalSquare,

                FloorCount = house.FloorCount,

                /// ОКТОМ
                OKTMO = new OKTMORefType { code = house.OKTMO },
                OlsonTZ = new nsiRef {
                    Code = house.OlsonTZ.Code,
                    GUID = house.OlsonTZ.Guid
                },
                State = new nsiRef {
                    Code = house.State.Code,
                    GUID = house.State.Guid
                }

            };
        }

        internal static importHouseUORequestApartmentHouseApartmentHouseToUpdate ApartmentHouseUpdate(HouseDto house, Guid transportGuid)
        {
            var value = new importHouseUORequestApartmentHouseApartmentHouseToUpdate {
                MinFloorCountSpecified = true,
                MinFloorCount = (sbyte)house.MinFloorCount,

                UndergroundFloorCount = house.UndergroundFloorCount,
                BasicCharacteristicts = new HouseBasicUpdateUOType {
                    FIASHouseGuid = house.FiasGuid,
                    CulturalHeritageSpecified = true,
                    CulturalHeritage = house.CulturalHeritage,

                    UsedYearSpecified = true,
                    UsedYear = (short)house.UsedYear,

                    TotalSquareSpecified = true,
                    TotalSquare = house.TotalSquare,

                    FloorCount = house.FloorCount,

                    /// ОКТОМ
                    OKTMO = new OKTMORefType { code = house.OKTMO },
                    OlsonTZ = new nsiRef {
                        Code = house.OlsonTZ.Code,
                        GUID = house.OlsonTZ.Guid
                    },
                    State = new nsiRef {
                        Code = house.State.Code,
                        GUID = house.State.Guid
                    }
                },
                TransportGUID = house.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(house.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { house.CadastrNumber};
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

        internal static importHouseUORequestLivingHouse LivingHouse(bool isCreate, HouseDto house)
        {
            var value = new importHouseUORequestLivingHouse {
                Item = isCreate ? LivingHouseToCreate(house) : (object)LivingHouseToUpdate(house)
            };
            return value;
        }

        #region Жилой дом
        internal static importHouseUORequestLivingHouseLivingHouseToCreate LivingHouseToCreate(HouseDto house)
        {
            var value = new importHouseUORequestLivingHouseLivingHouseToCreate {
                BasicCharacteristicts = house.BasicCharacteristicts(),
                TransportGUID = house.TransportGuid,
                HasBlocksSpecified = false,
                HasMultipleHousesWithSameAddressSpecified = false
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(house.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { house.CadastrNumber };
            }
            else {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
                value.BasicCharacteristicts.Items = new object[] { true };
            }

            return value;
        }

        internal static importHouseUORequestLivingHouseLivingHouseToUpdate LivingHouseToUpdate(HouseDto house)
        {
            var value = new importHouseUORequestLivingHouseLivingHouseToUpdate {
                BasicCharacteristicts = new HouseBasicUpdateUOType {
                    FIASHouseGuid = house.FiasGuid,
                    CulturalHeritageSpecified = true,
                    CulturalHeritage = house.CulturalHeritage,

                    UsedYearSpecified = true,
                    UsedYear = house.UsedYear,

                    TotalSquareSpecified = true,
                    TotalSquare = house.TotalSquare,

                    FloorCount = house.FloorCount,

                    /// ОКТОМ
                    OKTMO = new OKTMORefType { code = house.OKTMO },
                    OlsonTZ = new nsiRef {
                        Code = house.OlsonTZ.Code,
                        GUID = house.OlsonTZ.Guid
                    },
                    State = new nsiRef {
                        Code = house.State.Code,
                        GUID = house.State.Guid
                    }
                },
                TransportGUID = house.TransportGuid
            };

            value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.No_RSO_GKN_EGRP_Registered };
            value.BasicCharacteristicts.Items = new object[] { true };

            if (!string.IsNullOrEmpty(house.CadastrNumber)) {
                value.BasicCharacteristicts.ItemsElementName = new ItemsChoiceType5[] { ItemsChoiceType5.CadastralNumber };
                value.BasicCharacteristicts.Items = new object[] { house.CadastrNumber };
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
