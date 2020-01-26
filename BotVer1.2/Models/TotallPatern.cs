using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.Controllers;

namespace BotVer1._2.Models
{
    public class TotallPatern : Leaf, IComparable
    {
        public string MarketId { get; private set; }
        public int TotalMatched { get; private set; }
        public bool IsOrders = false;
        public TotallPatern(string name, string marketId, int totalMatched) :base(name){ MarketId = marketId; TotalMatched = totalMatched; }
        public List<Bet> bets = new List<Bet>();

        public override string SetTextNode()
        {
            return Name + "         $" + TotalMatched;
        }
        //public override void Interpret(IClient client)
        //{
        //    return;
        //}
        int IComparable.CompareTo(object obj)
        {
            TotallPatern temp = (TotallPatern)obj;
            if ((int)this.Name[11] > (int)temp.Name[11])
                return 1;
            if ((int)this.Name[11] < (int)temp.Name[11])
                return -1;
            return 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is TotallPatern && obj != null)
            {
                //Event temp = (Event)obj;
                if (this.MarketId == (obj as TotallPatern).MarketId)
                    return true;
            }
            return false;
        }
    }
}
