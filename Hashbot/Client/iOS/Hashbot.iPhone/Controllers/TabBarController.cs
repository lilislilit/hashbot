using System;
using MonoTouch.UIKit;

namespace Hashbot.iPhone
{
	public class TabBarController :UITabBarController{
		
		HashTagController TwitterTab, AppleTab, DribbleTab,GitHubTab;
		InfoController info;
		public TabBarController()
		{

			TwitterTab = new HashTagController();
		    TwitterTab.Title = TwitterTab.HashTag = "Twitter";

			AppleTab = new HashTagController();
			AppleTab.Title = AppleTab.HashTag = "Apple";

			DribbleTab = new HashTagController();
			DribbleTab.Title = DribbleTab.HashTag = "Dribble";

			GitHubTab = new HashTagController();
			GitHubTab.Title = GitHubTab.HashTag = "GitHub";
		    var tabs = new UIViewController[] {
				TwitterTab,DribbleTab, AppleTab, GitHubTab
			};
			NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Action,
				RightBarButtonHandler),true);

			ViewControllers = tabs;
			Title = SelectedViewController.Title;

	}
	
	private void RightBarButtonHandler(object sender,EventArgs args)
		{
			if(info==null) info = new InfoController();
			NavigationController.PushViewController(info,true);
	    }
	}
}


