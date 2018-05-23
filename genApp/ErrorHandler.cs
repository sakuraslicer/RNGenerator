using System;
using System.Collections.Generic;
using System.Text;

namespace genApp
{
    class ErrorHandler
    {
        public static void IncorrectString()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nВходная строка имела неверный формат\n");
            Console.ResetColor();
        }

        public static void GeneralError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nОшибка: некорректный ввод\n");
            Console.ResetColor();
        }
    }
}
