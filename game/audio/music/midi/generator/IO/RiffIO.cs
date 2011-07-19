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
    /// Saves and loads midi riffs
    /// </summary>
    public static class RiffIO
    {
        /// <summary>
        /// Saves riff or riffpacks
        /// </summary>
        /// <param name="iRiff">riff</param>
        /// <param name="xmlFileName">file name</param>
        public static void Save(this IRiff iRiff, string xmlFileName)
        {
            RiffPack riffPack;

            if (iRiff is Riff)
                riffPack = new RiffPack(iRiff);
            else if (iRiff is RiffPack)
                riffPack = (RiffPack)iRiff;
            else if (iRiff == null)
                riffPack = new RiffPack();
            else
                throw new Exception("Unrecognized riff type");

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
            XmlElement riffPackNode;
            XmlNodeList riffPackNodeList = xmlDocument.GetElementsByTagName("riffPack");

            if (riffPackNodeList.Count > 0)
            {
                riffPackNode = (XmlElement)riffPackNodeList[0];
                riffPackNode.RemoveAll();
            }
            else
            {
                riffPackNode = xmlDocument.CreateElement("riffPack");
            }

            riffPackNode.SetAttribute("Tempo", riffPack.Tempo.ToString());

            foreach (Riff riff in riffPack)
            {
                XmlNode riffNode = xmlDocument.CreateElement("riff");

                XmlElement riffElement = (XmlElement)riffNode;

                riffElement.SetAttribute("IsDrum", riff.IsDrum.ToString());
                riffElement.SetAttribute("MidiInstrument", riff.MidiInstrument.ToString());
                riffElement.SetAttribute("Tempo", riff.Tempo.ToString());
                riffElement.SetAttribute("Length", riff.Length.ToString(NumberFormatInfo.InvariantInfo));

                riffPackNode.AppendChild(riffNode);

                foreach (Note note in riff)
                {
                    XmlNode noteNode = xmlDocument.CreateElement("note");
                    XmlElement noteElement = (XmlElement)noteNode;

                    noteElement.SetAttribute("Length", note.Length.ToString(NumberFormatInfo.InvariantInfo));
                    noteElement.SetAttribute("Pitch", note.Pitch.ToString());
                    noteElement.SetAttribute("RiffPosition", note.RiffPosition.ToString(NumberFormatInfo.InvariantInfo));
                    noteElement.SetAttribute("Velocity", note.Velocity.ToString());

                    riffNode.AppendChild(noteNode);
                }
            }
            root.AppendChild(riffPackNode);

            xmlDocument.Save(xmlFileName);
        }

        /// <summary>
        /// Load riff pack from xml file name
        /// </summary>
        /// <param name="xmlFileName">xml file name</param>
        /// <returns>riff pack</returns>
        public static RiffPack Load(string xmlFileName)
        {
            RiffPack riffPack = new RiffPack();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileName);

            XmlNodeList riffPackList = xmlDocument.GetElementsByTagName("riffPack");
            XmlNode riffPackNode = riffPackList[0];


            riffPack.Tempo = int.Parse(riffPackNode.Attributes["Tempo"].Value);

            foreach (XmlNode riffNode in riffPackNode.ChildNodes)
            {
                Riff riff = new Riff();

                riff.IsDrum = bool.Parse(riffNode.Attributes["IsDrum"].Value);
                riff.MidiInstrument = int.Parse(riffNode.Attributes["MidiInstrument"].Value);
                riff.Tempo = int.Parse(riffNode.Attributes["Tempo"].Value);

                riffPack.Add(riff);

                foreach (XmlNode noteNode in riffNode.ChildNodes)
                {
                    double length = double.Parse(noteNode.Attributes["Length"].Value, NumberFormatInfo.InvariantInfo);
                    int pitch = int.Parse(noteNode.Attributes["Pitch"].Value);
                    double riffPosition = double.Parse(noteNode.Attributes["RiffPosition"].Value, NumberFormatInfo.InvariantInfo);
                    int velocity = int.Parse(noteNode.Attributes["Velocity"].Value);

                    Note note = new Note(riffPosition, length, pitch, velocity);

                    riff.Add(note);
                }
            }

            return riffPack;
        }
    }
}