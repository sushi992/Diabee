using Android.OS;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Diabee.Droid.OSDependency))]
namespace Diabee.Droid
{
    public class OSDependency : IOSDependency
    {
        public string GetPath()
        {
            var pathToDocuments = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath;

            if (!Directory.Exists(pathToDocuments))
            {
                Directory.CreateDirectory(pathToDocuments);
            }

            return pathToDocuments;
        }

        public string GetPlatformSpecificFileSaveDescription()
		{
			return "All PDF files will be saved under the main 'Documents' " +
				"folder on your Android device";
		}

        public Stream GetStream()
        {
            return Android.App.Application.Context.Assets.Open("diabee.png");
        }
    }
}