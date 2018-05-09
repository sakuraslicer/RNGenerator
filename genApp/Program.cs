// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com


namespace genApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread thread = new Thread(WriteStatus);
            //var connector = new Connector();
            //Console.Write("Server: ");
            //String server = Console.ReadLine();
            //Console.Write("User: ");
            //String uid = Console.ReadLine();
            //Console.Write("Password: ");
            //String pwd = Console.ReadLine();
            //Console.Write("Database: ");
            //String database = Console.ReadLine();

            //connector.Append(server, uid, pwd, database);

            //thread.Start();
            //Thread.Sleep(3500);
            //Console.Clear();

            while (true)
            {
                Menu.MainMenu();
            }
        }

        //static void WriteStatus()
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Соединение установлено");
        //    Console.ResetColor();
        //}
    }
}
