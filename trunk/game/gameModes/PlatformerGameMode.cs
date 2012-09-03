using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Represents the platformer game mode
    /// </summary>
    class PlatformerGameMode : AbstractGameMode
    {
        #region Constructor
        public PlatformerGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
        }
        #endregion

        protected override double BuildHoleLengthMultiplicator()
        {
            return 1.0;
        }

        protected override double BuildGroundSurfaceLengthMultiplicator()
        {
            return 1.0;
        }

        protected override double BuildMonsterDensityMultiplicator()
        {
            return 1.0;
        }

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            //do nothing
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

        protected override bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote()
        {
            return true;
        }

        protected override bool BuildIsShowExp()
        {
            return false;
        }

        protected override bool BuildIsAllowJumpOnBeaver()
        {
            return true;
        }

        protected override bool BuildIsBeaverAlwaysStrongAi()
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
