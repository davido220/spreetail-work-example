namespace SpreetailWorkSampleDavidOBrien
{
    class Program
    {
        static void Main(string[] args)
        {
            var appTest = new AppTest();
            appTest.Run();

            var limit = 10000;
            var app = new App(limit);
            app.Run();
        }
    }
}
