namespace TestConsole.Loggers
{
    static partial class Program
    {
        public abstract class DebugLogger : Logger
        {
            public abstract void Log(string Message, string Category);
        }
    }

    
}
