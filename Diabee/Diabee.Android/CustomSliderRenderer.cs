using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Diabee;
using Diabee.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace Diabee.Droid
{
    public class CustomSliderRenderer : SliderRenderer
    {
        public CustomSliderRenderer(Context context) : base(context) 
        {
        
        }

        protected override void
                 OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // Set custom drawable resource
                Control.SetProgressDrawableTiled(
                Resources.GetDrawable(
                Resource.Drawable.Custom_Slider_Style,
                (this.Context).Theme));

                // Hide thumb to make it look cool lol
                Control.SetThumb(new ColorDrawable(Android.Graphics.Color.Transparent));
            }
        }
    }
}