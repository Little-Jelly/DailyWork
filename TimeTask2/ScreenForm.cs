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
    public partial class ScreenForm : Form
    {
        public ScreenForm()
        {
            InitializeComponent();
        }
        private Form parentForm;
        public ScreenForm(Form parentForm)
        {
            this.parentForm = parentForm;
            InitializeComponent();
            this.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string passwd = this.textBox1.Text;
            if ("2014520" == passwd)
            {
                this.Visible = false;
                this.parentForm.Visible = true;
            }
            else
            {
                this.textBox1.Text = "";
            }
        }
    }
}
