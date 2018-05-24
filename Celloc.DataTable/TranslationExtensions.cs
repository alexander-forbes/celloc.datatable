using System.Text.RegularExpressions;

namespace Celloc.DataTable
{
	public static class TranslationExtensions
	{
		public static ((int Column, int Row), (int Column, int Row))? TranslateRange(this System.Data.DataTable table, string range)
		{
			ArgumentGuards.GuardAgainstNullRange(range);

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
			ArgumentGuards.GuardAgainstNullCell(cell);

			var tuple = CellIndex.Translate(cell, Offset.ZeroBased);

			if (!table.Contains(tuple))
				return null;

			return tuple;
		}
	}
}
