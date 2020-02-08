using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TimeTask2
{
    public partial class Main_Form : Form
    {
        // 今日任务
        private List<task_entity> todayTasks = new List<task_entity>();
        // 当前选择日期
        private string todayTime = "";
        // 选择的任务
        private task_entity select_task;
        // 添加Form对象
        private Add_Form addForm;
        // 桌面提醒Form对象
        private Next_Form nextForm;
        // 添加的新任务
        private task_entity add_task;
        // 修改后的任务
        private task_entity modify_task;
        // 锁屏form
        private Form screenForm;

        // 久坐提醒的时间控件
        private System.Timers.Timer tim = null;

        public Main_Form()
        {
            InitializeComponent();
            this.check_log();
            this.todayTime = this.monthCalendar1.SelectionStart.ToString("yyyyMMdd");
            this.ReadTasksFromXml(this.todayTime);
            this.toFillTasksToListView(this.todayTasks);
            this.select_task = new task_entity();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.screenForm = new ScreenForm(this);
            this.screenForm.Visible = false;
            tim = new System.Timers.Timer();
            //tim.Interval = 60000;
            tim.Elapsed += new System.Timers.ElapsedEventHandler(tim_Elapsed);
            //tim.Stop();
        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string today = this.monthCalendar1.SelectionStart.ToString("yyyyMMdd");
            this.todayTime = today;

            // 清理全局变量todayTasks
            this.todayTasks.Clear();
            this.ReadTasksFromXml(this.todayTime);
            this.toFillTasksToListView(this.todayTasks);

        }

        void tim_Elapsed(object sender, System.Timers.ElapsedEventArgs e)    
        {
            if (!this.screenForm.Visible)   // 当前没有开启久坐提醒功能
            {                
                // 设置久坐提醒时间
                string alertTime = this.textBox3.Text;
                int alertTime_int = 0;
                int.TryParse(alertTime, out alertTime_int);
                this.tim.Interval = 60000 * alertTime_int;
                int currentW = 0;
                int currentH = 0;
                this.To_GetScreenWidthHeight(ref currentW, ref currentH);
                this.To_FillScreen(this.screenForm, 0, 0, 0, 0, currentW, currentH);
                // 设置久坐提醒密码
                string alertPassword = this.textBox4.Text;
                
                this.screenForm.BackgroundImage = Image.FromFile(@"..\1565355542919.jpg");
                this.Visible = false;
                this.screenForm.Visible = true;
            }
        }

        // 根据日期从Xml获取tasks
        // today形如：20190810
        private void ReadTasksFromXml(string today){
            XmlDocument myXmlDoc = new XmlDocument();        // 初始化一个XML实例
            myXmlDoc.Load(@"../mylog.txt");                   // 通过文件路径加载日志文件
            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");   // 找到logs结点，即日志的根结点for
            string innerXmlInfo = rootNode.InnerXml.ToString();
            string outerXmlInfo = rootNode.OuterXml.ToString();
            XmlNodeList DateNodeList = rootNode.ChildNodes;   // 获取第一层子节点: date层
            foreach (XmlNode node in DateNodeList)
            {
                string date_id = node.Attributes[0].Value;  // 取出每个日期的值
                if (date_id.Equals(today))  // 如果给定的日期，与找到的日期匹配
                {
                    XmlNodeList tasks = node.ChildNodes;
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

        // 根据日期和taskId，从xml中删除任务
        private void deleteTask_to_xml(string today, string task_id)
        {
            // 获取到task的id的属型，通过这个属型id，删除该task
            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(@"../mylog.txt");
            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");
            XmlNodeList DateNodeList = rootNode.ChildNodes;
            foreach (XmlNode node in DateNodeList)
            {
                if (node.Attributes[0].Value.Equals(today)) // 如果找到该天的日期,取出task列表
                {
                    XmlNodeList TaskList = node.ChildNodes; // 获取当天的任务列表
                    foreach (XmlNode task in TaskList)
                    {
                        //if (task.Attributes[0].Value.Equals(task_id))   // 在任务当天的任务列表中找到需要删除的任务
                        //{
                        //    node.RemoveChild(task);
                        //    myXmlDoc.Save(@"../mylog.txt");

                        //}
                        if (task.Attributes[0].Value.Equals(task_id))
                        {
                            XmlNodeList details = task.ChildNodes;
                            details[6].InnerText = "1";
                            myXmlDoc.Save(@"../mylog.txt");
                        }
                    }
                }
            }
        }

        // 根据日期和taskId,从xml中修改任务
        private void modifyTask_to_xml(string today, string task_id)
        {
            // 获取到task的id的属型，通过这个属型id，删除该task
            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(@"../mylog.txt");
            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");
            XmlNodeList DateNodeList = rootNode.ChildNodes;
            foreach (XmlNode node in DateNodeList)
            {
                if (node.Attributes[0].Value.Equals(today)) // 如果找到该天的日期,取出task列表
                {
                    XmlNodeList TaskList = node.ChildNodes; // 获取当天的任务列表
                    foreach (XmlNode task in TaskList)
                    {
                        //if (task.Attributes[0].Value.Equals(task_id))   // 在任务当天的任务列表中找到需要删除的任务
                        //{
                        //    node.RemoveChild(task);
                        //    myXmlDoc.Save(@"../mylog.txt");

                        //}
                        if (task.Attributes[0].Value.Equals(task_id))   // 找到需要修改的任务
                        {
                            XmlNodeList details = task.ChildNodes;

                            details[0].InnerText = this.modify_task.Time ;
                            details[1].InnerText = this.modify_task.Topic;
                            details[2].InnerText = this.modify_task.People;
                            details[3].InnerText = this.modify_task.Result;
                            details[4].InnerText = this.modify_task.Resaon;
                            details[5].InnerText = this.modify_task.Dependence;
                            details[6].InnerText = this.modify_task.Isdelete = "0";
                            myXmlDoc.Save(@"../mylog.txt");
                        }
                    }
                }
            }
        }

        private int getTaskId(string today)
        {
            XmlDocument myXmlDoc = new XmlDocument();

            myXmlDoc.Load(@"../mylog.txt");                   // 通过文件路径加载日志文件

            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");   // 找到logs结点，即日志的根结点

            XmlNodeList DateNodeList = rootNode.ChildNodes;   // 获取第一层子节点: date层

            foreach (XmlNode node in DateNodeList)
            {
                if (node.Attributes[0].Value == todayTime)
                {
                    return node.ChildNodes.Count;
                }
            }
            return 0;
        }

        // 添加任务到当天的任务中
        private void addTask_to_xml(task_entity tasks)
        {
            // 程序需要处理的情况
            // 1. 如果当天有任务   2. 如果当天没有任务
            // 3. 没有该天的日期   4. 有该天的日期
            XmlDocument myXmlDoc = new XmlDocument();

            myXmlDoc.Load(@"../mylog.txt");                   // 通过文件路径加载日志文件

            XmlNode rootNode = myXmlDoc.SelectSingleNode("logs");   // 找到logs结点，即日志的根结点

            XmlNodeList DateNodeList = rootNode.ChildNodes;   // 获取第一层子节点: date层

            XmlElement task_Element = myXmlDoc.CreateElement("task");   // 创建task      
      
            XmlElement time_Node = myXmlDoc.CreateElement("time");
            time_Node.InnerText = tasks.Time;
            XmlElement topic_Node = myXmlDoc.CreateElement("topic");
            topic_Node.InnerText = tasks.Topic;
            XmlElement people_Node = myXmlDoc.CreateElement("people");
            people_Node.InnerText = tasks.People;
            XmlElement result_Node = myXmlDoc.CreateElement("result");
            result_Node.InnerText = tasks.Result;
            XmlElement reason_Node = myXmlDoc.CreateElement("reason");
            reason_Node.InnerText = tasks.Resaon;
            XmlElement dependence_Node = myXmlDoc.CreateElement("dependence");
            dependence_Node.InnerText = tasks.Dependence;
            XmlElement isdelete_Node = myXmlDoc.CreateElement("isdelete");
            isdelete_Node.InnerText = tasks.Isdelete;

            foreach (XmlNode node in DateNodeList)      // node即date结点
            {
                // 如果有没有日期，需要创建新的日期
                if (node.Attributes[0].Value == todayTime)  // 如果日期匹配需要添加任务的日期
                {
                    int num = node.ChildNodes.Count;    // num代表当天有多少个任务
                    string date_id = node.Attributes[0].Value;  // 取出每个日期的值
                    if (num > 0)    // 如果当天已经有了任务，需要取出最后的任务的号，用于添加任务的时候号码递增
                    {
                        string last_task_id = node.LastChild.Attributes[0].Value;
                        int last_task_id_n = 0;
                        int.TryParse(last_task_id, out last_task_id_n);     // 将当天的最后一次任务的task id转换成int型
                        task_Element.SetAttribute("id", (last_task_id_n + 1).ToString());
                        task_Element.AppendChild(time_Node);
                        task_Element.AppendChild(topic_Node);
                        task_Element.AppendChild(people_Node);
                        task_Element.AppendChild(result_Node);
                        task_Element.AppendChild(reason_Node);
                        task_Element.AppendChild(dependence_Node);
                        task_Element.AppendChild(isdelete_Node);
                        node.AppendChild(task_Element);
                        myXmlDoc.Save(@"../mylog.txt");
                        return;
                    }
                    else if (num == 0)
                    {     // 当前还没有任务
                        int last_task_id_n = 0;
                        task_Element.SetAttribute("id", (last_task_id_n + 1).ToString());
                        task_Element.AppendChild(time_Node);
                        task_Element.AppendChild(topic_Node);
                        task_Element.AppendChild(people_Node);
                        task_Element.AppendChild(result_Node);
                        task_Element.AppendChild(reason_Node);
                        task_Element.AppendChild(dependence_Node);
                        task_Element.AppendChild(isdelete_Node);
                        node.AppendChild(task_Element);
                        myXmlDoc.Save(@"../mylog.txt");
                        return;
                    }
                    else
                    {
                        // 异常，需要进行处理
                        return;
                    }
                }
                // 如果有日期没有任务，需要创建新的任务
                //string date_id = node.Attributes[0].Value;  // 取出每个日期的值
                // 如果有这个日期，则删除这个日期的结点，然后添加新结点
                //int num = node.ChildNodes.Count;    // num代表当天有多少个任务                                
            }
            XmlElement date_Element = myXmlDoc.CreateElement("date");   // 如果日期不匹配，需要添加日期结点
            date_Element.SetAttribute("id", this.todayTime.ToString());
            task_Element.SetAttribute("id", (1).ToString());
            task_Element.AppendChild(time_Node);
            task_Element.AppendChild(topic_Node);
            task_Element.AppendChild(people_Node);
            task_Element.AppendChild(result_Node);
            task_Element.AppendChild(reason_Node);
            task_Element.AppendChild(dependence_Node);
            task_Element.AppendChild(isdelete_Node);
            date_Element.AppendChild(task_Element);
            rootNode.AppendChild(date_Element);
            myXmlDoc.Save(@"../mylog.txt");
        }

        // 将当日的任务填写到ListView
        private void toFillTasksToListView(List<task_entity> tasks)
        {
            this.task_preview_ListView.Items.Clear();
            // 将task列表对ListView进行填充
            int task_num = tasks.Count;

            for (int i = 0; i < task_num; i++)
            {
                if (tasks[i].Isdelete != "1")
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = tasks[i].Task_id;
                    lvi.SubItems.Add(tasks[i].Time);
                    lvi.SubItems.Add(tasks[i].Topic);
                    lvi.SubItems.Add(tasks[i].People);
                    lvi.SubItems.Add(tasks[i].Dependence);
                    lvi.SubItems.Add(tasks[i].Result);
                    lvi.SubItems.Add(tasks[i].Resaon);
                    this.task_preview_ListView.Items.Add(lvi);
                    this.task_preview_ListView.EndUpdate();
                }
            }
        }

        // 检查日记文件是否存在
        private void check_log()
        {
            // 如果文件存在，则不管
            // 如果文件不存在，新建文件，命名为mylog.txt，路径为"../"
            if (File.Exists(@"..\mylog.txt"))
            {
                return;
            }
            else
            {
                // 需要新建文件               
                //System.IO.File.Create(@"..\mylog.txt");
                // 创建xml文件
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                XmlNode root = xmlDoc.CreateElement("logs");
                xmlDoc.AppendChild(root);
                xmlDoc.Save(@"..\mylog.txt");
            }
        }


        private void addNewTask(string taskid, string time, string topic, string people, string dependence, string result, string reason)
        {
            this.add_task.setNew( taskid,  time,  topic,  people,  dependence,  result,  reason, "0");            
            this.addTask_to_xml(this.add_task);
            this.todayTasks.Clear();
            this.ReadTasksFromXml(this.todayTime);
            this.toFillTasksToListView(this.todayTasks);
        }

        private void modifyTask(string taskid, string time, string topic, string people, string dependence, string result, string reason)
        {
            //this.add_task.setNew(taskid, time, topic, people, dependence, result, reason, "0");
            //this.addTask_to_xml(this.add_task);
            this.modify_task.setNew(taskid, time, topic, people, dependence, result, reason, "0");
            this.modifyTask_to_xml(this.todayTime, taskid);
            this.todayTasks.Clear();
            this.ReadTasksFromXml(this.todayTime);
            this.toFillTasksToListView(this.todayTasks);
        }

        private void task_preview_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.task_preview_ListView.SelectedIndices;
            if (indexes.Count > 0)
            {
                this.todayTime = this.monthCalendar1.SelectionStart.ToString("yyyyMMdd"); 
                string first = this.task_preview_ListView.SelectedItems[0].Text;                // 任务编号
                string second = this.task_preview_ListView.SelectedItems[0].SubItems[1].Text;   // 任务时间
                string third = this.task_preview_ListView.SelectedItems[0].SubItems[2].Text;    // 任务主题
                string forth = this.task_preview_ListView.SelectedItems[0].SubItems[3].Text;    // 相关人员
                string fifth = this.task_preview_ListView.SelectedItems[0].SubItems[4].Text;    // 依赖项目
                string sixth = this.task_preview_ListView.SelectedItems[0].SubItems[5].Text;    // 完成状态
                string seventh = this.task_preview_ListView.SelectedItems[0].SubItems[6].Text;    // 原因描述
                string eighth = "0";
                this.select_task.clear();
                this.select_task.setNew(first, second, third, forth, fifth, sixth, seventh, eighth);
            }

        }

        // 添加新任务，打开添加新任务form
        private void button4_Click(object sender, EventArgs e)
        {
            this.add_task = new task_entity();
            this.add_task.clear();
            this.todayTime = this.monthCalendar1.SelectionStart.ToString("yyyyMMdd");
            // 获取当天的任务数量,并生成下一个taskid
            int Count = getTaskId(this.todayTime);
            string taskid = (Count + 1).ToString();
            this.addForm = new Add_Form(this, taskid);
            this.addForm.ShowUpdate += new Add_Form.DisplayUpdateDelegate(addNewTask);
        }

        // 删除任务记录
        private void button5_Click(object sender, EventArgs e)
        {
            string taskid = this.select_task.Task_id;
            this.deleteTask_to_xml(this.todayTime, taskid);
            this.todayTasks.Clear();
            this.ReadTasksFromXml(this.todayTime);
            this.toFillTasksToListView(this.todayTasks);
        }

        // 修改任务记录
        private void button6_Click(object sender, EventArgs e)
        {
            this.modify_task = new task_entity();
            this.modify_task.clear();
            string taskid = this.select_task.Task_id;
            string time = this.select_task.Time;
            string topic = this.select_task.Topic;
            string people = this.select_task.People;
            string result = this.select_task.Result;
            string reason = this.select_task.Resaon;
            string dependence = this.select_task.Dependence;
            string isdelete = "0";
            this.addForm = new Add_Form(taskid, time, topic, people, dependence, result, reason);
            this.addForm.ShowUpdate += new Add_Form.DisplayUpdateDelegate(modifyTask);
        }

        // 打开桌面提醒功能
        private void button3_Click(object sender, EventArgs e)
        {
            if ("打开桌面提醒" == this.Desktop_Alert_bt.Text)
            {
                this.Desktop_Alert_bt.Text = "关闭桌面提醒";
                this.nextForm = new Next_Form();
                //this.nextForm.Visible = false;
                this.nextForm.Visible = true;
            }
            else if ("关闭桌面提醒" == this.Desktop_Alert_bt.Text)
            {
                this.Desktop_Alert_bt.Text = "打开桌面提醒";
                //this.nextForm.Visible = false;
                this.nextForm.Close();
            }
        }

        // 填充整个屏幕
        private void To_FillScreen(Form target, int width, int height, int startx, int starty, int currentWidth, int currentHeight)
        {      // 调整程序的尺寸，铺满全屏幕，包括覆盖任务栏      
            target.FormBorderStyle = FormBorderStyle.None;
            target.MaximumSize = new Size(currentWidth, currentHeight);
            target.WindowState = FormWindowState.Maximized;
        }

        // 开启锁屏，以响应久坐提醒
        private void button2_Click(object sender, EventArgs e)
        {
            if ("开启久坐提醒" == this.button2.Text)
            {                
                this.tim.Start();
                this.button2.Text = "关闭久坐提醒";
            }
            else if ("关闭久坐提醒" == this.button2.Text)
            {
                this.tim.Stop();
                this.button2.Text = "开启久坐提醒";
            }       
        }

        // 获取当前屏幕的宽度和高度
        private void To_GetScreenWidthHeight(ref int currentW, ref int currentH)
        {    
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);    // 获取全屏幕尺寸（包含任务栏）
            int width = ScreenArea.Width;
            int height = ScreenArea.Height;
            currentW = width;
            currentH = height;        
        }
    }
}
