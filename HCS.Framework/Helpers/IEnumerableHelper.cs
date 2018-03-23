using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Framework.Helpers
{
    public static class IEnumerableHelper
    {
        /// <summary>
        /// Разделяет последовательность на пакеты заданного размера
        /// </summary>
        /// <typeparam name="T">Параметр типа</typeparam>
        /// <param name="items">Последовательность</param>
        /// <param name="batchSize">Размер пакета</param>
        /// <returns></returns>
        public static IEnumerable<List<T>> ToBatch<T>(this IEnumerable<T> items, int batchSize)
        {
            List<T> batch = new List<T>(batchSize);
            foreach (var item in items) {
                batch.Add(item);
                if (batch.Count == batchSize) {
                    yield return batch;
                    batch = new List<T>(batchSize);
                }
            }
            if (batch.Any())
                yield return batch;
        }

        /// <summary>
        /// Разделяет последовательность на определенное количество пакетов
        /// </summary>
        /// <typeparam name="T">Параметр типа</typeparam>
        /// <param name="items">Последовательность</param>
        /// <param name="batchCount">Количество пакетов</param>
        /// <returns></returns>
        public static IEnumerable<List<T>> ToNBatches<T>(this IReadOnlyCollection<T> items, int batchCount)
        {
            var batchSize = items.Count / batchCount;
            return items.ToBatch(batchSize);
        }
    }
}
