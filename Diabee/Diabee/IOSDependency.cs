using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Diabee
{
    public interface IOSDependency
    {
        string GetPath();
        string GetPlatformSpecificFileSaveDescription();

        Stream GetStream();
    }
}
