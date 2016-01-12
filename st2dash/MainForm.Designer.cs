/*
 * Created by SharpDevelop.
 * User: Paul
 * Date: 27.06.2015
 * Time: 13:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace st2dash
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox textBoxInputFilename;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonInputFile;
		private System.Windows.Forms.TextBox textBoxOutputFilename;
		private System.Windows.Forms.Button buttonOutputFile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button Close;
    private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.TextBox textBoxMaxLatLongDeviation;
		private System.Windows.Forms.CheckBox checkBoxEarthTools;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
      this.textBoxInputFilename = new System.Windows.Forms.TextBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.buttonInputFile = new System.Windows.Forms.Button();
      this.textBoxOutputFilename = new System.Windows.Forms.TextBox();
      this.buttonOutputFile = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.buttonStart = new System.Windows.Forms.Button();
      this.Close = new System.Windows.Forms.Button();
      this.buttonHelp = new System.Windows.Forms.Button();
      this.textBoxMaxLatLongDeviation = new System.Windows.Forms.TextBox();
      this.checkBoxEarthTools = new System.Windows.Forms.CheckBox();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.label1 = new System.Windows.Forms.Label();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // textBoxInputFilename
      // 
      this.textBoxInputFilename.Location = new System.Drawing.Point(15, 41);
      this.textBoxInputFilename.Name = "textBoxInputFilename";
      this.textBoxInputFilename.Size = new System.Drawing.Size(396, 20);
      this.textBoxInputFilename.TabIndex = 0;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // buttonInputFile
      // 
      this.buttonInputFile.Location = new System.Drawing.Point(12, 12);
      this.buttonInputFile.Name = "buttonInputFile";
      this.buttonInputFile.Size = new System.Drawing.Size(75, 23);
      this.buttonInputFile.TabIndex = 1;
      this.buttonInputFile.Text = "Input File";
      this.buttonInputFile.UseVisualStyleBackColor = true;
      this.buttonInputFile.Click += new System.EventHandler(this.ButtonClickInputFile);
      // 
      // textBoxOutputFilename
      // 
      this.textBoxOutputFilename.Location = new System.Drawing.Point(15, 100);
      this.textBoxOutputFilename.Name = "textBoxOutputFilename";
      this.textBoxOutputFilename.Size = new System.Drawing.Size(396, 20);
      this.textBoxOutputFilename.TabIndex = 2;
      this.textBoxOutputFilename.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // buttonOutputFile
      // 
      this.buttonOutputFile.Location = new System.Drawing.Point(15, 70);
      this.buttonOutputFile.Name = "buttonOutputFile";
      this.buttonOutputFile.Size = new System.Drawing.Size(75, 23);
      this.buttonOutputFile.TabIndex = 3;
      this.buttonOutputFile.Text = "Output File";
      this.buttonOutputFile.UseVisualStyleBackColor = true;
      this.buttonOutputFile.Click += new System.EventHandler(this.ButtonClickOutputFile);
      // 
      // buttonStart
      // 
      this.buttonStart.Enabled = false;
      this.buttonStart.Location = new System.Drawing.Point(171, 171);
      this.buttonStart.Name = "buttonStart";
      this.buttonStart.Size = new System.Drawing.Size(75, 23);
      this.buttonStart.TabIndex = 4;
      this.buttonStart.Text = "Start";
      this.buttonStart.UseVisualStyleBackColor = true;
      this.buttonStart.Click += new System.EventHandler(this.ButtonClickStart);
      // 
      // Close
      // 
      this.Close.Location = new System.Drawing.Point(252, 171);
      this.Close.Name = "Close";
      this.Close.Size = new System.Drawing.Size(75, 23);
      this.Close.TabIndex = 7;
      this.Close.Text = "Close";
      this.Close.UseVisualStyleBackColor = true;
      this.Close.Click += new System.EventHandler(this.CancelClick);
      // 
      // buttonHelp
      // 
      this.buttonHelp.Location = new System.Drawing.Point(333, 171);
      this.buttonHelp.Name = "buttonHelp";
      this.buttonHelp.Size = new System.Drawing.Size(75, 23);
      this.buttonHelp.TabIndex = 8;
      this.buttonHelp.Text = "Help";
      this.buttonHelp.UseVisualStyleBackColor = true;
      this.buttonHelp.Click += new System.EventHandler(this.ButtonClickHelp);
      // 
      // textBoxMaxLatLongDeviation
      // 
      this.textBoxMaxLatLongDeviation.Location = new System.Drawing.Point(12, 156);
      this.textBoxMaxLatLongDeviation.Name = "textBoxMaxLatLongDeviation";
      this.textBoxMaxLatLongDeviation.Size = new System.Drawing.Size(32, 20);
      this.textBoxMaxLatLongDeviation.TabIndex = 10;
      this.textBoxMaxLatLongDeviation.Text = "2\r\n";
      this.textBoxMaxLatLongDeviation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBoxMaxLatLongDeviation.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
      // 
      // checkBoxEarthTools
      // 
      this.checkBoxEarthTools.Checked = true;
      this.checkBoxEarthTools.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxEarthTools.Location = new System.Drawing.Point(15, 126);
      this.checkBoxEarthTools.Name = "checkBoxEarthTools";
      this.checkBoxEarthTools.Size = new System.Drawing.Size(234, 24);
      this.checkBoxEarthTools.TabIndex = 12;
      this.checkBoxEarthTools.Text = "use earthtools.org to calculate altitude";
      this.checkBoxEarthTools.UseVisualStyleBackColor = true;
      this.checkBoxEarthTools.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 210);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(423, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripProgressBar1
      // 
      this.toolStripProgressBar1.Name = "toolStripProgressBar1";
      this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(107, 17);
      this.toolStripStatusLabel1.Text = "st2dash Version 1.4";
      this.toolStripStatusLabel1.Click += new System.EventHandler(this.ToolStripStatusLabel1Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(50, 159);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(111, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "max lat/long deviation";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(423, 232);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.checkBoxEarthTools);
      this.Controls.Add(this.textBoxMaxLatLongDeviation);
      this.Controls.Add(this.buttonHelp);
      this.Controls.Add(this.Close);
      this.Controls.Add(this.buttonInputFile);
      this.Controls.Add(this.textBoxInputFilename);
      this.Controls.Add(this.buttonStart);
      this.Controls.Add(this.buttonOutputFile);
      this.Controls.Add(this.textBoxOutputFilename);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Name = "MainForm";
      this.Text = "st2dash";
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

    private System.Windows.Forms.Label label1;
	}
}
