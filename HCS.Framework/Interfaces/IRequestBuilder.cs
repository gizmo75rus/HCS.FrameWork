using HCS.Framework.Base;
using HCS.Framework.RequestBuilders;

namespace HCS.Framework.Interfaces
{
    interface IRequestBuilder<TRequest, TData> where TRequest : class where TData : BaseDto
    {
        TRequest[] Build(BuilderOption option, TData data);
    }

    interface IRequestBuilder<TRequest> where TRequest : class
    {
        TRequest Build(BuilderOption option);
    }
}
