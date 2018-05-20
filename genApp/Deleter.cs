using System;
using System.IO;

namespace genApp
{
    class Deleter
    {
        public static void Delete()
        {

            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();

            signin:
            string SQL = string.Empty;
            string SQL2 = string.Empty;
            Console.WriteLine("\nВведите пароль для доступа к базе данных: ");
            string password = Console.ReadLine();

            if (password == "9")
            {
                Menu.MainMenu();
            }

            conn.ConnectionString = "server = 127.0.0.1; uid = root; pwd = " + password + "; database = DaidoMetalRussiaDB";

            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНекорректный пароль от базы данных");
                Console.ResetColor();
                goto signin;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Соединение с базой данных установлено\n");
            Console.ResetColor();

            menu:
            Console.WriteLine("1 - Очистить всю таблицу\n2 - Удалить диапазон кодов\n3 - Удалить файл из базы\n4 - Назад");
            Int32 target = Convert.ToInt32(Console.ReadLine());

            switch (target)
            {
                case 1:
                    Console.WriteLine("Вы действительно хотите очистить всю таблицу? y/n");
                    String answer = Console.ReadLine();
                    if(answer == "y")
                    {
                        SQL = "TRUNCATE TABLE code";
                        cmd.Connection = conn;
                        cmd.CommandText = SQL;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nТаблица успешно очищена");
                        Console.ResetColor();
                        goto menu;
                    }
                    else if(answer == "n")
                    {
                        goto menu;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nНекорректный ввод\n");
                        Console.ResetColor();
                        goto menu;
                    }
                case 2:
                    ReWrite:
                    Console.Write("\nУдалить в диапазоне \nОт: ");
                    Int64 first = Convert.ToInt64(Console.ReadLine());
                    Console.Write("До: ");
                    Int64 second = Convert.ToInt64(Console.ReadLine());
                    
                    if(first > second)
                    {
                        Console.WriteLine("Возможно, вы имели в виду от: " + second + " до: " + first + "\ny/n");
                        String ReWrite = Console.ReadLine();
                        if(ReWrite == "y")
                        {
                            Int64 middle = first;
                            first = second;
                            second = middle;
                        }
                        else if(ReWrite == "n")
                        {
                            Console.WriteLine("Ошибка: " + first + " > " + second);
                            goto ReWrite;
                        }
                        else
                        {
                            ErrorHandler.IncorrectString();
                            goto ReWrite;
                        }
                    }

                    for (Int64 i = first; i <= second; i++)
                    {
                        SQL = "DELETE FROM code WHERE id = " + i;    
                        cmd.Connection = conn;
                        cmd.CommandText = SQL;
                        cmd.ExecuteNonQuery();
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nУдаление в диапазоне от " + first + " до " + second + " успешно завершено\n");
                    Console.ResetColor();
                    conn.Close();
                    goto menu;
                case 3:
                    EnterAgain:
                    Console.WriteLine("\nВведите имя файла [без расширения] для удаления из базы данных ('9' для выхода в главное меню):");
                    string DeleteFileName = Console.ReadLine();


                    if (DeleteFileName == "9")
                    {
                        Menu.MainMenu();
                    }

                    string vTemp = String.Empty;

                    try
                    {
                        using (StreamReader sr = new StreamReader(File.Open("files/" + DeleteFileName + ".txt", FileMode.Open)))
                        {
                            Console.WriteLine("\nВыполняется удаление...");

                            while (!sr.EndOfStream)
                            {
                                string[] vvv;
                                vTemp = sr.ReadLine();
                                vvv = vTemp.Split(' ');
                                vTemp = vvv[0] + vvv[1] + vvv[2] + vvv[3];
                                SQL = "DELETE FROM code WHERE code_number = " + vTemp.ToString() + "";
                                cmd.Connection = conn;
                                cmd.CommandText = SQL;
                                cmd.ExecuteNonQuery();
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nУдаление данных файла " + DeleteFileName + ".txt из базы данных прошло успешно\n");
                            Console.ResetColor();
                            conn.Close();
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nФайл с указанным именем не найден");
                        Console.ResetColor();
                        goto EnterAgain;
                    }
                    break;
                case 4: break;
                default: break;
            }
        }
    }
}
