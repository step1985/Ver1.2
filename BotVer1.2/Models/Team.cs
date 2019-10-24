using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.TO;
using BotVer1._2.Controllers;

namespace BotVer1._2.Models
{
    public class Team
    {
        public string id;
        public string name;
        //public string countryCode;
        //DateTime? openDate;
        public DateTime? openDate;
        //{
        //    get { return openDate; }
        //    set
        //    {
        //        openDate = value;
        //        textTeamNode = OpenDate + " " + name;
        //        if (UpdateTeam != null)
        //            UpdateTeam(this, new ArgsEvent(textTeamNode));
        //    }
        //}

        string textTeamNode;
        public string TextTeamNode
        {
            get { return textTeamNode; }
            set
            {
                textTeamNode = value;
                if (EventTeam != null)
                    EventTeam(this, new ArgsEvent(TextTeamNode, index, true));
            }
        }
        public List<Totall> totalls = new List<Totall>();
        public int index;
        public bool IsCreatedTeam;

        MarketSort marketSort;
        string maxResults;
        ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
        MarketFilter marketFilter = new MarketFilter();
        public Team(string id, string name, DateTime? openDate)
        {
            this.id = id;
            this.name = name;
            this.openDate = openDate;

            marketSort = MarketSort.MAXIMUM_TRADED;
            maxResults = "200";
            marketProjections.Add(MarketProjection.EVENT);
            marketFilter.EventIds = new HashSet<String>() { this.id };
        }
        public void UpdateTotalls(IClient client)
        {
            Totall[] tempTotalls = new Totall[10];
            //MarketSort marketSort = MarketSort.MAXIMUM_TRADED;
            //string maxResults = "200";

            //ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
            //marketProjections.Add(MarketProjection.EVENT);

            //MarketFilter marketFilter = new MarketFilter();
            //marketFilter.EventIds = new HashSet<String>() { this.id };

            var marketCatalogues = client.listMarketCatalogue(marketFilter, marketProjections, marketSort, maxResults);

            foreach (MarketCatalogue market in marketCatalogues)
            {
                if (market.MarketName.Equals("Over/Under 0.5 Goals") || market.MarketName.Equals("Over/Under 1.5 Goals")
                    || market.MarketName.Equals("Over/Under 2.5 Goals") || market.MarketName.Equals("Over/Under 3.5 Goals")
                    || market.MarketName.Equals("Over/Under 4.5 Goals") || market.MarketName.Equals("Over/Under 5.5 Goals")
                    || market.MarketName.Equals("Over/Under 6.5 Goals") || market.MarketName.Equals("Over/Under 7.5 Goals")
                    || market.MarketName.Equals("Over/Under 8.5 Goals") || market.MarketName.Equals("Over/Under 9.5 Goals"))
                {
                    var searchTotall = totalls.FirstOrDefault(s => s.Equals(market));
                    if (searchTotall == null)
                    {
                        int ind = Convert.ToInt32(market.MarketName[11].ToString());
                        tempTotalls[ind] = new Totall(market.MarketId, market.MarketName, (int)market.TotalMatched);
                        tempTotalls[ind].EventTotall += Event_UpdateTotall;
                    }
                }                
            }
            if(!IsCreatedTeam)
                totalls = SortTotalls(tempTotalls);
        }

        private void Event_UpdateTotall(object sender, ArgsEvent e)
        {
            if (EventTeam != null)
                EventTeam(this, e);
        }

        public List<Totall> SortTotalls(Totall[] massTotalls)
        {
            List<Totall> tempTotalls = new List<Totall>();
            //int ind = 0;
            for (int i = 0; i < massTotalls.Length; i++)
            {
                if (massTotalls[i] != null)
                {
                    //massTotalls[i].index = tempTotalls.Count;
                    massTotalls[i].index = index; // index Team whose totalls
                    massTotalls[i].TextTotallNode = massTotalls[i].marketName + "         $" + massTotalls[i].TotalMatched;
                    tempTotalls.Add(massTotalls[i]);
                }
            }
            
            return tempTotalls;
        }

        public override bool Equals(object obj)
        {
            if (obj is Event && obj != null)
            {
                //Event temp = (Event)obj;
                if (this.id == (obj as Event).Id)
                    return true;
            }
            return false;
        }

        public event EventHandler<ArgsEvent> EventTeam;
    }
}
