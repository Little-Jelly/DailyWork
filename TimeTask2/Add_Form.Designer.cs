namespace TimeTask2
{
    partial class Add_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.taskid_tb = new System.Windows.Forms.TextBox();
            this.topic_tb = new System.Windows.Forms.TextBox();
            this.result_tb = new System.Windows.Forms.TextBox();
            this.time_tb = new System.Windows.Forms.TextBox();
            this.people_tb = new System.Windows.Forms.TextBox();
            this.reason_tb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dependence_tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "相关人员：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "完成状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "原因描述：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "任务主题：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "依赖项目：";
            // 
            // taskid_tb
            // 
            this.taskid_tb.Location = new System.Drawing.Point(146, 71);
            this.taskid_tb.Name = "taskid_tb";
            this.taskid_tb.ReadOnly = true;
            this.taskid_tb.Size = new System.Drawing.Size(136, 20);
            this.taskid_tb.TabIndex = 6;
            // 
            // topic_tb
            // 
            this.topic_tb.Location = new System.Drawing.Point(146, 122);
            this.topic_tb.Name = "topic_tb";
            this.topic_tb.Size = new System.Drawing.Size(136, 20);
            this.topic_tb.TabIndex = 7;
            // 
            // result_tb
            // 
            this.result_tb.Location = new System.Drawing.Point(377, 175);
            this.result_tb.Name = "result_tb";
            this.result_tb.Size = new System.Drawing.Size(136, 20);
            this.result_tb.TabIndex = 8;
            // 
            // time_tb
            // 
            this.time_tb.Location = new System.Drawing.Point(377, 71);
            this.time_tb.Name = "time_tb";
            this.time_tb.Size = new System.Drawing.Size(136, 20);
            this.time_tb.TabIndex = 9;
            // 
            // people_tb
            // 
            this.people_tb.Location = new System.Drawing.Point(377, 122);
            this.people_tb.Name = "people_tb";
            this.people_tb.Size = new System.Drawing.Size(136, 20);
            this.people_tb.TabIndex = 10;
            // 
            // reason_tb
            // 
            this.reason_tb.Location = new System.Drawing.Point(76, 262);
            this.reason_tb.Multiline = true;
            this.reason_tb.Name = "reason_tb";
            this.reason_tb.Size = new System.Drawing.Size(437, 123);
            this.reason_tb.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 414);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(307, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(97, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "编号：";
            // 
            // dependence_tb
            // 
            this.dependence_tb.Location = new System.Drawing.Point(146, 170);
            this.dependence_tb.Name = "dependence_tb";
            this.dependence_tb.Size = new System.Drawing.Size(136, 20);
            this.dependence_tb.TabIndex = 15;
            // 
            // Add_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 481);
            this.Controls.Add(this.dependence_tb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reason_tb);
            this.Controls.Add(this.people_tb);
            this.Controls.Add(this.time_tb);
            this.Controls.Add(this.result_tb);
            this.Controls.Add(this.topic_tb);
            this.Controls.Add(this.taskid_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Add_Form";
            this.Text = "添加任务";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox taskid_tb;
        private System.Windows.Forms.TextBox topic_tb;
        private System.Windows.Forms.TextBox result_tb;
        private System.Windows.Forms.TextBox time_tb;
        private System.Windows.Forms.TextBox people_tb;
        private System.Windows.Forms.TextBox reason_tb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dependence_tb;
    }
}