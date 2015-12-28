using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WorkGauge
{
	public class WorkGaugeHome : StackLayout
	{

		public CustomSearchBar SearchBox;
		public TableView ResultsTable = new TableView ();
		public ScrollView ContentHolder;
		public StackLayout SubContent;
		public ItemDatabase db = new ItemDatabase();
		public List<AnItem> itemList;
		BoxView line;

		public WorkGaugeHome ()
		{
			this.HeightRequest = Constants.DataRowContentHeight;
			line = new BoxView {
				WidthRequest = Constants.Width - 12,
				HeightRequest = 1,
				Color = Color.Gray,
				Opacity = 0.5
			};



			SearchBox = new CustomSearchBar {
				Placeholder = "Search Names or Skills"
			};

			SearchBox.TextChanged += (sender, e) => PopulateTable ();
			SearchBox.SearchButtonPressed += (sender, e) => PopulateTable();

			ResultsTable.HeightRequest = Constants.ResultsTableHeight;
			itemList = new List<AnItem> ();
			PopulateTable ();
			SubContent = new StackLayout {
				Children = {
					SearchBox,
					line,
					ResultsTable
				}};

			ContentHolder = new ScrollView ();
			ContentHolder.Padding = new Thickness (12,0, 12, 20);
			ContentHolder.Content = SubContent;
			ContentHolder.HeightRequest = Constants.DataRowContentHeight;


			Children.Add(ContentHolder);

		}


		public void PopulateTable() {
			if (null != ContentHolder) {
				ContentHolder.HeightRequest = Constants.DataRowContentHeight;
			}


			itemList.Clear ();
			var a = db.GetItems ();
			foreach (var i in a) {

				if (SearchBox.Text != "" && SearchBox.Text != String.Empty && SearchBox.Text != null) {
					if (null != i.Name && i.Name.ToLower().Contains (SearchBox.Text.ToLower())) {
						itemList.Add (i);
					} else if (null != i.Skills && i.Skills.ToLower().Contains (SearchBox.Text.ToLower())) {
						itemList.Add (i);
					}
				} 
				else {
					itemList.Add (i);
				}

			}
					
				

			ResultsTable.HeightRequest = (itemList.Count) * 42;

			if (null != itemList)
			{
				ResultsTable.Intent = TableIntent.Data;
				ResultsTable.Root = ResultsTableRoot();
				ResultsTable.HasUnevenRows = true;
			}
		}

		TableRoot ResultsTableRoot()
		{
			var temp = new TableRoot();
			var tempSection = new TableSection();
			foreach (var item in itemList)
			{
				
				tempSection.Add(new AnItemCell(item));
			}

			temp.Add(tempSection);
			return temp;
		}
	}
}


