using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Exceptions
{
    public class CannotFindInstallDirException : Exception
    {
        public CannotFindInstallDirException()
            : base(TS3Sky.Language.Dialog.CannotFindInstallDirException)
        {
        }
    }
}
