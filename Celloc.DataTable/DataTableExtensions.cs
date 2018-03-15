using System;
using System.Collections.Generic;

namespace Celloc.DataTable
{
	public static class DataTableExtensions
	{
		public static bool Contains(this System.Data.DataTable table, (int Column, int Row) cell)
		{
			GuardAgainstNullTable(table);

			if (table.Rows.Count - 1 < cell.Row)
				return false;

			return table.Rows[cell.Row].ItemArray.Length > cell.Column;
		}

		public static bool Contains(this System.Data.DataTable table, ((int Column, int Row),(int Column, int Range)) range)
		{
			GuardAgainstNullTable(table);

			return table.Contains(range.Item1) && table.Contains(range.Item2);
		}

		public static object GetValue(this System.Data.DataTable table, (int Column, int Row) cell)
		{
			GuardAgainstNullTable(table);
			
			return table.Contains(cell) ? table.Rows[cell.Row].ItemArray[cell.Column] : null;
		}

		public static object GetValue(this System.Data.DataTable table, string cell)
		{
			GuardAgainstNullTable(table);
			GuardAgainstNullCell(cell);

			return GetValue(table, CellIndex.Translate(cell, Offset.ZeroBased));
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Row)) range)
		{
			GuardAgainstNullTable(table);

			if (!table.Contains(range))
				return null;

			var rows = new List<List<object>>();

			for (var r = range.Item1.Row; r <= range.Item2.Row; r++)
			{
				var row = new List<object>();

				for (var column = range.Item1.Column; column <= range.Item2.Column; column++)
				{
					var value = table.Rows[r].ItemArray[column];
					row.Add(value);
				}

				rows.Add(row);
			}

			return rows;
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table, string range)
		{
			return GetValuesByRow(table, CellRange.Translate(range, Offset.ZeroBased));
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table)
		{
			GuardAgainstNullTable(table);
			return GetValuesByRow(table, ((0,0), (table.Columns.Count - 1, table.Rows.Count - 1)));
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Row)) range)
		{
			GuardAgainstNullTable(table);

			if (!table.Contains(range))
				return null;

			var columns = new List<List<object>>();

			for (var c = range.Item1.Column; c <= range.Item2.Column; c++)
			{
				var column = new List<object>();

				for (var r = range.Item1.Row; r <= range.Item2.Row; r++)
				{
					var value = table.Rows[r].ItemArray[c];
					column.Add(value);
				}

				columns.Add(column);
			}

			return columns;
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table, string range)
		{
			return GetValuesByColumn(table, CellRange.Translate(range, Offset.ZeroBased));
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table)
		{
			GuardAgainstNullTable(table);
			return GetValuesByColumn(table, ((0,0),(table.Columns.Count - 1, table.Rows.Count - 1)));
		}

		private static void GuardAgainstNullTable(System.Data.DataTable table)
		{
			if (table == null)
				throw new ArgumentNullException(nameof(table));
		}

		private static void GuardAgainstNullCell(string cell)
		{
			if (string.IsNullOrEmpty(cell))
				throw new ArgumentNullException(nameof(cell));
		}
	}
}
