using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a DKC-like liana
    /// </summary>
    class LianaSprite : StaticSprite, IMathSprite, IClimbable
    {
        #region Constants
        private const string tutorialComment = "Use this liana, it's fun.";

        private const int surfaceCount = 50;

        private const double maxRadius = 5.0;

        private const double slope = 0.2;

        private const double power = 2.0;

        private static double cycleLength = (double)surfaceCount;
        #endregion

        #region Fields and parts
        private Cycle movementCycle;

        private static Dictionary<int, Surface> internalSurfaceCache;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public LianaSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            movementCycle = new Cycle(cycleLength, true);
            //movementCycle.Fire();

            if (internalSurfaceCache == null)
                internalSurfaceCache = new Dictionary<int, Surface>();

            if (internalSurfaceCache.Count == 0)
            {
                for (int frameId = 0; frameId <= surfaceCount; frameId++)
                {
                    Surface currentFrame = BuildCurrentFrame(frameId);
                    internalSurfaceCache.Add(frameId, currentFrame);
                }
            }
        }
        #endregion

        #region Private Methods
        private Surface BuildCurrentFrame(int frameId)
        {
            int totalHeight = (int)(Height * Program.tileSize);
            int totalWidth = (int)(Width * Program.tileSize);
            Surface surface = new Surface(totalWidth, totalHeight, Program.bitDepth);
            surface.Transparent = true;
            int ropeDiameter = Program.tileSize / 8;
            int ropeRadius = ropeDiameter / 2;

            int y = 0;
            for (double yByTile = 0; yByTile < Height; yByTile+= (1.0 / Program.tileSize))
            {
                int x = (int)Math.Round((GetXPositionAt(yByTile, frameId) + Width / 2.0) * (double)Program.tileSize);
                surface.Fill(new Rectangle(x - ropeRadius, y, ropeDiameter, 1), Color.Wheat);
                y++;
            }

            return surface;
        }
        #endregion

        #region Internal Instance Methods
        internal double GetXPositionAt(double yOnLiana)
        {
            int frameId = (int)Math.Round(movementCycle.CurrentValue);
            return GetXPositionAt(yOnLiana, frameId);
        }

        internal double GetXPositionAt(double yOnLiana, int frameId)
        {
            double multiplier = ((double)frameId - cycleLength / 2.0) / (cycleLength / 2.0) * maxRadius;
            return Math.Pow(yOnLiana * slope, power) * multiplier;
        }
        #endregion

        #region Internal Static methods
        internal static void ClearCachedSurfaces()
        {
            if (internalSurfaceCache != null)
                internalSurfaceCache.Clear();
        }
        #endregion

        #region Overrides
        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 100.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildWidth(Random random)
        {
            return 4.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 14.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return internalSurfaceCache[(int)Math.Round(movementCycle.CurrentValue)];
        }

        public bool IsPlayerNeedToWalkUpToBind
        {
            get { return false; }
        }
        #endregion

        #region IClimbable Membres
        public bool IsGrowing
        {
            get
            {
                return false;
            }
            set
            {
                throw new Exception("Can't make the liana grow");
            }
        }

        public double MaxHeight
        {
            get { return Height; }
        }

        public double GrowthSpeed
        {
            get { return 0.0; }
        }
        #endregion

        #region IMathSprite Membres
        public bool IsDetectCollision(AbstractSprite otherSprite)
        {
            if (otherSprite.TopBound > YPosition)
                return false;
            else if (otherSprite.YPosition < TopBound)
                return false;
            else if (!(otherSprite is PlayerSprite))
                return false;

            double lowestYJunction = Math.Min(otherSprite.YPosition, YPosition);
            double xPositionAt = GetXPositionAt(lowestYJunction);

            bool isXCollision = otherSprite.LeftBound <= xPositionAt && otherSprite.RightBound >= xPositionAt;
            if (isXCollision)
            {
            }
            return isXCollision;
        }
        #endregion

        #region Properties
        public Cycle MovementCycle
        {
            get { return movementCycle; }
        }
        #endregion
    }
}
