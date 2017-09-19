using System;
using System.Collections.Generic;
using System.Text;

namespace SQLinq.Standard
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SQLInqDeleteColumnAttribute : Attribute
    {
        public object Enable { get; set; } = true;

        public object Disable { get; set; } = false;

        public SQLInqDeleteColumnAttribute()
        {
        }

        public SQLInqDeleteColumnAttribute(object enableValue, object disableValue)
        {
            this.Enable = enableValue;
            this.Disable = disableValue;
        }
    }
}
