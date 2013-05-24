using System;
using System.Drawing;

namespace Hashbot.IPhone
{
	public static class RectangleExtensions
	{
		public static RectangleF CenterHorizontIn(this RectangleF rectangle, RectangleF outerRectangle,int topMargin=0)
		{
			return new RectangleF((outerRectangle.Width-rectangle.Width)/2,topMargin,rectangle.Width, rectangle.Height);

		}

	}
}

