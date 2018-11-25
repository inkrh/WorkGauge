using System;

using Xamarin.Forms;

namespace WorkGauge
{
	public class ContactCell : ViewCell
	{
		public ContactHelper thisCH;
		public Grid cellGrid;
		public Label NameLabel;

		public ContactCell (ContactHelper item)
		{
			
			thisCH = item;

			cellGrid = new Grid {
				ColumnDefinitions = {
					new ColumnDefinition {
						Width = new GridLength(8, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength(1, GridUnitType.Star)
					}},
				RowDefinitions = {
					new RowDefinition {
						Height = new GridLength(1, GridUnitType.Star)
					}},
			};
			NameLabel = new Label {
                Text = thisCH.Name,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			Grid.SetColumn (NameLabel, 0);
			cellGrid.Children.Add (NameLabel);

			View = cellGrid;

			Tapped += RowTapped;
		}

		public void RowTapped(object sender, EventArgs e) {
			if (thisCH != null) {
				Constants.CurrentItem = null;
				Constants.CurrentItem = new AnItem {
                    Name = thisCH.Name,
					Email = thisCH.Email,
					Phone = thisCH.Phone
				};
				MessagingCenter.Send<string, AnItem> ("cell", "CONTACTTAPPED", Constants.CurrentItem);

			}
		}
	}
}


