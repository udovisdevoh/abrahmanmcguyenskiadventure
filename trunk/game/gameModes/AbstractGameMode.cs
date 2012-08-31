using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Represents game modes
    /// </summary>
    abstract class AbstractGameMode
    {
        #region Fields
        private double holeLengthMultiplicator;

        private double groundSurfaceLengthMultiplicator;

        private double monsterDensityMultiplicator;

        private bool isMusicSpeedUp;

        private bool isMushroomOverrideUpgrade;

        private bool isShowHealthBar;

        private bool isAllowBodhiAirJump;
        #endregion

        #region Constructor
        public AbstractGameMode()
        {
            holeLengthMultiplicator = BuildHoleLengthMultiplicator();
            groundSurfaceLengthMultiplicator = BuildGroundSurfaceLengthMultiplicator();
            monsterDensityMultiplicator = BuildMonsterDensityMultiplicator();
            isMusicSpeedUp = BuildIsMusicSpeedUp();
            isMushroomOverrideUpgrade = BuildIsMushroomOverrideUpgrade();
            isShowHealthBar = BuildIsShowHealthBar();
            isAllowBodhiAirJump = BuildIsAllowBodhiAirJump();
        }
        #endregion

        protected abstract double BuildHoleLengthMultiplicator();

        protected abstract double BuildGroundSurfaceLengthMultiplicator();

        protected abstract double BuildMonsterDensityMultiplicator();

        protected abstract bool BuildIsMusicSpeedUp();

        protected abstract bool BuildIsMushroomOverrideUpgrade();

        protected abstract bool BuildIsShowHealthBar();

        protected abstract bool BuildIsAllowBodhiAirJump();

        public abstract void HackPlayerSprite(PlayerSprite playerSprite);

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

        public bool IsAllowBodhiAirJump
        {
            get { return isAllowBodhiAirJump; }
        }
        #endregion

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
    }
}
