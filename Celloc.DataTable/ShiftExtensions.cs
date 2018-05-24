using System;

namespace Celloc.DataTable
{
	public static class ShiftExtensions
	{
		public static string Shift(this System.Data.DataTable table, string range, string toColumn)
		{
			ArgumentGuards.GuardAgainstNullTable(table);
			ArgumentGuards.GuardAgainstNullRange(range);

			if (string.IsNullOrEmpty(toColumn))
				throw new ArgumentNullException(nameof(toColumn));

			var tuple = table.TranslateRange(range);

			if (!tuple.HasValue)
				throw new ArgumentOutOfRangeException(nameof(range), $"The data table does not contain the range {range}.");

			if (!CellRange.IsSameColumn(tuple.Value))
				throw new ArgumentException("A shift cannot be performed across multiple columns.", nameof(range));

			return $"{toColumn}{tuple.Value.Item1.Row + 1}:{toColumn}{tuple.Value.Item2.Row + 1}";
		}
	}
}
