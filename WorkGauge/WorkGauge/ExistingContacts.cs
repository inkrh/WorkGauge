using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Contacts.Plugin.Abstractions;
using Contacts.Plugin;
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

		public async void PopulateContacts() {
			if (await CrossContacts.Current.RequestPermission ()) {
				rootList = new List<ContactHelper>();
				itemList = new List<ContactHelper> ();

				try {
					List<Contact> contacts = null;
					CrossContacts.Current.PreferContactAggregation = false;
					await Task.Run (() => {
						if (CrossContacts.Current.Contacts == null)
							return;
						contacts = CrossContacts.Current.Contacts
						.Where (c => !string.IsNullOrWhiteSpace (c.LastName) || !string.IsNullOrWhiteSpace (c.FirstName)).ToList ();
						contacts = contacts.OrderBy (c => c.LastName).ToList ();
					});

					foreach (Contact item in contacts) {
						var temp = new ContactHelper ();
						if (null != item.FirstName) {
							temp.FirstName = item.FirstName;
						}
						if (null != item.LastName) {
							temp.LastName = item.LastName;
						}
						if (null == item.FirstName && null == item.LastName && null != item.DisplayName) {
							temp.FirstName = item.DisplayName;
						}
						if (null == item.FirstName && null == item.LastName && null == item.DisplayName && null != item.Nickname) {
							temp.FirstName = item.Nickname;
						}
						if (item.Emails.Count > 0) {
							Dictionary<EmailType, String> tempEmail = new Dictionary<EmailType, string>();
							foreach(var a in item.Emails) {
								if(!tempEmail.ContainsKey(a.Type)) {
									tempEmail.Add(a.Type, a.Address);
								} else {
									tempEmail[a.Type] = a.Address;
								}
							}
							if(tempEmail.ContainsKey(EmailType.Work)) {
								temp.Email = tempEmail[EmailType.Work];
							} else if(tempEmail.ContainsKey(EmailType.Home)) {
								temp.Email = tempEmail[EmailType.Home];
							} else if(tempEmail.ContainsKey(EmailType.Other)) {
								temp.Email = tempEmail[EmailType.Other];
							}
							tempEmail = null;
						}

						if (item.Phones.Count > 0) {
							Dictionary<PhoneType, String> tempPhone = new Dictionary<PhoneType, string>();
							foreach(var a in item.Phones) {
								if(!tempPhone.ContainsKey(a.Type)) {
									tempPhone.Add(a.Type, a.Number);
								} else {
									tempPhone[a.Type] = a.Number;
								}
							}
							if(tempPhone.ContainsKey(PhoneType.Work)) {
								temp.Phone = tempPhone[PhoneType.Work];
							} else if(tempPhone.ContainsKey(PhoneType.Mobile)) {
								temp.Phone = tempPhone[PhoneType.Mobile];
							} else if(tempPhone.ContainsKey(PhoneType.Home)) {
								temp.Phone = tempPhone[PhoneType.Home];
							} else if(tempPhone.ContainsKey(PhoneType.Other)) {
								temp.Phone = tempPhone[PhoneType.Other];
							}
							tempPhone = null;
						}
						itemList.Add (temp);
						rootList.Add (temp);
					}
				} catch (Exception e) {
					Debug.WriteLine ("Populate Contacts Exception " + e.Message);
				}
			}
			PopulateTable ();
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
				if (SearchBox.Text != "" && SearchBox.Text != String.Empty && SearchBox.Text != null) {
					if (null != i.FirstName && i.FirstName.ToLower().Contains (SearchBox.Text.ToLower())) {
						itemList.Add (i);
					} else if (null != i.LastName && i.LastName.ToLower().Contains (SearchBox.Text.ToLower())) {
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

				tempSection.Add(new ContactCell(item));
			}

			temp.Add(tempSection);
			return temp;
		}
	}
}


