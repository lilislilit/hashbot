using System;
using MonoTouch.UIKit;

namespace Hashbot.IPhone
{
	public class TabBarController :UITabBarController
	{
		
		HashTagController TwitterTab, AppleTab, DribbleTab, GitHubTab;
		InfoController info;

		public TabBarController()
		{

			TwitterTab = new HashTagController();
			TwitterTab.Title = TwitterTab.HashTag = "Twitter";
			TwitterTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_twitter.png");

			AppleTab = new HashTagController();
			AppleTab.Title = AppleTab.HashTag = "Apple";
			AppleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_apple.png");

			DribbleTab = new HashTagController();
			DribbleTab.Title = DribbleTab.HashTag = "Dribble";
			DribbleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_dribbble.png");

			GitHubTab = new HashTagController();
			GitHubTab.Title = GitHubTab.HashTag = "GitHub";
			GitHubTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_github.png");

			var tabs = new UIViewController[] {
				TwitterTab, DribbleTab, AppleTab, GitHubTab
			};
			var infoButton = new UIBarButtonItem("Инфо", UIBarButtonItemStyle.Plain, RightBarButtonHandler);
			NavigationItem.SetRightBarButtonItem(infoButton, false);
			ViewControllers = tabs;
			NavigationItem.Title = SelectedViewController.Title;

		}

		private void RightBarButtonHandler(object sender, EventArgs args)
		{
			if (info == null)
				info = new InfoController();
			NavigationController.PushViewController(info, true);
		}
	}
}


