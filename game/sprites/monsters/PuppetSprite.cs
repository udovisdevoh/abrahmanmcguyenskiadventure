using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Puppet
    /// </summary>
    internal class PuppetSprite : MonsterSprite, IFluctuatingSafeDistance
    {
        #region Fields and parts
        private Cycle fluctuatingSafeDistanceCycle;

        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface deadSurface;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware of coporate puppets!";
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public PuppetSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            fluctuatingSafeDistanceCycle = new Cycle(40 * random.Next(0,40), true);
            fluctuatingSafeDistanceCycle.Fire();
            if (standRight == null)
            {
                if (Program.screenHeight > 720)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/1080/puppet/puppetStand.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/1080/puppet/puppetHit.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/1080/puppet/puppetWalk.png");
                }
                else if (Program.screenHeight > 480)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/720/puppet/puppetStand.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/720/puppet/puppetHit.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/720/puppet/puppetWalk.png");
                }
                else
                {
                    standRight = BuildSpriteSurface("./assets/rendered/480/puppet/puppetStand.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/480/puppet/puppetHit.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/480/puppet/puppetWalk.png");
                }

                standLeft = standRight.CreateFlippedHorizontalSurface();                
                hitLeft = hitRight.CreateFlippedHorizontalSurface();
                walkLeft = walkRight.CreateFlippedHorizontalSurface();

                deadSurface = hitRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsEnableSpontaneousConversion()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildJumpProbability()
        {
            return 0.03;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 0.5;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.35;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.65;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 6.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            if (CurrentJumpAcceleration != 0)
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return hitRight;
                    else
                        return hitLeft;
                }

                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
                else if (cycleDivision == 3)
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return hitRight;
                        else
                            return hitLeft;
                    }

                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return standRight;
                    else
                        return standLeft;
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                    return standRight;
                else
                    return standLeft;
            }
        }
        #endregion

        #region IFluctuatingSafeDistance Members
        public Cycle FluctuatingSafeDistanceCycle
        {
            get { return fluctuatingSafeDistanceCycle; }
        }

        public double GetCurrentSafeDistance()
        {
            if (fluctuatingSafeDistanceCycle.CurrentValue > fluctuatingSafeDistanceCycle.TotalTimeLength / 2.0)
                return 6.0;
            else
                return 0.0;
        }
        #endregion
    }
}
