using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLinq.Dialect;

namespace SQLinq.Standard.Test
{
    [TestClass]
    public class MyTest
    {
        
        [TestMethod]
        public void Test1()
        {
            string userName = null;
            var sqlinq = new SQLinq<UserEntity>(new MySqlDialect());
            var ss = sqlinq.Where(x => x.UserName.StartsWith(userName) || x.RealName.StartsWith(userName) && x.Enable == true);
            var result = ss.ToSQL();
            
        }
    }

    [SQLinqTable("user")]
    public class UserEntity
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string RealName { get; set; }

        public bool Enable { get; set; } = true;

        public string MobilePhone { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [SQLinqColumn(Ignore = true)]
        public List<Test> Roles { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<Test> Applications { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<Test> Resources { get; set; }

        [SQLinqColumn(Ignore = true)]
        public List<Test> ResourceScopes { get; set; }

        public static UserEntity NewUser(long id, string userName, string encryptedPwd, string salt, string realName)
        {
            var entity = new UserEntity();
            entity.Id = id;
            entity.UserName = userName;
            entity.Password = encryptedPwd;
            entity.Salt = salt;
            entity.RealName = realName;
            entity.Enable = true;
            return entity;
        }
    }

    public class Test
    {
        
    }
}
