using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Base;

namespace HCS.Framework.Dto
{
    public class ImportResultDto : BaseDto
    {
        public bool IsBroken { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        public object Package { get; set; }
    }
}
