﻿using System;
using System.Collections.Generic;
using System.Text;

namespace m3i
{
    public class Locales
    {
        /// <summary>
        /// 根据id获取名称
        /// </summary>
        public static string IdToName(int id)
        {
            return IdName[id];
        }

        /// <summary>
        /// 根据名称获取id (如果找不到, 则返回-1.)
        /// </summary>
        public static int NameToId(string name)
        {
            foreach (KeyValuePair<int, string> kvp in IdName)
            {
                if (kvp.Value == name)
                {
                    return kvp.Key;
                }
            }
            return -1;
        }

        public static string NameToLocaleName(string name)
        {
            return LocaleName[name];
        }

        private static Dictionary<int, string> _idName;
        /// <summary>
        /// 获取Id与名称的键值词典 (如: 通过0x0可以得到en-US)
        /// </summary>
        public static Dictionary<int, string> IdName
        {
            get
            {
                if (_idName == null)
                {
                    _idName = new Dictionary<int, string>()
                    {
                        {0x00, "en-US"},
                        {0x01, "zh-CN"},
                        {0x02, "zh-TW"},
                        {0x03, "cs-CZ"},
                        {0x04, "da-DK"},
                        {0x05, "nl-NL"},
                        {0x06, "fi-FI"},
                        {0x07, "fr-FR"},
                        {0x08, "de-DE"},
                        {0x09, "el-GR"},
                        {0x0A, "hu-HU"},
                        {0x0B, "it-IT"},
                        {0x0C, "ja-JP"},
                        {0x0D, "ko-KR"},
                        {0x0E, "no-NO"},
                        {0x0F, "pl-PL"},
                        {0x10, "pt-PT"},
                        {0x11, "pt-BR"},
                        {0x12, "ru-RU"},
                        {0x13, "es-ES"},
                        {0x14, "es-MX"},
                        {0x15, "sv-SE"},
                        {0x16, "th-TH"},
                    };
                }
                return _idName;
            }
        }

        private static Dictionary<string, string> _localeName;
        /// <summary>
        /// 获取名称与游戏名字的键值词典 (如通过en-US得到The Sims 3)
        /// </summary>
        public static Dictionary<string, string> LocaleName
        {
            get
            {
                if (_localeName == null)
                {
                    _localeName = new Dictionary<string, string>()
                    { 
                       { "en-US", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "zh-CN", Encoding.Unicode.GetString(new byte[] { 0x21, 0x6A, 0xDF, 0x62, 0xBA, 0x4E, 0x1F, 0x75, 0x33, 0x00 }) },
                       { "zh-TW", Encoding.Unicode.GetString(new byte[] { 0x21, 0x6A, 0xEC, 0x64, 0x02, 0x5E, 0x11, 0x6C, 0x33, 0x00 }) },
                       { "cs-CZ", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "da-DK", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "nl-NL", Encoding.Unicode.GetString(new byte[] { 0x44, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "fi-FI", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "fr-FR", Encoding.Unicode.GetString(new byte[] { 0x4C, 0x00, 0x65, 0x00, 0x73, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "de-DE", Encoding.Unicode.GetString(new byte[] { 0x44, 0x00, 0x69, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "el-GR", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "hu-HU", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "it-IT", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "ja-JP", Encoding.Unicode.GetString(new byte[] { 0xB6, 0x30, 0x65, 0xFF, 0xB7, 0x30, 0xE0, 0x30, 0xBA, 0x30, 0x13, 0xFF }) },
                       { "ko-KR", Encoding.Unicode.GetString(new byte[] { 0xEC, 0xC2, 0x88, 0xC9, 0x20, 0x00, 0x33, 0x00 }) },
                       { "no-NO", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "pl-PL", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "pt-PT", Encoding.Unicode.GetString(new byte[] { 0x4F, 0x00, 0x73, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "pt-BR", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "ru-RU", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "es-ES", Encoding.Unicode.GetString(new byte[] { 0x4C, 0x00, 0x6F, 0x00, 0x73, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "es-MX", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "sv-SE", Encoding.Unicode.GetString(new byte[] { 0x54, 0x00, 0x68, 0x00, 0x65, 0x00, 0x20, 0x00, 0x53, 0x00, 0x69, 0x00, 0x6D, 0x00, 0x73, 0x00, 0x20, 0x00, 0x33, 0x00 }) },
                       { "th-TH", Encoding.Unicode.GetString(new byte[] { 0x40, 0x0E, 0x14, 0x0E, 0x2D, 0x0E, 0x30, 0x0E, 0x0B, 0x0E, 0x34, 0x0E, 0x21, 0x0E, 0x2A, 0x0E, 0x4C, 0x0E, 0x20, 0x00, 0x33, 0x00 }) }
                    };
                }
                return _localeName;
            }
        }
    }
}
