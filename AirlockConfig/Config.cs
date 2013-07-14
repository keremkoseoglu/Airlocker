using System;
using Microsoft.Win32;

namespace AirlockConfig
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Config
	{
		public string targetPath;

		private const string REGPATH	= "Software\\Cozumevi\\Airlocker";
		private const string TARGETPATH = "targetPath";

		private Microsoft.Win32.RegistryKey rk1;
		private Microsoft.Win32.RegistryKey rk2;

		public Config()
		{
			//
			// TODO: Add constructor logic here
			//
			rk1 = Microsoft.Win32.Registry.LocalMachine;
			rk2 = rk1.OpenSubKey(REGPATH, true);
			if (rk2 == null)
			{
				rk1.CreateSubKey(REGPATH);
				rk2 = rk1.OpenSubKey(REGPATH, true);
				rk2.SetValue(TARGETPATH, "");
			}
		}

		public void readConfig()
		{
			targetPath = rk2.GetValue(TARGETPATH).ToString();
		}

		public void writeConfig()
		{
			rk2.SetValue(TARGETPATH, targetPath);
		}
	}
}
