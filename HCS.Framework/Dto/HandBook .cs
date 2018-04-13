using System;
using HCS.Framework.Base;

namespace HCS.Framework.Dto
{
    public class HandBook : BaseDto
    {
        public Guid? ParentGuid { get; set; }
        public bool IsActual { get; set; }
        public DateTime ModeficateDate { get; set; }
        public int RegistryNumber { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public Unit Unit { get; set; }
    }
}
