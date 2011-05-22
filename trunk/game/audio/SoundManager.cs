using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Audio;
using AbrahmanAdventure.sprites;
using System.Media;

namespace AbrahmanAdventure.audio
{
    internal class SoundManager
    {
        #region Fields and parts
        //private Sound jumpingSound = new Sound("./assets/sounds/Jump.ogg");
        private SoundPlayer jumpingSound = new SoundPlayer("./assets/sounds/Jump.wav");
        #endregion

        #region Constructor
        public SoundManager()
        {
            Mixer.ChannelsAllocated = 64;
        }
        #endregion

        #region Internal Methods
        internal void PlaySounds(HashSet<AbstractSprite> visibleSpriteList)
        {
            foreach (AbstractSprite sprite in visibleSpriteList)
            {
                PlaySounds(sprite);
            }
        }
        #endregion

        #region Public Methods
        private void PlaySounds(AbstractSprite sprite)
        {
            if (sprite is PlayerSprite && sprite.JumpingCycle.IsFirstFrame)
            {
                jumpingSound.Play();
            }
        }
        #endregion
    }
}
