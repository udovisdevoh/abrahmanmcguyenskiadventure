using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class KidSprite : MonsterSprite, IMovingGround, IHarvestable
    {
        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface dead;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "To throw the fat kid:\nJump on him, crouch, press attack,\nkeep attack pressed and release when you're ready.";
        #endregion

        #region Constructors
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public KidSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {

            if (standRight == null)
            {
                if (Program.screenHeight > 720)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/1080/kid/kidStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/1080/kid/kidWalk.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/1080/kid/kidHit.png");
                }
                else if (Program.screenHeight > 480)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/720/kid/kidStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/720/kid/kidWalk.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/720/kid/kidHit.png");
                }
                else
                {
                    standRight = BuildSpriteSurface("./assets/rendered/480/kid/kidStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/480/kid/kidWalk.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/480/kid/kidHit.png");
                }

                standLeft = standRight.CreateFlippedHorizontalSurface();
                walkLeft = walkRight.CreateFlippedHorizontalSurface();               
                hitLeft = hitRight.CreateFlippedHorizontalSurface();

                dead = hitRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Override Methods
        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 6;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.14;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.15;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.45;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 15.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.3;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.94;
        }

        protected override double BuildMaxHealth()
        {
            return 2.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return random.Next(0, 2) == 1;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 0.2;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
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

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
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

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            yOffset = 0;
            xOffset = 0;

            if (!IsAlive)
                return dead;

            if (IsDieOnTouchGround)
            {
                if (IsTryingToWalkRight)
                    return hitRight;
                else
                    return hitLeft;
            }

            if (CurrentJumpAcceleration != 0 && !HitCycle.IsFired)
            {
                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);
            if (cycleDivision == 1 || cycleDivision == 3)
            {
                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }
            else
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return hitRight;
                    else
                        return hitLeft;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return standRight;
                    else
                        return standLeft;
                }
            }
        }
        #endregion

        #region IHarvestable Membres
        public double ProjectileAttackStrengthCollision
        {
            get { return 2.0; }
        }
        #endregion
    }
}
