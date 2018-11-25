using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace WorkGauge
{
	public class ScoreBox : StackLayout
	{
		private BoxView GreenBox;
		private BoxView YellowBox;
		private BoxView RedBox;
		private TapGestureRecognizer gbTap;
		private TapGestureRecognizer ybTap;
		private TapGestureRecognizer rbTap;

		private int _value;

		public int Value { 
			get {
				return(_value);
			} 
			set {
				_value = value;
				OrganizeBoxes (null);
			}
		}

        private double BoxSize => (this.Width - 24) / 3;

        public ScoreBox ()
		{


            gbTap = new TapGestureRecognizer ();
			gbTap.Tapped += (object sender, EventArgs e) => {
				OrganizeBoxes(GreenBox);
			};

			ybTap = new TapGestureRecognizer ();
			ybTap.Tapped += (object sender, EventArgs e) => {
				OrganizeBoxes(YellowBox);
			};

			rbTap = new TapGestureRecognizer ();
			rbTap.Tapped += (object sender, EventArgs e) => {
				OrganizeBoxes(RedBox);
			};

			GreenBox = new BoxView {
                WidthRequest = BoxSize,
				HeightRequest = 36,
				Color = Color.Green
			};
			GreenBox.GestureRecognizers.Add (gbTap);

			YellowBox = new BoxView {
                WidthRequest = BoxSize,
				HeightRequest = 36,
				Color = Color.Yellow
			};
			YellowBox.GestureRecognizers.Add (ybTap);

			RedBox = new BoxView {
                WidthRequest = BoxSize,
				HeightRequest = 36,
				Color = Color.Red
			};
			RedBox.GestureRecognizers.Add (rbTap);


			Children.Add (RedBox);
			Children.Add (YellowBox);
			Children.Add (GreenBox);



			Orientation = StackOrientation.Horizontal;


		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width <= 0) return;
            if (null != GreenBox) GreenBox.WidthRequest = BoxSize;
            if (null != YellowBox) YellowBox.WidthRequest = BoxSize;
            if (null != RedBox) RedBox.WidthRequest = BoxSize;

        }


        public void OrganizeBoxes(BoxView which) {
			if (null != which) {
				if (which == GreenBox) {
					Value = 2;
				}
				if (which == YellowBox) {
					Value = 1;
				}
				if (which == RedBox) {
					Value = 0;
				}
			}

			if (Value == 2) {
				GreenBox.Color = Color.Green;
				YellowBox.Color = Color.Yellow;
				RedBox.Color = Color.Red;
			}
			if( Value == 1) {
				GreenBox.Color = Color.Gray;
				YellowBox.Color = Color.Yellow;
				RedBox.Color = Color.Red;
			}
			if (Value == 0) {
				GreenBox.Color = Color.Gray;
				YellowBox.Color = Color.Gray;
				RedBox.Color = Color.Red;
			}
			if (Value > 2)
				Value = 2;
		}
	}
}


