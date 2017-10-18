using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;
using SQLinq.Standard;
using Xunit;

namespace SQLInq.XunitTest
{
    public class SQLinqInsertTest
    {
        [Fact]
        public void ToSQL_001()
        {
            var data = new Person
            {
                FirstName = "Chris",
                LastName = "Pietschmann"
            };
            var target = new SQLinqInsert<Person>(data);
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("[Person]", actual.Table);

            Assert.Equal(7, actual.Fields.Count);
            Assert.Equal("@sqlinq_1", actual.Fields["ID"]);
            Assert.Equal("@sqlinq_2", actual.Fields["FirstName"]);
            Assert.Equal("@sqlinq_3", actual.Fields["LastName"]);
            Assert.Equal("@sqlinq_4", actual.Fields["Age"]);
            Assert.Equal("@sqlinq_5", actual.Fields["[Is_Employed]"]);
            Assert.Equal("@sqlinq_6", actual.Fields["ParentID"]);
            Assert.Equal("@sqlinq_7", actual.Fields["Column With Spaces"]);

            Assert.Equal(7, actual.Parameters.Count);
            Assert.Equal(Guid.Empty, actual.Parameters["@sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters["@sqlinq_2"]);
            Assert.Equal("Pietschmann", actual.Parameters["@sqlinq_3"]);
            Assert.Equal(0, actual.Parameters["@sqlinq_4"]);
            Assert.Equal(false, actual.Parameters["@sqlinq_5"]);
            Assert.Equal(Guid.Empty, actual.Parameters["@sqlinq_6"]);
            Assert.Equal(null, actual.Parameters["@sqlinq_7"]);
        }

        [Fact]
        public void ToSQL_Oracle_001()
        {
            var dialect = new OracleDialect();

            var data = new Person
            {
                FirstName = "Chris",
                LastName = "Pietschmann"
            };
            var target = new SQLinqInsert<Person>(data, dialect);
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("Person", actual.Table);

            Assert.Equal(7, actual.Fields.Count);
            Assert.Equal(":sqlinq_1", actual.Fields["ID"]);
            Assert.Equal(":sqlinq_2", actual.Fields["FirstName"]);
            Assert.Equal(":sqlinq_3", actual.Fields["LastName"]);
            Assert.Equal(":sqlinq_4", actual.Fields["Age"]);
            Assert.Equal(":sqlinq_5", actual.Fields["[Is_Employed]"]);
            Assert.Equal(":sqlinq_6", actual.Fields["ParentID"]);
            Assert.Equal(":sqlinq_7", actual.Fields["Column With Spaces"]);

            Assert.Equal(7, actual.Parameters.Count);
            Assert.Equal(Guid.Empty, actual.Parameters[":sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters[":sqlinq_2"]);
            Assert.Equal("Pietschmann", actual.Parameters[":sqlinq_3"]);
            Assert.Equal(0, actual.Parameters[":sqlinq_4"]);
            Assert.Equal(false, actual.Parameters[":sqlinq_5"]);
            Assert.Equal(Guid.Empty, actual.Parameters[":sqlinq_6"]);
            Assert.Equal(null, actual.Parameters[":sqlinq_7"]);
        }

        [Fact]
        public void ToSQL_002()
        {
            var data = new PersonView
            {
                FirstName = "Chris",
                LastName = "Pietschmann"
            };
            var target = new SQLinqInsert<PersonView>(data);
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("[vw_Person]", actual.Table);

            Assert.Equal(4, actual.Fields.Count);
            Assert.Equal("@sqlinq_1", actual.Fields["ID"]);
            Assert.Equal("@sqlinq_2", actual.Fields["First_Name"]);
            Assert.Equal("@sqlinq_3", actual.Fields["Last_Name"]);
            Assert.Equal("@sqlinq_4", actual.Fields["Age"]);

            Assert.Equal(4, actual.Parameters.Count);
            Assert.Equal(Guid.Empty, actual.Parameters["@sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters["@sqlinq_2"]);
            Assert.Equal("Pietschmann", actual.Parameters["@sqlinq_3"]);
            Assert.Equal(0, actual.Parameters["@sqlinq_4"]);
        }

        [Fact]
        public void ToSQL_003()
        {
            var data = new
            {
                ID = 1,
                FirstName = "Chris",
                LastName = "Pietschmann",
                Age = 0
            };
            var target = SQLinqHelper.Insert(data, "Person");
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("[Person]", actual.Table);

            Assert.Equal(4, actual.Fields.Count);
            Assert.Equal("@sqlinq_1", actual.Fields["ID"]);
            Assert.Equal("@sqlinq_2", actual.Fields["FirstName"]);
            Assert.Equal("@sqlinq_3", actual.Fields["LastName"]);
            Assert.Equal("@sqlinq_4", actual.Fields["Age"]);

            Assert.Equal(4, actual.Parameters.Count);
            Assert.Equal(1, actual.Parameters["@sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters["@sqlinq_2"]);
            Assert.Equal("Pietschmann", actual.Parameters["@sqlinq_3"]);
            Assert.Equal(0, actual.Parameters["@sqlinq_4"]);
        }

        [Fact]
        public void ToSQL_004()
        {
            var data = new PersonInsert
            {
                FirstName = "Chris",
                LastName = "Pietschmann"
            };
            var target = new SQLinqInsert<PersonInsert>(data);
            var actual = (SQLinqInsertResult)target.ToSQL(41, "foo");

            Assert.Equal("[PersonInsert]", actual.Table);

            Assert.Equal(7, actual.Fields.Count);
            Assert.Equal("@foo43", actual.Fields["FirstName"]);
            Assert.Equal("@foo44", actual.Fields["LastName"]);
            Assert.Equal("@foo45", actual.Fields["Age"]);
            Assert.Equal("@foo46", actual.Fields["IsEmployed"]);
            Assert.Equal("@foo47", actual.Fields["ParentID"]);
            Assert.Equal("@foo48", actual.Fields["Column With Spaces"]);

            Assert.Equal(7, actual.Parameters.Count);
            Assert.Equal("Chris", actual.Parameters["@foo43"]);
            Assert.Equal("Pietschmann", actual.Parameters["@foo44"]);
            Assert.Equal(0, actual.Parameters["@foo45"]);
            Assert.Equal(false, actual.Parameters["@foo46"]);
            Assert.Equal(Guid.Empty, actual.Parameters["@foo47"]);
            Assert.Equal(null, actual.Parameters["@foo48"]);
        }

        [Fact]
        public void ToSQL_005()
        {
            var data = new
            {
                ID = 1,
                FirstName = "Chris",
                LastName = "Pietschmann",
                Age = 0
            };
            var target = data.ToSQLinqInsert("Person");
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("[Person]", actual.Table);

            Assert.Equal(4, actual.Fields.Count);
            Assert.Equal("@sqlinq_1", actual.Fields["ID"]);
            Assert.Equal("@sqlinq_2", actual.Fields["FirstName"]);
            Assert.Equal("@sqlinq_3", actual.Fields["LastName"]);
            Assert.Equal("@sqlinq_4", actual.Fields["Age"]);

            Assert.Equal(4, actual.Parameters.Count);
            Assert.Equal(1, actual.Parameters["@sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters["@sqlinq_2"]);
            Assert.Equal("Pietschmann", actual.Parameters["@sqlinq_3"]);
            Assert.Equal(0, actual.Parameters["@sqlinq_4"]);
        }

        [Fact]
        public void ToSQL_006()
        {
            var data = new PersonInsert
            {
                FirstName = "Chris",
                LastName = "Pietschmann"
            };
            var target = data.ToSQLinqInsert();
            var actual = (SQLinqInsertResult)target.ToSQL(41, "foo");

            Assert.Equal("[PersonInsert]", actual.Table);

            Assert.Equal(7, actual.Fields.Count);
            Assert.Equal("@foo42", actual.Fields["ID"]);
            Assert.Equal("@foo43", actual.Fields["FirstName"]);
            Assert.Equal("@foo44", actual.Fields["LastName"]);
            Assert.Equal("@foo45", actual.Fields["Age"]);
            Assert.Equal("@foo46", actual.Fields["IsEmployed"]);
            Assert.Equal("@foo47", actual.Fields["ParentID"]);
            Assert.Equal("@foo48", actual.Fields["Column With Spaces"]);
            

            Assert.Equal(7, actual.Parameters.Count);
            Assert.Equal("Chris", actual.Parameters["@foo43"]);
            Assert.Equal("Pietschmann", actual.Parameters["@foo44"]);
            Assert.Equal(0, actual.Parameters["@foo45"]);
            Assert.Equal(false, actual.Parameters["@foo46"]);
            Assert.Equal(Guid.Empty, actual.Parameters["@foo47"]);
            Assert.Equal(null, actual.Parameters["@foo48"]);
        }

        [Fact]
        public void ToSQL_007()
        {
            var data = new ToSQL_007_Class
            {
                ID = 33,
                Name = "Chris",
                DotNotInsert = "Some Value"
            };
            var target = data.ToSQLinqInsert();
            var actual = (SQLinqInsertResult)target.ToSQL();

            Assert.Equal("[MyTable]", actual.Table);

            Assert.Equal(2, actual.Fields.Count);
            Assert.Equal("@sqlinq_1", actual.Fields["ID"]);
            Assert.Equal("@sqlinq_2", actual.Fields["Name"]);

            Assert.Equal(2, actual.Parameters.Count);
            Assert.Equal(33, actual.Parameters["@sqlinq_1"]);
            Assert.Equal("Chris", actual.Parameters["@sqlinq_2"]);
        }

        [Fact]
        public void TestToSQL_Insert_Success()
        {
            var data = new ToSQL_007_Class
            {
                ID = 33,
                Name = "Chris",
                DotNotInsert = "Some Value"
            };
            var target = data.ToSQLinqInsert();
            var actual = (SQLinqInsertResult)target.ToSQL();
            var query = actual.ToQuery();
        }

        [SQLinqTable("MyTable")]
        private class ToSQL_007_Class
        {
            public int ID { get; set; }
            public string Name { get; set; }

            [SQLinqColumn(Ignore = true)]
            public string DotNotInsert { get; set; }
        }
    }
}
