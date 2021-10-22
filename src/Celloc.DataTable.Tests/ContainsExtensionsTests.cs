using System;
using NUnit.Framework;

namespace Celloc.DataTable.Tests
{
	[TestFixture]
	public class When_calling_contains_on_contains_extensions_with_a_cell
	{
		private (int, int) _Cell;
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_Cell = (0, 0);
			_DataTable = new System.Data.DataTable();
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ContainsExtensions.Contains(null, _Cell));
			Assert.AreEqual($"Value cannot be null. (Parameter 'table')", exception.Message);
		}

		[Test]
		public void It_should_return_false_when_the_data_table_has_fewer_rows_than_the_specified_cell()
		{
			Assert.IsFalse(_DataTable.Contains(_Cell));
		}

		[Test]
		public void It_should_return_false_when_the_row_has_fewer_columns_than_the_specified_cell()
		{
			_DataTable.Rows.Add();
			Assert.IsFalse(_DataTable.Contains(_Cell));
		}

		[Test]
		public void It_should_return_true_when_the_data_table_has_the_row_and_column_specified_in_the_cell()
		{
			_DataTable.Rows.Add();
			_DataTable.Columns.Add();
			Assert.IsTrue(_DataTable.Contains(_Cell));
		}
	}

	[TestFixture]
	public class When_calling_contains_on_contains_extensions_with_a_range
	{
		private ((int, int), (int, int)) _Range;
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_Range = ((0, 0), (1, 1));
			_DataTable = new System.Data.DataTable();
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ContainsExtensions.Contains(null, _Range));
			Assert.AreEqual($"Value cannot be null. (Parameter 'table')", exception.Message);
		}

		[Test]
		public void It_should_return_false_when_the_data_table_does_not_contain_the_from_range()
		{
			Assert.IsFalse(_DataTable.Contains(_Range));
		}

		[Test]
		public void It_should_return_false_when_the_data_table_doesn_not_contain_the_to_range()
		{
			_DataTable.Columns.Add();
			_DataTable.Rows.Add();
			Assert.IsFalse(_DataTable.Contains(_Range));
		}

		[Test]
		public void It_should_return_true_when_the_data_table_has_the_from_and_to_range_specified()
		{
			_DataTable.Columns.Add();
			_DataTable.Columns.Add();
			_DataTable.Rows.Add();
			_DataTable.Rows.Add();
			Assert.IsTrue(_DataTable.Contains(_Range));
		}
	}
}
