using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Saves and loads predefined midi generators
    /// </summary>
    public static class MidiGeneratorIO
    {
        /// <summary>
        /// Save predefined midi generator
        /// </summary>
        /// <param name="generator">generator</param>
        /// <param name="xmlFileName">xml file name</param>
        public static void Save(this PredefinedGenerator generator, string xmlFileName)
        {
            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(xmlFileName);
            }
            catch (System.IO.FileNotFoundException)
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xmlWriter.WriteStartElement("song");
                xmlWriter.Close();
                xmlDocument.Load(xmlFileName);
            }

            XmlNode root = xmlDocument.DocumentElement;
            XmlElement generatorNode;
            XmlNodeList generatorNodeList = xmlDocument.GetElementsByTagName("predefinedGenerator");

            if (generatorNodeList.Count > 0)
            {
                generatorNode = (XmlElement)generatorNodeList[0];
                generatorNode.RemoveAll();
            }
            else
            {
                generatorNode = xmlDocument.CreateElement("predefinedGenerator");
            }

            generatorNode.SetAttribute("BarCount",generator.BarCount.ToString());
            generatorNode.SetAttribute("IsOverrideScale",generator.IsOverrideScale.ToString());
            generatorNode.SetAttribute("IsOverrideTempo",generator.IsOverrideTempo.ToString());
            generatorNode.SetAttribute("Modulation", generator.Modulation.ToString(NumberFormatInfo.InvariantInfo));
            generatorNode.SetAttribute("ScaleName",generator.ScaleName1);
            generatorNode.SetAttribute("Tempo",generator.Tempo.ToString());
            generatorNode.SetAttribute("LyricsToMusicPhase", generator.LyricsToMusicPhase.ToString(NumberFormatInfo.InvariantInfo));

            foreach (PredefinedGeneratorTrack generatorTrack in generator)
            {
                if (generatorTrack.MetaRiffPackName == null || generatorTrack.MetaRiffPackName.Trim() == "")
                    continue;
                XmlNode generatorTrackNode = xmlDocument.CreateElement("predefinedGeneratorTrack");
                XmlElement generatorTrackElement = (XmlElement)generatorTrackNode;

                generatorTrackElement.SetAttribute("MetaRiffPackName", generatorTrack.MetaRiffPackName);

                foreach (bool isBarOn in generatorTrack)
                {
                    XmlElement barInfo = xmlDocument.CreateElement("barInfo");
                    barInfo.SetAttribute("isOn", isBarOn.ToString());
                    generatorTrackNode.AppendChild(barInfo);
                }
                
                generatorNode.AppendChild(generatorTrackNode);
            }

            root.AppendChild(generatorNode);
            xmlDocument.Save(xmlFileName);
        }

        /// <summary>
        /// Load predefined midi generator
        /// </summary>
        /// <param name="xmlFileName">xml file name</param>
        /// <returns>predefined midi generator</returns>
        public static PredefinedGenerator Load(string xmlFileName)
        {
            PredefinedGenerator predefinedGenerator = new PredefinedGenerator();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileName);

            XmlNodeList predefinedGeneratorNodeList = xmlDocument.GetElementsByTagName("predefinedGenerator");
            XmlNode predefinedGeneratorNode = predefinedGeneratorNodeList[0];
            XmlElement predefinedGeneratorElement = (XmlElement)predefinedGeneratorNode;

            predefinedGenerator.BarCount = int.Parse(predefinedGeneratorElement.GetAttribute("BarCount"));
            predefinedGenerator.IsOverrideScale = bool.Parse(predefinedGeneratorElement.GetAttribute("IsOverrideScale"));
            predefinedGenerator.IsOverrideTempo = bool.Parse(predefinedGeneratorElement.GetAttribute("IsOverrideTempo"));
            predefinedGenerator.Modulation = double.Parse(predefinedGeneratorElement.GetAttribute("Modulation"),NumberFormatInfo.InvariantInfo);
            predefinedGenerator.ScaleName1 = predefinedGeneratorElement.GetAttribute("ScaleName");
            predefinedGenerator.Tempo = int.Parse(predefinedGeneratorElement.GetAttribute("Tempo"));
            predefinedGenerator.LyricsToMusicPhase = double.Parse(predefinedGeneratorElement.GetAttribute("LyricsToMusicPhase"), NumberFormatInfo.InvariantInfo);

            XmlNodeList predefinedGeneratorTrackNodeList = predefinedGeneratorElement.GetElementsByTagName("predefinedGeneratorTrack");

            int trackCounter = 0;
            foreach (XmlNode predefinedGeneratorTrackNode in predefinedGeneratorTrackNodeList)
            {
                XmlElement predefinedGeneratorTrackElement = (XmlElement)predefinedGeneratorTrackNode;
                PredefinedGeneratorTrack predefindedGeneratorTrack = predefinedGenerator[trackCounter];

                predefindedGeneratorTrack.MetaRiffPackName = predefinedGeneratorTrackElement.GetAttribute("MetaRiffPackName");

                int barInfoCounter = 0;
                XmlNodeList barInfoNodeList = predefinedGeneratorTrackElement.GetElementsByTagName("barInfo");
                foreach (XmlNode barInfoNode in barInfoNodeList)
                {
                    XmlElement barInfoElement = (XmlElement)barInfoNode;
                    predefindedGeneratorTrack[barInfoCounter] = bool.Parse(barInfoElement.GetAttribute("isOn"));
                    barInfoCounter++;
                }

                trackCounter++;
            }

            return predefinedGenerator;
        }
    }
}
