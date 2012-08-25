using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
//using System.Media;
using SdlDotNet.Audio;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Manages sound effects
    /// </summary>
    internal static class SoundManager
    {
        #region Fields and parts
        private static int volume;

        private static Sound jumpSound;

        private static Sound jumpDownSound;

        private static Sound hitSound;

        private static Sound hit2Sound;

        private static Sound ko2Sound;

        private static Sound attemptSound;

        private static Sound harvestSound;

        private static Sound gongSound;

        private static Sound arrowHitSound;

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

        private static Sound coinSound;

        private static Sound explosionSound;

        private static Sound bombTimerSound;

        private static Sound chargedSound;

        private static Sound vortexInSound;

        private static Sound vortexOutSound;

        private static Sound throwSound;

        private static Sound glassBreakSound;

        private static Sound beaverAttackSound;

        private static Sound beaverUpSound;

        private static Sound beaverHitSound;

        private static Sound diveInSound;
        
        private static Sound diveOutSound;

        private static Sound goreSound;

        private static Sound swordSound;

        private static Sound nunchakuSound;

        private static Sound catchSound;

        private static Sound enlightenmentSound;

        private static Sound bodhiJumpSound;

        private static Sound bodhiJumpSound2;

        private static Sound bodhiJumpSound3;

        private static Sound bodhiJumpSound4;

        private static Sound bodhiJumpSound5;

        private static Random random = new Random();

        private static int bodhiJumpSoundRandomIndex = 0;
        #endregion

        #region Constructors
        static SoundManager()
        {
            volume = PersistentConfig.SoundVolume;
            Mixer.SetAllChannelsVolume(volume * 8);

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
            coinSound = LoadSound("./assets/sounds/Coin.ogg");
            explosionSound = LoadSound("./assets/sounds/Explosion.ogg");
            bombTimerSound = LoadSound("./assets/sounds/BombTimer.ogg");
            chargedSound = LoadSound("./assets/sounds/Charged.ogg");
            vortexInSound = LoadSound("./assets/sounds/VortexIn.ogg");
            vortexOutSound = LoadSound("./assets/sounds/VortexOut.ogg");
            throwSound = LoadSound("./assets/sounds/Throw.ogg");
            glassBreakSound = LoadSound("./assets/sounds/GlassBreak.ogg");
            beaverAttackSound = LoadSound("./assets/sounds/BeaverAttack.ogg");
            beaverUpSound = LoadSound("./assets/sounds/BeaverUp.ogg");
            beaverHitSound = LoadSound("./assets/sounds/BeaverHit.ogg");
            diveInSound = LoadSound("./assets/sounds/DiveIn.ogg");
            diveOutSound = LoadSound("./assets/sounds/DiveOut.ogg");
            harvestSound = LoadSound("./assets/sounds/Harvest.ogg");
            gongSound = LoadSound("./assets/sounds/Gong.ogg");
            arrowHitSound = LoadSound("./assets/sounds/ArrowHit.ogg");
            goreSound = LoadSound("./assets/sounds/Gore.ogg");
            swordSound = LoadSound("./assets/sounds/Sword.ogg");
            nunchakuSound = LoadSound("./assets/sounds/Nunchaku.ogg");
            enlightenmentSound = LoadSound("./assets/sounds/Enlightenment.ogg");
            bodhiJumpSound = LoadSound("./assets/sounds/BodhiJump.ogg");
            bodhiJumpSound2 = LoadSound("./assets/sounds/BodhiJump2.ogg");
            bodhiJumpSound3 = LoadSound("./assets/sounds/BodhiJump3.ogg");
            bodhiJumpSound4 = LoadSound("./assets/sounds/BodhiJump4.ogg");
            bodhiJumpSound5 = LoadSound("./assets/sounds/BodhiJump5.ogg");

            catchSound = LoadSound("./assets/sounds/Catch.ogg");
            catchSound.Volume = 56;
        }

        /// <summary>
        /// Load sound in memory so it can be played later
        /// </summary>
        /// <param name="fileName">file</param>
        /// <returns>Loaded sound</returns>
        private static Sound LoadSound(string fileName)
        {
            Sound soundPlayer = new Sound(fileName);
            /*int previousVolume = soundPlayer.Volume;
            soundPlayer.Volume = 0;
            soundPlayer.Play();
            soundPlayer.Volume = previousVolume;*/
            return soundPlayer;
        }
        #endregion

        #region Internal Methods
        internal static void PreCache()
        {
        }

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

        internal static void PlayHarvesttSound()
        {
            harvestSound.Play();
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

        internal static void PlayCoinSound()
        {
            coinSound.Play();
        }

        internal static void PlayExplosionSound()
        {
            explosionSound.Play();
        }

        internal static void PlayBombTimerSound()
        {
            bombTimerSound.Play();
        }

        internal static void PlayChargedSound()
        {
            chargedSound.Play();
        }

        internal static void PlayVortexOutSound()
        {
            vortexOutSound.Play();
        }

        internal static void PlayVortexInSound()
        {
            vortexInSound.Play();
        }

        internal static void PlayThrowSound()
        {
            throwSound.Play();
        }

        internal static void PlayGlassBreakSound()
        {
            glassBreakSound.Play();
        }

        internal static void PlayBeaverAttackSound()
        {
            beaverAttackSound.Play();
        }

        internal static void PlayBeaverUpSound()
        {
            beaverUpSound.Play();
        }

        internal static void PlayBeaverHitSound()
        {
            beaverHitSound.Play();
        }

        internal static void PlayDiveInSound()
        {
            diveInSound.Play();
        }

        internal static void PlayDiveOutSound()
        {
            diveOutSound.Play();
        }

        internal static void PlayHarvestSound()
        {
            harvestSound.Play();
        }

        internal static void PlayGongSound()
        {
            gongSound.Play();
        }

        internal static void PlayArrowHitSound()
        {
            arrowHitSound.Play();
        }

        internal static void PlayGoreSound()
        {
            goreSound.Play();
        }

        internal static void PlaySwordSound()
        {
            swordSound.Play();
        }

        internal static void PlayNunchakuSound()
        {
            nunchakuSound.Play();
        }

        internal static void PlayCatchSound()
        {
            catchSound.Play();
        }

        internal static void PlayEnlightenmentSound()
        {
            enlightenmentSound.Play();
        }

        internal static void PlayBodhiJumpSound()
        {
            bodhiJumpSoundRandomIndex = random.Next(0, 5);

            switch (bodhiJumpSoundRandomIndex)
            {
                case 1:
                    bodhiJumpSound2.Play();
                    break;
                case 2:
                    bodhiJumpSound3.Play();
                    break;
                case 3:
                    bodhiJumpSound4.Play();
                    break;
                case 4:
                    bodhiJumpSound5.Play();
                    break;
                default:
                    bodhiJumpSound.Play();
                    break;

            }
        }
        #endregion

        #region Properties
        public static int Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                Mixer.SetAllChannelsVolume(volume * 8);
            }
        }
        #endregion
    }
}
