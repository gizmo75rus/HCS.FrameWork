using System.Collections.Generic;
using HCS.Framework.Dto;

namespace HCS.Framework.SourceData.Nsi
{
    public class AdditionalServiceData : Base.DtoData
    {
        public IEnumerable<UtilityListDto> Values { get; set; }
    }
}
