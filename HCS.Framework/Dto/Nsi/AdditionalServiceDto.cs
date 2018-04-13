using HCS.Framework.Base;
using System.Collections.Generic;

namespace HCS.Framework.Dto.Nsi
{
    public class AdditionalServiceDto : BaseDto
    {
        public IEnumerable<UtilityListDto> Values { get; set; }
    }

}
