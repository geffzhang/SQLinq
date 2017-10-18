# build status
[![Build Status](https://travis-ci.org/uliian/SQLinq.svg?branch=master)](https://travis-ci.org/uliian/SQLinq)

# SQLinq
SQLinq allows you to use LINQ (Language INtegrated Query) features of .NET to generate Ad-Hoc SQL Queries at runtime.

This allows for easy generation of ad-hoc SQL code using strongly typed LINQ code in .NET in a similar fashion to writing queries with Entity Framework. However, SQLinq is NOT an ORM. SQLinq is a library for generating ad-hoc, strongly typed SQL code at runtime. The generated SQL code can then be executed against a SQL database using either ADO.NET directly, or with Dapper.NET using the SQLinq Dapper nuget package. One of the benefits of SQLinq is that you can write ad-hoc SQL code at runtime that isn't just strongly typed, but benefits from compile time validation as well.

## Nuget Package


## SQLinq Usage

**Step 1:** Create your data object in code (like the following examples) that matches the database table or view you want to select from. It can either be a class or interface. You can also name the object and/or its properties differently than the database by using the SQLinqTable and SQLinqColumn attributes to specify their name in the database.  

```c#
[SQLinqTable("PersonTable")]
public class Person
{
    public Guid ID { get; set; }

    [SQLinqColumn("First_Name")]
    public string FirstName { get; set; }

    [SQLinqColumn("Last_Name")]
    public string LastName { get; set; }

    public int Age { get; set; }
}
```

**Step 2:** Use LINQ to generate the ad-hoc SQL query necessary.  

```c#
var query = from d in new SQLinq<Person>()
            where d.FirstName.StartsWith("C")
                 && d.Age > 18
            orderby d.FirstName
            select new {
                id = d.ID,
                firstName = d.FirstName
            };
```

**Step 3:** Generate the SQL code and necessary query parameter key/value pairs.  

```c#
var queryResult = query.ToSQL();

// get the full SQL code
var sqlCode = queryResult.ToQuery();

// get the query parameters necessary to execute the above query
var sqlParameters = queryResult.Parameters;
```

**Step 4:** Create SqlCommand and set the SQL code and Query Parameters  

```c#
var cmd = new SqlCommand(dbconnection, sqlCode);
foreach(var p in sqlParameters)
{
    cmd.Parameters.AddWithValue(p.Key, p.Value);
}
// now execute the command and get the results from the database
```