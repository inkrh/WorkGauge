using System;
using System.Diagnostics;

using Xamarin.Forms;

namespace WorkGauge
{
	public class AnItemCell : ViewCell
	{
		public AnItem thisItem;
		public Grid cellGrid;
		public Label NameLabel;
		public BoxView ScoreLabel;
		public MenuItem DeleteButton;
		public MenuItem EmailButton;

		public AnItemCell (AnItem item)
		{
			thisItem = item;
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
			Color textColor;

			textColor = thisItem.Busy == 0 ? Color.Black : Color.Red;


			NameLabel = new Label {
				Text = thisItem.Name + " - " + thisItem.Skills,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = textColor
			};


			int i = thisItem.CostScore + thisItem.QualityScore + thisItem.TimeScore;

			Color tempColor;

			if(i>=4) {
				tempColor = Color.Green;
			} else if (i<4 && i>=2) {
				tempColor = Color.Yellow;
			} else {
				tempColor = Color.Red;
			}

			ScoreLabel = new BoxView {
				HeightRequest = 20,
				WidthRequest = 20,
				Color = tempColor,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.Center
			};

		

			Grid.SetColumn (NameLabel, 0);
			Grid.SetColumn (ScoreLabel, 1);
		
			cellGrid.Children.Add (NameLabel);
			cellGrid.Children.Add (ScoreLabel);


			DeleteButton = new MenuItem ();
			DeleteButton.Text = "Delete";
			DeleteButton.IsDestructive = true;
			DeleteButton.Clicked += DeleteClicked;
			ContextActions.Add (DeleteButton);

			if (!string.IsNullOrEmpty (thisItem.Email) && thisItem.Email != string.Empty &&
			    thisItem.Email.Length > 6) {
				EmailButton = new MenuItem ();
				EmailButton.Text = "Email";
				EmailButton.IsDestructive = false;
				EmailButton.Clicked += EmailClicked;
				ContextActions.Add (EmailButton);
			}
//			 else if (!string.IsNullOrEmpty (thisItem.Phone) && thisItem.Phone.Length > 7) {
//				EmailButton = new MenuItem ();
//				EmailButton.Text = "Phone";
//				EmailButton.IsDestructive = false;
//				EmailButton.Clicked += PhoneClicked;
//				ContextActions.Add (EmailButton);
//			}

			Tapped += RowTapped;


			View = cellGrid;
		}

		public void RowTapped(object sender, EventArgs e) {
			if (thisItem != null) {
				
				Constants.CurrentItem = thisItem;

				Debug.WriteLine ("Tapped : " + Constants.CurrentItem.Name);
				MessagingCenter.Send<string, AnItem> ("cell", "CELLTAPPED", Constants.CurrentItem);
			}
		}

		public void DeleteClicked(object sender, EventArgs e) {
			if (thisItem != null) {
				Constants.CurrentItem = thisItem;
				Debug.WriteLine ("Delete Tapped : " + Constants.CurrentItem.Name);
				MessagingCenter.Send<string, AnItem> ("cell", "DELETE", Constants.CurrentItem);

			}
		}

		public void EmailClicked(object sender, EventArgs e) {
			if (thisItem != null) {
				Constants.CurrentItem = thisItem;
				Debug.WriteLine ("Email Tapped : " + Constants.CurrentItem.Email);
				MessagingCenter.Send<string, AnItem> ("cell", "EMAIL", Constants.CurrentItem);

			}
		}

		public void PhoneClicked(object sender, EventArgs e) {
			if (thisItem != null) {
				Constants.CurrentItem = thisItem;
				Debug.WriteLine ("Phone Tapped : " + Constants.CurrentItem.Phone);
			}
		}
	}
}


