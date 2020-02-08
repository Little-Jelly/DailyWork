using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTask2
{
    class task_entity
    {
        private string time = "";   // 任务时间

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        private string topic = "";  // 任务主题

        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }
        private string people = ""; // 相关人员

        public string People
        {
            get { return people; }
            set { people = value; }
        }
        private string result = ""; // 完成状态

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        private string resaon = ""; // 原因描述

        public string Resaon
        {
            get { return resaon; }
            set { resaon = value; }
        }
        private string task_id; // 编号

        public string Task_id
        {
            get { return task_id; }
            set { task_id = value; }
        }

        private string dependence;  // 依赖项目

        public string Dependence
        {
            get { return dependence; }
            set { dependence = value; }
        }
        private string isdelete;    // 删除标识符，如果为0，表示未删除，如果为1，表示已删除

        public string Isdelete
        {
            get { return isdelete; }
            set { isdelete = value;}
        }

        public task_entity()
        {

        }
        public task_entity(string taskid, string time, string topic, string people,string dependence, string result, string reason, string isdelete)
        {
            this.time = time;
            this.topic = topic;
            this.people = people;
            this.result = result;
            this.resaon = reason;
            this.task_id = taskid;
            this.dependence = dependence;
            this.isdelete = isdelete;
        }

        public void setNew(string taskid, string time, string topic, string people, string dependence, string result, string reason, string isdelete)
        {
            this.time = time;
            this.topic = topic;
            this.people = people;
            this.result = result;
            this.resaon = reason;
            this.task_id = taskid;
            this.dependence = dependence;
            this.isdelete = isdelete;
        }
        public void clear()
        {
            this.Dependence = "";
            this.People = "";
            this.Resaon = "";
            this.Topic = "";
            this.Time = "";
            this.Task_id = "";
            this.Result = "";
            this.isdelete = "";
        }

    }
}
