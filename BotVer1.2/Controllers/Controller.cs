using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.TO;
using BotVer1._2.Views;
using BotVer1._2.Models;
using System.Windows.Forms;
using System.Diagnostics;

namespace BotVer1._2.Controllers
{
    public class Controller
    {
        IView viewForm;
        public IClient client = null;
        List<TreeView> listTreeView;
        public List<Country> countries = new List<Country>();
        public List<CountryPatern> countriess = new List<CountryPatern>();
        public CurrentOrderSummaryReport listOrders;

        string[,] listCompetitionIds = new string[5, 2];
        public Controller(IView view, List<TreeView> listTreeView, List<CheckBox> checkBoxes)
        {
            viewForm = view;
            this.listTreeView = listTreeView;
            client = new JsonRpcClient();
            //listTreeView[0].Nodes.AddRange(()
            listOrders = client.listCurrentOrders();

            listCompetitionIds[0, 0] = "English Premier League";
            listCompetitionIds[0, 1] = "10932509";
            listCompetitionIds[1, 0] = "Spanish La Liga";
            listCompetitionIds[1, 1] = "117";
            listCompetitionIds[2, 0] = "French Ligue 1";
            listCompetitionIds[2, 1] = "55";
            listCompetitionIds[3, 0] = "German Bundesliga 1";
            listCompetitionIds[3, 1] = "59";
            listCompetitionIds[4, 0] = "Italian Serie A";
            listCompetitionIds[4, 1] = "81";

            var sw = new Stopwatch();
            sw.Start();
            //for (int i = 0; i < 5; i++)
            //{

            //    Country country = new Country(client, listCompetitionIds[i, 0], listCompetitionIds[i, 1], listTreeView[i]);
            //    country.EventCountry += Event_UpdateTreeview;
            //    countries.Add(country);
            //    Update(country);
            //    country.IsCreatedCountry = true;
            //}
            for (int i = 0; i < 5; i++)
            {
                CountryPatern country = new CountryPatern(listCompetitionIds[i, 0], listCompetitionIds[i, 1], checkBoxes[i]);
                countriess.Add(country);
                country.Interpret(listOrders.CurrentOrders);
                country.Operation();
                //listTreeView[i].Nodes.Add(country.childNode);
                foreach (var component in country.components)
                {
                    listTreeView[i].Nodes.Add(component.childNode);                                      
                }
                //checkBoxes[i].Text = country.Name;
                
            }

            sw.Stop();
            var sww = sw.ElapsedMilliseconds;
            //int a = 0;
            //foreach (var country in countriess)
            //{
            //    foreach (var component in country.components)
            //    {
            //        listTreeView[a].Nodes.Add(component.childNode);
            //    }
            //    a++;
            //}
        }

        private void Event_UpdateTreeview(object sender, ArgsEvent e)
        {
            Country country = (Country)sender;
            viewForm.UpdateTreeview(country, e);
        }
        public void Update(Country country)
        {
            country.UpdateTeams();
        }
    }
}
