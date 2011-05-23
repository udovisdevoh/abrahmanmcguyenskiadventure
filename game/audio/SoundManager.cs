using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using System.Media;
//using SdlDotNet.Audio;

namespace AbrahmanAdventure.audio
{
    internal static class SoundManager
    {
        #region Fields and parts
        private static SoundPlayer jumpingSound = new SoundPlayer("./assets/sounds/Jump.wav");

        private static SoundPlayer hitSound = new SoundPlayer("./assets/sounds/Hit.wav");

        private static SoundPlayer hit2Sound = new SoundPlayer("./assets/sounds/Hit2.wav");

        private static SoundPlayer koSound = new SoundPlayer("./assets/sounds/Ko.wav");

        private static SoundPlayer ko2Sound = new SoundPlayer("./assets/sounds/Ko2.wav");

        private static SoundPlayer attemptSound = new SoundPlayer("./assets/sounds/Attempt.wav");
        #endregion

        #region Constructors
        static SoundManager()
        {
            jumpingSound = LoadSound("./assets/sounds/Jump.wav");
            hitSound = LoadSound("./assets/sounds/Hit.wav");
            hit2Sound = LoadSound("./assets/sounds/Hit2.wav");
            koSound = LoadSound("./assets/sounds/Ko.wav");
            ko2Sound = LoadSound("./assets/sounds/Ko2.wav");
            attemptSound = LoadSound("./assets/sounds/Attempt.wav");
        }

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
            jumpingSound.Play();
        }

        internal static void PlayHitSound()
        {
            hitSound.Play();
        }

        internal static void PlayHit2Sound()
        {
            hit2Sound.Play();
        }

        internal static void PlayKoSound()
        {
            koSound.Play();
        }

        internal static void PlayKo2Sound()
        {
            ko2Sound.Play();
        }

        internal static void PlayAttemptSound()
        {
            attemptSound.Play();
        }
        #endregion
    }
}
