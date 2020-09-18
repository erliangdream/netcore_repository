using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DtoEmptyAttribute : Attribute
    {
        public string EmptyMessage { get; set; }

        public DtoEmptyAttribute(string emptyMessage)
        {
            EmptyMessage = emptyMessage;
        }

    }
}
