using HCS.Framework.Base;

namespace HCS.Framework.Dto
{
    public class UtilityListDto : BaseDto
    {
        public NsiRef ResourceRef { get; set; }
        public NsiRef ServiceRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
    }
}
