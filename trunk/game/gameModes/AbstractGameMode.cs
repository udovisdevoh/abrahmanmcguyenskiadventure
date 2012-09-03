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
    /// Represents game modes
    /// </summary>
    abstract class AbstractGameMode
    {
        #region Fields and parts
        protected Surface mainSurface;

        private double holeLengthMultiplicator;

        private double groundSurfaceLengthMultiplicator;

        private double monsterDensityMultiplicator;

        private bool isMusicSpeedUp;

        private bool isMushroomOverrideUpgrade;

        private bool isShowHealthBar;

        private bool isShowExp;

        private bool isTransformToBodhiWhenGetsEnoughMusicNote;
        #endregion

        #region Constructor
        public AbstractGameMode(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
            holeLengthMultiplicator = BuildHoleLengthMultiplicator();
            groundSurfaceLengthMultiplicator = BuildGroundSurfaceLengthMultiplicator();
            monsterDensityMultiplicator = BuildMonsterDensityMultiplicator();
            isMusicSpeedUp = BuildIsMusicSpeedUp();
            isMushroomOverrideUpgrade = BuildIsMushroomOverrideUpgrade();
            isShowHealthBar = BuildIsShowHealthBar();
            isTransformToBodhiWhenGetsEnoughMusicNote = BuildIsTransformToBodhiWhenGetsEnoughMusicNote();
            isShowExp = BuildIsShowExp();
        }
        #endregion

        protected abstract double BuildHoleLengthMultiplicator();

        protected abstract double BuildGroundSurfaceLengthMultiplicator();

        protected abstract double BuildMonsterDensityMultiplicator();

        protected abstract bool BuildIsMusicSpeedUp();

        protected abstract bool BuildIsMushroomOverrideUpgrade();

        protected abstract bool BuildIsShowHealthBar();

        protected abstract bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote();

        protected abstract bool BuildIsShowExp();

        public abstract void PerformDestroyMonsterExtraLogic(PlayerSprite playerSprite, MonsterSprite monsterSprite, int skillLevel);

        public abstract void HackPlayerSprite(PlayerSprite playerSprite);

        public abstract int GetExperienceNeededForLevel(int level);

        public abstract bool IsAllowShuriken(PlayerSprite playerSprite);

        public abstract bool IsAllowNunchaku(PlayerSprite playerSprite);

        public abstract bool IsAllowPunchKick(PlayerSprite playerSprite);

        public abstract bool IsAllowThrowNinjaRope(PlayerSprite playerSprite);

        public abstract bool IsAllowBodhiAirJump(PlayerSprite playerSprite);

        public abstract bool IsAllowCharge(PlayerSprite playerSprite);

        public abstract bool IsAllowAngleAttack(PlayerSprite playerSprite);

        public abstract void UpdateByFrame(double timeDelta, PlayerSprite playerSprite);

        #region Virtual Methods
        public virtual void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            if (!playerSprite.IsTiny && !playerSprite.IsNinja && !playerSprite.IsBodhi)
                ((PlayerSprite)playerSprite).ChangingSizeAnimationCycle.Fire();

            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.StopKiChargingSound();

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
                /*if (SongPlayer.IRiff != gameState.Song)
                {
                    SongPlayer.StopSync();
                    SongPlayer.IRiff = gameState.Song;
                    SongPlayer.PlayAsync();
                }*/
                //Only lose ninja status, no damage
            }
            else
            {
                playerSprite.IsTiny = true;
                playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision;
            }
        }

        public virtual void UpdateTouchMushroom(PlayerSprite playerSprite, MushroomSprite mushroomSprite)
        {
            SoundManager.PlayPowerUpSound();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            else
                playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.Health = playerSprite.MaxHealth;
            playerSprite.IsTiny = false;
            mushroomSprite.IsAlive = false;
            mushroomSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }
        #endregion

        #region Properties
        public double HoleLengthMultiplicator
        {
            get {return holeLengthMultiplicator;}
        }

        public double GroundSurfaceLengthMultiplicator
        {
            get { return groundSurfaceLengthMultiplicator; }
        }

        public double MonsterDensityMultiplicator
        {
            get { return monsterDensityMultiplicator; }
        }

        public bool IsMusicSpeedUp
        {
            get { return isMusicSpeedUp; }
        }

        public bool IsMushroomOverrideUpgrade
        {
            get { return isMushroomOverrideUpgrade; }
        }

        public bool IsShowHealthBar
        {
            get { return isShowHealthBar; }
        }

        public bool IsTransformToBodhiWhenGetsEnoughMusicNote
        {
            get { return isTransformToBodhiWhenGetsEnoughMusicNote; }
        }

        public bool IsShowExp
        {
            get { return isShowExp; }
        }
        #endregion
    }
}
