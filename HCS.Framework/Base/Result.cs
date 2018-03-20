using HCS.Interfaces;

namespace HCS.Framework.Base
{
    public class Result<TValue> where TValue : class
    {
        public bool HasError { get; set; }
        public IFault Fault { get; set; }
        public TValue Value { get; set; }
    }
}
