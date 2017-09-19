using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;
using Xunit;

namespace SQLInq.XunitTest
{
    public class DeleteSQLTest
    {
        [Fact]
        public void DeleteSQLTest_Success()
        {
            var result = new SQLinq<TestClass>().Where(x => x.Id == 1024).ToDeleteSQL();
            var q = result.ToQuery();
            var p = result.Parameters;

            Assert.Equal(q, "DELETE FROM [TestClass] WHERE [Id] = @sqlinq_1");
            Assert.Equal(p["@sqlinq_1"],1024);
        }
    }
}
