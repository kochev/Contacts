using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace System
{
    public static class Extensions
    {
        public static UIViewController TopViewController
        {
            get
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                    vc = vc.PresentedViewController;

                if (vc is UINavigationController navController)
                    vc = navController.ViewControllers.Last();

                return vc;
            }
        }

        public static void RountRect(this UIButton button, float radius = 8f)
        {
            button.Layer.CornerRadius = radius;
            button.Layer.MasksToBounds = true;
        }

        public static void SetBlackAndBold(this UILabel view, List<string> subtexts, UIColor color, float fontSize = 16f)
        {
            var fulltext = view.Text;
            var str = new NSMutableAttributedString(view.Text);
            foreach (var subtext in subtexts)
            {
                var indexOf = fulltext.IndexOf(subtext);
                if (indexOf != -1)
                {
                    str.AddAttribute(UIStringAttributeKey.ForegroundColor, color, new NSRange(indexOf, subtext.Length));
                    str.AddAttribute(UIStringAttributeKey.Font, UIFont.BoldSystemFontOfSize(fontSize),
                        new NSRange(indexOf, subtext.Length));
                }
            }

            view.AttributedText = str;
        }

        public static void SetBlackAndBold(this UILabel view, string fulltext, List<string> subtexts, UIColor color,
            float fontSize = 16f)
        {
            var str = new NSMutableAttributedString(fulltext);
            foreach (var subtext in subtexts)
            {
                var indexOf = fulltext.IndexOf(subtext);
                if (indexOf != -1)
                {
                    str.AddAttribute(UIStringAttributeKey.ForegroundColor, color, new NSRange(indexOf, subtext.Length));
                    str.AddAttribute(UIStringAttributeKey.Font, UIFont.BoldSystemFontOfSize(fontSize),
                        new NSRange(indexOf, subtext.Length));
                }
            }

            view.AttributedText = str;
        }


        public static void AddBottomBorder(this UITextField textField, UIColor color, float height = 1f)
        {
            var border = textField.Subviews?.FirstOrDefault(view => view.AccessibilityIdentifier == "bottom_border");
            border?.RemoveFromSuperview();

            textField.BorderStyle = UITextBorderStyle.None;
            textField.BackgroundColor = UIColor.Clear;

            var borderLine = new UIView(new CGRect(0, textField.Frame.Height - height, textField.Frame.Width,
                height))
            {
                BackgroundColor = color, AccessibilityIdentifier = "bottom_border"
            };
            textField.AddSubview(borderLine);
        }

        public static UIImage GetImageFromColor(this UIColor color, float height = 1)
        {
            var rect = new CGRect(0, 0, height, height);
            UIGraphics.BeginImageContext(rect.Size);
            var context = UIGraphics.GetCurrentContext();
            context.SetFillColor(color.CGColor);
            context.FillRect(rect);

            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        public static UIView ProcessView(CGRect rect, UIColor backgroundColor, UIColor indicatorColor)
        {
            var view = new UIView(rect) {BackgroundColor = UIColor.FromWhiteAlpha(0, 0.4f)};
            var proccesWrapper = new UIView(new CGRect(view.Frame.Width / 2 - 45, view.Frame.Height / 2 - 45, 90, 90))
            {
                BackgroundColor = backgroundColor
            };
            proccesWrapper.Layer.CornerRadius = 4;
            var indicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
            {
                Color = indicatorColor,
                Frame = new CGRect(proccesWrapper.Frame.Width / 2 - 35, proccesWrapper.Frame.Height / 2 - 35, 70, 70)
            };
            indicator.StartAnimating();
            proccesWrapper.AddSubview(indicator);
            view.AddSubview(proccesWrapper);
            return view;
        }
    }
}