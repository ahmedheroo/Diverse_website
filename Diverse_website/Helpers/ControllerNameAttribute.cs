using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Helpers
{
    public class ControllerNameAttribute : Attribute
    {
        public string Name { get; }

        public ControllerNameAttribute(string name)
        {
            Name = name;
        }
    }
}
