using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    class AdventureRpgGameMode : AbstractGameMode
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
            return true;
        }

        protected override bool BuildIsShowHealthBar()
        {
            return true;
        }

        protected override bool BuildIsAllowBodhiAirJump()
        {
            return true;
        }

        protected override bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote()
        {
            return false;
        }

        protected override bool BuildIsShowExp()
        {
            return true;
        }

        public override void PerformKillMonsterExtraLogic(PlayerSprite playerSprite, MonsterSprite monsterSprite, int skillLevel)
        {
            playerSprite.Experience += (int)Math.Round((monsterSprite.MaxHealth + monsterSprite.AttackStrengthCollision) * (double)(skillLevel + 1) * 10.0);

            while (playerSprite.Experience >= GetExperienceNeededForLevel(playerSprite.Level + 1))
            {
                playerSprite.Level++;
                SoundManager.PlayEnlightenmentSound();
            }
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            SoundManager.PlayHit2Sound();
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.StopKiChargingSound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision * 0.4;
        }

        public override int GetExperienceNeededForLevel(int level)
        {
            return (int)Math.Round(Math.Pow((double)(level + 1), 1.9) * 50.0);
        }
    }
}