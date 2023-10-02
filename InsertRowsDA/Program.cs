using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace InsertRowsDA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "EMP";
            cs.UserID = "sa";
            cs.Password= "sysadm";

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM COMPANY", cs.ConnectionString);
            // If the "InitialCatalog" is not in the Connection String
            // we initialize the adapter as:
            // SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EMP.dbo.COMPANY", cs.ConnectionString);

            DataSet ds = new DataSet();
            // adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey; // To copy also the primary key configuration. But will not copy foreign keys
            adapter.Fill(ds, "COMPANY-M");

            DataRow row = ds.Tables["COMPANY-M"].NewRow(); // CREATE A NEW DATAROW
            row["ID"] = 2;
            row["NAME"] = "Allen";                          
            row["AGE"] = 25;
            row["ADDRESS"] = "Texas";
            row["SALARY"] = 15000.00;
            ds.Tables["COMPANY-M"].Rows.Add(row);

            // We can also use numerical indexes
            row = ds.Tables[0].NewRow();
            row[0] = 1;
            row[1] = "Paul";
            row[2] = 32;
            row[3] = "California";
            row[4] = 20000.00;
            ds.Tables["COMPANY-M"].Rows.Add(row);

            row = ds.Tables["COMPANY-M"].NewRow(); 
            row["ID"] = 3;
            row["NAME"] = "Teddy";
            row["AGE"] = 23;
            row["ADDRESS"] = "Norway";
            row["SALARY"] = 20000.00;
            ds.Tables["COMPANY-M"].Rows.Add(row);

            row = ds.Tables["COMPANY-M"].NewRow();
            row["ID"] = 4;
            row["NAME"] = "Mark";
            row["AGE"] = 25;
            row["ADDRESS"] = "Richmond";
            row["SALARY"] = 65000.00;
            ds.Tables["COMPANY-M"].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            //adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
            adapter.Update(ds.Tables["COMPANY-M"]);

            Console.WriteLine(ds.GetXml());
            Console.ReadKey();
        }
    }
}
