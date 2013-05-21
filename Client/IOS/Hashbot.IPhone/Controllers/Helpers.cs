using System;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using System.Drawing;
using MonoTouch.Foundation;

namespace Hashbot.IPhone
{
	public static class Helpers
	{
		public static string FileById(string MessageId)
		{
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string localFilename = MessageId + ".png";
			string localPath = Path.Combine(documentsPath, localFilename);
			return localPath;
		}

		public static UIImage GetMaskedAvatar(this UIImage imageToMask, UIImage maskImage)
		{
			CGImage mask = CGImage.CreateMask((int)maskImage.Size.Width, (int)maskImage.Size.Height,
			                                  maskImage.CGImage.BitsPerComponent, maskImage.CGImage.BitsPerPixel,
			                                  maskImage.CGImage.BytesPerRow, maskImage.CGImage.DataProvider, null, false);

			return UIImage.FromImage(imageToMask.CGImage.WithMask(mask));
		}
	}
}

	




