using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using System.Media;

namespace AbrahmanAdventure.audio
{
    internal class SoundManager
    {
        #region Fields and parts
        private SoundPlayer jumpingSound = new SoundPlayer("./assets/sounds/Jump.wav");

        private SoundPlayer hitSound = new SoundPlayer("./assets/sounds/Hit.wav");
        
        private SoundPlayer koSound = new SoundPlayer("./assets/sounds/Ko.wav");
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
            if (!sprite.IsAlive && sprite.IsJustDied)
            {
                sprite.IsJustDied = false;
                koSound.Play();
            }
            else if (sprite is MonsterSprite && sprite.HitCycle.IsFirstFrame)
            {
                sprite.HitCycle.IsFirstFrame = false;
                hitSound.Play();
            }
            else if (sprite is PlayerSprite && sprite.JumpingCycle.IsFirstFrame)
            {
                sprite.JumpingCycle.IsFirstFrame = false;
                jumpingSound.Play();
            }
        }
        #endregion
    }
}
