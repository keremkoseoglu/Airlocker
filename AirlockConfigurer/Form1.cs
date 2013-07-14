using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace AirlockConfigurer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtBase;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.FolderBrowserDialog ofd;

		private AirlockConfig.Config config;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			config = new AirlockConfig.Config();
			config.readConfig();
			txtBase.Text = config.targetPath;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtBase = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.ofd = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Controls.Add(this.txtBase);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 56);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(395, 24);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.TabIndex = 5;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtBase
			// 
			this.txtBase.Location = new System.Drawing.Point(115, 24);
			this.txtBase.Name = "txtBase";
			this.txtBase.Size = new System.Drawing.Size(264, 20);
			this.txtBase.TabIndex = 4;
			this.txtBase.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 24);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "Base Target Path";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(328, 72);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(408, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(498, 103);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Airlock Configuration Utility";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				txtBase.Text = ofd.SelectedPath;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			config.targetPath = txtBase.Text;
			config.writeConfig();
			Application.Exit();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
	}
}
