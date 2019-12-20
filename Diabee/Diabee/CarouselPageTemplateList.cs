using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Diabee
{
    public class CarouselPageTemplateList : List<DataTemplate>
    {
        public CarouselPageTemplateList() : base() { }
        public CarouselPageTemplateList(IEnumerable<DataTemplate> collection) : base(collection) { }
        public CarouselPageTemplateList(int capacity) : base(capacity) { }
    }
}
