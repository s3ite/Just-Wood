namespace JustHood_SERVER
{
    public class ConsoleType
    {
        private ConsoleType(string value) { Value = value; }

        public string Value { get; set; }

        public static ConsoleType INFO   { get { return new ConsoleType("INFO"); } }
        public static ConsoleType DEBUG   { get { return new ConsoleType("DEBUG"); } }
        public static ConsoleType WARNING    { get { return new ConsoleType("WARNING"); } }
        public static ConsoleType ERROR { get { return new ConsoleType("ERROR"); } }

    }
}