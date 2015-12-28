namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс расширения базовых объектов для отношений один к одному
    /// </summary>
    /// <remarks>
    /// Расширение базовых объектов с использованием дополнительной таблицы имеющей
    /// связь с базовым объектом один к одному. 
    /// <para>Для получения данных используется маппинг хранимых процедур по следующей
    /// схеме: схема данных + имя типа + тип процедуры, т.е. для объекта расширения
    /// корреспондента <b>&quot;People&quot; </b>(таблица &quot;Contractor.People&quot;)
    /// используются хранимые процедуры:</para>
    /// <para> </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Тип процедуры</term>
    /// <description>Полное имя</description></listheader>
    /// <item>
    /// <term>Загрузка данных</term>
    /// <description>Contractor.PeopleLoad</description></item>
    /// <item>
    /// <term>Обновление данных</term>
    /// <description>Contractor.PeopleUpdate</description></item>
    /// <item>
    /// <term>Удаление данных</term>
    /// <description>Contractor.PeopleDelete</description></item>
    /// <item>
    /// <term>Создание</term>
    /// <description>Contractor.PeopleCreate</description></item></list>* обратите
    /// вмнимание &quot;Полное имя&quot; - это ключ маппинга хранимой процедуры, а не
    /// полное имя хранимой процедуры в базе данных. 
    /// <para> </para>
    /// </remarks>
    public interface IRelationSingle
    {
        string Schema { get; }
    }
}