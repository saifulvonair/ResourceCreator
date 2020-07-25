namespace ResourceCreator
{
    partial class ResCreator
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
            this.m_Canvas = new System.Windows.Forms.PictureBox();
            this.mUndo = new System.Windows.Forms.Button();
            this.mRedo = new System.Windows.Forms.Button();
            this.m_listItems = new System.Windows.Forms.ListBox();
            this.mCommit = new System.Windows.Forms.Button();
            this.m_grpRight = new System.Windows.Forms.GroupBox();
            this.m_grpLeft = new System.Windows.Forms.GroupBox();
            this.m_GridView = new ResourceCreator.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.m_Canvas)).BeginInit();
            this.m_grpRight.SuspendLayout();
            this.m_grpLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // m_Canvas
            // 
            this.m_Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_Canvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_Canvas.Location = new System.Drawing.Point(27, 36);
            this.m_Canvas.Name = "m_Canvas";
            this.m_Canvas.Size = new System.Drawing.Size(269, 195);
            this.m_Canvas.TabIndex = 1;
            this.m_Canvas.TabStop = false;
            this.m_Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.m_Canvas_Paint);
            this.m_Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Canvas_MouseDown);
            this.m_Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_Canvas_MouseMove);
            this.m_Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_Canvas_MouseUp);
            // 
            // mUndo
            // 
            this.mUndo.Location = new System.Drawing.Point(7, 3);
            this.mUndo.Name = "mUndo";
            this.mUndo.Size = new System.Drawing.Size(65, 23);
            this.mUndo.TabIndex = 5;
            this.mUndo.Text = "Undo";
            this.mUndo.UseVisualStyleBackColor = true;
            this.mUndo.Click += new System.EventHandler(this.mUndo_Click);
            // 
            // mRedo
            // 
            this.mRedo.Location = new System.Drawing.Point(78, 3);
            this.mRedo.Name = "mRedo";
            this.mRedo.Size = new System.Drawing.Size(66, 23);
            this.mRedo.TabIndex = 6;
            this.mRedo.Text = "Redo";
            this.mRedo.UseVisualStyleBackColor = true;
            this.mRedo.Click += new System.EventHandler(this.mRedo_Click);
            // 
            // m_listItems
            // 
            this.m_listItems.FormattingEnabled = true;
            this.m_listItems.Location = new System.Drawing.Point(11, 15);
            this.m_listItems.Name = "m_listItems";
            this.m_listItems.Size = new System.Drawing.Size(193, 147);
            this.m_listItems.TabIndex = 7;
            this.m_listItems.SelectedIndexChanged += new System.EventHandler(this.m_listItems_SelectedIndexChanged);
            this.m_listItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // mCommit
            // 
            this.mCommit.Location = new System.Drawing.Point(150, 3);
            this.mCommit.Name = "mCommit";
            this.mCommit.Size = new System.Drawing.Size(75, 23);
            this.mCommit.TabIndex = 10;
            this.mCommit.Text = "Save";
            this.mCommit.UseVisualStyleBackColor = true;
            this.mCommit.Click += new System.EventHandler(this.mCommit_Click);
            // 
            // m_grpRight
            // 
            this.m_grpRight.Controls.Add(this.m_Canvas);
            this.m_grpRight.Location = new System.Drawing.Point(399, 25);
            this.m_grpRight.Name = "m_grpRight";
            this.m_grpRight.Size = new System.Drawing.Size(394, 374);
            this.m_grpRight.TabIndex = 11;
            this.m_grpRight.TabStop = false;
            this.m_grpRight.Text = "Design Window";
            this.m_grpRight.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // m_grpLeft
            // 
            this.m_grpLeft.Controls.Add(this.m_GridView);
            this.m_grpLeft.Controls.Add(this.m_listItems);
            this.m_grpLeft.Location = new System.Drawing.Point(1, 25);
            this.m_grpLeft.Name = "m_grpLeft";
            this.m_grpLeft.Size = new System.Drawing.Size(394, 374);
            this.m_grpLeft.TabIndex = 12;
            this.m_grpLeft.TabStop = false;
            this.m_grpLeft.Text = "Property and Control";
            // 
            // m_GridView
            // 
            this.m_GridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_GridView.Location = new System.Drawing.Point(68, 232);
            this.m_GridView.Name = "m_GridView";
            this.m_GridView.Size = new System.Drawing.Size(228, 96);
            this.m_GridView.TabIndex = 9;
            this.m_GridView.TabStop = false;
            // 
            // ResCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(847, 413);
            this.Controls.Add(this.m_grpLeft);
            this.Controls.Add(this.m_grpRight);
            this.Controls.Add(this.mCommit);
            this.Controls.Add(this.mRedo);
            this.Controls.Add(this.mUndo);
            this.Name = "ResCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main window(saiful_vonair@yahoo.com)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResCreator_FormClosing);
            this.Load += new System.EventHandler(this.ResCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_Canvas)).EndInit();
            this.m_grpRight.ResumeLayout(false);
            this.m_grpLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_Canvas;
        private System.Windows.Forms.Button mUndo;
        private System.Windows.Forms.Button mRedo;
        private System.Windows.Forms.ListBox m_listItems;
        private GridView m_GridView;
        private System.Windows.Forms.Button mCommit;
        private System.Windows.Forms.GroupBox m_grpRight;
        private System.Windows.Forms.GroupBox m_grpLeft;
    }
}

