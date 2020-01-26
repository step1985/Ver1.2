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
    public class Composite : Component
    {
        public string id;
        public DateTime? openDate;
        public List<Component> components = new List<Component>();
        //public TreeNode childNode;
        
        public Composite(string name) : base(name) { }

        public override void Accept(ComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override Component GetChild(int index)
        {
            throw new NotImplementedException();
        }

        public override void Interpret(List<CurrentOrderSummary> currentOrders) // will call for type Composite (CountryPatern and EventPatern)
        {
            string textNode = SetTextNode();
            this.childNode = new TreeNode(textNode);
            Woker woker = new Woker(currentOrders);
            this.Accept(woker);
            foreach(var component in woker.components)
            {
                this.Add(component);
                component.Interpret(currentOrders);
            }
        }

        public override void Operation()
        {
            foreach(var component in this.components)
            {
               this.childNode.Nodes.Add(component.childNode);
                if(component is TotallPatern && (component as TotallPatern).IsOrders)
                {
                    this.childNode.ForeColor = System.Drawing.Color.Orange;
                    component.childNode.ForeColor = System.Drawing.Color.Blue;
                    component.childNode.ExpandAll();
                }
                component.Operation();
            }
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Check()
        {
            Woker woker = new Woker();
            this.Accept(woker);
            if (components.Count == woker.components.Count) return;

            foreach (var component in woker.components) // list totalls
            {
                
                var searchComponent = components.FirstOrDefault(s => s.Equals(component)); // Equals
                if(searchComponent == null)
                {
                    this.Add(component);
                    component.childNode = new TreeNode(component.SetTextNode());
                    this.childNode.Nodes.Add(component.childNode);
                    this.childNode.BackColor = System.Drawing.Color.Pink;
                    component.childNode.BackColor = System.Drawing.Color.Pink;

                    woker.PlaceBet(component);

                    
                    //this.childNode.Expand();
                }
            }
        }
        public override string SetTextNode()
        {
            return this.Name; //+ "  " + this.childNode.GetNodeCount(false);
        }
    }
}
