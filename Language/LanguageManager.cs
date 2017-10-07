﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TS3Sky.Language
{
    public class LanguageManager
    {
        private static string LocalPath = Environment.CurrentDirectory + @"\Config.ini";
        private static string LanguagePath = Environment.CurrentDirectory + @"\Languages";
        private static string LanguageFile;
        private const string Section = "Local";
        private const string Ident = "Language";

        public static string LocalLanguage { get; private set; }

        public static bool IsSettingExisted
        {
            get
            {
                LanguageReader llr = new LanguageReader(LocalPath);
                string local = llr.Read(Section, Ident, String.Empty);
                if (local.Length > 0) return true;
                else return false;
            }
        }

        public static string GetSystemLanguage()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower();
        }

        public static string[] GetLanguages()
        {
            List<string> languages = new List<string>();
            if (!Directory.Exists(LanguagePath)) Directory.CreateDirectory(LanguagePath);
            DirectoryInfo folder = new DirectoryInfo(LanguagePath);
            foreach (FileInfo next in folder.GetFiles())
            {
                if (next.Name.EndsWith(".ini"))
                {
                    try
                    {
                        LanguageReader llr = new LanguageReader(next.FullName);
                        string local = llr.Read("Language", "Name", String.Empty);
                        if (local.Length > 0) languages.Add(next.Name.Substring(0, next.Name.IndexOf('.')));
                    }
                    catch { }
                }
            }
            return languages.ToArray();
        }

        public static string GetLanguageName(string code)
        {
            LanguageReader llr = new LanguageReader(LanguagePath + "\\" + code + ".ini");
            string local = llr.Read("Language", "Name", String.Empty);
            if (local.Length > 0) return local;
            else return null;
        }

        public static void SaveLanguage(string code)
        {
            LanguageReader llr = new LanguageReader(LocalPath);
            llr.Write(Section, Ident, code);
            llr.UpdateFile();
        }

        public static void InitializeLocal()
        {
            // 初始化本地语言名
            LanguageReader llr = new LanguageReader(LocalPath);
            string local = llr.Read(Section, Ident, "zh-cn");
            LocalLanguage = local;
            LanguageFile = LanguagePath + "\\" + local + ".ini";
            if (!File.Exists(LanguageFile)) throw new Exception("Cannot find the language file. Program is stopped.");

            // 初始化最基本的语言
            Application.Initialize(local);
        }
        public static void ReadExternalLanguage()
        {
            LanguageReader lr = new LanguageReader(LanguageFile);

            Welcome.Initialize(lr);
            Package.Initialize(lr);
            Navigation.Initialize(lr);
            About.Initialize(lr);
            Operation.Initialize(lr);
            Dialog.Initialize(lr);
            ColorPickBar.Initialize(lr);
            Sky_Clear1.Initialize(lr);
            Sky_Clear2.Initialize(lr);
            Sky_ClearLight.Initialize(lr);
            Sky_ClearSky.Initialize(lr);
            Sky_ClearSea.Initialize(lr);

            lr.UpdateFile();
        }
    }
}
