namespace API_Kazakov.Models
{
    /// <summary>
    /// Класс Задачи
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Код задачи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Приоритет задачи
        /// </summary>
        public string Property { get; set;}
        /// <summary>
        /// Дата выполнения задачи
        /// </summary>
        public DateTime DataExcute { get; set;}
        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public bool Done { get; set;}
    }
}
