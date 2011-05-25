using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.ai
{
    class MonsterAi
    {
        internal void Update(MonsterSprite monster, PlayerSprite player, Level level, double timeDelta, Random random)
        {
            monster.IsTryingToWalk = false;

            monster.IsNeedToJumpAgain = false;

            #region AI Jumping logic
            if (monster.IsCanJump || monster.HitCycle.IsFired)
            {
                if (Math.Abs(monster.CurrentWalkingSpeed) < monster.WalkingAcceleration / 2.0)
                    monster.IsTryingToJump = true;

                /*if (player.Ground != null && monster.Ground != player.Ground && monster.YPosition > player.YPosition)
                    monster.IsTryingToJump = true;*/

                if (monster.Ground == null)
                    monster.IsTryingToJump = (random.Next(0, 3) == 0);
                else
                {
                    if (random.NextDouble() <= monster.JumpProbability)
                    {
                        monster.IsTryingToJump = (random.Next(0, 40) == 0);
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
                bool isFleeMode = (monster.IsFleeWhenAttacked && monster.HitCycle.IsFired) || (player.YPosition < monster.YPosition && (Math.Abs(monster.XPosition - player.XPosition) < player.Width / 2.0));

                if (monster.PunchedCycle.IsFired) //always flee after a punch
                    isFleeMode = true;

                if (Math.Abs(monster.XPosition - player.XPosition) < (0.75 * monster.Width))
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
                #endregion
            }
            else
            {
                #region Walking no AI
                if (Math.Abs(monster.CurrentWalkingSpeed) < monster.WalkingAcceleration / 2.0) //Change direction if can't move
                    monster.IsNoAiDefaultDirectionWalkingRight = !monster.IsNoAiDefaultDirectionWalkingRight;

                #region Some monsters should not fall in holes, they change direction instead
                Ground groundToTestSlope = monster.Ground;
                if (groundToTestSlope == null)
                    groundToTestSlope = GroundHelper.GetHighestVisibleGroundBelowSprite(monster, level);
                if (monster.IsAvoidFall && groundToTestSlope != null)
                {
                    double walkingDistance = timeDelta * monster.CurrentWalkingSpeed;

                    if (!monster.IsNoAiDefaultDirectionWalkingRight) //negative distance, (walking left)
                        walkingDistance *= -1;

                    double slope = Physics.GetSlopeRatio(monster, groundToTestSlope, walkingDistance, monster.IsNoAiDefaultDirectionWalkingRight);
                    if (slope > 6)//0.8)
                    {
                        if (!monster.IsAiEnabled)
                            monster.IsNoAiDefaultDirectionWalkingRight = !monster.IsNoAiDefaultDirectionWalkingRight;
                        else if (monster.IsCanJump)
                            monster.IsTryingToJump = true;
                    }
                }
                #endregion

                monster.IsTryingToWalk = true;
                monster.IsTryingToWalkRight = monster.IsNoAiDefaultDirectionWalkingRight;
                #endregion
            }
        }
    }
}
