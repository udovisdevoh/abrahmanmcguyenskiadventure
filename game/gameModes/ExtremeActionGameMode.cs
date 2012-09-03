using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    class ExtremeActionGameMode : AbstractGameMode
    {
        #region Constructor
        public ExtremeActionGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
        }
        #endregion

        protected override double BuildHoleLengthMultiplicator()
        {
            return 0.5;
        }

        protected override double BuildGroundSurfaceLengthMultiplicator()
        {
            return 1.5;
        }

        protected override double BuildMonsterDensityMultiplicator()
        {
            return 6;
        }

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            playerSprite.IsBodhi = true;
            playerSprite.IsTiny = false;
            playerSprite.IsNinja = false;
            playerSprite.IsRasta = false;
            playerSprite.IsDoped = false;
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            SoundManager.PlayHit2Sound();
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.StopKiChargingSound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision * 0.4;
        }

        protected override bool BuildIsMusicSpeedUp()
        {
            return true;
        }

        protected override bool BuildIsMushroomOverrideUpgrade()
        {
            return true;
        }

        protected override bool BuildIsShowHealthBar()
        {
            return true;
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

        public override bool IsAllowThrowBallOrShuriken(PlayerSprite playerSprite)
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
            return false;
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
