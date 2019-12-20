using Diabee;
using Diabee.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace Diabee.iOS
{
    public class CustomSliderRenderer : SliderRenderer
    {
        const string colorSliderStart = "#ff0066";
        const string colorSliderEnd = "#d9d9d9";

        protected override void
                 OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // Set custom resource
                Control.MaximumTrackTintColor = Color.FromHex(colorSliderEnd).ToUIColor();
                Control.MinimumTrackTintColor = Color.FromHex(colorSliderStart).ToUIColor();

                // Hide thumb to make it look cool lol
                Control.ThumbTintColor = Color.FromHex(colorSliderStart).ToUIColor();
            }
        }
    }
}