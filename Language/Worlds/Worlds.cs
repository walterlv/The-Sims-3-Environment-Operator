using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Languages
{
    public class Worlds
    {
        public static string O = null;
        public static string EP011 = null;
        public static string EP012 = null;
        public static string EP013 = null;
        public static string EP02 = null;
        public static string EP03 = null;
        public static string EP05 = null;
        public static string EP06 = null;
        public static string EP07 = null;

        private const string Node = "World";
        public static void ReadFile(XmlFiles reader)
        {
            O = reader.TryRead(O, Node, "O");
            EP011 = reader.TryRead(EP011, Node, "EP011");
            EP012 = reader.TryRead(EP012, Node, "EP012");
            EP013 = reader.TryRead(EP013, Node, "EP013");
            EP02 = reader.TryRead(EP02, Node, "EP02");
            EP03 = reader.TryRead(EP03, Node, "EP03");
            EP05 = reader.TryRead(EP05, Node, "EP05");
            EP06 = reader.TryRead(EP06, Node, "EP06");
            EP07 = reader.TryRead(EP07, Node, "EP07");
        }
    }
}
