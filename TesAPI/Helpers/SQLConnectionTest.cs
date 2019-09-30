using System;
using System.Data;
using System.Data.SqlClient;

namespace TesAPI.Helpers
{
    public interface ISQLConnectionTest
    {
        void TestSQL();

        string GetData();
    }

    public class SQLConnectionTest : ISQLConnectionTest
    {
        public  string ConvertDataTableToString(DataTable dataTable)
        {
            string data = string.Empty;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    data += dataTable.Columns[j].ColumnName + "~" + row[j];
                    if (j == dataTable.Columns.Count - 1)
                    {
                        if (i != (dataTable.Rows.Count - 1))
                            data += "$";
                    }
                    else
                        data += " | ";
                }
            }
            return data;
        }

        public string GetData()
        {
            // when the SQL server is in a seperate container the private/public ip needs to be used i.e 192.168.x.x(home) or 10.9.30.x(office)
            //  using (SqlConnection con = new SqlConnection("Data Source=127.0.0.1,1833;Initial Catalog=MyTestDB;User ID=sa;Password=Password12!;"))
            using (SqlConnection con = new SqlConnection("Data Source=10.9.30.28,1833;Initial Catalog=MyTestDB;User ID=sa;Password=Password12!;"))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customers", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        var tb = new DataTable();
                        tb.Load(reader);
                        //Console.WriteLine($"Data {ConvertDataTableToString(tb)}");
                        return ConvertDataTableToString(tb);
                    }
                }
                catch (Exception ex)
                {
                    return ($"Exception {ex}");
                }
            }
        }

        public void TestSQL()
        {
            using (SqlConnection con = new SqlConnection("Data Source=127.0.0.1,1833;Initial Catalog=MyTestDB;User ID=sa;Password=Password12!;"))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customers", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        var tb = new DataTable();
                        tb.Load(reader);
                        Console.WriteLine($"Data {ConvertDataTableToString(tb)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception {ex}");
                }
            }
        }

    }

}
