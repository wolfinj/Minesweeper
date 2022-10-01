namespace Minesweeper
{
    partial class Minesweeper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Minesweeper));
            this.Start = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(405, 1123);
            this.Start.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(397, 98);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_button);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(819, 1123);
            this.button2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(360, 98);
            this.button2.TabIndex = 1;
            this.button2.Text = "Hard";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(73, 1123);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(305, 97);
            this.restart.TabIndex = 2;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1211, 1250);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Button Start;

        #endregion

        private System.Windows.Forms.Button restart;
    }
}

