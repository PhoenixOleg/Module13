namespace Task_13_6_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = null;
            char[] delimeters = new char[] { ' ', '\r', '\n' };
            Dictionary<string, int> dict = new(); // Словарь: Слово (ключ), Частота (значение)

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

            #region Чтение данных из файла заполнение массива
            try
            {
                string text = File.ReadAllText(args[0]);
                text = new string(text.Where(c => !char.IsPunctuation(c)).ToArray()).ToUpper(); //Убраны знаки пунктуации и все переведено в верхний регистр
                words = text.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                GoodBye(ex.HResult, $"Произошла ошибка {ex.HResult} {ex.Message}");
            }
            #endregion Чтение данных из файла

            #region Заполнение словаря
            if (words != null)
            {
                foreach (string word in words)
                {
                    if (dict.ContainsKey(word)) 
                    {
                        dict[word] += 1;
                    }
                    else
                    {
                        dict.Add(word, 1);
                    }                    
                }
            }
            #endregion Заполнение словаря

            #region Сортировка словаря
            //Сортировка с записью результата в исходный словарь
            //Подглядел здесь https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
            dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            #endregion Сортировка словаря

            #region Вывод результатов
            //Выводим первые 10 строк
            Console.WriteLine("Нечестный вывод 10 победителей:");
            foreach (var key in dict.Keys.Take(10))
                Console.WriteLine($"Слово {key} встречается {dict[key]} раз");

            /*
            Но это "нечестный" ввод 10 победителей. Если бы это были результаты выборов, спортивных соревнований,
            то обязательно надо проверять, а не набрал ли 11-й и т. д. участник столько же баллов, сколько и 10-й
            Например, такой тестовый файл
            a a a a a a a a a a a a a a a 
            b b b b b b b b b b b b b b b
            c c c c c c c c c c c c c c
            d d d d d d d d d d d d d
            e e e e e e e e e e e e
            f f f f f f f f f f f
            g g g g g g g g g g
            i i i i i i i i i
            j j j j j j j j
            k k k k k k k
            l l l l l l l
            m m m m m m
            n n n n n n n

            провалит проверку - k, l, n - "финишировали" вместе
             */

            //Выводим строки с 10-ю максимальными значениями
            Console.WriteLine("\nЧестный вывод победителей c 10 максимальными значениями:");
            int idx = -1;
            string prevKey = "";
            foreach (string key in dict.Keys)
            {
                idx++;
                if (idx <= 9)
                {
                    prevKey = key;
                    Console.WriteLine($"Слово {key} встречается {dict[key]} раз");
                }
                else
                {
                    if (dict[prevKey] == dict[key]) //Сравниваем значение по ключу n-го участника со значением по ключу 10-го победителя
                    {
                        Console.WriteLine($"Слово {key} встречается {dict[key]} раз");
                    }
                    else 
                    { 
                        break; 
                    }
                }
            }
            #endregion Вывод результатов

            GoodBye(0, "");
        }

        private static void GoodBye(int exitCode, string msg)
        {
            if (exitCode != 0)
            {
                Console.WriteLine(msg);
            }

            Console.WriteLine("\nНажмите любую кнопку для выхода");
            Console.ReadKey();
            System.Environment.Exit(exitCode);
        }
    }
}