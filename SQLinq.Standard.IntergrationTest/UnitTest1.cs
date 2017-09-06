using System;
using SQLinq.Dialect;
using Xunit;

namespace SQLinq.Standard.IntergrationTest
{
    public class UnitTest1
    {
        [Fact]
        public void UpdateSetExpression_test()
        {
            var sqlinq = new SQLinqUpdate<ToSQL_009_Class>(new MySqlDialect());
            sqlinq.UpdateSet(() => new ToSQL_009_Class()
            {
                ID = 1,
                Name = "test"
            });
            var sql = sqlinq.ToSQL();
        }

        [SQLinqTable("MyTable")]
        private class ToSQL_009_Class
        {
            public int ID { get; set; }
            public string Name { get; set; }

            [SQLinqColumn()]
            public string DoNotUpdate { get; set; }
        }
    }
}
