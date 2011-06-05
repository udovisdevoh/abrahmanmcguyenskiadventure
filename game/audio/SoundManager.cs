using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
//using System.Media;
using SdlDotNet.Audio;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Manages sound effects
    /// </summary>
    internal static class SoundManager
    {
        #region Fields and parts
        private static Sound jumpSound;

        private static Sound jumpDownSound;

        private static Sound hitSound;

        private static Sound hit2Sound;

        private static Sound ko2Sound;

        private static Sound attemptSound;

        private static Sound punchSound;

        private static Sound helmetBumpSound;

        private static Sound helmetKickSound;

        private static Sound trampolineSound;

        private static Sound powerUpSound;

        private static Sound growSound;

        private static Sound fireBallSound;

        private static Sound bricksSound;

        private static Sound drinkSound;

        private static Sound musicNoteSound;

        private static Sound reggaeSound;
        #endregion

        #region Constructors
        static SoundManager()
        {
            Mixer.ChannelsAllocated = 64;
            jumpSound = LoadSound("./assets/sounds/Jump.ogg");
            jumpDownSound = LoadSound("./assets/sounds/JumpDown.ogg");
            hitSound = LoadSound("./assets/sounds/Hit.ogg");
            hit2Sound = LoadSound("./assets/sounds/Hit2.ogg");
            ko2Sound = LoadSound("./assets/sounds/Ko2.ogg");
            attemptSound = LoadSound("./assets/sounds/Attempt.ogg");
            punchSound = LoadSound("./assets/sounds/Punch.ogg");
            helmetBumpSound = LoadSound("./assets/sounds/HelmetBump.ogg");
            helmetKickSound = LoadSound("./assets/sounds/HelmetKick.ogg");
            trampolineSound = LoadSound("./assets/sounds/Trampoline.ogg");
            powerUpSound = LoadSound("./assets/sounds/PowerUp.ogg");
            growSound = LoadSound("./assets/sounds/Grow.ogg");
            fireBallSound = LoadSound("./assets/sounds/FireBall.ogg");
            bricksSound = LoadSound("./assets/sounds/Bricks.ogg");
            drinkSound = LoadSound("./assets/sounds/Drink.ogg");
            musicNoteSound = LoadSound("./assets/sounds/MusicNote.ogg");
            reggaeSound = LoadSound("./assets/sounds/Reggae.ogg");
        }

        /// <summary>
        /// Load sound in memory so it can be played later
        /// </summary>
        /// <param name="fileName">file</param>
        /// <returns>Loaded sound</returns>
        private static Sound LoadSound(string fileName)
        {
            Sound soundPlayer = new Sound(fileName);
            //soundPlayer.Load();
            return soundPlayer;
        }
        #endregion

        #region Internal Methods
        internal static void PlayJumpSound()
        {
            jumpSound.Play();
        }

        internal static void PlayJumpDownSound()
        {
            jumpDownSound.Play();
        }

        internal static void PlayHitSound()
        {
            hitSound.Play();
        }

        internal static void PlayHit2Sound()
        {
            hit2Sound.Play();
        }

        internal static void PlayKo2Sound()
        {
            ko2Sound.Play();
        }

        internal static void PlayAttemptSound()
        {
            attemptSound.Play();
        }

        internal static void PlayPunchSound()
        {
            punchSound.Play();
        }

        internal static void PlayHelmetBumpSound()
        {
            helmetBumpSound.Play();
        }

        internal static void PlayHelmetKickSound()
        {
            helmetKickSound.Play();
        }

        internal static void PlayTrampolineSound()
        {
            trampolineSound.Play();
        }

        internal static void PlayPowerUpSound()
        {
            powerUpSound.Play();
        }

        internal static void PlayGrowSound()
        {
            growSound.Play();
        }

        internal static void PlayFireBallSound()
        {
            fireBallSound.Play();
        }

        internal static void PlayBricksSound()
        {
            bricksSound.Play();
        }

        internal static void PlayDrinkSound()
        {
            drinkSound.Play();
        }

        internal static void PlayMusicNoteSound()
        {
            musicNoteSound.Play();
        }

        internal static void PlayReggaeSound()
        {
            reggaeSound.Play();
        }
        #endregion
    }
}
