namespace Celloc.DataTable
{
	public static class ContainsExtensions
	{
		public static bool Contains(this System.Data.DataTable table, (int Column, int Row) cell)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

			if (table.Rows.Count - 1 < cell.Row)
				return false;

			return table.Rows[cell.Row].ItemArray.Length > cell.Column;
		}

		public static bool Contains(this System.Data.DataTable table, ((int Column, int Row), (int Column, int Range)) range)
		{
			ArgumentGuards.GuardAgainstNullTable(table);

			return table.Contains(range.Item1) && table.Contains(range.Item2);
		}
	}
}
