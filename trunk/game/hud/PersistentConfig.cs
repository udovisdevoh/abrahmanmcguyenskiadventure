using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.hud
{
    internal static class PersistentConfig
    {
        #region Constants
        private static string configFileName;
        #endregion

        #region Fields and parts
        private static XmlDocument xmlDocument;
        #endregion

        #region Constructor
        static PersistentConfig()
        {
            configFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "abrahmanConfig.xml");

            xmlDocument = new XmlDocument();
            if (File.Exists(configFileName))
                xmlDocument.Load(configFileName);
            else
                xmlDocument.AppendChild(xmlDocument.CreateElement("config"));
        }
        #endregion

        #region Internal Methods
        internal static void Clear(Program program)
        {
            if (File.Exists(configFileName))
                File.Delete(configFileName);

            xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateElement("config"));

            SongPlayer.Volume = MusicVolume;
            TutorialTalker.Volume = VoiceVolume;
            SoundManager.Volume = SoundVolume;
            Program.isFullScreen = IsFullScreen;
            Program.screenWidth = ScreenWidth;
            Program.screenHeight = ScreenHeight;
            program.InitSurfaceViewPortRatioSettingsEtc();
        }
        #endregion

        #region Private Methods
        private static string GetConfigItem(string tagName)
        {
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName(tagName);
            return xmlNodeList[0].InnerText;
        }

        private static bool IsConfigItemExist(string tagName)
        {
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName(tagName);
            return xmlNodeList.Count > 0;
        }

        private static void SetConfigItem(string tagName, string value)
        {
            if (IsConfigItemExist(tagName))
            {
                XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName(tagName);
                xmlNodeList[0].InnerText = value;
            }
            else
            {
                XmlElement element = xmlDocument.CreateElement(tagName);
                XmlNode configNode = xmlDocument.GetElementsByTagName("config")[0];

                configNode.AppendChild(element);
                element.AppendChild(xmlDocument.CreateTextNode(value));
            }

            xmlDocument.Save(configFileName);
        }
        #endregion

        #region Properties
        public static int JumpButton
        {
            get
            {
                if (IsConfigItemExist("jumpButton"))
                    return int.Parse(GetConfigItem("jumpButton"));
                else
                    return 2;
            }
            set
            {
                SetConfigItem("jumpButton", value.ToString());
            }
        }

        public static int AttackButton
        {
            get
            {
                if (IsConfigItemExist("attackButton"))
                    return int.Parse(GetConfigItem("attackButton"));
                else
                    return 3;
            }
            set
            {
                SetConfigItem("attackButton", value.ToString());
            }
        }

        public static int LeaveBeaverButton
        {
            get
            {
                if (IsConfigItemExist("leaveBeaverButton"))
                    return int.Parse(GetConfigItem("leaveBeaverButton"));
                else
                    return 1;
            }
            set
            {
                SetConfigItem("leaveBeaverButton", value.ToString());
            }
        }

        public static int ScreenWidth
        {
            get
            {
                if (IsConfigItemExist("screenWidth"))
                    return int.Parse(GetConfigItem("screenWidth"));
                else
                    return 640;
            }
            set
            {
                SetConfigItem("screenWidth", value.ToString());
            }
        }

        public static int ScreenHeight
        {
            get
            {
                if (IsConfigItemExist("screenHeight"))
                    return int.Parse(GetConfigItem("screenHeight"));
                else
                    return 480;
            }
            set
            {
                SetConfigItem("screenHeight", value.ToString());
            }
        }

        public static int MusicVolume
        {
            get
            {
                if (IsConfigItemExist("musicVolume"))
                    return int.Parse(GetConfigItem("musicVolume"));
                else
                    return 10;
            }
            set
            {
                SetConfigItem("musicVolume", value.ToString());
            }
        }

        public static int SoundVolume
        {
            get
            {
                if (IsConfigItemExist("soundVolume"))
                    return int.Parse(GetConfigItem("soundVolume"));
                else
                    return 8;
            }
            set
            {
                SetConfigItem("soundVolume", value.ToString());
            }
        }

        public static int VoiceVolume
        {
            get
            {
                if (IsConfigItemExist("voiceVolume"))
                    return int.Parse(GetConfigItem("voiceVolume"));
                else
                    return 10;
            }
            set
            {
                SetConfigItem("voiceVolume", value.ToString());
            }
        }

        public static bool IsFullScreen
        {
            get
            {
                if (IsConfigItemExist("isFullScreen"))
                    return bool.Parse(GetConfigItem("isFullScreen"));
                else
                    return false;
            }
            set
            {
                SetConfigItem("isFullScreen", value.ToString());
            }
        }
        #endregion
    }
}