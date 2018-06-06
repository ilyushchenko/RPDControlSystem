using RPDControlSystem.Models.RPD;

namespace RPDControlSystem.ExtemtionMethods
{
    public static class DisciplineTypeExtentions
    {
        public static string Name(this DisciplineType discipline, DisciplineType type)
        {
            switch (type)
            {
                case DisciplineType.Base:
                    return "Базовая";
                case DisciplineType.Additional:
                    return "Профильная";
                default:
                    return "Неизвестный тип";
            }
        }
    }
}
