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
        #region Constructor
        public RacingGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
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
            return 8.0;
        }

        protected override double BuildBlockDensityMultiplicator()
        {
            return 0.2;
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

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            playerSprite.MaxRunningSpeed = 1.2;
            playerSprite.MaxWalkingSpeed = 0.9;
            playerSprite.WalkingAcceleration = 0.012;

            playerSprite.IsTiny = false;
            playerSprite.IsNinja = true;
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.PlayHit2Sound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision;
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
            return true;
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
