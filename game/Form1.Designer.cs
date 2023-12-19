namespace game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundPanel = new System.Windows.Forms.Panel();
            this.GameInfo = new System.Windows.Forms.Label();
            this.inventory = new System.Windows.Forms.Panel();
            this.clerkTasksInfo = new System.Windows.Forms.Label();
            this.hackerTasksInfo = new System.Windows.Forms.Label();
            this.avatar = new System.Windows.Forms.PictureBox();
            this.SQLink = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Location = new System.Drawing.Point(57, 46);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(1054, 650);
            this.backgroundPanel.TabIndex = 0;
            this.backgroundPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundPanel_Paint);
            this.backgroundPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.backgroundPanel_MouseClick);
            // 
            // GameInfo
            // 
            this.GameInfo.AutoSize = true;
            this.GameInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GameInfo.Location = new System.Drawing.Point(1198, 340);
            this.GameInfo.Name = "GameInfo";
            this.GameInfo.Size = new System.Drawing.Size(46, 17);
            this.GameInfo.TabIndex = 2;
            this.GameInfo.Text = "label1";
            // 
            // inventory
            // 
            this.inventory.Location = new System.Drawing.Point(57, 731);
            this.inventory.Name = "inventory";
            this.inventory.Size = new System.Drawing.Size(1054, 162);
            this.inventory.TabIndex = 4;
            this.inventory.Paint += new System.Windows.Forms.PaintEventHandler(this.inventory_Paint);
            this.inventory.DoubleClick += new System.EventHandler(this.inventory_DoubleClick);
            this.inventory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inventory_MouseClick);
            // 
            // clerkTasksInfo
            // 
            this.clerkTasksInfo.AutoSize = true;
            this.clerkTasksInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clerkTasksInfo.Location = new System.Drawing.Point(1198, 46);
            this.clerkTasksInfo.Name = "clerkTasksInfo";
            this.clerkTasksInfo.Size = new System.Drawing.Size(116, 17);
            this.clerkTasksInfo.TabIndex = 6;
            this.clerkTasksInfo.Text = "Задачи на день:";
            // 
            // hackerTasksInfo
            // 
            this.hackerTasksInfo.AutoSize = true;
            this.hackerTasksInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hackerTasksInfo.Location = new System.Drawing.Point(1560, 46);
            this.hackerTasksInfo.Name = "hackerTasksInfo";
            this.hackerTasksInfo.Size = new System.Drawing.Size(67, 17);
            this.hackerTasksInfo.TabIndex = 7;
            this.hackerTasksInfo.Text = "Пакости:";
            // 
            // avatar
            // 
            this.avatar.Location = new System.Drawing.Point(1201, 637);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(256, 256);
            this.avatar.TabIndex = 8;
            this.avatar.TabStop = false;
            // 
            // SQLink
            // 
            this.SQLink.Location = new System.Drawing.Point(1201, 495);
            this.SQLink.Name = "SQLink";
            this.SQLink.Size = new System.Drawing.Size(548, 136);
            this.SQLink.TabIndex = 9;
            this.SQLink.Paint += new System.Windows.Forms.PaintEventHandler(this.SQLink_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 921);
            this.Controls.Add(this.SQLink);
            this.Controls.Add(this.avatar);
            this.Controls.Add(this.hackerTasksInfo);
            this.Controls.Add(this.clerkTasksInfo);
            this.Controls.Add(this.inventory);
            this.Controls.Add(this.GameInfo);
            this.Controls.Add(this.backgroundPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel backgroundPanel;
        private System.Windows.Forms.Label GameInfo;
        private System.Windows.Forms.Panel inventory;
        private System.Windows.Forms.Label clerkTasksInfo;
        private System.Windows.Forms.Label hackerTasksInfo;
        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.Panel SQLink;
    }
}

