﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Language
{
    public class Navigation
    {
        public static string WelcomeLabel = "Welcome";
        public static string Welcome = "Start";
        public static string Weather = "Weather";
        public static string SettingsLabel = "Advanced";
        public static string OtherInfoLabel = "More Information";
        public static string About = "About";

        private const string Section = "Navigation";
        public static void Initialize(LanguageReader lr)
        {
            WelcomeLabel = lr.Read(Section, "WelcomeLabel", WelcomeLabel);
            Welcome = lr.Read(Section, "Welcome", Welcome);
            Weather = lr.Read(Section, "Weather", Weather);
            SettingsLabel = lr.Read(Section, "SettingsLabel", SettingsLabel);
            OtherInfoLabel = lr.Read(Section, "OtherInfoLabel", OtherInfoLabel);
            About = lr.Read(Section, "About", About);
        }
    }
}
