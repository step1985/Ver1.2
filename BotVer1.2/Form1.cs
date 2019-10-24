using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotVer1._2.Controllers;
using BotVer1._2.Models;
using BotVer1._2.Views;
using System.Threading;

namespace BotVer1._2
{
    public partial class Form1 : Form, IView
    {
        Controller controller;
        List<TreeView> listTreeView = new List<TreeView>();
        public Form1()
        {
            InitializeComponent();

            listTreeView.Add(this.treeView1);
            listTreeView.Add(this.treeView2);
            listTreeView.Add(this.treeView3);
            listTreeView.Add(this.treeView4);
            listTreeView.Add(this.treeView5);

            controller = new Controller(this, listTreeView);
            
        }

        public void UpdateTreeview(Country country, ArgsEvent e)
        {
            if (e.Flag)
                country.treeView.Nodes.Add(e.TextNode); // add Team to TreeView
            else
                country.treeView.Nodes[e.Index].Nodes.Add(e.TextNode); // add Totall to TreeView
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(StartApp);
            t1.Start();
        }

        void StartApp()
        {
            while(true)
            {
                foreach(Country country in controller.countries)
                {
                    controller.Update(country);
                }
            }
        }
    }
}
