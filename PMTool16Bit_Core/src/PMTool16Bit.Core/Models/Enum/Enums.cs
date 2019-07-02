namespace PMTool16Bit.Models.Enum
{
    public enum PriorityLevelEnum
    {
        Low,
        Medium,
        High,
        Critical
    }

    public static class PriorityLevels
    {
        public static string EnumToString(int priorityLevel)
        {
            var priorityLevelEnum = (PriorityLevelEnum)priorityLevel;
            return priorityLevelEnum.ToString();
        }
    }
}