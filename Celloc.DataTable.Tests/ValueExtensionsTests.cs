using System;
using System.Data;
using System.Linq;
using NUnit.Framework;

namespace Celloc.DataTable.Tests
{
	[TestFixture]
	public class When_calling_get_value_on_value_extensions_with_a_cell_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValue(null, (0, 0)));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_cell_does_not_exist_in_the_data_table()
		{
			Assert.IsNull(_DataTable.GetValue(_Cell));
		}

		[Test]
		public void It_should_return_the_value_of_the_cell()
		{
			const string value = "Value-1";
			_DataTable.Columns.Add();
			_DataTable.Rows.Add(value);
			Assert.AreEqual(value, _DataTable.GetValue(_Cell));
		}
	}

	[TestFixture]
	public class When_calling_get_value_on_value_extensions_with_a_cell_name
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValue(null, "A1"));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_cell_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => new System.Data.DataTable().GetValue(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: cell", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_cell_parameter_is_empty()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => new System.Data.DataTable().GetValue(string.Empty));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: cell", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_cell_does_not_exist_in_the_data_table()
		{
			Assert.IsNull(_DataTable.GetValue("A1"));
		}

		[Test]
		public void It_should_return_the_value_of_the_cell()
		{
			const string value = "Value-1";
			_DataTable.Columns.Add();
			_DataTable.Rows.Add(value);
			Assert.AreEqual(value, _DataTable.GetValue("A1"));
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_row_on_value_extensions_with_a_cell_range_name
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
			_DataTable.Columns.Add("Column-1");
			_DataTable.Columns.Add("Column-2");
			_DataTable.Columns.Add("Column-3");
			_DataTable.Columns.Add("Column-4");
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByRow(null, "A1:B1"));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => new System.Data.DataTable().GetValuesByRow(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_empty()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => new System.Data.DataTable().GetValuesByRow(string.Empty));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_range_does_not_exist_in_the_data_table()
		{
			Assert.IsNull(_DataTable.GetValuesByRow("A40:A56"));
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2", "Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6", "Value-7", "Value-8");

			var values = _DataTable.GetValuesByRow("A1:B2");

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-2" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-5", "Value-6" }, values[1]);
		}

		[Test]
		public void It_should_return_the_values_starting_from_the_specified_row()
		{
			_DataTable.Rows.Add("Value-1", "Value-2", "Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6", "Value-7", "Value-8");

			var values = _DataTable.GetValuesByRow("B2:?2");

			CollectionAssert.AreEqual(new[] { "Value-6", "Value-7", "Value-8" }, values[0]);
		}

		[Test]
		public void It_should_replace_the_unknowns_with_the_last_column_and_row_indices()
		{
			_DataTable.Rows.Add("Value-1", "Value-2", "Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6", "Value-7", "Value-8");
			_DataTable.Rows.Add("Value-9", "Value-10", "Value-11", "Value-12");

			var values = _DataTable.GetValuesByRow("B2:??");

			CollectionAssert.AreEqual(new[] { "Value-6", "Value-7", "Value-8" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-10", "Value-11", "Value-12" }, values[1]);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetValuesByRow("A3:A?"));
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_row_on_value_extensions_with_a_cell_range_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByRow(null, ((0, 0), (1, 1))));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_from_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			var from = (12, 16);
			var to = (14, 16);

			Assert.IsNull(_DataTable.GetValuesByRow((from, to)));
		}

		[Test]
		public void It_should_return_null_when_the_to_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			_DataTable.Rows.Add();

			var from = (0, 0);
			var to = (14, 16);

			Assert.IsNull(_DataTable.GetValuesByRow((from, to)));
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			var from = (0, 0);
			var to = (1, 1);

			var values = _DataTable.GetValuesByRow((from, to));

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-2" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-3", "Value-4" }, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_row_on_value_extensions
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
			_DataTable.Columns.Add("Column-1");
			_DataTable.Columns.Add("Column-2");
			_DataTable.Columns.Add("Column-3");
			_DataTable.Columns.Add("Column-4");
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByRow(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetValuesByRow());
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2", "Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6", "Value-7", "Value-8");

			var values = _DataTable.GetValuesByRow();

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-2", "Value-3", "Value-4" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-5", "Value-6", "Value-7", "Value-8" }, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_column_on_value_extensions_with_a_cell_range_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByColumn(null, ((0, 0), (1, 1))));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_from_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			var from = (12, 16);
			var to = (14, 16);

			Assert.IsNull(_DataTable.GetValuesByColumn((from, to)));
		}

		[Test]
		public void It_should_return_null_when_the_to_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			_DataTable.Rows.Add();

			var from = (0, 0);
			var to = (14, 16);

			Assert.IsNull(_DataTable.GetValuesByColumn((from, to)));
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			var from = (0, 0);
			var to = (1, 1);

			var values = _DataTable.GetValuesByColumn((from, to));

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-3" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-2", "Value-4" }, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_column_on_value_extensions_with_a_cell_range_name
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByColumn(null, "A1:B2"));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.GetValuesByColumn(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_empty()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => _DataTable.GetValuesByColumn(string.Empty));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_from_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			Assert.IsNull(_DataTable.GetValuesByColumn("A45:A47"));
		}

		[Test]
		public void It_should_return_null_when_the_to_cell_in_the_range_does_not_exist_in_the_data_table()
		{
			_DataTable.Rows.Add();
			Assert.IsNull(_DataTable.GetValuesByColumn("A1:A26"));
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			var values = _DataTable.GetValuesByColumn("A1:B2");

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-3" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-2", "Value-4" }, values[1]);
		}

		[Test]
		public void It_should_return_the_values_starting_from_the_specified_row()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6");
			_DataTable.Rows.Add("Value-7", "Value-8");

			var values = _DataTable.GetValuesByColumn("B2:B?");

			CollectionAssert.AreEqual(new[] { "Value-4", "Value-6", "Value-8" }, values[0]);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetValuesByColumn("A3:A?"));
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_column_on_value_extensions
	{
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_DataTable = new System.Data.DataTable();
			_DataTable.Columns.Add("Column-1");
			_DataTable.Columns.Add("Column-2");
			_DataTable.Columns.Add("Column-3");
			_DataTable.Columns.Add("Column-4");
		}

		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetValuesByColumn(null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetValuesByColumn());
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.Rows.Add("Value-1", "Value-2", "Value-3", "Value-4");
			_DataTable.Rows.Add("Value-5", "Value-6", "Value-7", "Value-8");

			var values = _DataTable.GetValuesByColumn();

			CollectionAssert.AreEqual(new[] { "Value-1", "Value-5" }, values[0]);
			CollectionAssert.AreEqual(new[] { "Value-2", "Value-6" }, values[1]);
			CollectionAssert.AreEqual(new[] { "Value-3", "Value-7" }, values[2]);
			CollectionAssert.AreEqual(new[] { "Value-4", "Value-8" }, values[3]);
		}
	}

	[TestFixture]
	public class When_calling_get_rows_on_value_extensions_with_a_range
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetRows(null, "A1:A1"));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetRows("A1:A1"));
		}

		[Test]
		public void It_should_throw_an_exception_when_the_column_is_not_the_same()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");

			var exception = Assert.Throws<Exception>(() => _DataTable.GetRows("A1:B1"));
			Assert.AreEqual("The specified range ((0, 0), (1, 0)) is invalid - expected a single column.", exception.Message);
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.LoadDataRow(new object[] { "Value-1", "Value-2" }, true);

			var expected = new[]
			{
				_DataTable.LoadDataRow(new object[] { "Value-3", "Value-4" }, true),
				_DataTable.LoadDataRow(new object[] { "Value-5", "Value-6" }, true),
				_DataTable.LoadDataRow(new object[] { "Value-7", "Value-8" }, true)
			};

			var rows = _DataTable.GetRows("A2:A?");

			Assert.IsTrue(rows.SequenceEqual(expected, DataRowComparer.Default));
		}
	}

	[TestFixture]
	public class When_calling_get_rows_on_value_extensions_with_a_tuple
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
			var exception = Assert.Throws<ArgumentNullException>(() => ValueExtensions.GetRows(null, ((0, 0), (0, 0))));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_return_null_when_the_table_does_not_have_any_rows_or_columns()
		{
			_DataTable.Columns.Clear();
			Assert.IsNull(_DataTable.GetRows(((0, 0), (0, 0))));
		}

		[Test]
		public void It_should_throw_an_exception_when_the_column_is_not_the_same()
		{
			_DataTable.Rows.Add("Value-1", "Value-2");

			var exception = Assert.Throws<Exception>(() => _DataTable.GetRows(((0, 0), (1, 0))));
			Assert.AreEqual("The specified range ((0, 0), (1, 0)) is invalid - expected a single column.", exception.Message);
		}

		[Test]
		public void It_should_return_the_values_in_the_range()
		{
			_DataTable.LoadDataRow(new object[] { "Value-1", "Value-2" }, true);

			var expected = new[]
			{
				_DataTable.LoadDataRow(new object[] { "Value-3", "Value-4" }, true),
				_DataTable.LoadDataRow(new object[] { "Value-5", "Value-6" }, true),
				_DataTable.LoadDataRow(new object[] { "Value-7", "Value-8" }, true)
			};

			var rows = _DataTable.GetRows(((0, 1), (0, 3)));

			Assert.IsTrue(rows.SequenceEqual(expected, DataRowComparer.Default));
		}
	}
}
