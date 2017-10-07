using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Exceptions
{
    public class LanguageFileNotFoundException : Exception
    {
        public LanguageFileNotFoundException(string local)
            : base(local + " Language File Not Found")
        { }
    }
}
