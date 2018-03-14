using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Interfaces;

namespace HCS.Framework.Implement
{
    /// <summary>
    /// Параметр
    /// </summary>
    internal class Parameter : IParameter
    {
        string _name;
        Type _type;
        object _value;
        public string Name => _name;
        public Type Type => _type;
        public object Value => _value;
        public Parameter(string name, Type type, object value)
        {
            _name = name;
            _type = type;
            _value = value;
        }
        public Parameter(string name, object value)
        {
            _name = name;
            _type = value.GetType();
            _value = value;
        }
    }
}
