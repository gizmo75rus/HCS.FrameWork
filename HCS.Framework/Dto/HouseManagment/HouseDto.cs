using System;
using System.Collections.Generic;
using HCS.Framework.Base;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Dto.HouseManagment
{
    public class HouseDto : BaseDto,IEgrpRelation
    {
        public HouseTypes Type { get; set; }
        public string FiasGuid { get; set; }
        public string CadastrNumber { get; set; }
        public bool HasNotRelation { get; set; }
        public DateTime ModificateDate { get; set; }
        public string UndergroundFloorCount { get; set; }

        public byte MinFloorCount { get; set; }
        public decimal TotalSquare { get; set; }
        public short UsedYear { get; set; }
        public string FloorCount { get; set; }
        public bool CulturalHeritage { get; set; }

        public string OKTMO { get; set; }

        public NsiRef State { get; set; }
        public NsiRef OlsonTZ { get; set; }
        public NsiRef OverhaulFormingKind { get; set; }
        public NsiRef ManagmentType { get; set; }

        public List<EntranceDto> Entrances { get; set; }
        public List<PremisesDto> ResidentialPremises { get; set; }
        public List<PremisesDto> NoResidentialPremises { get; set; }
        public List<BlockDto> Blocks { get; set; }
        public HouseDto()
        {
            Entrances = new List<EntranceDto>();
            ResidentialPremises = new List<PremisesDto>();
            NoResidentialPremises = new List<PremisesDto>();
            Blocks = new List<BlockDto>();
        }
    }

    public class BlockDto : BaseDto, IEgrpRelation
    {
        public string CadastrNumber { get; set; }
        public bool HasNotRelation { get; set; }
        public bool BlockNum { get; set; }
        public decimal TotalArea { get; set; }
        public decimal GrossArea { get; set; }
        public bool IsResential { get; set; }
        public List<RoomDto> Rooms { get; set; }
        public BlockDto()
        {
            Rooms = new List<RoomDto>();
        }

    }

    public class PremisesDto : BaseDto,IEgrpRelation
    {
        public bool IsResential { get; set; }
        public bool? IsCommonProperty { get; set; }
        public string CadastrNumber { get; set; }
        public bool HasNotRelation { get; set; }
        public string EntranceNum { get; set; }
        public string PremiseNumber { get; set; }
        public sbyte Floor { get; set; }
        public decimal? TotalSquare { get; set; }
        public decimal Square { get; set; }
        public decimal? GrossArea { get; set; }
        public NsiRef PremiseCharacteristic { get; set; }
        public List<RoomDto> Rooms { get; set; }
        public PremisesDto()
        {
            Rooms = new List<RoomDto>();
        }

    }

    public class RoomDto : BaseDto,IEgrpRelation {
        public string CadastrNumber { get; set; }
        public bool HasNotRelation { get; set; }
        public string RoomNumber { get; set; }
        public decimal Square { get; set; }

    };

    public class EntranceDto : BaseDto
    {
        public string EntranceNum { get; set; }
        public sbyte StoreysCount { get; set; }
        public int CreationYear { get; set; }
    }

    public class LiftDto : BaseDto
    {
        public string FactoryNum { get; set; }
        public int OperatingLimit { get; set; }
        public NsiRef Type { get; set; }
    }

    public enum HouseTypes
    {
        Apartment,
        Living,
        Blocked
    }
}
