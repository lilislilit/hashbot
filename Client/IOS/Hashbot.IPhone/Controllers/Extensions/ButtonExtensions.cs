using System;
using MonoTouch.UIKit;

namespace Hashbot.IPhone.ButtonExtensions
{
	public static class ButtonExtensions
	{
		public static void SetBackground(this UIButton button, string path, int? leftCap = null, int? topCap = null)
		{
			var buttonState = UIControlState.Normal;
			BackgroundForState(button, path, leftCap, topCap, buttonState);


		}

		public static void SetSelectedBackground(this UIButton button, string path, int? leftCap = null, int? topCap = null)
		{
			var buttonState = UIControlState.Highlighted;
			BackgroundForState(button, path, leftCap, topCap, buttonState);
		}

		private static void BackgroundForState(UIButton button, string path, int? leftCap, int? topCap, UIControlState buttonState)
		{
			var backgroundImage = UIImage.FromFile(path);
			if (leftCap.HasValue || topCap.HasValue)
			{
				var backgroundStretch = backgroundImage.StretchableImage(leftCap ?? 0, topCap ?? 0);
				button.SetBackgroundImage(backgroundStretch, buttonState);
			} else
			{
				button.SetBackgroundImage(backgroundImage, buttonState);
			}
		}
	}
}

