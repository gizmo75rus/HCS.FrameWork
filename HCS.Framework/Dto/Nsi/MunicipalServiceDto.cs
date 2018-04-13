using System.Collections.Generic;
using HCS.Framework.Base;

namespace HCS.Framework.Dto.Nsi
{
    public class MunicipalServiceDto : BaseDto
    {
        public IEnumerable<UtilityListDto> Values { get; set; }
    }
}
