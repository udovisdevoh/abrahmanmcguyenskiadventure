using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    class AdventureRpgGameMode : AbstractGameMode
    {
        #region Constructor
        public AdventureRpgGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
        }
        #endregion

        private Surface surfaceToDrawLoadingProgress;

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
            switch (playerSprite.Level)
            {
                case 0:
                    playerSprite.IsTiny = true;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 1:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 2:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = true;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 3:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = true;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = true;
                    playerSprite.IsBodhi = false;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = true;
                    break;
            }
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
                if (playerSprite.IsTiny)
                    playerSprite.ChangingSizeAnimationCycle.Fire();
                else
                    playerSprite.PowerUpAnimationCycle.Fire();
                HackPlayerSprite(playerSprite);
            }
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            SoundManager.PlayHit2Sound();
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.StopKiChargingSound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision * 0.4;
        }

        public override void UpdateTouchMushroom(PlayerSprite playerSprite, MushroomSprite mushroomSprite)
        {
            SoundManager.PlayPowerUpSound();

            playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.Health -= mushroomSprite.AttackStrengthCollision;
            mushroomSprite.IsAlive = false;
            mushroomSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        public override int GetExperienceNeededForLevel(int level)
        {
            return (int)Math.Round(Math.Pow((double)(level + 1), 1.9) * 50.0);
        }

        public override bool IsAllowThrowBallOrShuriken(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 5 || playerSprite.Level == 3;
        }

        public override bool IsAllowNunchaku(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 4;
        }

        public override bool IsAllowPunchKick(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 0 || playerSprite.IsBeaver;
        }

        public override bool IsAllowThrowNinjaRope(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 6;
        }

        public override bool IsAllowBodhiAirJump(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 10;
        }

        public override bool IsAllowCharge(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 9;
        }

        public override bool IsAllowAngleAttack(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 8;
        }
    }
}