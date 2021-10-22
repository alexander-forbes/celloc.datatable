using System;
using NUnit.Framework;

namespace Celloc.DataTable.Tests
{
	[TestFixture]
	public class When_calling_shift_on_shift_extensions
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
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ShiftExtensions.Shift(null, "A1:A1", "B"));
			Assert.AreEqual($"Value cannot be null. (Parameter 'table')", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_a_null_range()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.Shift(null, "B"));
			Assert.AreEqual($"Value cannot be null. (Parameter 'range')", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_an_empty_range()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.Shift("", "B"));
			Assert.AreEqual($"Value cannot be null. (Parameter 'range')", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_a_null_to_column()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.Shift("A1:A1", null));
			Assert.AreEqual($"Value cannot be null. (Parameter 'toColumn')", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_for_an_empty_to_column()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.Shift("A1:A1", ""));
			Assert.AreEqual($"Value cannot be null. (Parameter 'toColumn')", exception.Message, exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_spans_multiple_columns()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");

			var exception = Assert.Throws<ArgumentException>(() => _DataTable.Shift("A1:B1", "A"));
			Assert.AreEqual($"A shift cannot be performed across multiple columns. (Parameter 'range')", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_data_table_does_not_contain_the_range()
		{
			var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _DataTable.Shift("A20:A40", "A"));
			Assert.AreEqual($"The data table does not contain the range A20:A40. (Parameter 'range')", exception.Message);
		}

		[Test]
		public void It_should_shift_the_range_to_the_new_column()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			Assert.AreEqual("A1:A2", _DataTable.Shift("B1:B2", "A"));
		}

		[Test]
		public void It_should_shift_the_range_with_an_unknown_number_of_rows()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			Assert.AreEqual("A1:A2", _DataTable.Shift("B1:B?", "A"));
		}

		[Test]
		public void It_should_shift_the_range_with_an_unknown_number_of_columns_for_the_same_column()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			Assert.AreEqual("A1:A2", _DataTable.Shift("B1:??", "A"));
		}
	}
}
