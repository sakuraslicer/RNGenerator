using System;
using System.Collections.Generic;
using System.Linq;

namespace genApp
{
    class Connector
    {
        private List<ConnectData> _connect = new List<ConnectData>();

        public void Append(string server, string uid, string pwd, string database)
        {
            var last = _connect.LastOrDefault();
            if(last != null)
            {
                last.Server = server;
                last.Uid = uid;
                last.Pwd = pwd;
                last.Database = database;
            }
            _connect.Add(new ConnectData() { Server = server, Uid = uid, Pwd = pwd, Database = database });
        }

        public class ConnectData
        {
            private static string server;
            public string Server { get { return server; } set { server = value; } }
            
            private static string uid;
            public string Uid { get { return uid; } set { uid = value; } }

            private static string pwd;
            public string Pwd { get { return pwd; } set { pwd = value; } }

            private static string database;
            public string Database { get { return database; } set { database = value; } }

            public static string connection = "server = " + server + "; uid = " + uid + "; pwd = " + pwd + "; database = " + database;
        }
    }
}
