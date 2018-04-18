namespace HCS.Framework.Polices
{
    public enum Actions
    {
        /// <summary>
        /// Требуется бросить исключение
        /// </summary>
        NeedException,

        /// <summary>
        /// Требуется прервать выполнение операции
        /// </summary>
        Abort,

        /// <summary>
        /// Повторить действие
        /// </summary>
        TryAgain,

        /// <summary>
        /// Ошибка, требуется обработка на следующем уровне
        /// </summary>
        Error,

        /// <summary>
        /// Нет объекта
        /// </summary>
        Empty
    }
}

