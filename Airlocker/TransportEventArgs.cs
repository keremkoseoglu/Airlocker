using System;

namespace Airlocker
{
	/// <summary>
	/// Summary description for TransportEventArgs.
	/// </summary>
	public class transportEventArgs: System.EventArgs
	{
		public int max;
		public int pos;
		public string info;

		public transportEventArgs()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
