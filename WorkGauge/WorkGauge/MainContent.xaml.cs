using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkGauge
{
	public partial class MainContent : ContentPage
	{
		public ItemDatabase db = new ItemDatabase();
		public DetailPage DetailContent = new DetailPage();
		public WorkGaugeHome Resultscontent = new WorkGaugeHome();
		public ExistingContacts ExistingContactContet = new ExistingContacts();
		public bool Results;

		public MainContent ()
		{
			InitializeComponent ();
			//repeated because Xamarin.Forms
			Constants.Height = Acr.DeviceInfo.DeviceInfo.Instance.ScreenHeight;
			Constants.Width = Acr.DeviceInfo.DeviceInfo.Instance.ScreenWidth;
			//set row heights
			icon.Height = Constants.TopRowHeight;
			buttons.Height = Constants.BottomButtonRowHeight;
			data.Height = Constants.DataRowContentHeight;

			Resultscontent.HeightRequest = Constants.DataRowContentHeight;
			Resultscontent.ResultsTable.HeightRequest = Constants.ResultsTableHeight;
			DetailContent.HeightRequest = Constants.DataRowContentHeight;
			ExistingContactContet.HeightRequest = Constants.DataRowContentHeight;
			ExistingContactContet.ResultsTable.HeightRequest = Constants.ResultsTableHeight;

			MessagingCenter.Subscribe<string, AnItem> (this, "CELLTAPPED", (sender, args) => {
				Debug.WriteLine("CellTapped : " + Constants.CurrentItem.Name + " CostScore : " + Constants.CurrentItem.CostScore);
				HandleSubmit("Cell");	
			});

			MessagingCenter.Subscribe<string, AnItem> (this, "DELETE", (sender, args) => {
				Debug.WriteLine("Delete : " + Constants.CurrentItem.Name + " CostScore : " + Constants.CurrentItem.CostScore);
				HandleDelete();	
			});

			MessagingCenter.Subscribe<string, AnItem> (this, "EMAIL", (sender, args) => {
				Debug.WriteLine("Email : " + Constants.CurrentItem.Name + " Email : " + Constants.CurrentItem.Email);
				HandleEmail();	
			});

			MessagingCenter.Subscribe<string, AnItem> (this, "PHONE", (sender, args) => {
				Debug.WriteLine("Phone : " + Constants.CurrentItem.Name + " Number : " + Constants.CurrentItem.Phone);
				HandlePhone();	
			});

			MessagingCenter.Subscribe<string, AnItem> (this, "CONTACTTAPPED", (sender, args) => {
				Debug.WriteLine("Name : " + Constants.CurrentItem.Name);
				HandleNew();
			});


			MainSLO.Children.Add (Resultscontent); //(ResultsContent);
			Resultscontent.PopulateTable();
			Results = true;
			Submit.IsVisible=true;
			Submit.Text = "Add New"; 
			Submit.TextColor = Color.Blue;
			Submit.Clicked += (sender, e) => HandleSubmit ((sender as Button).Text);

			Cancel.IsVisible = false;
			Cancel.Text = "Back";
			Cancel.TextColor = Color.Red;
			Cancel.Clicked += (sender, e) => HandleCancel ();
		}
		
		public void HandleCancel() {
			Results = true;
			Constants.CurrentItem = null;
			MainSLO.Children.Remove (DetailContent);
			MainSLO.Children.Remove (ExistingContactContet);
			MainSLO.Children.Add (Resultscontent);
			Submit.Text = "Add New";
			Cancel.IsVisible = false;
			Resultscontent.PopulateTable ();
		}

		public void HandleNew() {
			MainSLO.Children.Remove (ExistingContactContet);
			DetailContent = null;
			DetailContent = new DetailPage ();
			DetailContent.Populate (Constants.CurrentItem);
			PopulateDetail ();
			MainSLO.Children.Add (DetailContent);
			Results = false;
			Submit.Text = "Save";
			Cancel.IsVisible = true;

		}

		public async void HandleSubmit(string cmd) {
			if (cmd == "Save") {
				if (string.IsNullOrEmpty (Constants.CurrentItem.Name) || Constants.CurrentItem.Name == String.Empty
				    || Constants.CurrentItem.Skills == null || Constants.CurrentItem.Skills == "" || Constants.CurrentItem.Skills == String.Empty) {
					await MessageBox ("Please enter a name and a key skill", "Missing Information", "Ok", null);
					return;
				}
				if (Constants.CurrentItem != null) {
					//update these just in case
					Constants.CurrentItem.CostScore = DetailContent.CostScoreBox.Value;
					Constants.CurrentItem.QualityScore = DetailContent.QualityScoreBox.Value;
					Constants.CurrentItem.TimeScore = DetailContent.TimeScoreBox.Value;

					db.SaveItem (Constants.CurrentItem);
				}
				Constants.CurrentItem = null;

				MainSLO.Children.Remove (DetailContent);
				MainSLO.Children.Add (Resultscontent);
				Resultscontent.PopulateTable ();
				Submit.Text = "Add New";
				Cancel.IsVisible = false;
				Results = true;
			} else if (cmd == "Add New") {
				MainSLO.Children.Remove (Resultscontent);
				Constants.CurrentItem = null;
				MainSLO.Children.Add (ExistingContactContet);
				ExistingContactContet.PopulateTable ();
				ExistingContactContet.PopulateContacts ();
				Submit.Text = "New";
				Cancel.IsVisible = true;
				Results = false;
			} else if (cmd == "New") {
				Constants.CurrentItem = null;
				HandleNew ();
			} else if (cmd == "Cell") {
				MainSLO.Children.Remove (Resultscontent);
				DetailContent = null;
				DetailContent = new DetailPage ();
				DetailContent.HeightRequest = Constants.DataRowContentHeight;
				DetailContent.Populate (Constants.CurrentItem);
				//repeated because Xamarin.Forms
				PopulateDetail ();

				MainSLO.Children.Add (DetailContent);
				Submit.Text = "Save";
				Cancel.IsVisible = true;
				Results = false;
			}
		}

		public void PopulateDetail() {
			DetailContent.IDEntry.Text = Constants.CurrentItem.ID.ToString();
			DetailContent.NameEntry.Text = Constants.CurrentItem.Name;
			DetailContent.NotesEntry.Text = Constants.CurrentItem.Notes;
			DetailContent.PhoneEntry.Text = Constants.CurrentItem.Phone;
			DetailContent.EmailEntry.Text = Constants.CurrentItem.Email;
			DetailContent.SkillsEntry.Text = Constants.CurrentItem.Skills;
			DetailContent.CostScoreBox.Value = Constants.CurrentItem.CostScore;
			DetailContent.QualityScoreBox.Value = Constants.CurrentItem.QualityScore;
			DetailContent.TimeScoreBox.Value = Constants.CurrentItem.TimeScore;

			if (Constants.CurrentItem.Busy != 0) {
				DetailContent.IsBusy.Text = "Busy";
				DetailContent.IsBusy.TextColor = Color.Red;
			} else {
				DetailContent.IsBusy.Text = "Available";
				DetailContent.IsBusy.TextColor = Color.Blue;
			}
			//repeated because bloody XF doesn't update controls properly
		}

		public void HandleDelete() {
			Resultscontent.db.DeleteItem (Constants.CurrentItem.ID);
			Constants.CurrentItem = null;
			Resultscontent.PopulateTable ();
		}

		public async void HandleEmail() {
			Device.OpenUri(new Uri("mailto:"+Constants.CurrentItem.Email));
			Constants.CurrentItem = null;
		}


		public async void HandlePhone() {
			var dialer = DependencyService.Get<IDialer>();
			if (dialer != null) {
				dialer.Dial (Constants.CurrentItem.Phone);
			}
			Constants.CurrentItem = null;
		}


		async Task<string> MessageBox(string text, string title, string option1, string option2) {
			bool answer = true;
			if(null == option2) {
				await DisplayAlert (title, text, option1);
			} else {
				//options shown right to left
				answer = await DisplayAlert (title, text, option2, option1);

			}

			return answer ? option2 : option1;


		}
	}
}

