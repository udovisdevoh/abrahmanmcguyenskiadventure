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

        private static Sound kiBallSound;

        private static Sound largeKiBallSound;

        private static Sound kiChargingSound;

        private static Sound kiChargedSound;

        private static Sound ringSound;

        private static Sound loseNotesSound;

        private static Random random = new Random();

        private static Channel playerHitChannel = new Channel(0);

        private static Channel monsterHitChannel = new Channel(1);

        private static Channel powerUpChannel = new Channel(2);

        private static Channel kiChargingSoundChannel = new Channel(3);

        private static Channel breakChannel = new Channel(4);

        private static Channel playerChannel = new Channel(5);

        private static Channel timerChannel = new Channel(6);

        private static Channel beaverChannel = new Channel(7);

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
            kiBallSound = LoadSound("./assets/sounds/KiBall.ogg");
            largeKiBallSound = LoadSound("./assets/sounds/LargeKiBall.ogg");
            loseNotesSound = LoadSound("./assets/sounds/LoseNotes.ogg");
            
            ringSound = LoadSound("./assets/sounds/Ring.ogg");
            ringSound.Volume = 52;
            
            kiChargingSound = LoadSound("./assets/sounds/KiCharging.ogg");
            kiChargingSound.Volume = 52;

            kiChargedSound = LoadSound("./assets/sounds/KiCharged.ogg");
            kiChargedSound.Volume = 52;

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
            playerChannel.Play(jumpSound);
        }

        internal static void PlayJumpDownSound()
        {
            playerChannel.Play(jumpDownSound);
        }

        internal static void PlayHitSound()
        {
            monsterHitChannel.Play(hitSound);
        }

        internal static void PlayHit2Sound()
        {
            playerHitChannel.Play(hit2Sound);
        }

        internal static void PlayLoseNotesSound()
        {
            powerUpChannel.Play(loseNotesSound);
        }

        internal static void PlayKo2Sound()
        {
            playerHitChannel.Play(ko2Sound);
        }

        internal static void PlayAttemptSound()
        {
            playerChannel.Play(attemptSound);
        }

        internal static void PlayPunchSound()
        {
            playerChannel.Play(punchSound);
        }

        internal static void PlayHelmetBumpSound()
        {
            breakChannel.Play(helmetBumpSound);
        }

        internal static void PlayHelmetKickSound()
        {
            playerChannel.Play(helmetKickSound);
        }

        internal static void PlayTrampolineSound()
        {
            playerChannel.Play(trampolineSound);
        }

        internal static void PlayPowerUpSound()
        {
            powerUpChannel.Play(powerUpSound);
        }

        internal static void PlayGrowSound()
        {
            powerUpChannel.Play(growSound);
        }

        internal static void PlayFireBallSound()
        {
            playerChannel.Play(fireBallSound);
        }

        internal static void PlayBricksSound()
        {
            breakChannel.Play(bricksSound);
        }

        internal static void PlayDrinkSound()
        {
            powerUpChannel.Play(drinkSound);
        }

        internal static void PlayReggaeSound()
        {
            powerUpChannel.Play(reggaeSound);
        }

        internal static void PlayCoinSound()
        {
            powerUpChannel.Play(coinSound);
        }

        internal static void PlayExplosionSound()
        {
            breakChannel.Play(explosionSound);
        }

        internal static void PlayBombTimerSound()
        {
            timerChannel.Play(bombTimerSound);
        }

        internal static void PlayChargedSound()
        {
            kiChargingSoundChannel.Play(chargedSound);
        }

        internal static void PlayVortexOutSound()
        {
            playerChannel.Play(vortexOutSound);
        }

        internal static void PlayVortexInSound()
        {
            playerChannel.Play(vortexInSound);
        }

        internal static void PlayThrowSound()
        {
            playerChannel.Play(throwSound);
        }

        internal static void PlayGlassBreakSound()
        {
            breakChannel.Play(glassBreakSound);
        }

        internal static void PlayBeaverAttackSound()
        {
            beaverChannel.Play(beaverAttackSound);
        }

        internal static void PlayBeaverUpSound()
        {
            beaverChannel.Play(beaverUpSound);
        }

        internal static void PlayBeaverHitSound()
        {
            beaverChannel.Play(beaverHitSound);
        }

        internal static void PlayDiveInSound()
        {
            playerChannel.Play(diveInSound);
        }

        internal static void PlayDiveOutSound()
        {
            playerChannel.Play(diveOutSound);
        }

        internal static void PlayHarvestSound()
        {
            playerChannel.Play(harvestSound);
        }

        internal static void PlayGongSound()
        {
            powerUpChannel.Play(gongSound);
        }

        internal static void PlayArrowHitSound()
        {
            breakChannel.Play(arrowHitSound);
        }

        internal static void PlayGoreSound()
        {
            playerChannel.Play(gongSound);
        }

        internal static void PlaySwordSound()
        {
            playerChannel.Play(swordSound);
        }

        internal static void PlayNunchakuSound()
        {
            playerChannel.Play(nunchakuSound);
        }

        internal static void PlayCatchSound()
        {
            playerChannel.Play(catchSound);
        }

        internal static void PlayEnlightenmentSound()
        {
            powerUpChannel.Play(enlightenmentSound);
        }

        internal static void PlayKiBallSound()
        {
            playerChannel.Play(kiBallSound);
        }

        internal static void PlayLargeKiBallSound()
        {
            playerChannel.Play(largeKiBallSound);
        }

        internal static void PlayRingSound()
        {
            powerUpChannel.Play(ringSound);
        }

        internal static void PlayKiChargingSound()
        {
            kiChargingSoundChannel.Play(kiChargingSound);
        }

        internal static void PlayKiChargedSound()
        {
            kiChargingSoundChannel.Play(kiChargedSound, true);
        }

        internal static void StopKiChargingSound()
        {
            kiChargingSoundChannel.Stop();
        }
        internal static void PlayBodhiJumpSound()
        {
            bodhiJumpSoundRandomIndex = random.Next(0, 5);

            switch (bodhiJumpSoundRandomIndex)
            {
                case 1:
                    playerChannel.Play(bodhiJumpSound2);
                    break;
                case 2:
                    playerChannel.Play(bodhiJumpSound3);
                    break;
                case 3:
                    playerChannel.Play(bodhiJumpSound4);
                    break;
                case 4:
                    playerChannel.Play(bodhiJumpSound5);
                    break;
                default:
                    playerChannel.Play(bodhiJumpSound);
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
