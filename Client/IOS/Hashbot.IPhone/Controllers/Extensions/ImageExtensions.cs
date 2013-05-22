using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace Hashbot.IPhone.ImageExtensions
{
	public static class ImageExtensions
	{
		public static UIImage GetMaskedAvatar(this UIImage imageToMask, UIImage maskImage)
		{
			CGImage mask = CGImage.CreateMask((int)maskImage.Size.Width, (int)maskImage.Size.Height,
			                                  maskImage.CGImage.BitsPerComponent, maskImage.CGImage.BitsPerPixel,
			                                  maskImage.CGImage.BytesPerRow, maskImage.CGImage.DataProvider, null, false);

			return UIImage.FromImage(imageToMask.CGImage.WithMask(mask));
		}
	}
}

