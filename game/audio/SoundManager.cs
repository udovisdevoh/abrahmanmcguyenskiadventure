using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using System.Media;
//using SdlDotNet.Audio;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Manages sound effects
    /// </summary>
    internal static class SoundManager
    {
        #region Fields and parts
        private static SoundPlayer jumpSound;

        private static SoundPlayer jumpDownSound;

        private static SoundPlayer hitSound;

        private static SoundPlayer hit2Sound;

        private static SoundPlayer ko2Sound;

        private static SoundPlayer attemptSound;

        private static SoundPlayer punchSound;

        private static SoundPlayer helmetBumpSound;
        #endregion

        #region Constructors
        static SoundManager()
        {
            jumpSound = LoadSound("./assets/sounds/Jump.wav");
            jumpDownSound = LoadSound("./assets/sounds/JumpDown.wav");
            hitSound = LoadSound("./assets/sounds/Hit.wav");
            hit2Sound = LoadSound("./assets/sounds/Hit2.wav");
            ko2Sound = LoadSound("./assets/sounds/Ko2.wav");
            attemptSound = LoadSound("./assets/sounds/Attempt.wav");
            punchSound = LoadSound("./assets/sounds/Punch.wav");
            helmetBumpSound = LoadSound("./assets/sounds/HelmetBump.wav");
        }

        /// <summary>
        /// Load sound in memory so it can be played later
        /// </summary>
        /// <param name="fileName">file</param>
        /// <returns>Loaded sound</returns>
        private static SoundPlayer LoadSound(string fileName)
        {
            SoundPlayer soundPlayer = new SoundPlayer(fileName);
            soundPlayer.Load();
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
        #endregion
    }
}
