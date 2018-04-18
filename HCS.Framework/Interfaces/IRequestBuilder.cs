using HCS.Framework.Base;
using HCS.Framework.RequestBuilders;

namespace HCS.Framework.Interfaces
{
    interface IRequestBuilder<TRequst, TSourceData> where TRequst : class where TSourceData : DtoData
    {
        TRequst Build(BuilderOption option, TSourceData data);
    }

    interface IRequestBuilder<TRequest> where TRequest : class
    {
        TRequest Build(BuilderOption option);
    }
}
