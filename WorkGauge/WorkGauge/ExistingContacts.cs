using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.ContactService;
//using Contacts.Plugin.Abstractions;
//using Contacts.Plugin;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace WorkGauge
{
	public class ExistingContacts : StackLayout
	{
		public CustomSearchBar SearchBox;
		public TableView ResultsTable = new TableView ();
		public ScrollView ContentHolder;
		public StackLayout SubContent;
		public List<ContactHelper> rootList;
		public List<ContactHelper> itemList;
		BoxView line;

		public ExistingContacts ()
		{
			line = new BoxView {
				WidthRequest = Constants.Width - 12,
				HeightRequest = 1,
				Color = Color.Gray,
				Opacity = 0.5
			};
			SearchBox = new CustomSearchBar {
				Placeholder = "Search names"
			};

			SearchBox.TextChanged += (sender, e) => PopulateTable ();
			SearchBox.SearchButtonPressed += (sender, e) => PopulateTable();

			ResultsTable.HeightRequest = Constants.ResultsTableHeight;
			itemList = new List<ContactHelper> ();
			rootList = new List<ContactHelper> ();
			SubContent = new StackLayout {
				Children = {
					SearchBox,
					line,
					ResultsTable
				}};

			ContentHolder = new ScrollView ();
			ContentHolder.Padding = new Thickness (12, 0, 12, 20);
			ContentHolder.Content = SubContent;
			ContentHolder.HeightRequest = Constants.DataRowContentHeight;


			Children.Add(ContentHolder);
		}

        public async void PopulateContacts()
        {
            IList<Plugin.ContactService.Shared.Contact> contacts;
            contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
            if (null == contacts) { return; }
            rootList = new List<ContactHelper>();
            itemList = new List<ContactHelper>();
            foreach (var item in contacts)
            {
                var temp = new ContactHelper();
                if (null != item.Name)
                {
                    temp.Name = item.Name;
                }
                if (null != item.Email)
                {
                    temp.Email = item.Email;
                }
                if (null != item.Number)
                {
                    temp.Phone = item.Number;
                }
                itemList.Add(temp);
                rootList.Add(temp);
            }
            PopulateTable();

        }

		public void PopulateTable() {
			if (null != ContentHolder) {
				ContentHolder.HeightRequest = Constants.DataRowContentHeight;
			}

			if (null == rootList || rootList.Count <= 0 || null == ResultsTable) {
				return;
			}
			itemList.Clear ();
			foreach (var i in rootList) {
                if (SearchBox.Text != "" && SearchBox.Text != String.Empty && SearchBox.Text != null)
                {
                    if (null != i.Name && i.Name.ToLower().Contains(SearchBox.Text.ToLower()))
                    {
                        itemList.Add(i);
                    }
                }
                else
                {
                    itemList.Add(i);
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

				tempSection.Add(new ContactCell(item));
			}

			temp.Add(tempSection);
			return temp;
		}
	}
}


