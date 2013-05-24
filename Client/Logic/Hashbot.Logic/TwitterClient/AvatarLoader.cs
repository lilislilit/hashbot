using System;
using System.Net;
using System.IO;

namespace Hashbot.Logic
{
	public class AvatarLoader
	{
		private string _documentsPath;
		public Action<string,string> ImageDownloaded;



		public AvatarLoader()
		{
			_documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		public string GetAvatarByUri(string uri, string userId)
		{

			var localFilename = userId + ".png";
			var localPath = Path.Combine(_documentsPath, localFilename);

			if (!File.Exists(localPath))
			{
				using (var webClient = new WebClient())
				{
					webClient.DownloadDataAsync(new Uri(uri));
					webClient.DownloadDataCompleted += (sender, e) => {
						if (e.Error == null)
						{
							var bytes = e.Result;


							File.WriteAllBytes(localPath, bytes);
					
							ImageDownloaded(localPath, uri);
						}

					};

					return String.Empty;
				}
			} else
			{
				return localPath;
			}
			 
		}
	}
}

