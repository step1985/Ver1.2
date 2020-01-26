using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.Controllers;
using System.Windows.Forms;
using BotVer1._2.TO;

namespace BotVer1._2.Models
{
    public abstract class Component
    {
        public string Name { get; set; }
        public int levelRecurs;
        public TreeNode childNode;
        public Component(string name)
        {
            Name = name;
        }
        public abstract void Interpret(List<CurrentOrderSummary> CurrentOrders);
        public abstract void Operation();
        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract Component GetChild(int index);
        public abstract void Accept (ComponentVisitor visitor);
        public abstract void Check();
        public abstract string SetTextNode();
    }
}
