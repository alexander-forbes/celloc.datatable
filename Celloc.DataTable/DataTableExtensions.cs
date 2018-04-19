using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

		public static bool Contains(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Range)) range)
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
			var rangeTuple = table.TranslateRange(range);
			return rangeTuple.HasValue ? GetValuesByRow(table, rangeTuple.Value) : null;
		}

		public static List<List<object>> GetValuesByRow(this System.Data.DataTable table)
		{
			GuardAgainstNullTable(table);
			return GetValuesByRow(table, ((0, 0), (table.Columns.Count - 1, table.Rows.Count - 1)));
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
			var rangeTuple = table.TranslateRange(range);
			return rangeTuple.HasValue ? GetValuesByColumn(table, rangeTuple.Value) : null;
		}

		public static List<List<object>> GetValuesByColumn(this System.Data.DataTable table)
		{
			GuardAgainstNullTable(table);
			return GetValuesByColumn(table, ((0, 0), (table.Columns.Count - 1, table.Rows.Count - 1)));
		}

		public static ((int Column, int Row), (int Column, int Row))? TranslateRange(this System.Data.DataTable table, string range)
		{
			GuardAgainstNullRange(range);

			range = TranslateUnknown(table, range);

			var tuple = CellRange.Translate(range, Offset.ZeroBased);

			if (!table.Contains(tuple))
				return null;

			return tuple;
		}

		private static string TranslateUnknown(System.Data.DataTable table, string range)
		{
			if (Regex.IsMatch(range, @"^[a-zA-Z]{0,3}[0-9]+:[a-zA-Z]{0,3}\?$"))
				return range.Replace("?", table.Rows.Count.ToString());

			if (Regex.IsMatch(range, @"^[a-zA-Z]{0,3}[0-9]+:\?[0-9]+$"))
			{
				var cells = range.Split(':');

				var from = CellIndex.Translate(cells[0], Offset.ZeroBased);
				var to = CellIndex.Translate((table.Columns.Count - 1, from.Row), Offset.ZeroBased);

				return $"{cells[0]}:{to}";
			}

			if (Regex.IsMatch(range, @"^[a-zA-Z]{0,3}[0-9]+:\?\?$"))
			{
				var cells = range.Split(':');

				var to = CellIndex.Translate((table.Columns.Count, table.Rows.Count));

				return $"{cells[0]}:{to}";
			}

			return range;
		}

		public static (int Column, int Row)? TranslateCell(this System.Data.DataTable table, string cell)
		{
			GuardAgainstNullCell(cell);

			var tuple = CellIndex.Translate(cell, Offset.ZeroBased);

			if (!table.Contains(tuple))
				return null;

			return tuple;
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

		private static void GuardAgainstNullRange(string range)
		{
			if (string.IsNullOrEmpty(range))
				throw new ArgumentNullException(nameof(range));
		}
	}
}
