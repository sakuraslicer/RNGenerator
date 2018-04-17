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

                m2:
                string SQL = string.Empty;
                string SQL2 = string.Empty;
                Console.WriteLine("\nВведите пароль для доступа к базе данных (введите '9' для выхода в главное меню): ");
                string password = Console.ReadLine();

                if (password == "9")
                {
                    Menu.MainMenu();
                }

                conn.ConnectionString = "server=127.0.0.1; uid=root;" +
                    "pwd =" + password + ";database=DaidoMetalRussiaDB";

                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    Console.WriteLine("\nНекорректный пароль от базы данных");
                    goto m2;
                }

                Console.WriteLine("\nСоединение с базой данных установлено");

                m3:
                Console.WriteLine("\nВведите имя файла для импорта в базу данных [без расширения] (введите '9' для выхода в главное меню):");
                string ImportFileName = Console.ReadLine();

                if (ImportFileName == "9")
                {
                    Menu.MainMenu();
                }

                string vTemp = String.Empty;

                //этот код выполняется уже тогда, когда проверено на несовпадение значений
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

                        Console.WriteLine("\nИмпорт из файла " + ImportFileName + ".txt в базу данных прошел успешно");
                        conn.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\nФайл с указанным именем не найден");
                    goto m3;
                }
            }
            #region Catch
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Number + " has occurred: " + ex.Message,
                    "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            #endregion
        }
    }
}