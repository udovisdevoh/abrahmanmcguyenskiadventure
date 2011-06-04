using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Content of an anarchy block
    /// </summary>
    enum BlockContent { MusicNote, Whisky, RastaHat, Peyote, Undefined };

    /// <summary>
    /// Anarchy block sprite
    /// </summary>
    internal class AnarchyBlockSprite : StaticSprite
    {
        #region Fields and parts
        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;

        private static Surface surfaceBrick;

        private Cycle blinkCycle;

        private Cycle bumpCycle;

        private bool isFinalized;

        private bool isBrick;

        private BlockContent blockContent;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public AnarchyBlockSprite(double xPosition, double yPosition, Random random)
            : this(xPosition, yPosition, random, BlockContent.Undefined, false)
        {
        }

        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="blockContent">block's content</param>
        /// <param name="isBrick">whether block looks like a breakable brick block (default:false)</param>
        public AnarchyBlockSprite(double xPosition, double yPosition, Random random, bool isBrick)
            : this(xPosition, yPosition, random, BlockContent.Undefined, isBrick)
        {
        }

        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="blockContent">block's content (default:undefined)</param>
        /// <param name="isBrick">whether block looks like a breakable brick block (default:false)</param>
        public AnarchyBlockSprite(double xPosition, double yPosition, Random random, BlockContent blockContent, bool isBrick)
            : base(xPosition, yPosition, random)
        {
            this.isBrick = isBrick;
            if (blockContent != BlockContent.Undefined)
            {
                this.blockContent = blockContent;
            }
            else
            {
                int blockContentId = random.Next(0, 7);
                if (blockContentId == 1)
                    this.blockContent = BlockContent.Whisky;
                else if (blockContentId == 2)
                    this.blockContent = BlockContent.Peyote;
                else if (blockContentId == 3)
                    this.blockContent = BlockContent.RastaHat;
                else
                    this.blockContent = BlockContent.MusicNote;
            }

            //blockContent = BlockContent.Whisky;
            isFinalized = false;

            if (surface1 == null || surface2 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock3.png");
                surfaceBrick = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock1.png");
            }

            bumpCycle = new Cycle(10, false);
            blinkCycle = new Cycle(100, true);
            blinkCycle.Fire();
        }
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            blinkCycle.Increment(1);
            bumpCycle.Increment(1);
            xOffset = 0;
            yOffset = 0;

            if (bumpCycle.IsFired)
                yOffset = bumpCycle.CurrentValue / -20.0;

            if (isFinalized)
                return surface3;

            if (isBrick)
                return surfaceBrick;

            if (blinkCycle.GetCycleDivision(2) == 0)
                return surface1;
            else
                return surface2;
        }
        #endregion

        #region Internal Methods
        internal AbstractSprite GetPowerUpSprite(AbstractSprite playerSprite, Random random)
        {
            switch (BlockContent)
            {
                case BlockContent.Whisky:
                    return new WhiskySprite(XPosition, TopBound, random);
                case BlockContent.RastaHat:
                    if (playerSprite.Health == playerSprite.MaxHealth)
                    {
                        RastaHatSprite rastaHatSprite = new RastaHatSprite(XPosition, TopBound, random);
                        return rastaHatSprite;
                    }
                    else
                    {
                        MushroomSprite mushroomSprite = new MushroomSprite(XPosition, TopBound, random);
                        mushroomSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                        return mushroomSprite;
                    }
                default:
                    if (playerSprite.Health == playerSprite.MaxHealth)
                    {
                        PeyoteSprite peyoteSprite = new PeyoteSprite(XPosition, TopBound, random);
                        return peyoteSprite;
                    }
                    else
                    {
                        MushroomSprite mushroomSprite = new MushroomSprite(XPosition, TopBound, random);
                        mushroomSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                        return mushroomSprite;
                    }
            }
        }
        #endregion

        #region Properties
        public Cycle BumpCycle
        {
            get { return bumpCycle; }
        }

        public bool IsFinalized
        {
            get { return isFinalized; }
            set { isFinalized = value; }
        }

        public BlockContent BlockContent
        {
            get { return blockContent; }
        }
        #endregion
    }
}
