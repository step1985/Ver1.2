using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotVer1._2.TO;
using BotVer1._2.Views;
using BotVer1._2.Models;
using System.Windows.Forms;

namespace BotVer1._2.Controllers
{
    public class Controller
    {
        IView viewForm;
        IClient client = null;
        List<TreeView> listTreeView;
        public List<Country> countries = new List<Country>();

        string[,] listCompetitionIds = new string[5, 2];
        public Controller(IView view, List<TreeView> listTreeView)
        {
            viewForm = view;
            this.listTreeView = listTreeView;
            client = new JsonRpcClient();
           
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

            for (int i = 0; i < 5; i++)
            {

                Country country = new Country(client, listCompetitionIds[i, 0], listCompetitionIds[i, 1], listTreeView[i]);
                country.EventCountry += Event_UpdateTreeview;
                countries.Add(country);
                Update(country);
                country.IsCreatedCountry = true;
            }
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
