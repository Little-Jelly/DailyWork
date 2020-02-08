using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTask2
{
    
    public partial class Add_Form : Form
    {
        public delegate void DisplayUpdateDelegate(string taskid, string time, string topic, string people, string dependence, string result, string reason);
        public event DisplayUpdateDelegate ShowUpdate;
        public Add_Form()
        {
            InitializeComponent();
        }
        private Form mainForm;
        public Add_Form(Form mainForm, string taskId)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.taskid_tb.Text = taskId.ToString();
            this.Visible = true;
        }

        public Add_Form(string taskid, string time, string topic, string people, string dependence, string result, string reason)
        {
            InitializeComponent();
            this.taskid_tb.Text = taskid;
            this.time_tb.Text = time;
            this.topic_tb.Text = topic;
            this.people_tb.Text = people;
            this.dependence_tb.Text = dependence;
            this.result_tb.Text = result;
            this.reason_tb.Text = reason;
            this.Visible = true;
        }

        // 保存按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(this.time_tb.Text == "" || this.topic_tb.Text == ""))
            {
                string taskid = this.taskid_tb.Text;
                string time = this.time_tb.Text;
                string topic = this.topic_tb.Text;
                string people = this.people_tb.Text;
                string dependence = this.dependence_tb.Text;
                string result = this.result_tb.Text;
                string reason = this.reason_tb.Text;
                ShowUpdate(taskid, time, topic, people, dependence, result, reason);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
