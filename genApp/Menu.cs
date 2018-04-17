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
                Console.WriteLine("");
                Console.WriteLine("\tЧТО ВЫ ХОТИТЕ ВЫПОЛНИТЬ?");
                Console.WriteLine("1 - Сгенерировать значения (коды)");
                Console.WriteLine("2 - Импортировать из файла в базу данных");
                Console.WriteLine("3 - Выход");
                Int64 target = 0;

                target = Convert.ToInt64(Console.ReadLine());

                switch (target)
                {
                    case 1:
                        Generator.Generate();
                        break;

                    case 2:
                        Importer.Import();
                        break;

                    case 3:
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("\nВы ничего не выбрали");
                        break;
                }
            }
            #region Catch
            catch (OutOfMemoryException)
            {
                Console.WriteLine("\nВведено слишком большое число. \nВведите 1, 2 или 3");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nВведены символы. \nВведите 1, 2 или 3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: " + ex.Message.ToString());
            }
            #endregion
        }
    }
}
