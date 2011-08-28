using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Talks about tutorial stuff
    /// </summary>
    internal static class TutorialTalker
    {
        #region Fields and parts
        private static HashSet<Type> listSpriteTalkedAbout;

        private static SpeechSynthesizer speechSynthesizer;
        #endregion

        #region Constructor
        static TutorialTalker()
        {
            listSpriteTalkedAbout = new HashSet<Type>();
            speechSynthesizer = new SpeechSynthesizer();
            Volume = PersistentConfig.VoiceVolume;
        }
        #endregion

        #region Internal Methods
        internal static void TryTalkAbout(SideScrollerSprite sprite)
        {
            if (speechSynthesizer.State != SynthesizerState.Ready)
                return;

            Type spriteType = sprite.GetType();
            if (!listSpriteTalkedAbout.Contains(spriteType))
            {
                listSpriteTalkedAbout.Add(spriteType);
                if (sprite.TutorialComment != null)
                    speechSynthesizer.SpeakAsync(sprite.TutorialComment.Replace('\n', ' '));
            }
        }

        internal static void Talk(string comment)
        {
            speechSynthesizer.SpeakAsync(comment);
        }

        internal static void Reset()
        {
            listSpriteTalkedAbout.Clear();
        }
        #endregion

        #region Properties
        public static int Volume
        {
            get { return speechSynthesizer.Volume / 10; }
            set
            {     
                if (value >= 0)
                    speechSynthesizer.Volume = value * 10;
            }
        }
        #endregion
    }
}
