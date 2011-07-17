using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Talks about tutorial stuff
    /// </summary>
    internal class TutorialTalker
    {
        #region Fields and parts
        private HashSet<Type> listSpriteTalkedAbout;

        private SpeechSynthesizer speechSynthesizer;
        #endregion

        #region Constructor
        public TutorialTalker()
        {
            listSpriteTalkedAbout = new HashSet<Type>();
            speechSynthesizer = new SpeechSynthesizer();
        }
        #endregion

        #region Internal Methods
        internal void TryTalkAbout(AbstractSprite sprite)
        {
            if (speechSynthesizer.State != SynthesizerState.Ready)
                return;

            Type spriteType = sprite.GetType();
            if (!listSpriteTalkedAbout.Contains(spriteType))
            {
                listSpriteTalkedAbout.Add(spriteType);
                if (sprite.TutorialComment != null)
                    speechSynthesizer.SpeakAsync(sprite.TutorialComment);
            }
        }

        internal void Talk(string comment)
        {
            speechSynthesizer.SpeakAsync(comment);
        }

        internal void Reset()
        {
            listSpriteTalkedAbout.Clear();
        }
        #endregion
    }
}
