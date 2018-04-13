using System.Collections.Generic;
using System.Linq;
using HCS.Framework.Enums;

namespace HCS.Framework.RequestBuilders
{
    public class BuilderOption
    {
        public Dictionary<ParametrType, object> Params { get; set; }
        public RequestDirection Direction { get; set; }
        public RequestCMD Command { get; set; }
        public bool IsOperator { get; set; }
        public BuilderOption()
        {
            Params = new Dictionary<ParametrType, object>();
        }
        public void Add(ParametrType type,object value)
        {
            if (Params.Any(x => x.Key == type))
                return;

            Params.Add(type, value);
        }
        public object Get(ParametrType type)
        {
            return Params[type];
        }
    }
}
