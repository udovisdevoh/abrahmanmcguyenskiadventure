using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.midi.generator;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Track types
    /// </summary>
    internal enum TrackType { Soprano, Alto, Tenor, Bass, Pad, ChromaticPercussion, Snare, Kick, OtherDrum }

    /// <summary>
    /// To generate songs
    /// </summary>
    internal static class SongGenerator
    {
        #region Field and parts
        private static List<string> melodicInstrumentNameList = null;

        private static List<string> chromaticPercussionInstrumentNameList = null;

        private static List<string> bassInstrumentNameList = null;

        private static List<string> slowSnareNameList = null;

        private static List<string> fastSnareNameList = null;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build a song
        /// </summary>
        /// <param name="seed">seed</param>
        /// <returns>Song</returns>
        internal static IRiff BuildSong(int seed)
        {
            Random random = new Random(seed);
            PredefinedGenerator predefinedGenerator = new PredefinedGenerator();

            predefinedGenerator.IsOverrideScale = true;
            predefinedGenerator.IsOverrideTempo = true;
            predefinedGenerator.Tempo = random.Next(80, 160);
            predefinedGenerator.Modulation = random.NextDouble() * 0.75 + 0.25;
            predefinedGenerator.ScaleName = Scales.GetRandomPentatonicScaleName(random);//predefinedGenerator.ScaleName = Scales.GetRandomScaleName(random);
            predefinedGenerator.IsOverrideKey = false;

            int songLength = random.Next(8, 17) * 2;

            AddRandomTrack(predefinedGenerator, TrackType.Soprano, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Alto, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Tenor, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Bass, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Pad, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.ChromaticPercussion, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Snare, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.Kick, songLength, predefinedGenerator.Tempo, random);
            AddRandomTrack(predefinedGenerator, TrackType.OtherDrum, songLength, predefinedGenerator.Tempo, random);

            MusicGenerator musicGenerator = new MusicGenerator(random);
            return musicGenerator.BuildSong(predefinedGenerator);
        }
        #endregion

        #region Private Methods
        private static void AddRandomTrack(PredefinedGenerator predefinedGenerator, TrackType trackType, int songLength, int tempo, Random random)
        {
            predefinedGenerator[(int)trackType].MetaRiffPackName = GetRandomMetaRiffPackName(trackType, tempo, random);

            for (int barId = 0; barId < songLength; barId++)
            {
                predefinedGenerator[(int)trackType][barId] = true;
            }
        }

        private static string GetRandomMetaRiffPackName(TrackType trackType, int tempo, Random random)
        {
            if (trackType == TrackType.Soprano || trackType == TrackType.Alto)
            {
                return GetMelodicInstrumentNameList()[random.Next(GetMelodicInstrumentNameList().Count)];
            }
            else if (trackType == TrackType.Tenor || trackType == TrackType.Bass)
            {
                return GetBassInstrumentNameList()[random.Next(GetBassInstrumentNameList().Count)];
            }
            else if (trackType == TrackType.Pad)
            {
                return "Pad";
            }
            else if (trackType == TrackType.ChromaticPercussion)
            {
                return GetChromaticPercussionInstrumentNameList()[random.Next(GetChromaticPercussionInstrumentNameList().Count)];
            }
            else if (trackType == TrackType.Snare)
            {
                if (tempo >= 120)
                {
                    return GetSlowSnareNameList()[random.Next(GetSlowSnareNameList().Count)];
                }
                else
                {
                    return GetFastSnareNameList()[random.Next(GetFastSnareNameList().Count)];
                }
            }
            else if (trackType == TrackType.Kick)
            {
                if (tempo >= random.Next(20) + 110)
                {
                    return "DrumBassBinary";
                }
                else
                {
                    return "DrumBassQuad";
                }
            }
            else if (trackType == TrackType.OtherDrum)
            {
                if (random.Next(0, 5) == 1)
                {
                    if (random.Next(0, 2) == 1)
                    {
                        return "DrumTernaryOne";
                    }
                    else
                    {
                        return "DrumTernaryTwo";
                    }
                }
                else
                {
                    if (tempo >= random.Next(20) + 110)
                    {
                        return "DrumQuad";
                    }
                    else
                    {
                        return "DrumOcta";
                    }
                }
            }
            return null;
        }

        private static List<string> GetSlowSnareNameList()
        {
            if (slowSnareNameList == null)
            {
                slowSnareNameList = new List<string>();
                slowSnareNameList.Add("DrumSnareDouble");
                slowSnareNameList.Add("DrumSnareQuad");
            }
            return slowSnareNameList;
        }

        private static List<string> GetFastSnareNameList()
        {
            if (fastSnareNameList == null)
            {
                fastSnareNameList = new List<string>();
                fastSnareNameList.Add("DrumSnareQuad");
                fastSnareNameList.Add("DrumSnareOcta");
            }
            return fastSnareNameList;
        }

        private static List<string> GetMelodicInstrumentNameList()
        {
            if (melodicInstrumentNameList == null)
            {
                melodicInstrumentNameList = new List<string>();
                melodicInstrumentNameList.Add("Arab");
                melodicInstrumentNameList.Add("Church");
                melodicInstrumentNameList.Add("GuitarFolk");
                melodicInstrumentNameList.Add("GuitarFolkTernary");
                melodicInstrumentNameList.Add("GuitarMetalBlack");
                melodicInstrumentNameList.Add("GuitarMetalSpeed");
                melodicInstrumentNameList.Add("Harpsichord");
                melodicInstrumentNameList.Add("MeditativeChina");
                melodicInstrumentNameList.Add("MeditativeIndia");
                melodicInstrumentNameList.Add("MeditativeIndonesia");
                melodicInstrumentNameList.Add("MeditativeIrish");
                melodicInstrumentNameList.Add("MeditativeJamaican");
                melodicInstrumentNameList.Add("MeditativeJapan");
                melodicInstrumentNameList.Add("MeditativeScottish");
                melodicInstrumentNameList.Add("MeditativeThailand");
                melodicInstrumentNameList.Add("MeditativeTibet");
                melodicInstrumentNameList.Add("MeditativeWest");
                melodicInstrumentNameList.Add("Persian");
                melodicInstrumentNameList.Add("PianoClassic");
                melodicInstrumentNameList.Add("SynthLead");
                melodicInstrumentNameList.Add("SynthLeadArpege");
                melodicInstrumentNameList.Add("TrumpetClassic");
                melodicInstrumentNameList.Add("ViolinCeltic");
            }
            return melodicInstrumentNameList;
        }

        private static List<string> GetBassInstrumentNameList()
        {
            if (bassInstrumentNameList == null)
            {
                bassInstrumentNameList = new List<string>();
                bassInstrumentNameList.Add("BassSynth");
            }
            return bassInstrumentNameList;
        }

        private static List<string> GetChromaticPercussionInstrumentNameList()
        {
            if (chromaticPercussionInstrumentNameList == null)
            {
                chromaticPercussionInstrumentNameList = new List<string>();
                chromaticPercussionInstrumentNameList.Add("DrumChromaticQuad");
                chromaticPercussionInstrumentNameList.Add("DrumChromaticTerminator");
                chromaticPercussionInstrumentNameList.Add("DrumChromaticWoodBlockTernary");
            }
            return chromaticPercussionInstrumentNameList;
        }
        #endregion
    }
}
