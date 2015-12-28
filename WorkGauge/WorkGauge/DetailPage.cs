using System;
using Xamarin.Forms;

namespace WorkGauge
{
	public class DetailPage : StackLayout
	{
		public Label IDLabel;
		public Label IDEntry;

		public Entry NameEntry;

		public Entry NotesEntry;

		public Entry SkillsEntry;

		public Entry PhoneEntry;

		public Entry EmailEntry;

		public Label CostLabel;
		public ScoreBox CostScoreBox;

		public Label QualityLabel;
		public ScoreBox QualityScoreBox;

		public Label TimeLabel;
		public ScoreBox TimeScoreBox;

		public Button IsBusy;

		public bool BusyBusy;

		public ScrollView ContentHolder;
		public StackLayout SubContent;

		public StackLayout IDStack;

		public DetailPage ()
		{
			MaxWidth = Constants.Width;
			IDStack = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			IDLabel = new Label {
				Text = "Record ID"
			};
			IDEntry = new Label ();

			NameEntry = new Entry {
				Placeholder="Name (required)"
			};
			NotesEntry = new Entry {
				Placeholder="Notes"
			};


			PhoneEntry = new Entry {
				Keyboard = Keyboard.Numeric,
				Placeholder="Phone"
			};

			EmailEntry = new Entry {
				Keyboard = Keyboard.Email,
				Placeholder = "Email"	
			};

			SkillsEntry = new Entry {
				Placeholder = "Key Skills (required)"
			};

			CostLabel = new Label {
				Text = "Value For Money"
			};
					

			CostScoreBox = new ScoreBox ();
			CostScoreBox.HeightRequest = 42;
			CostScoreBox.WidthRequest = MaxWidth;
			CostScoreBox.Value = 0;





			QualityLabel = new Label {
				Text = "Quality Of Work"
			};

			QualityScoreBox = new ScoreBox ();
			QualityScoreBox.HeightRequest = 42;
			QualityScoreBox.WidthRequest = MaxWidth;



			TimeLabel = new Label {
				Text = "Promptness Of Work"
			};

			TimeScoreBox = new ScoreBox ();
			TimeScoreBox.HeightRequest = 42;
			TimeScoreBox.WidthRequest = MaxWidth;



			IsBusy = new Button ();
			IsBusy.HorizontalOptions = LayoutOptions.Start;
			IsBusy.Clicked += (sender, e) => ChangeBusy ();
	

			NameEntry.TextChanged += (sender, e) => Constants.CurrentItem.Name = NameEntry.Text;

			NotesEntry.TextChanged += (sender, e) => Constants.CurrentItem.Notes = NotesEntry.Text;

			PhoneEntry.TextChanged += (sender, e) => {
				string temp = "";
				foreach (char a in PhoneEntry.Text) {
					if (char.IsNumber (a)) {
						temp = temp + a;
					}
				}
				PhoneEntry.Text = temp;

				Constants.CurrentItem.Phone = PhoneEntry.Text;
			};

			EmailEntry.TextChanged += (sender, e) => Constants.CurrentItem.Email = EmailEntry.Text;

			SkillsEntry.TextChanged += (sender, e) => Constants.CurrentItem.Skills = SkillsEntry.Text;

		
			SubContent = new StackLayout {
				Children = {
					IDLabel,
					IDEntry,
					NameEntry,
					PhoneEntry,
					EmailEntry,
					IsBusy,
					NotesEntry,
					SkillsEntry,
					CostLabel,
					CostScoreBox,
					QualityLabel,
					QualityScoreBox,
					TimeLabel,
					TimeScoreBox
				},
				Padding = new Thickness(0,0,0,20)
			};


			ContentHolder = new ScrollView ();
			ContentHolder.Padding = new Thickness (12, 20, 12, 0);
			ContentHolder.WidthRequest = MaxWidth;
			ContentHolder.HeightRequest = Constants.DataRowContentHeight;
			ContentHolder.Content = SubContent;


			Children.Add(ContentHolder);

			if (Constants.CurrentItem != null) {

				CostScoreBox.Value = Constants.CurrentItem.CostScore;
				QualityScoreBox.Value = Constants.CurrentItem.QualityScore;
				TimeScoreBox.Value = Constants.CurrentItem.TimeScore;
			}
		}

		public void Populate(AnItem item) {
			if (item == null) {
				Constants.CurrentItem = new AnItem ();
				NameEntry.Text = "";
				NotesEntry.Text = "";
				SkillsEntry.Text = "";
				CostScoreBox.Value = 0;
				QualityScoreBox.Value = 0;
				TimeScoreBox.Value = 0;
				Constants.CurrentItem.Busy = 0;
				IsBusy.Text = "Available";
				IsBusy.TextColor = Color.Blue;
			}

			CostScoreBox.Value = Constants.CurrentItem.CostScore;
			QualityScoreBox.Value = Constants.CurrentItem.QualityScore;
			TimeScoreBox.Value = Constants.CurrentItem.TimeScore;



			

		}

		public double MaxWidth { get; set; }



		public void ChangeBusy() {
			if (Constants.CurrentItem.Busy == 0) {
				Constants.CurrentItem.Busy = 1;
				IsBusy.Text = "Busy";
				IsBusy.TextColor = Color.Red;
			} else {
				Constants.CurrentItem.Busy = 0;
				IsBusy.Text = "Available";
				IsBusy.TextColor = Color.Blue;

			}
		}
	}
}


