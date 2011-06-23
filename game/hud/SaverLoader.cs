using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// To save and load game
    /// </summary>
    internal static class SaverLoader
    {
        /// <summary>
        /// To save game meta state
        /// </summary>
        private static XmlSerializer serializer = new XmlSerializer(typeof(GameMetaState));

        /// <summary>
        /// Load game from file
        /// </summary>
        /// <returns>Loaded game (or null if user cancelled or if file is invalid)</returns>
        public static GameMetaState LoadGame()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load game";
            openFileDialog.Filter = "XML file|*.savedgame.xml";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == null || openFileDialog.FileName == "")
                return null;

            using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
            {
                return (GameMetaState)serializer.Deserialize(streamReader);
            }
        }

        /// <summary>
        /// Save game to file
        /// </summary>
        /// <returns>Loaded game (or null if user cancelled or if file is invalid)</returns>
        public static void SaveGame(GameMetaState gameMetaState)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save game";
            saveFileDialog.Filter = "XML file|*.savedgame.xml";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == null || saveFileDialog.FileName == "")
                return;

            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
            {
                serializer.Serialize(streamWriter, gameMetaState);
            }
        }
    }
}
