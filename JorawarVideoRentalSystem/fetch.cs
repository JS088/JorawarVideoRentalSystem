using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace JorawarVideoRentalSystem
{
    public class fetch
    {
        //global declaration of the variable 
        SqlConnection sqlConn;
String connection_String = "Data Source=LAPTOP-1O8T59IS\\sqlexpress;Initial Catalog=JVideoDatabase;Integrated Security=True";
        SqlCommand sqlCmd;
        SqlDataReader sqlDatareader;
        int RentID = 0;
        // user define method that is used to get the record from the table
        public DataTable SelectQuery(String qry)
        {
            DataTable tbl = new DataTable();


            sqlConn = new SqlConnection(connection_String);

            sqlConn.Open();
            sqlCmd = new SqlCommand(qry, sqlConn);

            sqlDatareader = sqlCmd.ExecuteReader();

            tbl.Load(sqlDatareader);

            sqlConn.Close();

            return tbl;
        }
    }
}
