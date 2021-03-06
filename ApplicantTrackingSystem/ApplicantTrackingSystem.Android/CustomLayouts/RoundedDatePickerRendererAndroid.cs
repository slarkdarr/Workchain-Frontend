using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using ApplicantTrackingSystem.CustomLayouts;
using ApplicantTrackingSystem.Droid.CustomLayouts;
using Android.Content;

[assembly: ExportRenderer(typeof(RoundedDatePicker), typeof(RoundedDatePickerRendererAndroid))]
namespace ApplicantTrackingSystem.Droid.CustomLayouts
{
    public class RoundedDatePickerRendererAndroid : DatePickerRenderer
    {
        public RoundedDatePickerRendererAndroid(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Control.SetBackgroundResource(Resource.Layout.rounded_entry);
                var gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetCornerRadius(50f);
                //gradientDrawable.SetStroke(3, Android.Graphics.Color.ParseColor("#58327F"));
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);

                //Control.SetPadding(50, 60, Control.PaddingRight, 60);
            }
        }
    }
}
