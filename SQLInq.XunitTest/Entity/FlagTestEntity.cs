using System;
using System.Collections.Generic;
using System.Text;

namespace SQLInq.XunitTest.Entity
{
    public class FlagTestEntity
    {
        public bool TestB { get; set; }
        public FType MyType { get; set; }
    }

    [Flags]
    public enum FType : short
    {
        A = 1,
        B,
        C,
        D 
    }
}
