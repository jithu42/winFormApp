using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HamburgerMenuApp.Core
{
    class MysqlClass
    {
        private MySqlConnection con = null;
        private MySqlCommand cmd = null;
        private MySqlDataAdapter da = null;
        private MySqlDataReader dr;

        public MysqlClass(string str)
        {
            con = new MySqlConnection(str);
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
            con = null;
        }

        public MySqlDataReader Execute_query(string sql)
        {
            cmd = new MySqlCommand(sql, con);
            dr = cmd.ExecuteReader();
            return dr;
        }

        public DataSet ExecuteQueryReturnDataset(string sql)
        {
            DataSet ds = new DataSet();
            cmd = new MySqlCommand(sql, con);
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }

    }
}
