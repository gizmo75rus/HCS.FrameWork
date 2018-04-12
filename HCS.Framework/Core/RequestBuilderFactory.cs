using System;
using System.Collections.Generic;
using System.Linq;
using HCS.Framework.Base;
using HCS.Framework.Interfaces;
using HCS.Framework.RequestBuilders;
using HCS.Helpers;

namespace HCS.Framework.Core
{
    public class RequestBuilderFactory
    {
        Dictionary<KeyContainer, Type> _container = new Dictionary<KeyContainer, Type>();

        public event EventHandler<ErrorEventArgs> BuildError;

        public void Add<TRequest, TBuilder>(BuilderOption option)
        {
            if (_container.Any(x => x.Key.RequestType == typeof(TRequest) && x.Key.Option.Equals(option)))
                return;

            _container.Add(new KeyContainer { RequestType = typeof(TRequest), Option = option }, typeof(TBuilder));
        }
        public bool TryBuild<TRequest, TDto>(BuilderOption option, TDto data, out TRequest[] result)
            where TRequest : class
            where TDto : BaseDto
        {
            result = null;
            if (_container.Any(x => x.Key.RequestType == typeof(TRequest) && x.Key.Option.Equals(option)) == false) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, $"При сборке запроса {nameof(TRequest)} произошла ошибка: нет зарегистрированного сборщика запроса"));
                return false;
            }

            var type = _container.FirstOrDefault(x => x.Key.RequestType == typeof(TRequest) && x.Key.Option.Equals(option)).Value;
            var instance = Activator.CreateInstance(type);
            var builder = (IRequestBuilder<TRequest, TDto>)instance;

            try {
                result = builder.Build(option, data);
                return true;
            } catch (Exception ex) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, $"При сборке запроса {nameof(TRequest)} произошла ошибка:{ex.GetChainException()}"));
                return false;
            }
        }

        public bool TryBuild<TRequest>(BuilderOption option, out TRequest result) where TRequest : class
        {
            result = null;
            if (_container.Any(x => x.Key.RequestType == typeof(TRequest) && x.Key.Option.Equals(option)) == false) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, $"При сборке запроса {nameof(TRequest)} произошла ошибка: нет зарегистрированного сборщика запроса"));
                return false;
            }
            var type = _container.FirstOrDefault(x => x.Key.RequestType == typeof(TRequest) && x.Key.Option.Equals(option)).Value;
            var instance = Activator.CreateInstance(type);
            var builder = (IRequestBuilder<TRequest>)instance;

            try {
                result = builder.Build(option);
                return true;
            }
            catch (Exception ex) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, $"При сборке запроса {nameof(TRequest)} произошла ошибка:{ex.GetChainException()}"));
                return false;
            }

        }
        internal class KeyContainer
        {
            internal BuilderOption Option { get; set; }
            internal Type RequestType { get; set; }
        }

    }

}
