using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Represents the platformer game mode
    /// </summary>
    class PlatformerGameMode : AbstractGameMode
    {
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

        protected override bool BuildIsAllowBodhiAirJump()
        {
            return true;
        }

        protected override bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote()
        {
            return true;
        }

        protected override bool BuildIsShowExp()
        {
            return false;
        }

        public override void PerformKillMonsterExtraLogic(PlayerSprite playerSprite, MonsterSprite monsterSprite, int skillLevel)
        {
            //do nothing
        }

        public override int GetExperienceNeededForLevel(int level)
        {
            return 0;
        }
    }
}
