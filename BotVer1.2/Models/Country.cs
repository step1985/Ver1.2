using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.Controllers;
using BotVer1._2.TO;
using System.Windows.Forms;

namespace BotVer1._2.Models
{
    public class Country
    {
        public string name;
        public string competitionId;
        public string textCountryLabel;
        public List<Team> teams = new List<Team>();
        public TreeView treeView;
        public bool IsCreatedCountry;
        IClient client;

        MarketSort marketSort;
        MarketFilter marketFilter = new MarketFilter();
        public Country(IClient client, string name, string competitionId, TreeView treeView)
        {
            this.client = client;
            this.name = name;
            this.competitionId = competitionId;
            this.treeView = treeView;            

            marketSort = MarketSort.FIRST_TO_START;
            marketFilter.CompetitionIds = new HashSet<String>() { this.competitionId };
            marketFilter.TextQuery = " v ";

            //UpdateTeams();

            //IsCreatedCountry = true;
        }
        public void UpdateTeams()
        {
            //var marketSort = MarketSort.FIRST_TO_START;
            //var marketFilter = new MarketFilter();
            //marketFilter.CompetitionIds = new HashSet<String>() { this.competitionId };
            //marketFilter.TextQuery = " v ";

            var eventResult = client.listEventsRes(marketFilter, marketSort);

            List<Event> events = SortEventResult(eventResult); //sort only will created new object

            foreach (Event eventRes in events)
            {
                var searchEvent = teams.FirstOrDefault(s => s.Equals(eventRes)); //

                if (searchEvent == null)
                {
                    Team team = new Team(eventRes.Id, eventRes.Name, eventRes.OpenDate);
                    team.index = teams.Count; //think...
                    
                    team.EventTeam += Event_UpdateTeam;

                    DateTime dt = (DateTime)team.openDate;
                    team.TextTeamNode = dt.ToString("dd/MM HH:mm") + "   " + team.name; //call Event of Team

                    teams.Add(team);
                    team.UpdateTotalls(client);

                    team.IsCreatedTeam = true;
                }
                else
                    teams.FirstOrDefault(s => s.Equals(eventRes)).UpdateTotalls(client);
            }
        }

        private void Event_UpdateTeam(object sender, ArgsEvent e)
        {
            if (EventCountry != null)
                EventCountry(this, e);
        }

        public List<Event> SortEventResult(IList<EventResult> results)
        {
            List<Event> eventsTemp = new List<Event>();
            foreach (EventResult eventRes in results)
            {
                eventsTemp.Add(eventRes.Event);
            }
            //if (events.Count == 0) return events;

            if (IsCreatedCountry)
                return eventsTemp;

            var matches = eventsTemp.ToArray();
            Array.Sort(matches); // сортировка по дате
            List<Event> events = new List<Event>();
            events.AddRange(matches);          
            return events;
        }

        public event EventHandler<ArgsEvent> EventCountry;
    }
}
