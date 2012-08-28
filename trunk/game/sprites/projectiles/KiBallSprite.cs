using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class KiBallSprite : MonsterSprite, IPlayerProjectile, IAngleProjectile
    {
        #region Fields and parts
        private byte angleIndex = 2;

        private bool isLarge = false;

        private static string tutorialComment = "Throw the ki ball. Hold attack to charge.\nPress arrows to shoot in various\ndirections including diagonals.";

        private static Surface[,,] surfaceArray;

        private static Surface surface8aS;

        private static Surface surface8bS;

        private static Surface surface8cS;

        private static Surface surface8dS;

        private static Surface surface9aS;

        private static Surface surface9bS;

        private static Surface surface9cS;

        private static Surface surface9dS;

        private static Surface surface6aS;

        private static Surface surface6bS;

        private static Surface surface6cS;

        private static Surface surface6dS;

        private static Surface surface3aS;

        private static Surface surface3bS;

        private static Surface surface3cS;

        private static Surface surface3dS;

        private static Surface surface2aS;

        private static Surface surface2bS;

        private static Surface surface2cS;

        private static Surface surface2dS;

        private static Surface surface1aS;

        private static Surface surface1bS;

        private static Surface surface1cS;

        private static Surface surface1dS;

        private static Surface surface4aS;

        private static Surface surface4bS;

        private static Surface surface4cS;

        private static Surface surface4dS;

        private static Surface surface7aS;

        private static Surface surface7bS;

        private static Surface surface7cS;

        private static Surface surface7dS;

        private static Surface surface8aL;

        private static Surface surface8bL;

        private static Surface surface8cL;

        private static Surface surface8dL;

        private static Surface surface9aL;

        private static Surface surface9bL;

        private static Surface surface9cL;

        private static Surface surface9dL;

        private static Surface surface6aL;

        private static Surface surface6bL;

        private static Surface surface6cL;

        private static Surface surface6dL;

        private static Surface surface3aL;

        private static Surface surface3bL;

        private static Surface surface3cL;

        private static Surface surface3dL;

        private static Surface surface2aL;

        private static Surface surface2bL;

        private static Surface surface2cL;

        private static Surface surface2dL;

        private static Surface surface1aL;

        private static Surface surface1bL;

        private static Surface surface1cL;

        private static Surface surface1dL;

        private static Surface surface4aL;

        private static Surface surface4bL;

        private static Surface surface4cL;

        private static Surface surface4dL;

        private static Surface surface7aL;

        private static Surface surface7bL;

        private static Surface surface7cL;

        private static Surface surface7dL;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public KiBallSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            IsAffectedByGravity = false;
            IsCrossGrounds = true;

            if (surface8aS == null)
            {
                string resolution;
                if (Program.screenHeight > 720)
                    resolution = "1080";
                else if (Program.screenHeight > 480)
                    resolution = "720";
                else
                    resolution = "480";

                string size = "S";
                surface8aS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8a" + size + ".png");
                surface8bS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8b" + size + ".png");
                surface8cS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8c" + size + ".png");
                surface8dS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8d" + size + ".png");
                surface9aS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9a" + size + ".png");
                surface9bS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9b" + size + ".png");
                surface9cS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9c" + size + ".png");
                surface9dS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9d" + size + ".png");
                surface6aS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6a" + size + ".png");
                surface6bS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6b" + size + ".png");
                surface6cS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6c" + size + ".png");
                surface6dS = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6d" + size + ".png");
                surface3aS = surface9aS.CreateFlippedVerticalSurface();
                surface3bS = surface9bS.CreateFlippedVerticalSurface();
                surface3cS = surface9cS.CreateFlippedVerticalSurface();
                surface3dS = surface9dS.CreateFlippedVerticalSurface();
                surface2aS = surface8aS.CreateFlippedVerticalSurface();
                surface2bS = surface8bS.CreateFlippedVerticalSurface();
                surface2cS = surface8cS.CreateFlippedVerticalSurface();
                surface2dS = surface8dS.CreateFlippedVerticalSurface();
                surface1aS = surface3aS.CreateFlippedHorizontalSurface();
                surface1bS = surface3bS.CreateFlippedHorizontalSurface();
                surface1cS = surface3cS.CreateFlippedHorizontalSurface();
                surface1dS = surface3dS.CreateFlippedHorizontalSurface();
                surface4aS = surface6aS.CreateFlippedHorizontalSurface();
                surface4bS = surface6bS.CreateFlippedHorizontalSurface();
                surface4cS = surface6cS.CreateFlippedHorizontalSurface();
                surface4dS = surface6dS.CreateFlippedHorizontalSurface();
                surface7aS = surface9aS.CreateFlippedHorizontalSurface();
                surface7bS = surface9bS.CreateFlippedHorizontalSurface();
                surface7cS = surface9cS.CreateFlippedHorizontalSurface();
                surface7dS = surface9dS.CreateFlippedHorizontalSurface();

                size = "L";
                surface8aL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8a" + size + ".png");
                surface8bL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8b" + size + ".png");
                surface8cL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8c" + size + ".png");
                surface8dL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall8d" + size + ".png");
                surface9aL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9a" + size + ".png");
                surface9bL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9b" + size + ".png");
                surface9cL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9c" + size + ".png");
                surface9dL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall9d" + size + ".png");
                surface6aL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6a" + size + ".png");
                surface6bL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6b" + size + ".png");
                surface6cL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6c" + size + ".png");
                surface6dL = new Surface("./assets/rendered/" + resolution + "/kiball/KiBall6d" + size + ".png");
                surface3aL = surface9aL.CreateFlippedVerticalSurface();
                surface3bL = surface9bL.CreateFlippedVerticalSurface();
                surface3cL = surface9cL.CreateFlippedVerticalSurface();
                surface3dL = surface9dL.CreateFlippedVerticalSurface();
                surface2aL = surface8aL.CreateFlippedVerticalSurface();
                surface2bL = surface8bL.CreateFlippedVerticalSurface();
                surface2cL = surface8cL.CreateFlippedVerticalSurface();
                surface2dL = surface8dL.CreateFlippedVerticalSurface();
                surface1aL = surface3aL.CreateFlippedHorizontalSurface();
                surface1bL = surface3bL.CreateFlippedHorizontalSurface();
                surface1cL = surface3cL.CreateFlippedHorizontalSurface();
                surface1dL = surface3dL.CreateFlippedHorizontalSurface();
                surface4aL = surface6aL.CreateFlippedHorizontalSurface();
                surface4bL = surface6bL.CreateFlippedHorizontalSurface();
                surface4cL = surface6cL.CreateFlippedHorizontalSurface();
                surface4dL = surface6dL.CreateFlippedHorizontalSurface();
                surface7aL = surface9aL.CreateFlippedHorizontalSurface();
                surface7bL = surface9bL.CreateFlippedHorizontalSurface();
                surface7cL = surface9cL.CreateFlippedHorizontalSurface();
                surface7dL = surface9dL.CreateFlippedHorizontalSurface();

                surfaceArray = new Surface[2, 8, 4];
                surfaceArray[0, 0, 0] = surface8aS;
                surfaceArray[0, 0, 1] = surface8bS;
                surfaceArray[0, 0, 2] = surface8cS;
                surfaceArray[0, 0, 3] = surface8dS;
                surfaceArray[0, 1, 0] = surface9aS;
                surfaceArray[0, 1, 1] = surface9bS;
                surfaceArray[0, 1, 2] = surface9cS;
                surfaceArray[0, 1, 3] = surface9dS;
                surfaceArray[0, 2, 0] = surface6aS;
                surfaceArray[0, 2, 1] = surface6bS;
                surfaceArray[0, 2, 2] = surface6cS;
                surfaceArray[0, 2, 3] = surface6dS;
                surfaceArray[0, 3, 0] = surface3aS;
                surfaceArray[0, 3, 1] = surface3bS;
                surfaceArray[0, 3, 2] = surface3cS;
                surfaceArray[0, 3, 3] = surface3dS;
                surfaceArray[0, 4, 0] = surface2aS;
                surfaceArray[0, 4, 1] = surface2bS;
                surfaceArray[0, 4, 2] = surface2cS;
                surfaceArray[0, 4, 3] = surface2dS;
                surfaceArray[0, 5, 0] = surface1aS;
                surfaceArray[0, 5, 1] = surface1bS;
                surfaceArray[0, 5, 2] = surface1cS;
                surfaceArray[0, 5, 3] = surface1dS;
                surfaceArray[0, 6, 0] = surface4aS;
                surfaceArray[0, 6, 1] = surface4bS;
                surfaceArray[0, 6, 2] = surface4cS;
                surfaceArray[0, 6, 3] = surface4dS;
                surfaceArray[0, 7, 0] = surface7aS;
                surfaceArray[0, 7, 1] = surface7bS;
                surfaceArray[0, 7, 2] = surface7cS;
                surfaceArray[0, 7, 3] = surface7dS;
                surfaceArray[1, 0, 0] = surface8aL;
                surfaceArray[1, 0, 1] = surface8bL;
                surfaceArray[1, 0, 2] = surface8cL;
                surfaceArray[1, 0, 3] = surface8dL;
                surfaceArray[1, 1, 0] = surface9aL;
                surfaceArray[1, 1, 1] = surface9bL;
                surfaceArray[1, 1, 2] = surface9cL;
                surfaceArray[1, 1, 3] = surface9dL;
                surfaceArray[1, 2, 0] = surface6aL;
                surfaceArray[1, 2, 1] = surface6bL;
                surfaceArray[1, 2, 2] = surface6cL;
                surfaceArray[1, 2, 3] = surface6dL;
                surfaceArray[1, 3, 0] = surface3aL;
                surfaceArray[1, 3, 1] = surface3bL;
                surfaceArray[1, 3, 2] = surface3cL;
                surfaceArray[1, 3, 3] = surface3dL;
                surfaceArray[1, 4, 0] = surface2aL;
                surfaceArray[1, 4, 1] = surface2bL;
                surfaceArray[1, 4, 2] = surface2cL;
                surfaceArray[1, 4, 3] = surface2dL;
                surfaceArray[1, 5, 0] = surface1aL;
                surfaceArray[1, 5, 1] = surface1bL;
                surfaceArray[1, 5, 2] = surface1cL;
                surfaceArray[1, 5, 3] = surface1dL;
                surfaceArray[1, 6, 0] = surface4aL;
                surfaceArray[1, 6, 1] = surface4bL;
                surfaceArray[1, 6, 2] = surface4cL;
                surfaceArray[1, 6, 3] = surface4dL;
                surfaceArray[1, 7, 0] = surface7aL;
                surfaceArray[1, 7, 1] = surface7bL;
                surfaceArray[1, 7, 2] = surface7cL;
                surfaceArray[1, 7, 3] = surface7dL;
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return false;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return false;
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

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override double BuildMaxHealth()
        {
            return 100;
        }

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
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.85;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 1.15;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 15.0;
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
            return 1.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

            if (angleIndex == 0)
            {
                xOffset = 0;

                if (isLarge)
                    yOffset = 1.8;
                else
                    yOffset = 0.9;
            }
            else if (angleIndex == 4)
            {
                xOffset = 0;

                if (isLarge)
                    yOffset = -1.8;
                else
                    yOffset = -0.9;
            }
            else if (angleIndex == 2)
            {
                yOffset = 0;
                if (isLarge)
                    xOffset = -1.8;
                else
                    xOffset = -0.9;
            }
            else if (angleIndex == 6)
            {
                yOffset = 0;
                if (isLarge)
                    xOffset = 1.8;
                else
                    xOffset = 0.9;
            }
            else if (angleIndex == 1)
            {
                if (isLarge)
                {
                    xOffset = -1.38;
                    yOffset = 1.38;
                }
                else
                {
                    xOffset = -0.69;
                    yOffset = 0.69;
                }
            }
            else if (angleIndex == 3)
            {
                if (isLarge)
                {
                    xOffset = -1.38;
                    yOffset = -1.38;
                }
                else
                {
                    xOffset = -0.69;
                    yOffset = -0.69;
                }
            }
            else if (angleIndex == 5)
            {
                if (isLarge)
                {
                    xOffset = 1.38;
                    yOffset = -1.38;
                }
                else
                {
                    xOffset = 0.69;
                    yOffset = -0.69;
                }
            }
            else
            {
                if (isLarge)
                {
                    xOffset = 1.38;
                    yOffset = 1.38;
                }
                else
                {
                    xOffset = 0.69;
                    yOffset = 0.69;
                }
            }

            if (isLarge)
                return surfaceArray[1, angleIndex, cycleDivision];
            else
                return surfaceArray[0, angleIndex, cycleDivision];
        }
        #endregion

        #region Properties
        public bool IsLarge
        {
            get { return isLarge; }
            set
            {
                isLarge = value;
                if (isLarge)
                {
                    Height = 2.0;
                    Width = 2.0;
                }
                else
                {
                    Height = 1.0;
                    Width = 1.0;
                }
            }
        }

        public byte AngleIndex
        {
            get { return angleIndex; }
            set { angleIndex = value; }
        }
        #endregion
    }
}
