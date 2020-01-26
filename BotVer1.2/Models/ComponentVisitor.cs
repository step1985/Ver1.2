using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.TO;

namespace BotVer1._2.Models
{
    public abstract class ComponentVisitor
    {
        public abstract void Visit(Leaf leaf);
        //public abstract void Visit(CountryPatern country);
        //public abstract void Visit(EventPatern team);
        public abstract void Visit(Composite composite);
    }
}
