using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlHelper
{
	public class SqlTableUpdater
	{
		
		private SqlConnection connection;

		/*
		 * Parameter "connnection" should be open with "connection.Open()" to perform queries
		*/
		public SqlTableUpdater(SqlConnection connection)
		{
			this.connection = connection;
		}


		public void AddRow(string tableName, string[] columns, string[] values)
		{
			string query = "INSERT INTO " + tableName + " (";
			for (int i = 0; i < columns.Length; i++)
				query += columns[i] + ((i == columns.Length - 1) ? ") VALUES (" : ",");
			

			for (int i = 0; i < values.Length; i++)
				query += "\'"+ values[i] + ((i == values.Length - 1) ? "\')" : "\',");
			
			ExecuteCommand(query);
		}
		
		/*
		 * DELETE FROM tableName WHERE conditionColumn=targetValue
		*/
		public void RemoveRow(string tableName, string conditionColumn, string targetValue)
		{
			ExecuteCommand("DELETE FROM " + tableName + " WHERE " + conditionColumn + "=" + targetValue);
		}

		public void RemoveRowId(string tableName, string id)
		{
			ExecuteCommand("DELETE FROM " + tableName + " WHERE id=" + id);
		}

		private void ExecuteCommand(string query)
		{
			new SqlCommand(query, connection).ExecuteNonQuery();
		}
	}




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
	}


}

