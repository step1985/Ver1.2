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
    public class Leaf: Component
    {
        //public TreeNode childNode;
        public Leaf(string name) : base(name) { }

        public override void Accept(ComponentVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Check()
        {
            throw new NotImplementedException();
        }

        public override Component GetChild(int index)
        {
            throw new NotImplementedException();
        }

        public override void Interpret(List<CurrentOrderSummary> currentOrders)// add bets
        {
            this.childNode = new TreeNode(SetTextNode());

            //var totall = currentOrders.Where(x => x.MarketId == (this as TotallPatern).MarketId);
            foreach (var totall in currentOrders.Where(x => x.MarketId == (this as TotallPatern).MarketId))
            {
                (this as TotallPatern).IsOrders = true;

                var betId = totall.BetId;
                var placedDate = totall.PlacedDate;
                var status = totall.Status.ToString();
                var price = totall.PriceSize.Price;
                var size = totall.PriceSize.Size;

                Bet bet = new Bet(betId, placedDate, status, price, size);
                if(totall.SizeMatched != 0)
                    bet.isMatched = true;

                (this as TotallPatern).bets.Add(bet);
            }
        }

        public override void Operation()
        {
            foreach (var bet in (this as TotallPatern).bets)
            {
                this.childNode.Nodes.Add(bet.betNode);

                if(bet.isMatched)
                    bet.betNode.ForeColor = System.Drawing.Color.IndianRed;
                else
                    bet.betNode.ForeColor = System.Drawing.Color.Blue;
            }
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public override string SetTextNode()
        {
            throw new NotImplementedException();
        }
    }
}
