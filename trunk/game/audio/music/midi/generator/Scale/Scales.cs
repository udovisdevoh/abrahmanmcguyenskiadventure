using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Enumerations of scale presets
    /// </summary>
    public static class Scales
    {
        #region Fields
        private static Scale major;

        private static Scale minor;

        private static Scale majorPentatonic;

        private static Scale minorHarmonic;

        private static Scale chinese;

        private static Scale japanese;

        private static Scale indian;

        private static Scale indonesian;

        private static Scale arabic;

        private static Scale arabicPentatonic;

        private static Scale gypsy;

        private static Scale gypsyPentatonic;

        private static Scale hungarian;

        private static Scale spanish;

        private static Scale blues;

        private static Scale hexatonic;

        private static Scale octatonic;

        private static Scale dodecaphonic;

        private static Scale minorPentatonic;

        private static Dictionary<string, Scale> scaleList;

        private static List<string> nameList = null;

        private static List<string> pentatonicNameList = null;

        private static List<string> majorScaleNameList = null;

        private static List<string> minorScaleNameList = null;

        private static List<string> evilScaleNameList = null;
        #endregion

        #region Costructors
        /// <summary>
        /// Build scales
        /// </summary>
        static Scales()
        {
            scaleList = new Dictionary<string, Scale>();

            major = new Scale();
            major.Add(2);
            major.Add(2);
            major.Add(1);
            major.Add(2);
            major.Add(2);
            major.Add(2);
            major.Add(1);
            scaleList.Add("major", major);

            minor = new Scale();
            minor.Add(2);
            minor.Add(1);
            minor.Add(2);
            minor.Add(2);
            minor.Add(1);
            minor.Add(2);
            minor.Add(2);
            scaleList.Add("minor", minor);

            minorHarmonic = new Scale();
            minorHarmonic.Add(2);
            minorHarmonic.Add(1);
            minorHarmonic.Add(2);
            minorHarmonic.Add(2);
            minorHarmonic.Add(1);
            minorHarmonic.Add(3);
            minorHarmonic.Add(1);
            scaleList.Add("minorHarmonic", minorHarmonic);

            chinese = new Scale();
            chinese.Add(3);
            chinese.Add(2);
            chinese.Add(2);
            chinese.Add(3);
            chinese.Add(2);
            scaleList.Add("chinese", chinese);

            japanese = new Scale();
            japanese.Add(3);
            japanese.Add(2);
            japanese.Add(1);
            japanese.Add(4);
            japanese.Add(2);
            scaleList.Add("japanese", japanese);

            indian = new Scale();
            indian.Add(4);
            indian.Add(1);
            indian.Add(2);
            indian.Add(3);
            indian.Add(2);
            scaleList.Add("indian", indian);

            indonesian = new Scale();
            indonesian.Add(2);
            indonesian.Add(4);
            indonesian.Add(1);
            indonesian.Add(1);
            indonesian.Add(4);
            scaleList.Add("indonesian", indonesian);

            arabic = new Scale();
            arabic.Add(1);
            arabic.Add(3);
            arabic.Add(1);
            arabic.Add(2);
            arabic.Add(1);
            arabic.Add(3);
            arabic.Add(1);
            scaleList.Add("arabic", arabic);

            arabicPentatonic = new Scale();
            arabicPentatonic.Add(1);
            arabicPentatonic.Add(3);
            arabicPentatonic.Add(1);
            arabicPentatonic.Add(2);
            arabicPentatonic.Add(1);
            scaleList.Add("arabicPentatonic", arabicPentatonic);

            gypsy = new Scale();
            gypsy.Add(2);
            gypsy.Add(1);
            gypsy.Add(3);
            gypsy.Add(1);
            gypsy.Add(1);
            gypsy.Add(2);
            gypsy.Add(2);
            scaleList.Add("gypsy", gypsy);

            gypsyPentatonic = new Scale();
            gypsyPentatonic.Add(2);
            gypsyPentatonic.Add(1);
            gypsyPentatonic.Add(3);
            gypsyPentatonic.Add(1);
            gypsyPentatonic.Add(1);
            scaleList.Add("gypsyPentatonic", gypsyPentatonic);

            hungarian = new Scale();
            hungarian.Add(2);
            hungarian.Add(1);
            hungarian.Add(3);
            hungarian.Add(1);
            hungarian.Add(1);
            hungarian.Add(3);
            hungarian.Add(1);
            scaleList.Add("hungarian", hungarian);

            spanish = new Scale();
            spanish.Add(1);
            spanish.Add(3);
            spanish.Add(1);
            spanish.Add(2);
            spanish.Add(1);
            spanish.Add(2);
            spanish.Add(2);
            scaleList.Add("spanish", spanish);

            blues = new Scale();
            blues.Add(3);
            blues.Add(2);
            blues.Add(1);
            blues.Add(1);
            blues.Add(3);
            blues.Add(2);
            scaleList.Add("blues", blues);

            hexatonic = new Scale();
            hexatonic.Add(2);
            hexatonic.Add(2);
            hexatonic.Add(2);
            hexatonic.Add(2);
            hexatonic.Add(2);
            hexatonic.Add(2);
            scaleList.Add("hexatonic", hexatonic);

            octatonic = new Scale();
            octatonic.Add(1);
            octatonic.Add(2);
            octatonic.Add(1);
            octatonic.Add(2);
            octatonic.Add(1);
            octatonic.Add(2);
            octatonic.Add(1);
            octatonic.Add(2);
            scaleList.Add("octatonic", octatonic);

            dodecaphonic = new Scale();
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            dodecaphonic.Add(1);
            scaleList.Add("dodecaphonic", dodecaphonic);

            majorPentatonic = new Scale();
            majorPentatonic.Add(2);
            majorPentatonic.Add(2);
            majorPentatonic.Add(1);
            majorPentatonic.Add(2);
            majorPentatonic.Add(2);
            scaleList.Add("majorPentatonic", majorPentatonic);

            minorPentatonic = new Scale();
            minorPentatonic.Add(2);
            minorPentatonic.Add(1);
            minorPentatonic.Add(2);
            minorPentatonic.Add(2);
            minorPentatonic.Add(1);
            scaleList.Add("minorPentatonic", minorPentatonic);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get scape
        /// </summary>
        /// <param name="name">scale's name</param>
        /// <returns>scale</returns>
        public static Scale GetScale(string name)
        {
            return scaleList[name];
        }

        /// <summary>
        /// Get name list
        /// </summary>
        /// <returns>name list</returns>
        public static List<string> GetNameList()
        {
            if (nameList == null)
                nameList = new List<string>(from name in scaleList.Keys orderby name select name);
            return nameList;
        }

        public static List<string> GetPentatonicNameList()
        {
            if (pentatonicNameList == null)
            {
                pentatonicNameList = new List<string>();
                pentatonicNameList.Add("chinese");
                pentatonicNameList.Add("japanese");
                pentatonicNameList.Add("indian");
                pentatonicNameList.Add("indonesian");
                pentatonicNameList.Add("arabicPentatonic");
                pentatonicNameList.Add("gypsyPentatonic");
                pentatonicNameList.Add("blues");
                pentatonicNameList.Add("hexatonic");
                pentatonicNameList.Add("majorPentatonic");
                pentatonicNameList.Add("minorPentatonic");
            }
            return pentatonicNameList;
        }

        private static List<string> GetMinorScaleNameList()
        {
            if (minorScaleNameList == null)
            {
                minorScaleNameList = new List<string>();
                //minorScaleNameList.Add("gypsyPentatonic");
                minorScaleNameList.Add("minorPentatonic");
            }
            return minorScaleNameList;
        }

        private static List<string> GetEvilScaleNameList()
        {
            if (evilScaleNameList == null)
            {
                evilScaleNameList = new List<string>();
                evilScaleNameList.Add("japanese");
                evilScaleNameList.Add("indonesian");
                //evilScaleNameList.Add("hexatonic");
                evilScaleNameList.Add("octatonic");
            }
            return evilScaleNameList;
        }

        private static List<string> GetMajorScaleNameList()
        {
            if (majorScaleNameList == null)
            {
                majorScaleNameList = new List<string>();
                /*majorScaleNameList.Add("chinese");
                majorScaleNameList.Add("indian");
                majorScaleNameList.Add("arabicPentatonic");
                majorScaleNameList.Add("blues");*/
                majorScaleNameList.Add("majorPentatonic");
            }
            return majorScaleNameList;
        }

        public static string GetRandomScaleName(Random random)
        {
            return GetNameList()[random.Next(GetNameList().Count)];
        }

        internal static string GetRandomPentatonicScaleName(Random random)
        {
            return GetPentatonicNameList()[random.Next(GetPentatonicNameList().Count)];
        }

        internal static string GetRandomMinorScaleName(Random random)
        {
            return GetMinorScaleNameList()[random.Next(GetMinorScaleNameList().Count)];
        }

        internal static string GetRandomEvilScaleName(Random random)
        {
            return GetEvilScaleNameList()[random.Next(GetEvilScaleNameList().Count)];
        }

        internal static string GetRandomMajorScaleName(Random random)
        {
            return GetMajorScaleNameList()[random.Next(GetMajorScaleNameList().Count)];
        }
        #endregion

        #region Properties
        /// <summary>
        /// Major
        /// </summary>
        public static Scale Major
        {
            get { return major; }
        }

        /// <summary>
        /// Minor
        /// </summary>
        public static Scale Minor
        {
            get { return minor; }
        }

        /// <summary>
        /// Minor harmonic
        /// </summary>
        public static Scale MinorHarmonic
        {
            get { return minorHarmonic; }
        }

        /// <summary>
        /// Chinese
        /// </summary>
        public static Scale Chinese
        {
            get { return chinese; }
        }

        /// <summary>
        /// Japanese
        /// </summary>
        public static Scale Japanese
        {
            get { return japanese; }
        }

        /// <summary>
        /// Indian
        /// </summary>
        public static Scale Indian
        {
            get { return indian; }
        }

        /// <summary>
        /// Indonesian
        /// </summary>
        public static Scale Indonesian
        {
            get { return indonesian; }
        }

        /// <summary>
        /// Arabic
        /// </summary>
        public static Scale Arabic
        {
            get { return arabic; }
        }

        /// <summary>
        /// Arabic pentatonic
        /// </summary>
        public static Scale ArabicPentatonic
        {
            get { return arabicPentatonic; }
        }

        /// <summary>
        /// Gypsy
        /// </summary>
        public static Scale Gypsy
        {
            get { return gypsy; }
        }

        /// <summary>
        /// Gypsy Pentatonic
        /// </summary>
        public static Scale GypsyPentatonic
        {
            get { return gypsyPentatonic; }
        }

        /// <summary>
        /// Hungarian
        /// </summary>
        public static Scale Hungarian
        {
            get { return hungarian; }
        }

        /// <summary>
        /// Spanish
        /// </summary>
        public static Scale Spanish
        {
            get { return spanish; }
        }

        /// <summary>
        /// Blues
        /// </summary>
        public static Scale Blues
        {
            get { return blues; }
        }

        /// <summary>
        /// Hexatonic
        /// </summary>
        public static Scale Hexatonic
        {
            get { return hexatonic; }
        }

        /// <summary>
        /// Octatonic
        /// </summary>
        public static Scale Octatonic
        {
            get { return octatonic; }
        }

        /// <summary>
        /// Dodecaphonic
        /// </summary>
        public static Scale Dodecaphonic
        {
            get { return dodecaphonic; }
        }

        /// <summary>
        /// Major pentatonic
        /// </summary>
        public static Scale MajorPentatonic
        {
            get { return majorPentatonic; }
        }

        /// <summary>
        /// Minor pentatonic
        /// </summary>
        public static Scale MinorPentatonic
        {
            get { return minorPentatonic; }
        }
        #endregion
    }
}
