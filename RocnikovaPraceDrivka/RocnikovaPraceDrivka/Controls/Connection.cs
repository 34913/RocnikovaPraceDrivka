using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace RocnikovaPraceDrivka.Controls
{
    public class Connection
    {
        protected string tablename;
        protected string orderby;
        protected int limit;
        protected string where;
        protected char separator;

        //

        public string DbName { get; } = "f142730";

        public string TableName { get => tablename; }

        public string OrderBy { get => orderby; }

        public int Limit { get => limit; }

        public string Where { get => where; }

        public char Separator { get => separator; }

        //

        protected SqlCommand command;
        protected SqlConnection sqlCon;
        protected string strSqlCommand;
        
        public bool SqlDisposed { get; private set; }

        //

        protected bool firstConnectivity = true;
        
        public static bool Connected { get; private set; } = false;
        
        public static string ConnectionType { get; private set; }

        //
        //
        
        public Connection(string tablename, string orderby, int limit, string where, char separator = ';')
        {
            this.tablename = tablename;
            this.orderby = orderby;
            this.limit = limit;
            this.where = where;

            this.separator = separator;

            SqlDisposed = true;

            if (firstConnectivity)
            {
                //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

                //Connection.CheckConnection();

                firstConnectivity = false;
            }
        }

        //

        /// <summary>
        /// selects the things of exact type from DB
        /// </summary>
        /// <param name="something">object has to equals something</param>
        /// <param name="equalsWhat">which value to select</param>
		public string Select(Action func, string something, string equalsWhat)
        {
            if (!Connected)
                return String.Empty;

            strSqlCommand = "SELECT * FROM " + TableName + " ORDER BY " + OrderBy + " LIMIT " + Limit + " WHERE " + something + " = " + equalsWhat;
            string line = string.Empty;

            try
            {
                using (sqlCon = new SqlConnection(DbName))
                {
                    sqlCon.Open();

                    func();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreach (object obj in reader)
                            {
                                line += obj.ToString() + separator;
                            }
                        }
                    }

                    sqlCon.Close();

                }
                DisposeCon();
            }
            catch(Exception exc)
			{
                throw exc;
			}
            return line;
        }

		public void Add(Action func, string[] arguments)
        {
            if (!Connected)
                return;

            string args = string.Empty;
            string vals = string.Empty;
            foreach (string str in arguments)
			{
                args += str + ',';
                vals += "@" + str + ',';
			}

            args = args.Remove(args.Length - 1);
            vals = vals.Remove(vals.Length - 1);

            strSqlCommand = "INSERT INTO " + TableName + " (" + args + ") VALUES(" + vals + ")";

            try
            {
                using (sqlCon = new SqlConnection(DbName))
                {
                    sqlCon.Open();

                    func();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    sqlCon.Close();
                }
                DisposeCon();
            }
            catch(Exception exc)
			{
                throw exc;
			}
        }

        public void Update(Action func, string[] argumentsSet, string[] argumentsWhere)
		{
            if (!Connected)
                return;

            string set = string.Empty;
            string whr = string.Empty;

            foreach (string str in argumentsSet)
                set += str + " = @" + str + ',';
            set = set.Remove(set.Length - 1);

            foreach (string str in argumentsWhere)
                whr += str + " = @" + str + ',';
            whr = whr.Remove(whr.Length - 1);

            strSqlCommand = "UPDATE " + tablename + " SET new = @new WHERE name = @name, new = @new";

            try
            {
                using (sqlCon = new SqlConnection(DbName))
                {
                    sqlCon.Open();

                    func();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    sqlCon.Close();
                }
                DisposeCon();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Delete(Action func, string[] arguments)
		{
            if (!Connected)
                return;

            string args = string.Empty;
            string vals = string.Empty;
            foreach (string str in arguments)
            {
                args += str + ',';
                vals += "@" + str + ',';
            }

            args = args.Remove(args.Length - 1);
            vals = vals.Remove(vals.Length - 1);

            strSqlCommand = "DELETE FROM " + TableName + " WHERE " + args + " = " + vals;
            string line = string.Empty;

            try
            {
                using (sqlCon = new SqlConnection(DbName))
                {
                    sqlCon.Open();

                    func();
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    sqlCon.Close();

                }
                DisposeCon();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        //

        public void CreateCommand()
        {
            try
            {
                command = new SqlCommand(strSqlCommand, sqlCon);
            }
            catch(Exception exc)
			{
                throw exc;
			}
            SqlDisposed = false;
        }

        public void AddNumber(string val, int num)
		{
            command.Parameters.Add("@" + val, SqlDbType.Int).Value = num;
        }

        public void AddString(string val, string str, int maxLength)
        {
            command.Parameters.Add("@" + val, SqlDbType.VarChar, maxLength).Value = str;
        }

        public void DisposeCon()
		{
            command.Dispose();
            SqlDisposed = true;
		}

        //
        //

        public static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckConnection();
        }
        
        public static bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            var profiles = Connectivity.ConnectionProfiles;

            if (current == NetworkAccess.Internet)
                Connected = true;
            else
                Connected = false;

            if (profiles.Contains(ConnectionProfile.WiFi))
                ConnectionType = profiles.FirstOrDefault().ToString();
            //else
            //    ConnectionType = profiles.FirstOrDefault().ToString();

            return Connected;
        }

    }
}
