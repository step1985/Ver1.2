using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.TO;

namespace BotVer1._2.Models
{
    public class Totall
    {
        public string marketId;
        public string marketName;
        int totalMatched;
        public int TotalMatched
        {
            get { return totalMatched; }
            set
            {
                totalMatched = value;
                textTotallNode = marketName + "         $" + totalMatched;
                //if (UpdateTotall != null)
                //    UpdateTotall(this, new ArgsEvent(textTotallNode));
            }
        }
        string textTotallNode;
        public string TextTotallNode
        {
            get { return textTotallNode; }
            set
            {
                textTotallNode = value;
                if (EventTotall != null)
                    EventTotall(this, new ArgsEvent(textTotallNode, index, false));
            }
        }

        public List<Bet> bets = new List<Bet>();
        public int index;
        public Totall(string marketId, string marketName, int totalMatched)
        {
            this.marketId = marketId;
            this.marketName = marketName;
            TotalMatched = totalMatched;
        }

        public override bool Equals(object obj)
        {
            if (obj is MarketCatalogue && obj != null)
            {
                if (this.marketName == (obj as MarketCatalogue).MarketName)
                    return true;
            }
            return false;
        }

        public event EventHandler<ArgsEvent> EventTotall;

    }
}
