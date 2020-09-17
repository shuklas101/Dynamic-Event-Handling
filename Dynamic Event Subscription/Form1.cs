using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection; 
using CommonLibrary;
namespace Dynamic_Event_Subscription
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            SubscriptionManager.AddSubscriber(Assembly.LoadFrom("MethodHandlerLibrary.dll"));
            SubscriptionManager.PublishEvents(this);
            SubscriptionManager.LinkPublishers();
            InitializeComponent();
        }
        [EventAtrributePublisher("DynamicEventMethod")]
        public event EventHandler<ParameterEventArgs> CustomEvent;
        private void button1_Click(object sender, EventArgs e)
        {
            if (CustomEvent != null)
            {
                Hashtable parameterCollection = new Hashtable();
                parameterCollection.Add("name", lblDynamic.Text);
                ParameterEventArgs pe = new ParameterEventArgs(parameterCollection);
                CustomEvent(this, pe);
                lblDynamic.Text = pe.ParameterCollection["name"].ToString();
            }
        }
    }
}