// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Hashbot.IPhone
{
	[Register ("HashTagController")]
	partial class HashTagController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblTag { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblTag != null) {
				lblTag.Dispose ();
				lblTag = null;
			}
		}
	}
}
