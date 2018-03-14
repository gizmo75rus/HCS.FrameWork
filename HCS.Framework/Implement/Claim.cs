using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Framework.Enums;
using HCS.Framework.Interfaces;
using HCS.Globals;

namespace HCS.Framework.Implement
{
    /// <summary>
    /// Запрос для пакета 
    /// </summary>
    internal class Claim : IClaim
    {
        EndPoints _recipient;
        ClaimDirection _direction;
        ICollection<Parameter> _parameters;
        public EndPoints GetRecipient => _recipient;

        public ClaimDirection GetDirection => _direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recepient">Конечная точка получателя </param>
        /// <param name="direction">Направление запроса</param>
        public Claim(EndPoints recepient, ClaimDirection direction)
        {
            _recipient = recepient;
            _direction = direction;
            _parameters = new List<Parameter>();
        }

        /// <summary>
        /// Получить все параметры
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IParameter> GetParameters()
        {
            return _parameters;
        }

        /// <summary>
        /// Получить все параметры c указаным типом
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IEnumerable<IParameter> GetParameters(Type Type)
        {
            return _parameters.Where(x => x.Type == Type);
        }

        /// <summary>
        /// Получить пораметр с именем
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IParameter GetParameter(string name)
        {
            return _parameters.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Добавить параметр
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(IParameter parameter)
        {
            _parameters.Add((Parameter)parameter);
        }
    }
}
