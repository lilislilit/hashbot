using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Hashbot.IPhone
{
	public class TabBarController :UITabBarController
	{
		
		HashTagController TwitterTab, AppleTab, DribbleTab, GitHubTab;
		UINavigationController NCTwitter,NCApple,NCDribble, NCGithub; // _twitterNavControlle


		public TabBarController()
		{
			NCTwitter= new UINavigationController();
			TwitterTab = new HashTagController();
			TwitterTab.Title = TwitterTab.HashTag = "Twitter";
			TwitterTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_twitter.png");
			NCTwitter.PushViewController(TwitterTab, false);

			NCApple = new UINavigationController();
			AppleTab = new HashTagController();
			AppleTab.Title = AppleTab.HashTag = "Apple";
			AppleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_apple.png");
			NCApple.PushViewController(AppleTab, false);

			NCDribble = new UINavigationController();
			DribbleTab = new HashTagController();
			DribbleTab.Title = DribbleTab.HashTag = "Dribble";
			DribbleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_dribbble.png");
			NCDribble.PushViewController(DribbleTab, false);

			NCGithub = new UINavigationController();
			GitHubTab = new HashTagController();
			GitHubTab.Title = GitHubTab.HashTag = "GitHub";
			GitHubTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_github.png");
		     NCGithub.PushViewController(GitHubTab, false);

			var tabs = new UIViewController[] {
			  NCTwitter,NCApple,NCDribble,NCGithub
			};
			HidesBottomBarWhenPushed = true;
			ViewControllers = tabs;
			NavigationItem.Title = SelectedViewController.Title;

		}

		}


	}



