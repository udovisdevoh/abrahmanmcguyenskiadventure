﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    class ExtremeActionGameMode : AbstractGameMode
    {
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
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision * 0.3;
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

        protected override bool BuildIsAllowBodhiAirJump()
        {
            return false;
        }
    }
}
