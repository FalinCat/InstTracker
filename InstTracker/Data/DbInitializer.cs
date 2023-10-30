using InstTracker.Data.Model;

namespace InstTracker.Data
{
    internal static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Instances.Any())
            {
                return;
            }

            var instances = new List<Instance>()
            {
                new Instance() {Name = "Логово Крофинов", Schedule = "30 3 * * 2,6"},
                new Instance() {Name = "Трон Падшего Императора", Schedule = "30 3 * * 3,5"},
                new Instance() {Name = "Алтарь Балрога", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Обитель Теней", Schedule = "30 3 * * 4,6"},
                new Instance() {Name = "Святилище Печатей", Schedule = "30 3 * * 2,6"},
                new Instance() {Name = "Обитель Этины", Schedule = "30 3 * * 0,3"},
                new Instance() {Name = "Цитадель Стихий", Schedule = "30 3 * * 1,4"},
                new Instance() {Name = "Храм Энергий Астатина", Schedule = "30 3 * * 0,5"},
                new Instance() {Name = "Гробница Правителей", Schedule = "30 3 * * 0,3"},
                new Instance() {Name = "Древняя Кузница Богов", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Храм Высших Элементов", Schedule = "30 3 * * 0,4"},
                new Instance() {Name = "Небесный Пандемониум", Schedule = "30 3 * * 1,4"},
                new Instance() {Name = "Языческий Храм", Schedule = "30 3 * * 1,4"},
                new Instance() {Name = "Башня Дерзости", Schedule = "30 3 * * 2,6"},
                new Instance() {Name = "Арена Богов", Schedule = "30 3 * * 2,6"},
                new Instance() {Name = "Арена Младших Богов", Schedule = "30 3 * * 3,6"},

                new Instance() {Name = "Октавис - Битва с Октависом", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Кимерианы обычный", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Кимерианы экстрим", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Истхина - Обитель Истхины", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Древний Лабиринт Картии", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Байлор", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Фринтеза - Последнее Пристанище Короля", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Фрея - Замок Снежной Королевы", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Экстрим Фрея", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Тиада - Семя Разрушения", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Дневной Закен", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Валлок", Schedule = "30 3 * * 3,6"},
                new Instance() {Name = "Революционеры Серой Тени", Schedule = "30 3 * * 3,6"},

                new Instance() {Name = "Таути", Schedule = "30 3 * * 3"},
                new Instance() {Name = "Экстрим Таути - (битва на пределе)", Schedule = "30 3 * * 3"},
                new Instance() {Name = "Блесс Октавис - (битва на пределе)", Schedule = "30 3 * * 3"},
                new Instance() {Name = "Блесс Истхина", Schedule = "30 3 * * 3"},
                new Instance() {Name = "Ночной Закен - Битва с Капитаном Голдбергом", Schedule = "30 3 * * 3"},
                new Instance() {Name = "Экимус ( СОИ )", Schedule = "30 3 * * 3"},

                new Instance() {Name = "Экстрим Фринтезза", Schedule = "30 3 * * 4"},

                new Instance() {Name = "Занния, Королева Пиратов", Schedule = "30 3 * * 1,5"},
            };

            context.Instances.AddRange(instances);
            context.SaveChanges();

            var events = new List<EventEntity>()
            {
                new EventEntity() { Name = "TvT", Schedule="50 6,9,14 * * *" },
                new EventEntity() { Name = "TvT", Schedule="30 1,12,17,19,23 * * *" },
                new EventEntity() { Name = "TvT", Schedule="30 16,19,20 * * *" },

                new EventEntity() { Name = "Мясорубка", Schedule="50 4,8,16,20 * * *" },

                new EventEntity() { Name = "Нашествие Эльпи", Schedule="20 2,7,10,14,16,18,20,23 * * *" },

                new EventEntity() { Name = "Счастливые Полчаса", Schedule="0 1,4,6,9,16,19,22 * * *" },
                new EventEntity() { Name = "Счастливые Полчаса", Schedule="30 2,11,14 * * *" },

                new EventEntity() { Name = "Сокровища Тову", Schedule="20 0,4,8,11,13,17,19,21 * * *" },
            };
            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}