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
    //public delegate void Del(IClient client);
    public delegate void Del();
    public partial class Form1 : Form, IView
    {
        //IClient client = new JsonRpcClient();
        Thread t1;
        Del del;
        Controller controller;
        List<TreeView> listTreeView = new List<TreeView>();
        List<CheckBox> checkBoxes = new List<CheckBox>();
        public Form1()
        {
            InitializeComponent();

            listTreeView.Add(this.treeView1);
            listTreeView.Add(this.treeView2);
            listTreeView.Add(this.treeView3);
            listTreeView.Add(this.treeView4);
            listTreeView.Add(this.treeView5);

            checkBoxes.Add(this.checkBox1);
            checkBoxes.Add(this.checkBox2);
            checkBoxes.Add(this.checkBox3);
            checkBoxes.Add(this.checkBox4);
            checkBoxes.Add(this.checkBox5);

            controller = new Controller(this, listTreeView, checkBoxes);
            
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
            if (t1 == null)
            {
                t1 = new Thread(StartApp);
                t1.Start();
            }
            else
                t1.Resume();
        }

        void StartApp()
        {
            int i = 1;
            while (true)
            {
                foreach (CountryPatern country in controller.countriess)
                {
                    if (country.checkBox.Checked)
                    {
                        toolStripStatusLabel1.Text = i.ToString() + " Check " + country.Name;
                        foreach (var component in country.components)
                        {
                                del = new Del(component.Check);
                                if (InvokeRequired)
                                    //Invoke(del, controller.client); // send parameter
                                    Invoke(del);
                                else
                                //del(controller.client); // send parametr
                                del();
                        }
                    }
                }
                i++;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            button2_Click(sender, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t1 != null)
            {
                t1.Suspend();
            }
        }

        //void StartApp()
        //{
        //    while(true)
        //    {
        //        foreach(Country country in controller.countries)
        //        {
        //            controller.Update(country);
        //        }
        //    }
        //}
    }
}
