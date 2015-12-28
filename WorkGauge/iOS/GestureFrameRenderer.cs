using WorkGauge;
using WorkGauge.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof (GestureFrame), typeof (GestureFrameRenderer))]

namespace WorkGauge.iOS
{
    public class GestureFrameRenderer : FrameRenderer
    {
        private UISwipeGestureRecognizer swipeDown;
        private UISwipeGestureRecognizer swipeLeft;
        private UISwipeGestureRecognizer swipeRight;
        private UISwipeGestureRecognizer swipeUp;
        private UITapGestureRecognizer tapped;

        public GestureFrameRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            tapped = new UITapGestureRecognizer(() =>
            {
                var _gi = (GestureFrame) Element;
                _gi.OnTapped();
            });

            swipeDown = new UISwipeGestureRecognizer(
                () =>
                {
                    var _gi = (GestureFrame) Element;
                    _gi.OnSwipeDown();
                }
                )
            {
                Direction = UISwipeGestureRecognizerDirection.Down,
            };

            swipeUp = new UISwipeGestureRecognizer(
                () =>
                {
                    var _gi = (GestureFrame) Element;
                    _gi.OnSwipeTop();
                }
                )
            {
                Direction = UISwipeGestureRecognizerDirection.Up,
            };

            swipeLeft = new UISwipeGestureRecognizer(
                () =>
                {
                    var _gi = (GestureFrame) Element;
                    _gi.OnSwipeLeft();
                }
                )
            {
                Direction = UISwipeGestureRecognizerDirection.Left,
            };

            swipeRight = new UISwipeGestureRecognizer(
                () =>
                {
                    var _gi = (GestureFrame) Element;
                    _gi.OnSwipeRight();
                }
                )
            {
                Direction = UISwipeGestureRecognizerDirection.Right,
            };

            if (e.NewElement == null)
            {
                if (swipeDown != null)
                {
                    RemoveGestureRecognizer(swipeDown);
                }
                if (swipeUp != null)
                {
                    RemoveGestureRecognizer(swipeUp);
                }
                if (swipeLeft != null)
                {
                    RemoveGestureRecognizer(swipeLeft);
                }
                if (swipeRight != null)
                {
                    RemoveGestureRecognizer(swipeRight);
                }
            }

            if (e.OldElement == null)
            {
                AddGestureRecognizer(swipeDown);
                AddGestureRecognizer(swipeUp);
                AddGestureRecognizer(swipeLeft);
                AddGestureRecognizer(swipeRight);
                AddGestureRecognizer(tapped);
            }
        }
    }
}