using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlHelper
{
	public class SqlTablePrinter
	{
		private SqlConnection connection;

		/*
		 * Parameter "connnection" should be open with "connection.Open()" to perform queries
		*/
		public SqlTablePrinter(SqlConnection connection)
		{
			this.connection = connection;
		}
		public DataTable GetTable(string tableName)
		{
			string query = "SELECT * FROM " + tableName;
			SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
			DataTable table = new DataTable();
			dataAdapter.Fill(table);

			return table;
		}
		
		public void PrintTable(string tableName)
		{
			PrintTable(GetTable(tableName));
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
			SqlConnection connection = new SqlConnection (@"Data Source=DESKTOP-8UOLSK2\SQLEXPRESS;Initial Catalog=kurs3;Integrated Security=True");
			SqlTablePrinter printer = new SqlTablePrinter(connection);
			
			//pass the name of the table you want to print
			printer.PrintTable("role");

			//dont forget to disconnect
			connection.Close();
		}


	}
}

