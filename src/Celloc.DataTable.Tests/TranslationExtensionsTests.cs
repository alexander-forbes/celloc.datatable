using System;
using NUnit.Framework;

namespace Celloc.DataTable.Tests
{
	[TestFixture]
	public class When_calling_translate_range_on_translation_extensions
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
			_DataTable.Columns.Add("Column-1");
			_DataTable.Columns.Add("Column-2");
		}

		[Test]
		public void It_should_throw_an_exception_for_a_null_range()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.TranslateRange(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_an_empty_range()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.TranslateRange(string.Empty));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_replace_the_unknown_row_with_the_last_row_index()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6");
			_DataTable.Rows.Add("Value-7", "Value-8");

			var tuple = _DataTable.TranslateRange("B2:B?");

			Assert.AreEqual(((1, 1), (1, 3)), tuple);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.TranslateRange("A2:A?"));
		}
	}

	[TestFixture]
	public class When_calling_translate_cell_on_translation_extensions
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
			_DataTable.Columns.Add("Column-1");
			_DataTable.Columns.Add("Column-2");
		}

		[Test]
		public void It_should_throw_an_exception_for_a_null_cell()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.TranslateCell(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: cell", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_an_empty_cell()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.TranslateCell(string.Empty));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: cell", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_replace_the_unknown_row_with_the_last_row_index()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			var tuple = _DataTable.TranslateCell("B2");

			Assert.AreEqual((1, 1), tuple);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.TranslateCell("A2"));
		}
	}
}
