using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBconnectivity
{
    public class DBaccessor
    {
        private string istrcon = null;
        public DBaccessor( string astrcon) 
        {
           istrcon = astrcon;
        }
        public int RunDMLQuery(string astrQuery)
        {
            int lintresult = 0;
            using(SqlConnection conn = new SqlConnection(istrcon))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = astrQuery;
                conn.Open();
                lintresult = cmd.ExecuteNonQuery();
            }
            return lintresult;
        }
        public DataTable GetDataTableByQuery(string lstrQuery)
        {
            DataTable lobjDT = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(istrcon))
                {
                  using (SqlDataAdapter adapter = new SqlDataAdapter(lstrQuery,conn))
                  {
                        adapter.Fill(lobjDT);
                  }
                }
            }
            catch (Exception ex)
            { 
               Console.WriteLine(ex.Message);
            }
            return lobjDT;
        }
        public void PrintDataTable(DataTable aobjDT)
        {
            foreach (DataRow row in aobjDT.Rows)
            {
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    Console.Write($"{row[i]}\t\t");
                }
                Console.WriteLine();
            }
        }
    }
}
