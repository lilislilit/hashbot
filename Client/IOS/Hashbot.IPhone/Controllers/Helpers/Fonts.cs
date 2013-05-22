using System;
using MonoTouch.UIKit;

namespace Hashbot.IPhone
{
	public static class Fonts
	{
		private const string _helvetica = "HelveticaNeue";
		private const string _helveticaBold = "HelveticaNeue-Bold";

		public static UIFont HelveticaBold(int size, bool bold=false)
		{

			return UIFont.FromName(_helveticaBold, size);
		}

		public static UIFont Helvetica(int size)
		{
			return UIFont.FromName(_helvetica, size);
		}
	}
}

