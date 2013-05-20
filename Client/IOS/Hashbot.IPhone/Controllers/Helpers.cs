using System;
using System.IO;
namespace Hashbot.IPhone
{
	public static class Helpers
	{
		public static string FileById(string MessageId)
		{
			string documentsPath =Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string localFilename = MessageId +".png";
			string localPath = Path.Combine (documentsPath, localFilename);
			return localPath;
		}
	}
}

