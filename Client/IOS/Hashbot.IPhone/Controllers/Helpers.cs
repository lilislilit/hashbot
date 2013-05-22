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

		public static void SetBackground(this UIButton button, string path, int leftCap = 0, int topCap = 0)
		{
			var buttonState = UIControlState.Normal;
			BackgroundForState(button, path, leftCap, topCap, buttonState);


		}

		public static void SetSelectedBackground(this UIButton button, string path, int leftCap = 0, int topCap = 0)
		{
			var buttonState = UIControlState.Selected;
			BackgroundForState(button, path, leftCap, topCap, buttonState);
		}

		private static void BackgroundForState(UIButton button, string path, int leftCap, int topCap, UIControlState buttonState)
		{
			var backgroundImage = UIImage.FromFile(path);
			if (leftCap != 0 || topCap != 0)
			{
				var backgroundStretch = backgroundImage.StretchableImage(leftCap, topCap);
				button.SetBackgroundImage(backgroundStretch, buttonState);
			} else
			{
				button.SetBackgroundImage(backgroundImage, buttonState);
			}
		}
	}
}

	




