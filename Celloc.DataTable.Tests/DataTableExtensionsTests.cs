﻿using System;
using NUnit.Framework;

namespace Celloc.DataTable.Tests
{
	[TestFixture]
    public class When_calling_contains_on_datatable_extensions_with_a_cell
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.Contains(null, _Cell));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
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
	public class When_calling_contains_on_datatable_extensions_with_a_range
	{
		private ((int, int),(int, int)) _Range;
		private System.Data.DataTable _DataTable;

		[SetUp]
		public void Setup()
		{
			_Range = ((0, 0), (1,1));
			_DataTable = new System.Data.DataTable();
		}
		
		[Test]
		public void It_should_throw_an_exception_when_the_table_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.Contains(null, _Range));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
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

	[TestFixture]
	public class When_calling_get_value_on_datatable_extensions_with_a_cell_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValue(null, (0, 0)));
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
	public class When_calling_get_value_on_datatable_extensions_with_a_cell_name
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValue(null, "A1"));
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
	public class When_calling_get_values_by_row_on_datatable_extensions_with_a_cell_range_name
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByRow(null, "A1:B1"));
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
			_DataTable.Rows.Add("Value-1", "Value-2");
			_DataTable.Rows.Add("Value-3", "Value-4");

			var values = _DataTable.GetValuesByRow("A1:B2");

			CollectionAssert.AreEqual(new[] {"Value-1", "Value-2"}, values[0]);
			CollectionAssert.AreEqual(new[] {"Value-3", "Value-4"}, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_row_on_datatable_extensions_with_a_cell_range_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByRow(null, ((0,0),(1,1))));
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

			CollectionAssert.AreEqual(new[] {"Value-1", "Value-2"}, values[0]);
			CollectionAssert.AreEqual(new[] {"Value-3", "Value-4"}, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_column_on_datatable_extensions_with_a_cell_range_index
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByColumn(null, ((0,0),(1,1))));
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

			CollectionAssert.AreEqual(new[] {"Value-1", "Value-3"}, values[0]);
			CollectionAssert.AreEqual(new[] {"Value-2", "Value-4"}, values[1]);
		}
	}

	[TestFixture]
	public class When_calling_get_values_by_column_on_datatable_extensions_with_a_cell_range_name
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
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByColumn(null, "A1:B2"));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: table", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_null()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByColumn(_DataTable, null));
			Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: range", exception.Message);
		}

		[Test]
		public void It_should_throw_an_exception_when_the_range_parameter_is_empty()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableExtensions.GetValuesByColumn(_DataTable, string.Empty));
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

			CollectionAssert.AreEqual(new[] {"Value-1", "Value-3"}, values[0]);
			CollectionAssert.AreEqual(new[] {"Value-2", "Value-4"}, values[1]);
		}
	}
}