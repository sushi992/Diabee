using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Diabee.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Diabee.Droid
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0); 
            toast.Show();
        }

        public void ShortAlert(string message)
        {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }
    }
}