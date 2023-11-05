namespace Task_13_6_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = null;
            char[] delimeters = new char[] { ' ', '\r', '\n' };

            #region Проверка входного параметра
            if (args.Length == 0 || args.Length > 1)
            {
                GoodBye(-1, "Некоррректно заданы/не заданы аргументы командной строки. Аргументом должен быть полный путь к текстовому файлу. Например, C:\\Files\\Text.txt");
            }

            if (!File.Exists(args[0]))
            {
                GoodBye(-2, $"Файл {args[0]} не существует или к нему нет доступа");
            }
            #endregion Проверка входного параметра

            #region Чтение данных из файла заполнение масс
            try
            {
                string text = File.ReadAllText(args[0]);
                words = text.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                GoodBye(ex.HResult, $"Произошла ошибка {ex.HResult} {ex.Message}");
            }
            #endregion Чтение данных из файла
        }

        private static void GoodBye(int exitCode, string msg)
        {
            if (exitCode != 0)
            {
                Console.WriteLine(msg);
            }

            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadKey();
            System.Environment.Exit(exitCode);
        }
    }
}