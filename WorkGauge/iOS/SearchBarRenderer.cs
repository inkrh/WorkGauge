using WorkGauge;
using WorkGauge.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]

namespace WorkGauge.iOS
{
	public class CustomSearchBarRenderer : SearchBarRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<SearchBar> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.ShowsCancelButton = false;
				Control.SearchBarStyle = UISearchBarStyle.Minimal;
				Control.BackgroundColor = UIColor.White;

			}
		}
	}
}

