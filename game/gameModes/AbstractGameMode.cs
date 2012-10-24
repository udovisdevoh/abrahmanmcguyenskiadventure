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

        private double levelSizeMultiplicator;

        private double musicNoteDensityMultiplicator;

        private double blockDensityMultiplicator;

        private bool isMusicSpeedUp;

        private bool isMushroomOverrideUpgrade;

        private bool isShowHealthBar;

        private bool isShowExp;

        private bool isTransformToBodhiWhenGetsEnoughMusicNote;

        private bool isBeaverAlwaysStrongAi;

        private bool isCurvyWaveOnly;

        private bool isBoundAlwaysWall;

        private bool isShowNoteCounter;

        private bool isNoteGivesFullHealthMax99;

        private bool isNinjaFlipProportionalToWalkingSpeed;

        private bool isAllowJumpOnBeaver;
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
            isBeaverAlwaysStrongAi = BuildIsBeaverAlwaysStrongAi();
            levelSizeMultiplicator = BuildLevelSizeMultiplicator();
            isCurvyWaveOnly = BuildIsCurvyWaveOnly();
            isBoundAlwaysWall = BuildIsBoundAlwaysWall();
            musicNoteDensityMultiplicator = BuildMusicNoteDensityMultiplicator();
            blockDensityMultiplicator = BuildBlockDensityMultiplicator();
            isShowNoteCounter = BuildIsShowNoteCounter();
            isNoteGivesFullHealthMax99 = BuildIsNoteGivesFullHealthMax99();
            isNinjaFlipProportionalToWalkingSpeed = BuildIsNinjaFlipProportionalToWalkingSpeed();
            isAllowJumpOnBeaver = BuildIsAllowJumpOnBeaver();
        }
        #endregion

        protected abstract double BuildLevelSizeMultiplicator();

        protected abstract double BuildHoleLengthMultiplicator();

        protected abstract double BuildGroundSurfaceLengthMultiplicator();

        protected abstract double BuildMonsterDensityMultiplicator();

        protected abstract double BuildMusicNoteDensityMultiplicator();

        protected abstract double BuildBlockDensityMultiplicator();

        protected abstract bool BuildIsAllowJumpOnBeaver();

        protected abstract bool BuildIsCurvyWaveOnly();

        protected abstract bool BuildIsShowNoteCounter();

        protected abstract bool BuildIsMusicSpeedUp();

        protected abstract bool BuildIsMushroomOverrideUpgrade();

        protected abstract bool BuildIsShowHealthBar();

        protected abstract bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote();

        protected abstract bool BuildIsShowExp();

        protected abstract bool BuildIsBeaverAlwaysStrongAi();

        protected abstract bool BuildIsBoundAlwaysWall();

        protected abstract bool BuildIsNoteGivesFullHealthMax99();

        protected abstract bool BuildIsNinjaFlipProportionalToWalkingSpeed();

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
        public virtual void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite, SpritePopulation spritePopulation)
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

        public double LevelSizeMultiplicator
        {
            get { return levelSizeMultiplicator; }
        }

        public double MusicNoteDensityMultiplicator
        {
            get { return musicNoteDensityMultiplicator; }
        }

        public double BlockDensityMultiplicator
        {
            get { return blockDensityMultiplicator; }
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

        public bool IsBeaverAlwaysStrongAi
        {
            get { return isBeaverAlwaysStrongAi; }
        }

        public bool IsCurvyWaveOnly
        {
            get { return isCurvyWaveOnly; }
        }

        public bool IsBoundAlwaysWall
        {
            get { return isBoundAlwaysWall; }
        }

        public bool IsShowNoteCounter
        {
            get { return isShowNoteCounter; }
        }

        public bool IsNoteGivesFullHealthMax99
        {
            get { return isNoteGivesFullHealthMax99; }
        }

        public bool IsNinjaFlipProportionalToWalkingSpeed
        {
            get { return isNinjaFlipProportionalToWalkingSpeed; }
        }

        public bool IsAllowJumpOnBeaver
        {
            get { return isAllowJumpOnBeaver; }
        }
        #endregion
    }
}
