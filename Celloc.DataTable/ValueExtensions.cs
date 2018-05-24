using System;
using System.Collections.Generic;
using System.Data;

namespace Celloc.DataTable
{
	public static class ValueExtensions
	{
		public static object GetValue(this System.Data.DataTable table, (int Column, int Row) cell)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

			return table.Contains(cell) ? table.Rows[cell.Row].ItemArray[cell.Column] : null;
		}

		public static object GetValue(this System.Data.DataTable table, string cell)
		{
			ArgumentGuards.GuardAgainstNullTable(table);
			ArgumentGuards.GuardAgainstNullCell(cell);

			return GetValue(table, CellIndex.Translate(cell, Offset.ZeroBased));
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Row)) range)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

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
			var rangeTuple = table.TranslateRange(range);
			return rangeTuple.HasValue ? GetValuesByRow(table, rangeTuple.Value) : null;
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table)
		{
			ArgumentGuards.GuardAgainstNullTable(table);
			return GetValuesByRow(table, ((0, 0), (table.Columns.Count - 1, table.Rows.Count - 1)));
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Row)) range)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

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
			var rangeTuple = table.TranslateRange(range);
			return rangeTuple.HasValue ? GetValuesByColumn(table, rangeTuple.Value) : null;
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table)
		{
			ArgumentGuards.GuardAgainstNullTable(table);
			return GetValuesByColumn(table, ((0, 0), (table.Columns.Count - 1, table.Rows.Count - 1)));
		}

		public static IEnumerable<DataRow> GetRows(this System.Data.DataTable table, string range)
		{
			ArgumentGuards.GuardAgainstNullTable(table);
			ArgumentGuards.GuardAgainstNullRange(range);

			var tuple = table.TranslateRange(range);

			return tuple.HasValue ? table.GetRows(tuple.Value) : null;
		}

		public static IEnumerable<DataRow> GetRows(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Row)) range)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

			if(!table.Contains(range))
				return null;

			if (!CellRange.IsSameColumn(range))
				throw new Exception($"The specified range {range} is invalid - expected a single column.");

			var rows = new List<DataRow>();

			for (var i = range.Item1.Row; i <= range.Item2.Row; i++)
				rows.Add(table.Rows[i]);

			return rows;
		}
	}
}
