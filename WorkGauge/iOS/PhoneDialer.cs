using WorkGauge;
using UIKit;
using Foundation;

namespace WorkGauge.iOS
{

	public class PhoneDialer : IDialer {

		public bool Dial(string number) {
			return UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number));
		}
	}
}

