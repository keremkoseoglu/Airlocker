using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Airlocker
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ProgressBar progMain;
		private System.Windows.Forms.Label lblFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Timers.Timer timClose;

		private transporter myTransporter;

		public frmMain(string[] args)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = Application.ProductName + " " + Application.ProductVersion.Substring(0, 3) + " - written by Kerem Koseoglu";

			try
			{
				myTransporter = new transporter();

				myTransporter.itemAnalysed		+= new transporter.transportEvent(itemAnalysed);
				myTransporter.subItemDetermined	+= new transporter.transportEvent(subItemDetermined);
				myTransporter.itemCopied		+= new transporter.transportEvent(subItemDetermined);
				myTransporter.noParameter		+= new transporter.transportEvent(subItemDetermined);
			
				// Analyse parameters
				try
				{
					myTransporter.analyseParameters(args);
				}
				catch(Exception ex)
				{
					setFile(ex.ToString());
				}
			}
			catch(ConfigException ex)
			{
				setFile("Please run the configuration utility first...");
			}

		}

		private void itemAnalysed(object sender, transportEventArgs e)
		{
			setFile(e.info);
			progMain.Maximum = e.max;
			progMain.Value = e.pos;
			this.Show();
			Application.DoEvents();

			if (e.max == e.pos)
			{
				try
				{
					setStatus("Determining subitems...");
					myTransporter.determineSubItems();

					setStatus("Copying items...");
					myTransporter.copyAllItems();

					setStatus("Complete!");
					timClose.Enabled = true;
				}
				catch(Exception ex)
				{
					setFile(ex.ToString());
				}
			}
		}

		private void subItemDetermined(object sender, transportEventArgs e)
		{
			setFile(e.info);
			progMain.Maximum = e.max;
			progMain.Value = e.pos;
			this.Show();
			Application.DoEvents();
		}

		public void setStatus(string Status)
		{
			lblStatus.Text	= Status;
			lblFile.Text	= "";
			Application.DoEvents();
		}

		public void setFile(string File)
		{
			lblFile.Text = File;
			Application.DoEvents();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.progMain = new System.Windows.Forms.ProgressBar();
			this.lblFile = new System.Windows.Forms.Label();
			this.timClose = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timClose)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(106, 97);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(128, 8);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(296, 16);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Airlocker";
			// 
			// progMain
			// 
			this.progMain.Location = new System.Drawing.Point(128, 80);
			this.progMain.Name = "progMain";
			this.progMain.Size = new System.Drawing.Size(296, 23);
			this.progMain.TabIndex = 2;
			// 
			// lblFile
			// 
			this.lblFile.Location = new System.Drawing.Point(128, 32);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(296, 40);
			this.lblFile.TabIndex = 3;
			this.lblFile.Text = "Airlocker";
			// 
			// timClose
			// 
			this.timClose.SynchronizingObject = this;
			this.timClose.Elapsed += new System.Timers.ElapsedEventHandler(this.timClose_Elapsed);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(432, 117);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.progMain);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.timClose)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			// Create form
			frmMain myFrmMain = new frmMain(args);

			// Start application
			Application.Run(myFrmMain);
		}

		private void timClose_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Application.Exit();
		}
	}
}
