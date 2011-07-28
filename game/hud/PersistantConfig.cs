using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.hud
{
    internal static class PersistantConfig
    {
        #region Constants
        private const string configFileName = "./config.xml";
        #endregion

        #region Fields and parts
        private static XmlDocument xmlDocument;
        #endregion

        #region Constructor
        static PersistantConfig()
        {
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