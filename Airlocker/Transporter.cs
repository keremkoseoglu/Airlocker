using System;
using System.IO;

namespace Airlocker
{
	/// <summary>
	/// Summary description for Transporter.
	/// </summary>
	public class transporter
	{
		private const int MAXITEM = 10000;

		public delegate void transportEvent(object sender, transportEventArgs e);
		public event transportEvent itemAnalysed;
		public event transportEvent subItemDetermined;
		public event transportEvent itemCopied;
		public event transportEvent noParameter;
		public transportEventArgs tea;

		private bool deleteAfterCopy;
		private string[] item;
		private int itemCount;
		private int subItemCount;
		private string targetRoot;
		private AirlockConfig.Config config;

		private System.IO.FileInfo fi;
		private System.IO.DirectoryInfo di;


		public transporter()
		{
			//
			// TODO: Add constructor logic here
			//
			config			= new AirlockConfig.Config();
			config.readConfig();
			if (config.targetPath.Length <= 0) throw new ConfigException();

			tea				= new transportEventArgs();
			item			= new string[MAXITEM];
			itemCount		= 0;
			targetRoot		= config.targetPath + "\\" + System.Net.Dns.GetHostName() + "\\";
			
			deleteAfterCopy	= false;
		}

		public void analyseParameters(string[] args)
		{
			// First parameter must be -c or -d
			if (args.Length < 1) 
			{
				doNoParameter();
				return;
			}
				
			switch (args[0].ToLower())
			{
				case "-c":
					deleteAfterCopy = false;
					break;
				case "-m":
					deleteAfterCopy = true;
					break;
				default:
					doNoParameter();
					return;
					break;
			}

			// Other parameters...
			tea.max = args.Length;
			tea.pos = 1;
			
			for (int n = 1; n < args.Length; n++)
			{
				item[n - 1] = args[n];
				itemCount++;

				tea.pos++;
				itemAnalysed(this, tea);
			}
		}

		public void determineSubItems()
		{
			copyItems(true);
		}
		public void copyAllItems()
		{
			copyItems(false);
		}

		private void doNoParameter()
		{
			tea.max = 1;
			tea.pos = 1;
			tea.info = "First parameter must be -c or -m (Copy / Move)...";
			noParameter(this, tea);
		}

		private void copyItems(bool CountOnly)
		{
			string curItem;		
			string curChar;		
			string curSou;	

			if (CountOnly)
			{
				tea.max			= 1;
				tea.pos			= 0;
				subItemCount	= 0;
			}
			else
			{
				tea.max			= subItemCount;
				tea.pos			= 0;
			}

			for (int n = 0; n < itemCount; n++)
			{
				curItem = item[n];
				curSou = "";
				// Create first letter (drive) as folder
				createDirectory(curItem.Substring(0, 1), CountOnly);
				// Create subdirectories in path
				for (int m = 0; m < curItem.Length; m++)
				{
					curChar = curItem.Substring(m, 1);
					curSou += curChar;
					if (isSeparator(curChar))
					{
						di = new DirectoryInfo(curSou);
						if (di.Exists) createDirectory(curSou, CountOnly);
					}
				}
				// If item is file, simply copy the file
				fi = new FileInfo(curItem);
				if (fi.Exists)
				{
					copyFile(curItem, CountOnly);
					if (deleteAfterCopy && !CountOnly) fi.Delete();
				}

				// If item is directory, copy it, all files and subdirectories
				di = new DirectoryInfo(curItem);
				if (di.Exists) 
				{
					copyDirTree(curItem, CountOnly);
					if (deleteAfterCopy && !CountOnly) di.Delete(true);
				}

			}
		}

		private bool isSeparator(string Character)
		{
			if (Character == ":"	||
				Character == "\\"	||
				Character == "/") return true;
			else return false;
		}

		private void createDirectory(string Source, bool CountOnly)
		{
			string newDir;
			DirectoryInfo ldi;

			if (CountOnly)
			{
				subItemCount++;
				tea.info = Source;
				subItemDetermined(this, tea);
			}
			else
			{
				newDir = targetRoot + Source.Replace(":", "");
				ldi = new DirectoryInfo(newDir);
				if (!ldi.Exists) ldi.Create();
				tea.pos++;
				tea.info = Source;
				itemCopied(this, tea);
			}
		}

		private void copyFile(string Source, bool CountOnly)
		{
			string newFile;

			if (CountOnly)
			{
				subItemCount++;
				tea.info = Source;
				subItemDetermined(this, tea);
			}
			else
			{
				newFile = targetRoot + Source.Replace(":", "");
				FileInfo lfi = new FileInfo(Source);
                FileInfo nfi = new FileInfo(newFile);
                if (lfi.LastWriteTime > nfi.LastWriteTime)
                {
                    try
                    {
                        lfi.CopyTo(newFile, true);
                    }
                    catch { }
                }
				tea.pos++;
				tea.info = Source;
				itemCopied(this, tea);
			}
		}

	
		private void copyDirTree(string Folder, bool CountOnly)
		{
			DirectoryInfo ldi = new DirectoryInfo(Folder);

			createDirectory(Folder, CountOnly);

			foreach(DirectoryInfo ldi2 in ldi.GetDirectories())
			{
				copyDirTree(ldi2.FullName, CountOnly);
			}

			foreach(FileInfo lfi in ldi.GetFiles())
			{
				copyFile(lfi.FullName, CountOnly);
			}
			
		}
	}
}
