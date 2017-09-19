using System;
using System.Linq;
using SQLinq.Dialect;
using Xunit;

namespace SQLInq.XunitTest
{
    public class InCaseTest
    {
        [Fact]
        public void Test1()
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6 };
            var result = new SQLinq.SQLinq<TestClass>(new MySqlDialect())
                .Where(x => arr.Contains(x.Id));
            var sql = result.ToSQL();
            sql.ToQuery();
            Assert.Equal("SELECT * FROM `TestClass` WHERE `Id` IN @sqlinq_1",sql.ToQuery());
            Assert.NotEmpty(sql.Parameters);
            Assert.Equal(sql.Parameters["@sqlinq_1"],arr);
        }
    }

    public class TestClass
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
