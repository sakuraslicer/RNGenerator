// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace genApp
{
    class Generator
    {
        public static void Generate()
        {
            Int64 count = 0;
            try
            {
                MySqlConnection conn = new MySqlConnection();

                #region Количество значений
                CorrectInput:
                Console.WriteLine("\nСколько уникальных значений сгенерировать?");
                try
                {
                    count = Convert.ToInt64(Console.ReadLine());
                    if (count == 9)
                    {
                        Menu.MainMenu();
                    }
                }
                catch (Exception)
                {
                    ErrorHandler.IncorrectString();
                    goto CorrectInput;
                }
                #endregion

                #region Имя файла
                Console.WriteLine("\nВведите имя файла [без расширения файла] для сохранения значений.");
                string fileName = Console.ReadLine();

                if (fileName == "9")
                {
                    Menu.MainMenu();
                }
                #endregion

                #region Пароль от БД для проверки на уникальность
                m300:
                Console.WriteLine("\nДля проверки на уникальность, введите пароль для доступа к базе данных)");
                string password = Console.ReadLine();

                conn.ConnectionString = "server = 127.0.0.1; uid = root; pwd = " + password + "; database = DaidoMetalRussiaDB";
                //conn.ConnectionString = Connector.ConnectData.connection;

                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНекорректный пароль от базы данных");
                    Console.ResetColor();
                    goto m300;
                }
                #endregion

                #region Генерация
                Console.WriteLine("\n\nВыполняется процесс генерации...");

                string[] sequence_array = new string[1];
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    Array.Resize<string>(ref sequence_array, (int)count);
                    for (int p = 0; p < 12; p++)
                    {
                        sequence_array[i] += random.Next(10).ToString();
                    }
                }
                #endregion

                #region Проверка внутри файла на уникальность
                string[] array = new string[count];
                List<string> CompList = new List<string>();

                CompList.AddRange(sequence_array);

                for (int i = 0; i < count; i++)
                {
                    string q = CompList[i];
                    for (int g = 0; g < count; g++)
                    {
                        if (q.Contains(sequence_array[g]))
                        {
                            array[i] = sequence_array[g];
                        }
                    }
                }
                #endregion

                #region Запись в файл без сравнения с БД
                string folder = "files";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                TextWriter writerTxt = new StreamWriter(Path.Combine(folder, fileName + ".txt"));
                foreach (var item in array)
                {
                    if (item != null)
                    {
                        string segment1 = item.ToString().Substring(0, 3);
                        string segment2 = item.ToString().Substring(3, 3);
                        string segment3 = item.ToString().Substring(6, 3);
                        string segment4 = item.ToString().Substring(9, 3);
                        writerTxt.WriteLine(segment1 + " " + segment2 + " " + segment3 + " " + segment4);
                    }
                }
                writerTxt.Close();
                #endregion

                string[] MiddleArray;
                string[] ResultArrayFromTXT = new string[count];

                // Количество строк в файле
                var reader2 = File.OpenText(Path.Combine(folder, fileName + ".txt"));

                for (int g = 0; g < count; g++)
                {
                    string tmp = reader2.ReadLine();
                    MiddleArray = tmp.Split(' ');
                    tmp = MiddleArray[0] + MiddleArray[1] + MiddleArray[2] + MiddleArray[3];
                    ResultArrayFromTXT[g] = tmp;
                }
                reader2.Close();

                // Количество записей в БД
                MySqlCommand CountCode = new MySqlCommand("SELECT COUNT(*) FROM code;");
                CountCode.Connection = conn;
                CountCode.ExecuteNonQuery();
                DataTable CountCodeTable = new DataTable();
                MySqlDataAdapter CountAdapter = new MySqlDataAdapter(CountCode);
                CountAdapter.Fill(CountCodeTable);
                int CountRowInDB = Convert.ToInt32(CountCodeTable.Rows[0].ItemArray.GetValue(0));

                TextWriter writerTxt2 = new StreamWriter(Path.Combine(folder, fileName + "_" + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + ".txt"));
                int EqualsNumber = 0;
                for (int a = 0; a < count; a++)
                {
                    // Массив значений из БД
                    MySqlCommand CodeSelect = new MySqlCommand("SELECT code_number FROM code WHERE code_number = " + ResultArrayFromTXT[a] + ";");
                    CodeSelect.Connection = conn;
                    object result = CodeSelect.ExecuteScalar();
                    if (result == null)
                    {
                        string segment1 = ResultArrayFromTXT[a].ToString().Substring(0, 3);
                        string segment2 = ResultArrayFromTXT[a].Substring(3, 3);
                        string segment3 = ResultArrayFromTXT[a].Substring(6, 3);
                        string segment4 = ResultArrayFromTXT[a].Substring(9, 3);
                        writerTxt2.WriteLine(segment1 + " " + segment2 + " " + segment3 + " " + segment4);
                    }
                    else
                    {
                        EqualsNumber++;
                    }
                }

                writerTxt2.Close();
                conn.Close();

                // Оставить только уникальный файл. Удалить файл без сравнения с БД
                File.Delete("files/" + fileName + ".txt");
                ;
                
                Console.WriteLine("\nГлобальная проверка на уникальность прошла успешно. Количество совпадений: " + EqualsNumber + ".");
                Console.WriteLine("Удалено записей: " + EqualsNumber + ".");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nГенерация успешно завершена\n\n");
                Console.ResetColor();
            }
            catch (Exception)
            {
                ErrorHandler.IncorrectString();
            }
        }
    }
}