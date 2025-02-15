namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startDependenceId = 1;
        private static int nextDependenceId = startDependenceId;
        internal static int NextDependenceId { get => nextDependenceId++; }

        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        public static DateTime? StartProjectDate { set; get; } = new DateTime(2024, 10, 3);
        public static DateTime Clock { set; get; } = new DateTime(2024, 10, 3);

    }


    internal static List<DO.Dependence> Dependences { get; } = new();
    internal static List<DO.Worker> Workers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
}
