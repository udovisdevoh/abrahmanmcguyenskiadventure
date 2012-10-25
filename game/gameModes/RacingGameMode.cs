using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    /// <summary>
    /// This game mode is not being used
    /// </summary>
    class RacingGameMode : AbstractGameMode
    {
        #region Fields and parts
        private Random noteSpawningRandom;
        #endregion

        #region Constructor
        public RacingGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
            noteSpawningRandom = new Random();
        }
        #endregion

        protected override double BuildHoleLengthMultiplicator()
        {
            return 1.5;
        }

        protected override double BuildGroundSurfaceLengthMultiplicator()
        {
            return 3.0;
        }

        protected override double BuildMonsterDensityMultiplicator()
        {
            return 1.0;
        }

        protected override double BuildLevelSizeMultiplicator()
        {
            return 2.0;
        }

        protected override double BuildMusicNoteDensityMultiplicator()
        {
            return 4.0;
        }

        protected override double BuildBlockDensityMultiplicator()
        {
            return 0.2;
        }

        protected override bool BuildIsAllowDash()
        {
            return true;
        }

        protected override bool BuildIsNoteGivesFullHealthMax99()
        {
            return true;
        }

        protected override bool BuildIsMusicSpeedUp()
        {
            return false;
        }

        protected override bool BuildIsMushroomOverrideUpgrade()
        {
            return false;
        }

        protected override bool BuildIsShowHealthBar()
        {
            return false;
        }

        protected override bool BuildIsAllowJumpOnBeaver()
        {
            return false;
        }

        protected override bool BuildIsCurvyWaveOnly()
        {
            return true;
        }

        protected override bool BuildIsBeaverAlwaysStrongAi()
        {
            return true;
        }

        protected override bool BuildIsBoundAlwaysWall()
        {
            return true;
        }

        protected override bool BuildIsShowNoteCounter()
        {
            return true;
        }

        protected override bool BuildIsNinjaFlipProportionalToWalkingSpeed()
        {
            return true;
        }

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            playerSprite.MaxRunningSpeed = 1.2;
            playerSprite.MaxWalkingSpeed = 0.9;
            playerSprite.WalkingAcceleration = 0.012;

            playerSprite.IsTiny = false;
            playerSprite.IsNinja = true;
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite, SpritePopulation spritePopulation)
        {
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            if (playerSprite.MusicNoteCount > 0)
                SoundManager.PlayLoseNotesSound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision;

            playerSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration * 1.5;
            playerSprite.IGround = null;
            playerSprite.JumpingCycle.Fire();

            for (int musicNoteCounter = 0; musicNoteCounter < playerSprite.MusicNoteCount; musicNoteCounter++)
            {
                MusicNoteSprite musicNote = new MusicNoteSprite(playerSprite.XPosition, playerSprite.TopBound - 1.0, noteSpawningRandom);
                musicNote.ExpirationCycle.Fire();

                musicNote.ExpirationCycle.CurrentValue = noteSpawningRandom.NextDouble() * musicNote.ExpirationCycle.TotalTimeLength;

                musicNote.JumpingCycle.Fire();

                spritePopulation.Add(musicNote);

                musicNote.CurrentWalkingSpeed = noteSpawningRandom.NextDouble() * 6.0;
                musicNote.IsTryingToWalkRight = noteSpawningRandom.NextDouble() > 0.5;
                musicNote.CurrentJumpAcceleration = -(noteSpawningRandom.NextDouble() * 30.0);
                musicNote.IsAffectedByGravity = true;
                musicNote.IsCurrentlyInFreeFallX = true;
                musicNote.IsCurrentlyInFreeFallY = true;

                double xOffset = noteSpawningRandom.NextDouble() * 2.0;

                if (musicNote.IsTryingToWalkRight)
                    musicNote.XPosition += xOffset;
                else
                    musicNote.XPosition -= xOffset;
            }

            playerSprite.MusicNoteCount = 0;
        }

        protected override bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote()
        {
            return false;
        }

        protected override bool BuildIsShowExp()
        {
            return false;
        }

        public override void PerformDestroyMonsterExtraLogic(PlayerSprite playerSprite, MonsterSprite monsterSprite, int skillLevel)
        {
            //do nothing
        }

        public override int GetExperienceNeededForLevel(int level)
        {
            return 0;
        }

        public override bool IsAllowShuriken(PlayerSprite playerSprite)
        {
            return false;
        }

        public override bool IsAllowNunchaku(PlayerSprite playerSprite)
        {
            return true;
        }

        public override bool IsAllowPunchKick(PlayerSprite playerSprite)
        {
            return true;
        }

        public override bool IsAllowThrowNinjaRope(PlayerSprite playerSprite)
        {
            return true;
        }

        public override bool IsAllowBodhiAirJump(PlayerSprite playerSprite)
        {
            return true;
        }

        public override bool IsAllowCharge(PlayerSprite playerSprite)
        {
            return true;
        }

        public override bool IsAllowAngleAttack(PlayerSprite playerSprite)
        {
            return true;
        }

        public override void UpdateByFrame(double timeDelta, PlayerSprite playerSprite)
        {
            //do nothing
        }
    }
}
