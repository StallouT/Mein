using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MeinClass
{
    public class Mein
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(@"Data Source = DESKTOP-5QJFN1Q\SQLEXPRESS; Initial Catalog = MeinKompf; Integrated Security = true;");
        }

        public static void CreateTable(string NameTable, DataSet dataSet)
        {
            DataTable Table = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM " + NameTable, CreateConnection());
            ITableMapping Map = Adapter.TableMappings.Add(NameTable, NameTable);
            Map.ColumnMappings.Add("Id", "Код");
            Adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            Adapter.Fill(dataSet, NameTable);
        }

    }
}
