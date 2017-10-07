using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Exceptions
{
    public class CannotFindInstallDirException : Exception
    {
        public CannotFindInstallDirException()
            : base("Cannot Find Install Dir Exception")
        {
        }
    }
}
