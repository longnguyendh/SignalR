namespace SignalRSample
{
    public static class ViewerCounterDictionaryInMem
    {
        static ViewerCounterDictionaryInMem()
        {
            ViewerCounterDictionary = new Dictionary<string, int>();
            ViewerCounterDictionary.Add(ReactjsCource, 0);
            ViewerCounterDictionary.Add(JavaCourse, 0);
            ViewerCounterDictionary.Add(CsharpCourse, 0);
        }

        public const string CsharpCourse = "Csharp";
        public const string JavaCourse = "Java";
        public const string ReactjsCource = "Reactjs";

        public static Dictionary<string, int> ViewerCounterDictionary;


    }
}
