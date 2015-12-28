/*
namespace BusinessObjects.Rules
{
    public delegate bool RuleHandler<T>(T target);
    /// <summary>
    /// Интерфейс бизнес правила
    /// </summary>
    public interface IBusinessRuleMethod<T> : IBusinessMethod
    {
        /// <summary>
        /// Приоритет
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Описание
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Уровень ошибки при проверке
        /// </summary>
        RuleSeverity Severity { get; set; }
        /// <summary>
        /// Остановка процесса проверки
        /// </summary>
        bool StopProcessing { get; set; }

        /// <summary>
        /// Проверка соответствия бизнес правилу
        /// </summary>
        ///<param name="target">Целевой объект</param>
        /// <returns></returns>
        bool Invoke(T target);
        /// <summary>
        /// Целевой объект
        /// </summary>
        T Target { get; set; }
        /// <summary>
        /// Выполнен ли метод
        /// </summary>
        bool Passed { get; set; }
        /// <summary>
        /// Информация о выполнении
        /// </summary>
        string Info { get; set; }
        string Errors { get; set; }

    }

}
*/
