// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;

namespace genApp
{
    class Importer
    {
        public static void Import()
        {
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                MySql.Data.MySqlClient.MySqlCommand cmd;

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                cmd = new MySql.Data.MySqlClient.MySqlCommand();

                bool ExistDir = Directory.Exists(@"files");
                Console.ForegroundColor = ConsoleColor.Red;
                if (!ExistDir) { Console.WriteLine("\nДиректория <files> не найдена. Она создается автоматически, если происходит процесс генерации\n"); Console.ResetColor(); Menu.MainMenu(); }
                Console.ResetColor();

                m2:
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
                    goto m2;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Соединение с базой данных установлено\n");
                Console.ResetColor();

                try
                {
                    string[] files = Directory.GetFiles(@"files", "*.txt");
                    
                    for(int i = 0; i < files.Length; i++)
                    {
                        string[] clear = files[i].Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    
                    Console.WriteLine("\nДоступные файлы: \n");
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine(i + 1 + ") " + files[i]);
                    }
                }
                catch (Exception)
                {
                }
                m3:
                Console.WriteLine("\nВведите имя файла [без расширения] для импорта в базу данных:");
                string ImportFileName = Console.ReadLine();
      

                if (ImportFileName == "9")
                {
                    Menu.MainMenu();
                }
                string vTemp = String.Empty;

                //этот код выполняется тогда, когда проверено на несовпадение значений
                try
                {
                    using (StreamReader sr = new StreamReader(File.Open("files/" + ImportFileName + ".txt", FileMode.Open)))
                    {
                        Console.WriteLine("\nВыполняется импорт в базу данных...");

                        while (!sr.EndOfStream)
                        {
                            string[] vvv;
                            vTemp = sr.ReadLine();
                            vvv = vTemp.Split(' ');
                            vTemp = vvv[0] + vvv[1] + vvv[2] + vvv[3];
                            SQL = "INSERT INTO code (code_number, isChecked) VALUES('" + vTemp.ToString() + "', '0');";
                            cmd.Connection = conn;
                            cmd.CommandText = SQL;
                            cmd.ExecuteNonQuery();
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nИмпорт из файла " + ImportFileName + ".txt в базу данных прошел успешно\n");
                        Console.ResetColor();
                        conn.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nФайл с именем" + ImportFileName + " не найден");
                    Console.ResetColor();
                    goto m3;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Number + " has occurred: " + ex.Message,
                    "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}