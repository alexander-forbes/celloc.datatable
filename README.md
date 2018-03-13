# Celloc.DataTable
A DataTable extension for Celloc.

## NuGet
To install the package from NuGet, run the following command:
`Install-Package Celloc.DataTable`

## Usage
If cell or range does not exist in the DataTable a `null` will be returned.

### Contains
To check whether the DataTable has a specific cell, use the `Contains` method:

```C#
myDataTable.Contains((0,0));
```

To check whether the DataTable has a specific cell range:
```C#
myDataTable.Contains(((0,0),(1,1)));
```

### GetValue
To get the value of a specific cell, use the `GetValue` method:
```C#
myDataTable.GetValue((0,0));
```
or
```C#
myDataTable.GetValue("A1");
```

### GetValuesByRow
Assume the following table:
```
A1, B1, C1
A2, B2, C2
A3, B3, C3
```
To get the values of a range, by row, use the `GetValuesByRow` method:
```C#
var result = myDataTable.GetValuesByRow("A1:B2");
// result[0] == [A1, B1]
// result[1] == [A2, B2]
```
or
```C#
var result = myDataTable.GetValuesByRow(((0,0),(1,1)));
// result[0] == [A1, B1]
// result[1] == [A2, B2]

```
### GetValuesByColumn
Assume the following table:
```
A1, B1, C1
A2, B2, C2
A3, B3, C3
```
To get the values of a range, by column, use the `GetValuesByColumn` method:
```C#
var result = myDataTable.GetValuesByColumn("A1:B2");
// result[0] == [A1, A2]
// result[1] == [B1, B2]
```
or
```C#
var result = myDataTable.GetValuesByColumn(((0,0),(1,1)));
// result[0] == [A1, A2]
// result[1] == [B1, B2]
```
