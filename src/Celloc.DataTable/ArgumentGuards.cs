using System;

namespace Celloc.DataTable
{
	internal class ArgumentGuards
	{
		public static void GuardAgainstNullTable(System.Data.DataTable table)
		{
			if (table == null)
				throw new ArgumentNullException(nameof(table));
		}

		public static void GuardAgainstNullCell(string cell)
		{
			if (string.IsNullOrEmpty(cell))
				throw new ArgumentNullException(nameof(cell));
		}

		public static void GuardAgainstNullRange(string range)
		{
			if (string.IsNullOrEmpty(range))
				throw new ArgumentNullException(nameof(range));
		}
	}
}
