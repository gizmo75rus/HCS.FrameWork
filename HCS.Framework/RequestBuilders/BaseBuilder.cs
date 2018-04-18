using System;
using HCS.Framework.Base;
using HCS.Helpers;

namespace HCS.Framework.RequestBuilders
{
    public abstract class BaseBuilder
    {
        public event EventHandler<ErrorEventArgs> BuildError;
        public THeaderType Create<THeaderType>(bool IsOperator, object orgPPAGUID) where THeaderType : class
        {
            try {
                var tInstance = Activator.CreateInstance(typeof(THeaderType));

                var props = tInstance.GetType().GetProperties();

                foreach (var prop in props) {
                    switch (prop.Name) {
                        case "Item":
                            if (string.IsNullOrEmpty(orgPPAGUID.ToString().ToLower())) {
                                throw new ArgumentException("Не указан идентификатор организации в ГИС");
                            }
                            prop.SetValue(tInstance, orgPPAGUID.ToString().ToLower());
                            break;
                        case "ItemElementName":
                            prop.SetValue(tInstance, Enum.Parse(prop.PropertyType, "orgPPAGUID"));
                            break;
                        case "MessageGUID":
                            prop.SetValue(tInstance, Guid.NewGuid().ToString());
                            break;
                        case "Date":
                            prop.SetValue(tInstance, DateTime.Now);
                            break;
                        case "IsOperatorSignatureSpecified":
                            if (IsOperator)
                                prop.SetValue(tInstance, true);
                            break;
                        case "IsOperatorSignature":
                            if (IsOperator)
                                prop.SetValue(tInstance, true);
                            break;
                    }
                }

                return tInstance as THeaderType;
            }
            catch (SystemException exc) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, "При сборке запроса произошла ошибка:" + exc.GetChainException()));
            }
            catch (Exception exc) {
                BuildError?.Invoke(this, new ErrorEventArgs(true, "При сборке запроса произошла ошибка:" + exc.GetChainException()));
            }

            return null;
        }
        protected string RequestID = "signed-data-container";
        protected const int LIMIT_BY_REQUEST = 550;
    }
}
