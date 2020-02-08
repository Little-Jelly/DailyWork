using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TimeTask2
{
    public partial class Next_Form : Form
    {
        private System.Timers.Timer tim = null;
        // 今日任务
        private List<task_entity> todayTasks = new List<task_entity>();
        public Next_Form()
        {
            InitializeComponent();
            this.ControlBox = false;
            //Control.CheckForIllegalCrossThreadCalls = false;     
            string today = DateTime.Now.ToString("yyyyMMdd");
            this.todayTasks.Clear();
            //this.Desktop_lv.Items.Clear();
            this.ReadTasksFromXml(today);
            this.toFillTasksToListView(this.todayTasks);
        }
        void tim_Elapsed(object sender, System.Timers.ElapsedEventArgs e)     // 每秒钟，轮询读取IC卡号
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            this.todayTasks.Clear();
            //this.Desktop_lv.Items.Clear();
            this.ReadTasksFromXml(today);
            this.toFillTasksToListView(this.todayTasks);
        }

        // 根据日期从Xml获取tasks
        // today形如：20190810
        private void ReadTasksFromXml(string today)
        {
            XmlDocument myXmlDoc = new XmlDocument();        // 初始化一个XML实例
            myXmlDoc.Load(@"../mylog.txt");                   // 通过文件路径加载日志文件
            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");   // 找到logs结点，即日志的根结点for
            string innerXmlInfo = rootNode.InnerXml.ToString();
            string outerXmlInfo = rootNode.OuterXml.ToString();
            XmlNodeList DateNodeList = rootNode.ChildNodes;   // 获取第一层子节点: date（日期）层
            foreach (XmlNode node in DateNodeList)
            {
                string date_id = node.Attributes[0].Value;  // 取出每个日期的值
                if (date_id.Equals(today))  // 如果给定的日期，与找到的日期匹配
                {
                    XmlNodeList tasks = node.ChildNodes;    // 获取当日的所有任务
                    int num = tasks.Count;
                    for (int i = 0; i < num; i++)   // 遍历今天所有的task
                    {
                        task_entity my_task = new task_entity();
                        XmlNodeList details = tasks[i].ChildNodes;  // 获取任务中的细节：time、topic、people、result、reason
                        for (int j = 0; j < details.Count; j++)
                        {
                            my_task.Time = details[0].InnerText;
                            my_task.Topic = details[1].InnerText;
                            my_task.People = details[2].InnerText;
                            my_task.Result = details[3].InnerText;
                            my_task.Resaon = details[4].InnerText;
                            my_task.Dependence = details[5].InnerText;
                            my_task.Isdelete = details[6].InnerText;
                        }
                        my_task.Task_id = tasks[i].Attributes[0].Value;
                        this.todayTasks.Add(my_task);
                    }
                }
            }
        }

        // 将当日的任务填写到ListView
        private void toFillTasksToListView(List<task_entity> tasks)
        {
            //this.listView1.Items.Clear();
            this.Desktop_lv.Items.Clear();
            // 将task列表对ListView进行填充
            int task_num = tasks.Count;

            for (int i = 0; i < task_num; i++)
            {
                if (tasks[i].Isdelete != "1")
                {
                    //ListViewItem lvi = new ListViewItem();
                    //lvi.Text = tasks[i].Time;
                    //lvi.SubItems.Add(tasks[i].Topic);
                    ////Desktop_lv.Items.Add(lvi);
                    //Desktop_lv.EndUpdate();

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = tasks[i].Time;
                    lvi.SubItems.Add(tasks[i].Topic);
                    //lvi.BackColor = Color.Green;  // 添加行背景颜色
                    if("Done".ToLower()==tasks[i].Result.ToLower()){
                        //lvi.BackColor = Color.Green;
                        lvi.ForeColor = Color.Green;
                    }else if("Doing".ToLower()==tasks[i].Result.ToLower()){
                        //lvi.BackColor = Color.Yellow;
                        lvi.ForeColor = Color.DarkGoldenrod;
                    }
                    else if ("Not Yet".ToLower() == tasks[i].Result.ToLower())
                    {
                        //lvi.BackColor = Color.Red;
                        lvi.ForeColor = Color.Red;
                    }
                    this.Desktop_lv.Items.Add(lvi);
                    this.Desktop_lv.EndUpdate();
                }
            }
        }

        private void To_GetScreenWidthHeight(ref int currentW, ref int currentH)
        {    // 获取当前屏幕的宽度和高度
            //Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);      // 获取非全屏幕尺寸（不包含任务栏）
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);    // 获取全屏幕尺寸（包含任务栏）
            int width = ScreenArea.Width;
            int height = ScreenArea.Height;
            currentW = width;
            currentH = height;
        }

        private void Next_Form_Load(object sender, EventArgs e)
        {
            int currentW = 0;
            int currentH = 0;
            this.To_GetScreenWidthHeight(ref currentW, ref currentH);
            int startX = currentW - 350;
            int startY = 50;
            this.Location = new Point(startX, startY);
            tim = new System.Timers.Timer();
            tim.Interval = 1000;
            tim.Elapsed += new System.Timers.ElapsedEventHandler(tim_Elapsed);
            tim.Start();
        }

   

   


    }
}
