using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    /// <summary>
    /// This game mode is not being used
    /// </summary>
    class RacingGameMode : AbstractGameMode
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
            return true;
        }

        protected override bool BuildIsAllowBodhiAirJump()
        {
            return true;
        }

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            playerSprite.MaxRunningSpeed *= 2;
            playerSprite.MaxWalkingSpeed *= 2;
            playerSprite.IsTiny = false;
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();

            SoundManager.PlayHit2Sound();
            if (playerSprite.IsDoped)
                playerSprite.IsDoped = false;
            if (playerSprite.IsRasta)
                playerSprite.IsRasta = false;
            if (playerSprite.IsBodhi)
            {
                playerSprite.IsBodhi = false;
                playerSprite.IsNinja = true;
            }
            else if (playerSprite.IsNinja)
            {
                playerSprite.IsNinja = false;
            }
            else
            {
                playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision;
            }
        }
    }
}
