using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.Controllers;
using BotVer1._2.TO;

namespace BotVer1._2.Models
{
    public class Woker : ComponentVisitor
    {
        public List<Component> components;
        List<CurrentOrderSummary> CurrentOrders;
        IClient client = null;
        MarketSort marketSort;
        MarketFilter marketFilter = new MarketFilter();
        public Woker(List<CurrentOrderSummary> CurrentOrders = null)
        {
            client = new JsonRpcClient();
            this.CurrentOrders = CurrentOrders;

        }
        public override void Visit(Leaf leaf)
        {
            throw new NotImplementedException();
        }

        public override void Visit(Composite composite)
        {
            if (composite is CountryPatern)
            {
                marketSort = MarketSort.FIRST_TO_START;
                marketFilter.CompetitionIds = new HashSet<String>() { (composite as CountryPatern).CompetitionId };
                marketFilter.TextQuery = " v ";

                var eventResult = client.listEventsRes(marketFilter, marketSort);
                List<Component> events = SortEventResult(eventResult);
                components = events;
            }
            else
            {
                marketSort = MarketSort.MAXIMUM_TRADED;
                string maxResults = "200";
                ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
                //MarketFilter marketFilter = new MarketFilter();
                marketProjections.Add(MarketProjection.EVENT);
                marketFilter.EventIds = new HashSet<String>() { (composite as EventPatern).Id };

                var marketCatalogues = client.listMarketCatalogue(marketFilter, marketProjections, marketSort, maxResults);

                List<Component> totalls = new List<Component>();
                foreach (MarketCatalogue market in marketCatalogues)
                {
                    if(market.MarketName.StartsWith("Over/Under"))
                    {
                        TotallPatern totallPatern = new TotallPatern(market.MarketName, market.MarketId, (int)market.TotalMatched);
                        totalls.Add(totallPatern);
                    }
                }
                var _totalls = totalls.ToArray();
                Array.Sort(_totalls);
                List<Component> temp = new List<Component>();
                temp.AddRange(_totalls);
                components = temp;
            }
        }

        public void PlaceBet(Component component) // component is a totall
        {
            double maxPrice = 1.04;
            if(component.Name.Equals("Over/Under 4.5 Goals") || component.Name.Equals("Over/Under 5.5 Goals")|| component.Name.Equals("Over/Under 6.5 Goals"))
            {
                if (component.Name.Equals("Over/Under 6.5 Goals")) maxPrice = 1.03;

                IList<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();

                for (double price = 1.01; price < maxPrice; price += 0.01)
                {
                    var limitOrder = new LimitOrder();
                    limitOrder.PersistenceType = PersistenceType.PERSIST; //save bet in-play
                    limitOrder.Price = price;
                    limitOrder.Size = 10; // placing a bet below minimum stake, expecting a error in report

                    var placeInstruction = new PlaceInstruction();
                    placeInstruction.Handicap = 0;
                    placeInstruction.Side = Side.LAY;
                    placeInstruction.OrderType = OrderType.LIMIT;
                    placeInstruction.LimitOrder = limitOrder;
                    placeInstruction.SelectionId = GetSelectionId(client, (component as TotallPatern).MarketId);

                    placeInstructions.Add(placeInstruction);
                }

                var customerRef = (component as TotallPatern).MarketId;
                
                PlaceExecutionReport placeExecutionReport = client.placeOrders((component as TotallPatern).MarketId, customerRef, placeInstructions);

                foreach(PlaceInstructionReport placeInstructionReport in placeExecutionReport.InstructionReports)
                {
                    var betId = placeInstructionReport.BetId;
                    var placedDate = placeInstructionReport.PlacedDate;
                    var status = placeInstructionReport.Status.ToString();
                    var price = placeInstructionReport.Instruction.LimitOrder.Price;
                    var size = placeInstructionReport.Instruction.LimitOrder.Size;

                    Bet bet = new Bet(betId, placedDate, status, price, size);

                    (component as TotallPatern).bets.Add(bet);
                    component.childNode.Nodes.Add(bet.betNode);

                    //component.childNode.BackColor = System.Drawing.Color.Pink;
                    component.childNode.Expand();

                    bet.betNode.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }
        public long GetSelectionId(IClient client, string marketId)
        {
            IList<string> marketIds = new List<string>();
            marketIds.Add(marketId);

            ISet<PriceData> priceData = new HashSet<PriceData>();
            priceData.Add(PriceData.EX_ALL_OFFERS);

            var priceProjection = new PriceProjection();
            priceProjection.PriceData = priceData;

            IList<MarketBook> marketBook = new List<MarketBook>();
            while (marketBook.Count == 0)
            {
                marketBook = client.listMarketBook(marketIds, priceProjection);
            }
            long selectionId = marketBook[0].Runners[0].SelectionId; // mistake
            return selectionId;
        }

        public List<Component> SortEventResult(IList<EventResult> results)
        {
            List<Event> eventsTemp = new List<Event>();
            foreach (EventResult eventRes in results)
            {
                eventsTemp.Add(eventRes.Event);
            }
            //if (events.Count == 0) return events;

            //if (IsCreatedCountry)
            //    return eventsTemp;

            var matches = eventsTemp.ToArray();
            Array.Sort(matches); // сортировка по дате
            List<Event> events = new List<Event>();
            events.AddRange(matches);

            List<Component> teams = new List<Component>();
            foreach(Event ev in events)
            {
                EventPatern _event = new EventPatern(ev.Name, ev.Id, ev.OpenDate);
                teams.Add(_event);
            }
            return teams;
        }
    }
}
