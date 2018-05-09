// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace genApp
{
    class Menu
    {
        public static void MainMenu()
        {
            try
            {
                Console.WriteLine("\tЧТО НЕОБХОДИМО ВЫПОЛНИТЬ?\n");
                Console.WriteLine("1 - Генерация");
                Console.WriteLine("2 - Импорт");
                Console.WriteLine("3 - Удаление");
                Console.WriteLine("4 - Выход");
                Int16 target = 0;
                target = Convert.ToInt16(Console.ReadLine());

                switch (target)
                {
                    case 1:
                        Generator.Generate();
                        break;

                    case 2:
                        Importer.Import();
                        break;

                    case 3:
                        Deleter.Delete();
                        break;

                    case 4:
                        Environment.Exit(1);
                        break;

                    default:
                        ErrorHandler.IncorrectString();
                        break;
                }
            }
            catch (Exception)
            {
                ErrorHandler.IncorrectString();
            }
        }
    }
}
