using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Hashbot.IPhone
{
	public class TabBarController :UITabBarController
	{
		
		private HashTagController _twitterTab;
		private HashTagController _appleTab;
		private HashTagController _dribbleTab;
		private HashTagController _gitHubTab;

		private UINavigationController _twitterNavController;
		private UINavigationController _appleNavController;
		private UINavigationController _dribbleNavController;
		private UINavigationController _githubNavController; 

		public TabBarController()
		{
			_twitterNavController= new UINavigationController();
			_twitterTab = new HashTagController();
			_twitterTab.Title = _twitterTab.HashTag = "#Twitter";
			_twitterTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_twitter.png");
			_twitterNavController.PushViewController(_twitterTab, false);

			_appleNavController = new UINavigationController();
			_appleTab = new HashTagController();
			_appleTab.Title = _appleTab.HashTag = "#Apple";
			_appleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_apple.png");
			_appleNavController.PushViewController(_appleTab, false);

			_dribbleNavController = new UINavigationController();
			_dribbleTab = new HashTagController();
			_dribbleTab.Title = _dribbleTab.HashTag = "#Dribble";
			_dribbleTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_dribbble.png");
			_dribbleNavController.PushViewController(_dribbleTab, false);

			_githubNavController = new UINavigationController();
			_gitHubTab = new HashTagController();
			_gitHubTab.Title = _gitHubTab.HashTag = "#GitHub";
			_gitHubTab.TabBarItem.Image = UIImage.FromFile("ios/TabBar/icon_github.png");
		    _githubNavController.PushViewController(_gitHubTab, false);

			HidesBottomBarWhenPushed = true;
			ViewControllers = new UIViewController[] {
				_twitterNavController,_appleNavController,_dribbleNavController,_githubNavController
			};

			NavigationItem.Title = SelectedViewController.Title;

		}

		}


	}



