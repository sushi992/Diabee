using System;
using System.IO;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Diabee.iOS.OSDependency))]
namespace Diabee.iOS
{
    public class OSDependency : IOSDependency
    {
        public string GetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public string GetPlatformSpecificFileSaveDescription()
        {
            return "All PDFs will be saved within the 'Diabee' folder. You " +
                "can find this by navigating to it via the 'Files' app on " +
                "your iPhone.";
        }

        public Stream GetStream()
        {
            var image = UIImage.FromBundle("diabee.png");
            return image.AsPNG().AsStream();
        }
    }
}