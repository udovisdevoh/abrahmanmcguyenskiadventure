using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.ai
{
    /// <summary>
    /// Manages monster's AI
    /// </summary>
    internal class MonsterAi
    {
        #region Internal Methods
        /// <summary>
        /// Update monster from AI
        /// </summary>
        /// <param name="monster">monster</param>
        /// <param name="player">player</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="random">random number generator</param>
        internal void Update(MonsterSprite monster, PlayerSprite player, Level level, double timeDelta, HashSet<SideScrollerSprite> visibleSpriteList, Random random)
        {
            double slope;
            monster.IsTryingToWalk = false;
            monster.IsNeedToJumpAgain = false;
            bool isFleeMode = false;
            double playerMonsterDistanceX = Math.Abs(monster.XPosition - player.XPosition);

            #region AI safe distance logic
            bool isSafeDistanceDontMove = false;
            if (monster.IsAiEnabled)
            {
                double safeDistanceAi;

                if (monster is IFluctuatingSafeDistance)
                    safeDistanceAi = ((IFluctuatingSafeDistance)monster).GetCurrentSafeDistance();
                else
                    safeDistanceAi = monster.SafeDistanceAi;

                if (playerMonsterDistanceX < safeDistanceAi)
                {
                    if (playerMonsterDistanceX > safeDistanceAi - 2.0)
                    {
                        monster.IsTryingToWalkRight = monster.XPosition < player.XPosition;
                        isSafeDistanceDontMove = true;
                    }
                    else
                        isFleeMode = true;
                }
            }
            #endregion

            #region AI Jumping logic
            if (monster.IsNoAiAlwaysBounce && monster.IGround != null)
            {
                monster.IsTryingToJump = true;
            }
            else if (monster.IsWalkEnabled && (monster.IsCanJump || monster.HitCycle.IsFired))
            {
                if (Math.Abs(monster.CurrentWalkingSpeed) < monster.WalkingAcceleration / 2.0 && !isSafeDistanceDontMove)
                    monster.IsTryingToJump = true;
                else if (TryGetSlopeRatio(monster, level, timeDelta, monster.IsTryingToWalkRight, visibleSpriteList, out slope) && (slope < -6 || (slope > 6 && monster.IsAvoidFall)))
                    monster.IsTryingToJump = true;

                if (monster is BeaverSprite && playerMonsterDistanceX < monster.Width)
                    monster.IsTryingToJump = false;

                /*if (player.IsGrounded && monster.Ground != player.Ground && monster.YPosition > player.YPosition)
                    monster.IsTryingToJump = true;*/

                if (monster.IGround == null)
                {
                    if (monster.IsInWater)
                        monster.IsTryingToJump = (random.Next(0, 15) == 0);
                    else
                        monster.IsTryingToJump = (random.Next(0, 3) == 0);
                }
                else
                {
                    if (!monster.IsTryingToJump)
                    {
                        if (random.NextDouble() <= monster.JumpProbability)
                        {
                            monster.IsTryingToJump = (random.Next(0, 40) == 0);
                        }
                    }
                }
            }
            else
            {
                monster.IsTryingToJump = false;
            }
            #endregion

            if (monster.IsAiEnabled || monster.PunchedCycle.IsFired)
            {
                #region AI walking logic
                bool wasTryingToWalkRight = monster.IsTryingToWalkRight;

                isFleeMode |= (monster.IsFleeWhenAttacked && monster.HitCycle.IsFired) || (player.YPosition < monster.YPosition && (playerMonsterDistanceX < player.Width / 2.0));

                if (monster.PunchedCycle.IsFired) //always flee after a punch
                    isFleeMode = true;

                if (Math.Abs(monster.XPosition - player.XPosition) < (0.75 * monster.Width) || isSafeDistanceDontMove)
                {
                    monster.IsTryingToWalk = false;//Too close, don't chase nor flee
                }
                else if (monster.XPosition < player.XPosition)
                {
                    monster.IsTryingToWalk = true;
                    if (isFleeMode)
                        monster.IsTryingToWalkRight = false;
                    else
                        monster.IsTryingToWalkRight = true;
                }
                else if (monster.XPosition > player.XPosition)
                {
                    monster.IsTryingToWalk = true;
                    if (isFleeMode)
                        monster.IsTryingToWalkRight = true;
                    else
                        monster.IsTryingToWalkRight = false;
                }

                #region We manage the behavior of a boo that stops when being looked at
                if (monster is IFlyingOnEqualDistance && ((IFlyingOnEqualDistance)monster).IsOnlyMoveWhenNotBeingLookedAt)
                {
                    if ((player.IsTryingToWalkRight && player.XPosition < monster.XPosition) || (!player.IsTryingToWalkRight && player.XPosition > monster.XPosition))
                    {
                        monster.IsTryingToWalk = false;
                        monster.CurrentWalkingSpeed = 0.0;
                    }
                }
                #endregion

                if (wasTryingToWalkRight != monster.IsTryingToWalkRight)
                    monster.CurrentWalkingSpeed = 0.0;

                #endregion
            }
            else
            {
                #region Walking no AI
                if (monster is IGrowable && ((IGrowable)monster).GrowthCycle.IsFired)
                {
                    monster.IsTryingToWalk = false;
                    return;
                }

                if (monster.IsWalkEnabled && Math.Abs(monster.CurrentWalkingSpeed) < monster.WalkingAcceleration / 2.0) //Change direction if can't move
                {
                    if (monster.IsNoAiChangeDirectionWhenStucked)
                    {
                        if (monster is HelmetSprite)
                            SoundManager.PlayHelmetBumpSound();

                        monster.IsNoAiDefaultDirectionWalkingRight = !monster.IsNoAiDefaultDirectionWalkingRight;
                        if (monster.IsFullSpeedAfterBounceNoAi)
                            monster.CurrentWalkingSpeed = monster.MaxWalkingSpeed;
                    }
                    else if (monster.IsNoAiDieWhenStucked)
                    {
                        if (monster is IPlayerProjectile)
                            SoundManager.PlayHelmetBumpSound();
                        else if (monster is CrystalBallSprite)
                            SoundManager.PlayGlassBreakSound();

                        monster.IsAlive = false;
                        monster.YPosition = Program.totalHeightTileCount + 1.0;
                    }
                }

                #region Some monsters should not fall in holes, they change direction instead
                if (monster.IsAvoidFall && TryGetSlopeRatio(monster, level, timeDelta, monster.IsNoAiDefaultDirectionWalkingRight, visibleSpriteList, out slope))
                {
                    if (slope > 6)//0.8)
                    {
                        monster.IsNoAiDefaultDirectionWalkingRight = !monster.IsNoAiDefaultDirectionWalkingRight;
                    }
                }
                #endregion

                monster.IsTryingToWalk = true;

                if (monster.IsNoAiChangeDirectionByCycle)
                {
                    bool wasTryingToWalkRight = monster.IsTryingToWalkRight;
                    monster.ChangeDirectionNoAiCycle.Increment(timeDelta);
                    monster.IsTryingToWalkRight = monster.ChangeDirectionNoAiCycle.GetCycleDivision(2.0) == 1;
                    if (wasTryingToWalkRight != monster.IsTryingToWalkRight)
                    {
                        monster.CurrentWalkingSpeed = 0;
                    }
                }
                else
                {
                    monster.IsTryingToWalkRight = monster.IsNoAiDefaultDirectionWalkingRight;
                }
                #endregion
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try get slope ratio for monster
        /// </summary>
        /// <param name="monster">monster</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="isWalkingRight">whether spirte is walking right</param>
        /// <param name="slope">slope ratio</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <returns>whether could get slope ratio or not</returns>
        private bool TryGetSlopeRatio(SideScrollerSprite monster, Level level, double timeDelta, bool isWalkingRight, HashSet<SideScrollerSprite> visibleSpriteList, out double slope)
        {
            slope = 0;
            IGround groundToTestSlope = monster.IGround;
            if (groundToTestSlope == null)
                groundToTestSlope = IGroundHelper.GetHighestVisibleIGroundBelowSprite(monster, level, visibleSpriteList);

            if (groundToTestSlope == null)
                return false;

            if (groundToTestSlope is SideScrollerSprite)
            {
                slope = 0.0;
                return true;
            }
            
            double walkingDistance = timeDelta * monster.CurrentWalkingSpeed;

            if (!isWalkingRight) //negative distance, (walking left)
                walkingDistance *= -1;

            slope = Physics.GetSlopeRatio(monster, groundToTestSlope, walkingDistance, isWalkingRight);

            return true;
        }
        #endregion
    }
}
