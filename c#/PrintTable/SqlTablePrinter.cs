using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlHelper
{
	public class SqlTablePrinter
	{
		
		private SqlConnection connection;


		public SqlTablePrinter(string connectionQuery)
		{
			connection = new SqlConnection(connectionQuery);
			connection.Open();
		}

		public DataTable GetTable(string tableName)
		{
			string query = "SELECT * FROM " + tableName;
			SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
			DataTable table = new DataTable();
			dataAdapter.Fill(table);

			return table;
		}

		public void CloseConnection()
		{
			connection.Close();
		}

		public void PrintTable(DataTable table)
		{
			foreach(DataRow row in table.Rows)
			{
				foreach(DataColumn column in table.Columns)
				{
					Console.Write(row[column] + "(" +  row[column].GetType() + ") | ");
				}
				Console.WriteLine("");
			}
		}

		public static void Main(string[] args)
		{
			//pass your ssms connection name (DESKTOP-[random chars]\SQLEXPRESS)
			//initial catalog is your db name
			SqlTablePrinter printer = new SqlTablePrinter(@"Data Source=DESKTOP-8UOLSK2\SQLEXPRESS;Initial Catalog=kurs3;Integrated Security=True");
			
			//pass the name of the table you want to print
			DataTable table = printer.GetTable("role");
			printer.PrintTable(table);

			//dont forget to disconnect
			printer.CloseConnection();
		}


	}
}

