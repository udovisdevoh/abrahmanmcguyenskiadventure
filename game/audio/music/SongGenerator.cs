﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.midi.generator;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Track types
    /// </summary>
    internal enum TrackType { Melody, /*Melody2, Tenor,*/ Pad, Bass, Snare, Kick, OtherDrum }

    internal enum SongType { Menu, Level, Invincibility, Ninja, Map }

    /// <summary>
    /// To generate songs
    /// </summary>
    internal static class SongGenerator
    {
        #region Constants
        private const int instrumentTypeCount = 6;
        #endregion

        #region Field and parts
        private static List<string> melodicInstrumentNameList = null;

        private static List<string> chromaticPercussionInstrumentNameList = null;

        private static List<string> bassInstrumentNameList = null;

        private static List<string> slowSnareNameList = null;

        private static List<string> fastSnareNameList = null;

        private static IRiff invincibilitySong = null;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build a song
        /// </summary>
        /// <param name="seed">seed</param>
        /// <param name="gameMode">game mode (can be null for default behavior)</param>
        /// <returns>Song</returns>
        internal static IRiff BuildSong(int seed, int skillLevel, SongType songType, AbstractGameMode gameMode)
        {
            Random random = new Random(seed);
            PredefinedGenerator predefinedGenerator = new PredefinedGenerator();
            int songLength;

            if (songType == SongType.Ninja)
                skillLevel += 3;
            else if (gameMode != null && gameMode.IsMusicSpeedUp)
                skillLevel += 4;

            predefinedGenerator.IsOverrideScale = true;
            predefinedGenerator.IsOverrideTempo = true;
            predefinedGenerator.Tempo = random.Next(70 + skillLevel / 2, 140 + skillLevel);
            predefinedGenerator.Modulation = random.NextDouble() * 0.75 + 0.25;
            predefinedGenerator.ScaleName1 = GetRandomScaleName(Math.Max(0,skillLevel - random.Next(0,3)), random);//predefinedGenerator.ScaleName = Scales.GetRandomPentatonicScaleName(random);//predefinedGenerator.ScaleName = Scales.GetRandomScaleName(random);
            predefinedGenerator.ScaleName2 = GetRandomScaleName(skillLevel + random.Next(0, 3), random);//predefinedGenerator.ScaleName = Scales.GetRandomPentatonicScaleName(random);//predefinedGenerator.ScaleName = Scales.GetRandomScaleName(random);
            predefinedGenerator.IsOverrideKey = false;
            predefinedGenerator.ScaleCycleLength = (int)Math.Round(Math.Pow(2.0, (double)random.Next(0, 5)));

            if (songType == SongType.Map)
                predefinedGenerator.Tempo -= 25;
            
            songLength = random.Next(8, 17) * 2;
            double barDensity = Math.Min(1.0, 0.5 + (((double)skillLevel) / 20.0));//0.5 for easy, 1.0 for hard)


            if (gameMode != null && gameMode.IsMusicSpeedUp)
            {
                predefinedGenerator.Tempo += random.Next(20,80);
                barDensity = 1.0;
            }


            if (songType == SongType.Invincibility)
            {
                barDensity = 1.0;
                songLength = 4;
                predefinedGenerator.ScaleName1 = "majorPentatonic";
                predefinedGenerator.ScaleName2 = "majorPentatonic";
                predefinedGenerator.Tempo = 280;
            }
            else if (songType == SongType.Ninja)
            {
                predefinedGenerator.ScaleName1 = "japanese";
                predefinedGenerator.ScaleName2 = "japanese";

                if (random.NextDouble() > 0.75)
                {
                    if (random.Next(0,2) == 1)
                        predefinedGenerator.ScaleName1 = "chinese";
                    else
                        predefinedGenerator.ScaleName2 = "chinese";
                }
                songLength = 8;
                predefinedGenerator.Modulation = random.NextDouble() * 0.1;
            }

            AddRandomTrack(predefinedGenerator, TrackType.Melody, songLength, predefinedGenerator.Tempo, barDensity, random);
            //AddRandomTrack(predefinedGenerator, TrackType.Melody2, songLength, predefinedGenerator.Tempo, barDensity, random);
            //AddRandomTrack(predefinedGenerator, TrackType.Tenor, songLength, predefinedGenerator.Tempo, barDensity, random);
            AddRandomTrack(predefinedGenerator, TrackType.Bass, songLength, predefinedGenerator.Tempo, barDensity, random);
            AddRandomTrack(predefinedGenerator, TrackType.Pad, songLength, predefinedGenerator.Tempo, barDensity, random);
            AddRandomTrack(predefinedGenerator, TrackType.Snare, songLength, predefinedGenerator.Tempo, barDensity, random);
            AddRandomTrack(predefinedGenerator, TrackType.Kick, songLength, predefinedGenerator.Tempo, barDensity, random);
            AddRandomTrack(predefinedGenerator, TrackType.OtherDrum, songLength, predefinedGenerator.Tempo, barDensity, random);

            //FillBlank(predefinedGenerator, (TrackType)random.Next(0, instrumentTypeCount), songLength, random);
            FillBlank(predefinedGenerator, TrackType.Melody, songLength, random);

            MusicGenerator musicGenerator = new MusicGenerator(random);
            return musicGenerator.BuildSong(predefinedGenerator);
        }

        /// <summary>
        /// Get invincibility song (or build it if it doesn't exist
        /// </summary>
        /// <param name="seed">seed</param>
        /// <returns>invincibility song</returns>
        internal static IRiff GetInvincibilitySong(int seed, AbstractGameMode gameMode)
        {
            if (invincibilitySong == null)
                invincibilitySong = BuildSong(seed, 0, SongType.Invincibility, gameMode);

            return invincibilitySong;
        }

        /// <summary>
        /// Reset invincibility song
        /// </summary>
        internal static void ResetInvincibilitySong()
        {
            invincibilitySong = null;
        }
        #endregion

        #region Private Methods
        private static void AddRandomTrack(PredefinedGenerator predefinedGenerator, TrackType trackType, int songLength, int tempo, double timePercent, Random random)
        {
            predefinedGenerator[(int)trackType].MetaRiffPackName = GetRandomMetaRiffPackName(trackType, tempo, random);

            int addedBarCount = 0;

            /*if (trackType == TrackType.Snare || trackType == TrackType.Kick)
            {
                predefinedGenerator[(int)trackType][0] = true;
                predefinedGenerator[(int)trackType][1] = true;
                addedBarCount++;
                addedBarCount++;
            }*/

            while ((double)addedBarCount / (double)songLength < timePercent)
            {
                int barId = random.Next(0, songLength) / 2 * 2;
                if (!predefinedGenerator[(int)trackType][barId])
                {
                    predefinedGenerator[(int)trackType][barId] = true;
                    addedBarCount++;
                }
                if (!predefinedGenerator[(int)trackType][barId + 1])
                {
                    predefinedGenerator[(int)trackType][barId + 1] = true;
                    addedBarCount++;
                }
            }
        }

        private static void FillBlank(PredefinedGenerator predefinedGenerator, TrackType trackType, int songLength, Random random)
        {
            for (int barId = 0; barId < songLength; barId++)
                if (!predefinedGenerator[(int)trackType][barId])
                    predefinedGenerator[(int)trackType][barId] = true;
        }

        private static string GetRandomMetaRiffPackName(TrackType trackType, int tempo, Random random)
        {
            if (trackType == TrackType.Melody /*|| trackType == TrackType.Melody2 || trackType == TrackType.Melody3 || trackType == TrackType.Melody4*/)
            {
                return GetMelodicInstrumentNameList()[random.Next(GetMelodicInstrumentNameList().Count)];
            }
            else if (/*trackType == TrackType.Tenor || */trackType == TrackType.Bass)
            {
                return GetBassInstrumentNameList()[random.Next(GetBassInstrumentNameList().Count)];
            }
            else if (trackType == TrackType.Pad)
            {
                return "Pad";
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
                if (random.Next(0, 3) == 1)
                {
                    return GetChromaticPercussionInstrumentNameList()[random.Next(GetChromaticPercussionInstrumentNameList().Count)];
                }
                else
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
            }
            return null;
        }

        private static List<string> GetSlowSnareNameList()
        {
            if (slowSnareNameList == null)
            {
                slowSnareNameList = new List<string>();
                slowSnareNameList.Add("DrumSnareDouble");
                //slowSnareNameList.Add("DrumSnareQuad");
            }
            return slowSnareNameList;
        }

        private static List<string> GetFastSnareNameList()
        {
            if (fastSnareNameList == null)
            {
                fastSnareNameList = new List<string>();
                fastSnareNameList.Add("DrumSnareQuad");
                //fastSnareNameList.Add("DrumSnareOcta");
            }
            return fastSnareNameList;
        }

        private static List<string> GetMelodicInstrumentNameList()
        {
            if (melodicInstrumentNameList == null)
            {
                melodicInstrumentNameList = new List<string>();
                /*melodicInstrumentNameList.Add("Arab");
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
                melodicInstrumentNameList.Add("ViolinCeltic");*/
                melodicInstrumentNameList.Add("Melody");
            }
            return melodicInstrumentNameList;
        }

        private static List<string> GetBassInstrumentNameList()
        {
            if (bassInstrumentNameList == null)
            {
                bassInstrumentNameList = new List<string>();
                bassInstrumentNameList.Add("Bass");
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

        private static string GetRandomScaleName(int skillLevel, Random random)
        {
            double minorProbability = 0;
            double evilProbability = 0;

            switch (skillLevel)
            {
                case 0:
                    minorProbability = 0.666;
                    evilProbability = 0.0;
                    break;
                case 1:
                    minorProbability = 0.666;
                    evilProbability = 0.0;
                    break;
                case 2:
                    minorProbability = 0.666;
                    evilProbability = 0.0;
                    break;
                case 3:
                    minorProbability = 0.666;
                    evilProbability = 0.0;
                    break;
                case 4:
                    minorProbability = 0.75;
                    evilProbability = 0.1;
                    break;
                case 5:
                    minorProbability = 0.8;
                    evilProbability = 0.2;
                    break;
                case 6:
                    minorProbability = 0.8;
                    evilProbability = 0.3;
                    break;
                case 7:
                    minorProbability = 0.8;
                    evilProbability = 0.4;
                    break;
                case 8:
                    minorProbability = 0.8;
                    evilProbability = 0.5;
                    break;
                case 9:
                    minorProbability = 0.8;
                    evilProbability = 0.6;
                    break;
                default:
                    minorProbability = 1.0;
                    evilProbability = 0.666;
                    break;
            }

            if (random.NextDouble() < minorProbability)
            {
                if (random.NextDouble() < evilProbability)
                    return Scales.GetRandomEvilScaleName(random);
                else
                    return Scales.GetRandomMinorScaleName(random);
            }
            else if (random.NextDouble() < evilProbability)
            {
                return Scales.GetRandomEvilScaleName(random);
            }
            else
            {
                return Scales.GetRandomMajorScaleName(random);
            }
        }
        #endregion
    }
}
