using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a player
    /// </summary>
    internal class PlayerSprite : AbstractSprite
    {
        #region Fields and parts
        private static Surface walking1NinjaLeftSurface;

        private static Surface walking2NinjaLeftSurface;

        private static Surface walking1NinjaRightSurface;

        private static Surface walking2NinjaRightSurface;

        private static Surface walking1NinjaDopedLeftSurface;

        private static Surface walking2NinjaDopedLeftSurface;

        private static Surface walking1NinjaDopedRightSurface;

        private static Surface walking2NinjaDopedRightSurface;

        private static Surface standingNinjaLeftSurface;

        private static Surface standingNinjaRightSurface;

        private static Surface standingNinjaDopedLeftSurface;

        private static Surface standingNinjaDopedRightSurface;

        private static Surface crouchedNinjaRight;

        private static Surface crouchedNinjaLeft;

        private static Surface crouchedNinjaHitRight;

        private static Surface crouchedNinjaHitLeft;

        private static Surface crouchedNinjaDopedRight;

        private static Surface crouchedNinjaDopedLeft;

        private static Surface crouchedNinjaDopedHitRight;

        private static Surface crouchedNinjaDopedHitLeft;

        private static Surface ninjaFlipRight1;

        private static Surface ninjaFlipRight2;

        private static Surface ninjaFlipRight3;

        private static Surface ninjaFlipRight4;

        private static Surface ninjaFlipRight5;

        private static Surface ninjaFlipRight6;

        private static Surface ninjaFlipRight7;

        private static Surface ninjaFlipRight8;

        private static Surface ninjaFlipLeft1;

        private static Surface ninjaFlipLeft2;

        private static Surface ninjaFlipLeft3;

        private static Surface ninjaFlipLeft4;

        private static Surface ninjaFlipLeft5;

        private static Surface ninjaFlipLeft6;

        private static Surface ninjaFlipLeft7;

        private static Surface ninjaFlipLeft8;

        private static Surface ninjaFlipDopedRight1;

        private static Surface ninjaFlipDopedRight2;

        private static Surface ninjaFlipDopedRight3;

        private static Surface ninjaFlipDopedRight4;

        private static Surface ninjaFlipDopedRight5;

        private static Surface ninjaFlipDopedRight6;

        private static Surface ninjaFlipDopedRight7;

        private static Surface ninjaFlipDopedRight8;

        private static Surface ninjaFlipDopedLeft1;

        private static Surface ninjaFlipDopedLeft2;

        private static Surface ninjaFlipDopedLeft3;

        private static Surface ninjaFlipDopedLeft4;

        private static Surface ninjaFlipDopedLeft5;

        private static Surface ninjaFlipDopedLeft6;

        private static Surface ninjaFlipDopedLeft7;

        private static Surface ninjaFlipDopedLeft8;

        private static Surface ninjaKatanaStand1Right;

        private static Surface ninjaKatanaStand2Right;

        private static Surface ninjaKatanaStand3Right;

        private static Surface ninjaDopedKatanaStand1Right;

        private static Surface ninjaDopedKatanaStand2Right;

        private static Surface ninjaDopedKatanaStand3Right;

        private static Surface ninjaKatanaStand1Left;

        private static Surface ninjaKatanaStand2Left;

        private static Surface ninjaKatanaStand3Left;

        private static Surface ninjaDopedKatanaStand1Left;

        private static Surface ninjaDopedKatanaStand2Left;

        private static Surface ninjaDopedKatanaStand3Left;

        private static Surface ninjaKatanaJump1Right;

        private static Surface ninjaKatanaJump2Right;

        private static Surface ninjaKatanaJump3Right;

        private static Surface ninjaDopedKatanaJump1Right;

        private static Surface ninjaDopedKatanaJump2Right;

        private static Surface ninjaDopedKatanaJump3Right;

        private static Surface ninjaKatanaJump1Left;

        private static Surface ninjaKatanaJump2Left;

        private static Surface ninjaKatanaJump3Left;

        private static Surface ninjaDopedKatanaJump1Left;

        private static Surface ninjaDopedKatanaJump2Left;

        private static Surface ninjaDopedKatanaJump3Left;

        private static Surface ninjaKatanaCrouched1Right;

        private static Surface ninjaKatanaCrouched2Right;

        private static Surface ninjaKatanaCrouched3Right;

        private static Surface ninjaDopedKatanaCrouched1Right;

        private static Surface ninjaDopedKatanaCrouched2Right;

        private static Surface ninjaDopedKatanaCrouched3Right;

        private static Surface ninjaKatanaCrouched1Left;

        private static Surface ninjaKatanaCrouched2Left;

        private static Surface ninjaKatanaCrouched3Left;

        private static Surface ninjaDopedKatanaCrouched1Left;

        private static Surface ninjaDopedKatanaCrouched2Left;

        private static Surface ninjaDopedKatanaCrouched3Left;

        private static Surface ninjaDopedCrouchedHitRight;

        private static Surface ninjaDopedCrouchedHitLeft;

        private static Surface ninjaDopedHitRight;

        private static Surface ninjaDopedHitLeft;

        private static Surface ninjaCrouchedHitRight;

        private static Surface ninjaCrouchedHitLeft;

        private static Surface ninjaHitRight;

        private static Surface ninjaHitLeft;

        private static Surface ninjaNunchaku1Right;

        private static Surface ninjaNunchaku2Right;

        private static Surface ninjaNunchaku3Right;

        private static Surface ninjaNunchaku4Right;

        private static Surface ninjaNunchaku5Right;

        private static Surface ninjaNunchaku6Right;

        private static Surface ninjaNunchaku7Right;

        private static Surface ninjaNunchaku8Right;

        private static Surface ninjaCrouchedNunchaku1Right;

        private static Surface ninjaCrouchedNunchaku2Right;

        private static Surface ninjaCrouchedNunchaku3Right;

        private static Surface ninjaCrouchedNunchaku4Right;

        private static Surface ninjaCrouchedNunchaku5Right;

        private static Surface ninjaCrouchedNunchaku6Right;

        private static Surface ninjaCrouchedNunchaku7Right;

        private static Surface ninjaCrouchedNunchaku8Right;

        private static Surface ninjaDopedNunchaku1Right;

        private static Surface ninjaDopedNunchaku2Right;

        private static Surface ninjaDopedNunchaku3Right;

        private static Surface ninjaDopedNunchaku4Right;

        private static Surface ninjaDopedNunchaku5Right;

        private static Surface ninjaDopedNunchaku6Right;

        private static Surface ninjaDopedNunchaku7Right;

        private static Surface ninjaDopedNunchaku8Right;

        private static Surface ninjaDopedCrouchedNunchaku1Right;

        private static Surface ninjaDopedCrouchedNunchaku2Right;

        private static Surface ninjaDopedCrouchedNunchaku3Right;

        private static Surface ninjaDopedCrouchedNunchaku4Right;

        private static Surface ninjaDopedCrouchedNunchaku5Right;

        private static Surface ninjaDopedCrouchedNunchaku6Right;

        private static Surface ninjaDopedCrouchedNunchaku7Right;

        private static Surface ninjaDopedCrouchedNunchaku8Right;

        private static Surface ninjaNunchaku1Left;

        private static Surface ninjaNunchaku2Left;

        private static Surface ninjaNunchaku3Left;

        private static Surface ninjaNunchaku4Left;

        private static Surface ninjaNunchaku5Left;

        private static Surface ninjaNunchaku6Left;

        private static Surface ninjaNunchaku7Left;

        private static Surface ninjaNunchaku8Left;

        private static Surface ninjaCrouchedNunchaku1Left;

        private static Surface ninjaCrouchedNunchaku2Left;

        private static Surface ninjaCrouchedNunchaku3Left;

        private static Surface ninjaCrouchedNunchaku4Left;

        private static Surface ninjaCrouchedNunchaku5Left;

        private static Surface ninjaCrouchedNunchaku6Left;

        private static Surface ninjaCrouchedNunchaku7Left;

        private static Surface ninjaCrouchedNunchaku8Left;

        private static Surface ninjaDopedNunchaku1Left;

        private static Surface ninjaDopedNunchaku2Left;

        private static Surface ninjaDopedNunchaku3Left;

        private static Surface ninjaDopedNunchaku4Left;

        private static Surface ninjaDopedNunchaku5Left;

        private static Surface ninjaDopedNunchaku6Left;

        private static Surface ninjaDopedNunchaku7Left;

        private static Surface ninjaDopedNunchaku8Left;

        private static Surface ninjaDopedCrouchedNunchaku1Left;

        private static Surface ninjaDopedCrouchedNunchaku2Left;

        private static Surface ninjaDopedCrouchedNunchaku3Left;

        private static Surface ninjaDopedCrouchedNunchaku4Left;

        private static Surface ninjaDopedCrouchedNunchaku5Left;

        private static Surface ninjaDopedCrouchedNunchaku6Left;

        private static Surface ninjaDopedCrouchedNunchaku7Left;

        private static Surface ninjaDopedCrouchedNunchaku8Left;

        private static Surface walking1LeftSurface;

        private static Surface walking1LeftSurfaceRasta;

        private static Surface walking1LeftSurfaceRastaDoped;

        private static Surface walking1RightSurface;

        private static Surface walking1RightSurfaceRasta;

        private static Surface walking1RightSurfaceRastaDoped;

        private static Surface walking2LeftSurface;

        private static Surface walking2LeftSurfaceRasta;

        private static Surface walking2LeftSurfaceRastaDoped;

        private static Surface walking2RightSurface;

        private static Surface walking2RightSurfaceRasta;

        private static Surface walking2RightSurfaceRastaDoped;

        private static Surface walking1LeftSurfaceTiny;

        private static Surface walking1RightSurfaceTiny;

        private static Surface walking2LeftSurfaceTiny;

        private static Surface walking2RightSurfaceTiny;

        private static Surface walking1LeftSurfaceTinyDoped;

        private static Surface walking1RightSurfaceTinyDoped;

        private static Surface walking2LeftSurfaceTinyDoped;

        private static Surface walking2RightSurfaceTinyDoped;

        private static Surface standingLeftSurface;

        private static Surface standingLeftSurfaceRasta;

        private static Surface standingLeftSurfaceRastaDoped;

        private static Surface standingRightSurface;

        private static Surface standingRightSurfaceRasta;

        private static Surface standingRightSurfaceRastaDoped;

        private static Surface hitLeftSurface;

        private static Surface hitRightSurface;

        private static Surface hitLeftSurfaceTiny;

        private static Surface hitRightSurfaceTiny;

        private static Surface hitLeftSurfaceTinyDoped;

        private static Surface hitRightSurfaceTinyDoped;

        private static Surface crouchedRightSurface;

        private static Surface crouchedRightSurfaceRasta;

        private static Surface crouchedRightSurfaceRastaDoped;

        private static Surface crouchedLeftSurface;

        private static Surface crouchedLeftSurfaceRasta;

        private static Surface crouchedLeftSurfaceRastaDoped;

        private static Surface crouchedHitRightSurface;

        private static Surface crouchedHitLeftSurface;

        private static Surface attackFrame1RightSurface;

        private static Surface attackFrame1RightSurfaceRasta;

        private static Surface attackFrame1RightSurfaceRastaDoped;

        private static Surface attackFrame2RightSurface;

        private static Surface attackFrame2RightSurfaceRasta;

        private static Surface attackFrame2RightSurfaceRastaDoped;

        private static Surface kickFrame1RightSurface;

        private static Surface kickFrame1RightSurfaceRasta;

        private static Surface kickFrame1RightSurfaceRastaDoped;

        private static Surface kickFrame2RightSurface;

        private static Surface kickFrame2RightSurfaceRasta;

        private static Surface kickFrame2RightSurfaceRastaDoped;

        private static Surface crouchedAttackFrame1RightSurface;

        private static Surface crouchedAttackFrame1RightSurfaceRasta;

        private static Surface crouchedAttackFrame1RightSurfaceRastaDoped;

        private static Surface crouchedAttackFrame2RightSurface;

        private static Surface crouchedAttackFrame2RightSurfaceRasta;

        private static Surface crouchedAttackFrame2RightSurfaceRastaDoped;

        private static Surface attackFrame1LeftSurface;

        private static Surface attackFrame1LeftSurfaceRasta;

        private static Surface attackFrame1LeftSurfaceRastaDoped;

        private static Surface attackFrame2LeftSurface;

        private static Surface attackFrame2LeftSurfaceRasta;

        private static Surface attackFrame2LeftSurfaceRastaDoped;

        private static Surface kickFrame1LeftSurface;

        private static Surface kickFrame1LeftSurfaceRasta;

        private static Surface kickFrame1LeftSurfaceRastaDoped;

        private static Surface kickFrame2LeftSurface;

        private static Surface kickFrame2LeftSurfaceRasta;

        private static Surface kickFrame2LeftSurfaceRastaDoped;

        private static Surface crouchedAttackFrame1LeftSurface;

        private static Surface crouchedAttackFrame1LeftSurfaceRasta;

        private static Surface crouchedAttackFrame1LeftSurfaceRastaDoped;

        private static Surface crouchedAttackFrame2LeftSurface;

        private static Surface crouchedAttackFrame2LeftSurfaceRasta;

        private static Surface crouchedAttackFrame2LeftSurfaceRastaDoped;

        private static Surface deadSurface;

        private static Surface walking1LeftSurfaceDoped;

        private static Surface walking1RightSurfaceDoped;

        private static Surface walking2LeftSurfaceDoped;

        private static Surface walking2RightSurfaceDoped;

        private static Surface standingLeftSurfaceDoped;

        private static Surface standingRightSurfaceDoped;

        private static Surface hitLeftSurfaceDoped;

        private static Surface hitRightSurfaceDoped;

        private static Surface crouchedRightSurfaceDoped;

        private static Surface crouchedLeftSurfaceDoped;

        private static Surface crouchedHitRightSurfaceDoped;

        private static Surface crouchedHitLeftSurfaceDoped;

        private static Surface attackFrame1RightSurfaceDoped;

        private static Surface attackFrame2RightSurfaceDoped;

        private static Surface kickFrame1RightSurfaceDoped;

        private static Surface kickFrame2RightSurfaceDoped;

        private static Surface crouchedAttackFrame1RightSurfaceDoped;

        private static Surface crouchedAttackFrame2RightSurfaceDoped;

        private static Surface attackFrame1LeftSurfaceDoped;

        private static Surface attackFrame2LeftSurfaceDoped;

        private static Surface kickFrame1LeftSurfaceDoped;

        private static Surface kickFrame2LeftSurfaceDoped;

        private static Surface crouchedAttackFrame1LeftSurfaceDoped;

        private static Surface crouchedAttackFrame2LeftSurfaceDoped;

        private static Surface kickFrame1RightSurfaceTiny;

        private static Surface kickFrame2RightSurfaceTiny;

        private static Surface kickFrame1LeftSurfaceTiny;

        private static Surface kickFrame2LeftSurfaceTiny;

        private static Surface standingRightSurfaceTiny;

        private static Surface standingLeftSurfaceTiny;

        private static Surface attackFrame1RightSurfaceTiny;

        private static Surface attackFrame2RightSurfaceTiny;

        private static Surface attackFrame1LeftSurfaceTiny;

        private static Surface attackFrame2LeftSurfaceTiny;

        private static Surface kickFrame1RightSurfaceTinyDoped;

        private static Surface kickFrame2RightSurfaceTinyDoped;

        private static Surface kickFrame1LeftSurfaceTinyDoped;

        private static Surface kickFrame2LeftSurfaceTinyDoped;

        private static Surface standingRightSurfaceTinyDoped;

        private static Surface standingLeftSurfaceTinyDoped;

        private static Surface attackFrame1RightSurfaceTinyDoped;

        private static Surface attackFrame2RightSurfaceTinyDoped;

        private static Surface attackFrame1LeftSurfaceTinyDoped;

        private static Surface attackFrame2LeftSurfaceTinyDoped;

        private static Surface flyRightSurface;

        private static Surface flyRightSurfaceDoped;

        private static Surface flyLeftSurface;

        private static Surface flyLeftSurfaceDoped;

        private static Surface beaverStandTallRight;

        private static Surface beaverWalkTallRight;

        private static Surface beaverAttackTallRight;

        private static Surface beaverHitTallRight;

        private static Surface beaverCrouchedTallRight;

        private static Surface beaverStandTallDopedRight;

        private static Surface beaverWalkTallDopedRight;

        private static Surface beaverAttackTallDopedRight;

        private static Surface beaverHitTallDopedRight;

        private static Surface beaverCrouchedTallDopedRight;

        private static Surface beaverStandTallRastaRight;

        private static Surface beaverWalkTallRastaRight;

        private static Surface beaverAttackTallRastaRight;

        private static Surface beaverHitTallRastaRight;

        private static Surface beaverCrouchedTallRastaRight;

        private static Surface beaverStandTallRastaDopedRight;

        private static Surface beaverWalkTallRastaDopedRight;

        private static Surface beaverAttackTallRastaDopedRight;

        private static Surface beaverHitTallRastaDopedRight;

        private static Surface beaverCrouchedTallRastaDopedRight;

        private static Surface beaverStandTinyRight;

        private static Surface beaverWalkTinyRight;

        private static Surface beaverAttackTinyRight;

        private static Surface beaverHitTinyRight;

        private static Surface beaverCrouchedTinyRight;

        private static Surface beaverStandTinyDopedRight;

        private static Surface beaverWalkTinyDopedRight;

        private static Surface beaverAttackTinyDopedRight;

        private static Surface beaverHitTinyDopedRight;

        private static Surface beaverCrouchedTinyDopedRight;

        private static Surface beaverStandTallLeft;

        private static Surface beaverWalkTallLeft;

        private static Surface beaverAttackTallLeft;

        private static Surface beaverHitTallLeft;

        private static Surface beaverCrouchedTallLeft;

        private static Surface beaverStandTallDopedLeft;

        private static Surface beaverWalkTallDopedLeft;

        private static Surface beaverAttackTallDopedLeft;

        private static Surface beaverHitTallDopedLeft;

        private static Surface beaverCrouchedTallDopedLeft;

        private static Surface beaverStandTallRastaLeft;

        private static Surface beaverWalkTallRastaLeft;

        private static Surface beaverAttackTallRastaLeft;

        private static Surface beaverHitTallRastaLeft;

        private static Surface beaverCrouchedTallRastaLeft;

        private static Surface beaverStandTallRastaDopedLeft;

        private static Surface beaverStandTallRastaDopedFlyLeft;

        private static Surface beaverStandTallRastaDopedFlyRight;

        private static Surface beaverStandTallRastaFlyLeft;

        private static Surface beaverStandTallRastaFlyRight;

        private static Surface beaverWalkTallRastaDopedLeft;

        private static Surface beaverAttackTallRastaDopedLeft;

        private static Surface beaverHitTallRastaDopedLeft;

        private static Surface beaverCrouchedTallRastaDopedLeft;

        private static Surface beaverStandTinyLeft;

        private static Surface beaverWalkTinyLeft;

        private static Surface beaverAttackTinyLeft;

        private static Surface beaverHitTinyLeft;

        private static Surface beaverCrouchedTinyLeft;

        private static Surface beaverStandTinyDopedLeft;

        private static Surface beaverWalkTinyDopedLeft;

        private static Surface beaverAttackTinyDopedLeft;

        private static Surface beaverHitTinyDopedLeft;

        private static Surface beaverCrouchedTinyDopedLeft;

        private static Surface beaverStandTallNinjaRight;

        private static Surface beaverWalkTallNinjaRight;

        private static Surface beaverAttackTallNinjaRight;

        private static Surface beaverHitTallNinjaRight;

        private static Surface beaverCrouchedTallNinjaRight;

        private static Surface beaverStandTallNinjaDopedRight;

        private static Surface beaverWalkTallNinjaDopedRight;

        private static Surface beaverAttackTallNinjaDopedRight;

        private static Surface beaverHitTallNinjaDopedRight;

        private static Surface beaverCrouchedTallNinjaDopedRight;

        private static Surface beaverStandTallNinjaLeft;

        private static Surface beaverWalkTallNinjaLeft;

        private static Surface beaverAttackTallNinjaLeft;

        private static Surface beaverHitTallNinjaLeft;

        private static Surface beaverCrouchedTallNinjaLeft;

        private static Surface beaverStandTallNinjaDopedLeft;

        private static Surface beaverWalkTallNinjaDopedLeft;

        private static Surface beaverAttackTallNinjaDopedLeft;

        private static Surface beaverHitTallNinjaDopedLeft;

        private static Surface beaverCrouchedTallNinjaDopedLeft;

        private static Surface rastaFlyCrouchedRight;

        private static Surface rastaFlyCrouchedLeft;

        private static Surface rastaFlyCrouchedDopedRight;

        private static Surface rastaFlyCrouchedDopedLeft;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = null;

        private Cycle powerUpAnimationCycle;

        private Cycle changingSizeAnimationCycle;

        private Cycle throwBallCycle;

        private Cycle invincibilityCycle;

        private Cycle fromVortexCycle;

        private Cycle ninjaFlipCycle;

        private Cycle nunchakuCycle;

        /// <summary>
        /// When sprite is currently moving from one pipe to another (destination pipe)
        /// This value is normally null
        /// </summary>
        private PipeSprite destinationPipe = null;

        /// <summary>
        /// Latest beaver that was left voluntarily by ninja
        /// </summary>
        private BeaverSprite latestNinjaBeaver = null;

        /// <summary>
        /// Whether sprite can throw fire balls
        /// </summary>
        private bool isDoped = false;

        /// <summary>
        /// Whether sprite can fly
        /// </summary>
        private bool isRasta = false;

        /// <summary>
        /// Whether sprite is on beaver
        /// </summary>
        private bool isBeaver = false;

        /// <summary>
        /// Whether sprite is currently a ninja
        /// </summary>
        private bool isNinja = false;

        /// <summary>
        /// Whether sprite is currently trying to throw a fire ball
        /// </summary>
        private bool isTryThrowingBall = false;

        /// <summary>
        /// Whether sprite is currently using nunchaku
        /// </summary>
        private bool isTryUseNunchaku = false;

        /// <summary>
        /// Whether sprite is tryting to throw a ninja's rope
        /// </summary>
        private bool isTryThrowNinjaRope = false;

        /// <summary>
        /// Default health
        /// </summary>
        private double defaultHealth = 0.5;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player's sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public PlayerSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            IsTiny = true;
            Health = defaultHealth;
            powerUpAnimationCycle = new Cycle(30, false);
            changingSizeAnimationCycle = new Cycle(20, false);
            throwBallCycle = new Cycle(2.5, false);
            invincibilityCycle = new Cycle(400, false);
            fromVortexCycle = new Cycle(15, false);
            ninjaFlipCycle = new Cycle(15, true);
            nunchakuCycle = new Cycle(7, true);

            if (beaverStandTallRight == null)
            {
                string resolutionPath;

                if (Program.screenHeight > 720)
                    resolutionPath = "1080";
                else if (Program.screenHeight > 480)
                    resolutionPath = "720";
                else
                    resolutionPath = "480";

                beaverStandTallRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTall.png");
                beaverWalkTallRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTall.png");
                beaverAttackTallRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTall.png");
                beaverHitTallRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTall.png");
                beaverCrouchedTallRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTall.png");
                beaverStandTallDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTallDoped.png");
                beaverWalkTallDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallDoped.png");
                beaverAttackTallDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTallDoped.png");
                beaverHitTallDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTallDoped.png");
                beaverCrouchedTallDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTallDoped.png");
                beaverStandTallRastaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTallRasta.png");
                beaverWalkTallRastaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallRasta.png");
                beaverAttackTallRastaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTallRasta.png");
                beaverHitTallRastaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTallRasta.png");
                beaverCrouchedTallRastaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTallRasta.png");
                beaverStandTallRastaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTallRastaDoped.png");
                beaverWalkTallRastaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallRastaDoped.png");
                beaverAttackTallRastaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTallRastaDoped.png");
                beaverHitTallRastaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTallRastaDoped.png");
                beaverCrouchedTallRastaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTallRastaDoped.png");
                beaverStandTinyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTiny.png");
                beaverWalkTinyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTiny.png");
                beaverAttackTinyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTiny.png");
                beaverHitTinyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTiny.png");
                beaverCrouchedTinyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTiny.png");
                beaverStandTinyDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTinyDoped.png");
                beaverWalkTinyDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTinyDoped.png");
                beaverAttackTinyDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTinyDoped.png");
                beaverHitTinyDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTinyDoped.png");
                beaverCrouchedTinyDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTinyDoped.png");
                ninjaKatanaStand1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana1Ninja.png");
                ninjaKatanaStand2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana2Ninja.png");
                ninjaKatanaStand3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana3Ninja.png");
                ninjaDopedKatanaStand1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana1NinjaDoped.png");
                ninjaDopedKatanaStand2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana2NinjaDoped.png");
                ninjaDopedKatanaStand3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/katana3NinjaDoped.png");
                ninjaKatanaJump1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump1Ninja.png");
                ninjaKatanaJump2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump2Ninja.png");
                ninjaKatanaJump3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump3Ninja.png");
                ninjaDopedKatanaJump1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump1NinjaDoped.png");
                ninjaDopedKatanaJump2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump2NinjaDoped.png");
                ninjaDopedKatanaJump3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/swordJump3NinjaDoped.png");
                ninjaKatanaCrouched1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword1CrouchedNinja.png");
                ninjaKatanaCrouched2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword2CrouchedNinja.png");
                ninjaKatanaCrouched3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword3CrouchedNinja.png");
                ninjaDopedKatanaCrouched1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword1CrouchedNinjaDoped.png");
                ninjaDopedKatanaCrouched2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword2CrouchedNinjaDoped.png");
                ninjaDopedKatanaCrouched3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/sword3CrouchedNinjaDoped.png");
                ninjaFlipRight1 = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/flipNinja.png");
                ninjaFlipRight2 = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/flipNinja45.png");
                ninjaFlipDopedRight1 = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/flipNinjaDoped.png");
                ninjaFlipDopedRight2 = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/flipNinja45Doped.png");
                ninjaDopedCrouchedNunchaku1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku1CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku2CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku3CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku4Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku4CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku5Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku5CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku6Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku6CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku7Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku7CrouchedNinjaDoped.png");
                ninjaDopedCrouchedNunchaku8Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku8CrouchedNinjaDoped.png");
                ninjaDopedNunchaku1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku1NinjaDoped.png");
                ninjaDopedNunchaku2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku2NinjaDoped.png");
                ninjaDopedNunchaku3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku3NinjaDoped.png");
                ninjaDopedNunchaku4Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku4NinjaDoped.png");
                ninjaDopedNunchaku5Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku5NinjaDoped.png");
                ninjaDopedNunchaku6Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku6NinjaDoped.png");
                ninjaDopedNunchaku7Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku7NinjaDoped.png");
                ninjaDopedNunchaku8Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku8NinjaDoped.png");
                ninjaCrouchedNunchaku1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku1CrouchedNinja.png");
                rastaFlyCrouchedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/rastaFlyCrouched.png");
                rastaFlyCrouchedDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/rastaFlyCrouchedDoped.png");
                beaverStandTallRastaDopedFlyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallRastaDopedFly.png");
                beaverStandTallRastaFlyRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallRastaFly.png");
                beaverStandTallNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTallNinja.png");
                beaverWalkTallNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallNinja.png");
                beaverAttackTallNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTallNinja.png");
                beaverHitTallNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTallNinja.png");
                beaverCrouchedTallNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTallNinja.png");
                beaverStandTallNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverStandStandTallNinjaDoped.png");
                beaverWalkTallNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverWalkStandTallNinjaDoped.png");
                beaverAttackTallNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverAttackStandTallNinjaDoped.png");
                beaverHitTallNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverHitStandTallNinjaDoped.png");
                beaverCrouchedTallNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/beaver/BeaverCrouchedStandTallNinjaDoped.png");
                ninjaDopedCrouchedHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedHitNinjaDoped.png");
                ninjaDopedHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/hitNinjaDoped.png");
                ninjaCrouchedHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedHitNinja.png");
                ninjaHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/hitNinja.png");
                ninjaCrouchedNunchaku2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku2CrouchedNinja.png");
                ninjaCrouchedNunchaku3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku3CrouchedNinja.png");
                ninjaCrouchedNunchaku4Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku4CrouchedNinja.png");
                ninjaCrouchedNunchaku5Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku5CrouchedNinja.png");
                ninjaCrouchedNunchaku6Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku6CrouchedNinja.png");
                ninjaCrouchedNunchaku7Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku7CrouchedNinja.png");
                ninjaCrouchedNunchaku8Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku8CrouchedNinja.png");
                ninjaNunchaku1Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku1Ninja.png");
                ninjaNunchaku2Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku2Ninja.png");
                ninjaNunchaku3Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku3Ninja.png");
                ninjaNunchaku4Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku4Ninja.png");
                ninjaNunchaku5Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku5Ninja.png");
                ninjaNunchaku6Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku6Ninja.png");
                ninjaNunchaku7Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku7Ninja.png");
                ninjaNunchaku8Right = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/nunchaku8Ninja.png");
                walking1NinjaRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/walk1Ninja.png");
                walking2NinjaRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/walk2Ninja.png");
                walking1NinjaDopedRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/walk1NinjaDoped.png");
                walking2NinjaDopedRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/walk2NinjaDoped.png");
                crouchedNinjaRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedNinja.png");
                crouchedNinjaHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedHitNinja.png");
                crouchedNinjaDopedRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedNinjaDoped.png");
                crouchedNinjaDopedHitRight = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/crouchedHitNinjaDoped.png");
                standingNinjaRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/standNinja.png");
                standingNinjaDopedRightSurface = BuildSpriteSurface("./assets/rendered/" + resolutionPath + "/abrahman/standNinjaDoped.png");

                beaverStandTallLeft = beaverStandTallRight.CreateFlippedHorizontalSurface();
                beaverWalkTallLeft = beaverWalkTallRight.CreateFlippedHorizontalSurface();
                beaverAttackTallLeft = beaverAttackTallRight.CreateFlippedHorizontalSurface();
                beaverHitTallLeft = beaverHitTallRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallLeft = beaverCrouchedTallRight.CreateFlippedHorizontalSurface();
                beaverStandTallDopedLeft = beaverStandTallDopedRight.CreateFlippedHorizontalSurface();
                beaverWalkTallDopedLeft = beaverWalkTallDopedRight.CreateFlippedHorizontalSurface();
                beaverAttackTallDopedLeft = beaverAttackTallDopedRight.CreateFlippedHorizontalSurface();
                beaverHitTallDopedLeft = beaverHitTallDopedRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallDopedLeft = beaverCrouchedTallDopedRight.CreateFlippedHorizontalSurface();
                beaverStandTallRastaLeft = beaverStandTallRastaRight.CreateFlippedHorizontalSurface();
                beaverWalkTallRastaLeft = beaverWalkTallRastaRight.CreateFlippedHorizontalSurface();
                beaverAttackTallRastaLeft = beaverAttackTallRastaRight.CreateFlippedHorizontalSurface();
                beaverHitTallRastaLeft = beaverHitTallRastaRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallRastaLeft = beaverCrouchedTallRastaRight.CreateFlippedHorizontalSurface();
                beaverStandTallRastaDopedLeft = beaverStandTallRastaDopedRight.CreateFlippedHorizontalSurface();
                beaverWalkTallRastaDopedLeft = beaverWalkTallRastaDopedRight.CreateFlippedHorizontalSurface();
                beaverAttackTallRastaDopedLeft = beaverAttackTallRastaDopedRight.CreateFlippedHorizontalSurface();
                beaverHitTallRastaDopedLeft = beaverHitTallRastaDopedRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallRastaDopedLeft = beaverCrouchedTallRastaDopedRight.CreateFlippedHorizontalSurface();
                beaverStandTinyLeft = beaverStandTinyRight.CreateFlippedHorizontalSurface();
                beaverWalkTinyLeft = beaverWalkTinyRight.CreateFlippedHorizontalSurface();
                beaverAttackTinyLeft = beaverAttackTinyRight.CreateFlippedHorizontalSurface();
                beaverHitTinyLeft = beaverHitTinyRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTinyLeft = beaverCrouchedTinyRight.CreateFlippedHorizontalSurface();
                beaverStandTinyDopedLeft = beaverStandTinyDopedRight.CreateFlippedHorizontalSurface();
                beaverWalkTinyDopedLeft = beaverWalkTinyDopedRight.CreateFlippedHorizontalSurface();
                beaverAttackTinyDopedLeft = beaverAttackTinyDopedRight.CreateFlippedHorizontalSurface();
                beaverHitTinyDopedLeft = beaverHitTinyDopedRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTinyDopedLeft = beaverCrouchedTinyDopedRight.CreateFlippedHorizontalSurface();
                beaverStandTallRastaFlyLeft = beaverStandTallRastaFlyRight.CreateFlippedHorizontalSurface();
                beaverStandTallRastaDopedFlyLeft = beaverStandTallRastaDopedFlyRight.CreateFlippedHorizontalSurface();
                rastaFlyCrouchedDopedLeft = rastaFlyCrouchedDopedRight.CreateFlippedHorizontalSurface();
                rastaFlyCrouchedLeft = rastaFlyCrouchedRight.CreateFlippedHorizontalSurface();
                beaverStandTallNinjaLeft = beaverStandTallNinjaRight.CreateFlippedHorizontalSurface();
                beaverWalkTallNinjaLeft = beaverWalkTallNinjaRight.CreateFlippedHorizontalSurface();
                beaverAttackTallNinjaLeft = beaverAttackTallNinjaRight.CreateFlippedHorizontalSurface();
                beaverHitTallNinjaLeft = beaverHitTallNinjaRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallNinjaLeft = beaverCrouchedTallNinjaRight.CreateFlippedHorizontalSurface();
                beaverStandTallNinjaDopedLeft = beaverStandTallNinjaDopedRight.CreateFlippedHorizontalSurface();
                beaverWalkTallNinjaDopedLeft = beaverWalkTallNinjaDopedRight.CreateFlippedHorizontalSurface();
                beaverAttackTallNinjaDopedLeft = beaverAttackTallNinjaDopedRight.CreateFlippedHorizontalSurface();
                beaverHitTallNinjaDopedLeft = beaverHitTallNinjaDopedRight.CreateFlippedHorizontalSurface();
                beaverCrouchedTallNinjaDopedLeft = beaverCrouchedTallNinjaDopedRight.CreateFlippedHorizontalSurface();
                walking1NinjaLeftSurface = walking1NinjaRightSurface.CreateFlippedHorizontalSurface();
                walking2NinjaLeftSurface = walking2NinjaRightSurface.CreateFlippedHorizontalSurface();
                walking1NinjaDopedLeftSurface = walking1NinjaDopedRightSurface.CreateFlippedHorizontalSurface();
                walking2NinjaDopedLeftSurface = walking2NinjaDopedRightSurface.CreateFlippedHorizontalSurface();
                crouchedNinjaLeft = crouchedNinjaRight.CreateFlippedHorizontalSurface();
                crouchedNinjaHitLeft = crouchedNinjaHitRight.CreateFlippedHorizontalSurface();
                crouchedNinjaDopedLeft = crouchedNinjaDopedRight.CreateFlippedHorizontalSurface();
                crouchedNinjaDopedHitLeft = crouchedNinjaDopedHitRight.CreateFlippedHorizontalSurface();
                standingNinjaLeftSurface = standingNinjaRightSurface.CreateFlippedHorizontalSurface();
                standingNinjaDopedLeftSurface = standingNinjaDopedRightSurface.CreateFlippedHorizontalSurface();
                ninjaFlipRight3 = ninjaFlipRight1.CreateRotatedSurface(270);
                ninjaFlipRight4 = ninjaFlipRight2.CreateRotatedSurface(270);
                ninjaFlipRight5 = ninjaFlipRight1.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipRight6 = ninjaFlipRight2.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipRight7 = ninjaFlipRight3.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipRight8 = ninjaFlipRight4.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipDopedRight3 = ninjaFlipDopedRight1.CreateRotatedSurface(270);
                ninjaFlipDopedRight4 = ninjaFlipDopedRight2.CreateRotatedSurface(270);
                ninjaFlipDopedRight5 = ninjaFlipDopedRight1.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipDopedRight6 = ninjaFlipDopedRight2.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipDopedRight7 = ninjaFlipDopedRight3.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipDopedRight8 = ninjaFlipDopedRight4.CreateFlippedHorizontalSurface().CreateFlippedVerticalSurface();
                ninjaFlipLeft1 = ninjaFlipRight1.CreateFlippedHorizontalSurface();
                ninjaFlipLeft2 = ninjaFlipRight2.CreateFlippedHorizontalSurface();
                ninjaFlipLeft3 = ninjaFlipRight3.CreateFlippedHorizontalSurface();
                ninjaFlipLeft4 = ninjaFlipRight4.CreateFlippedHorizontalSurface();
                ninjaFlipLeft5 = ninjaFlipRight5.CreateFlippedHorizontalSurface();
                ninjaFlipLeft6 = ninjaFlipRight6.CreateFlippedHorizontalSurface();
                ninjaFlipLeft7 = ninjaFlipRight7.CreateFlippedHorizontalSurface();
                ninjaFlipLeft8 = ninjaFlipRight8.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft1 = ninjaFlipDopedRight1.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft2 = ninjaFlipDopedRight2.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft3 = ninjaFlipDopedRight3.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft4 = ninjaFlipDopedRight4.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft5 = ninjaFlipDopedRight5.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft6 = ninjaFlipDopedRight6.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft7 = ninjaFlipDopedRight7.CreateFlippedHorizontalSurface();
                ninjaFlipDopedLeft8 = ninjaFlipDopedRight8.CreateFlippedHorizontalSurface();
                ninjaKatanaStand1Left = ninjaKatanaStand1Right.CreateFlippedHorizontalSurface();
                ninjaKatanaStand2Left = ninjaKatanaStand2Right.CreateFlippedHorizontalSurface();
                ninjaKatanaStand3Left = ninjaKatanaStand3Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaStand1Left = ninjaDopedKatanaStand1Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaStand2Left = ninjaDopedKatanaStand2Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaStand3Left = ninjaDopedKatanaStand3Right.CreateFlippedHorizontalSurface();
                ninjaKatanaJump1Left = ninjaKatanaJump1Right.CreateFlippedHorizontalSurface();
                ninjaKatanaJump2Left = ninjaKatanaJump2Right.CreateFlippedHorizontalSurface();
                ninjaKatanaJump3Left = ninjaKatanaJump3Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaJump1Left = ninjaDopedKatanaJump1Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaJump2Left = ninjaDopedKatanaJump2Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaJump3Left = ninjaDopedKatanaJump3Right.CreateFlippedHorizontalSurface();
                ninjaKatanaCrouched1Left = ninjaKatanaCrouched1Right.CreateFlippedHorizontalSurface();
                ninjaKatanaCrouched2Left = ninjaKatanaCrouched2Right.CreateFlippedHorizontalSurface();
                ninjaKatanaCrouched3Left = ninjaKatanaCrouched3Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaCrouched1Left = ninjaDopedKatanaCrouched1Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaCrouched2Left = ninjaDopedKatanaCrouched2Right.CreateFlippedHorizontalSurface();
                ninjaDopedKatanaCrouched3Left = ninjaDopedKatanaCrouched3Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedHitLeft = ninjaDopedCrouchedHitRight.CreateFlippedHorizontalSurface();
                ninjaDopedHitLeft = ninjaDopedHitRight.CreateFlippedHorizontalSurface();
                ninjaCrouchedHitLeft = ninjaCrouchedHitRight.CreateFlippedHorizontalSurface();
                ninjaHitLeft = ninjaHitRight.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku1Left = ninjaDopedCrouchedNunchaku1Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku2Left = ninjaDopedCrouchedNunchaku2Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku3Left = ninjaDopedCrouchedNunchaku3Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku4Left = ninjaDopedCrouchedNunchaku4Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku5Left = ninjaDopedCrouchedNunchaku5Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku6Left = ninjaDopedCrouchedNunchaku6Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku7Left = ninjaDopedCrouchedNunchaku7Right.CreateFlippedHorizontalSurface();
                ninjaDopedCrouchedNunchaku8Left = ninjaDopedCrouchedNunchaku8Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku1Left = ninjaDopedNunchaku1Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku2Left = ninjaDopedNunchaku2Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku3Left = ninjaDopedNunchaku3Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku4Left = ninjaDopedNunchaku4Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku5Left = ninjaDopedNunchaku5Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku6Left = ninjaDopedNunchaku6Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku7Left = ninjaDopedNunchaku7Right.CreateFlippedHorizontalSurface();
                ninjaDopedNunchaku8Left = ninjaDopedNunchaku8Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku1Left = ninjaCrouchedNunchaku1Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku2Left = ninjaCrouchedNunchaku2Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku3Left = ninjaCrouchedNunchaku3Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku4Left = ninjaCrouchedNunchaku4Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku5Left = ninjaCrouchedNunchaku5Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku6Left = ninjaCrouchedNunchaku6Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku7Left = ninjaCrouchedNunchaku7Right.CreateFlippedHorizontalSurface();
                ninjaCrouchedNunchaku8Left = ninjaCrouchedNunchaku8Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku1Left = ninjaNunchaku1Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku2Left = ninjaNunchaku2Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku3Left = ninjaNunchaku3Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku4Left = ninjaNunchaku4Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku5Left = ninjaNunchaku5Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku6Left = ninjaNunchaku6Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku7Left = ninjaNunchaku7Right.CreateFlippedHorizontalSurface();
                ninjaNunchaku8Left = ninjaNunchaku8Right.CreateFlippedHorizontalSurface();
            }

            #region We preload the textures that use lazy initialization
            GetDeadSurface();

            GetWalking1RightSurface(false, false, false);
            GetWalking1RightSurface(true, false, false);
            GetWalking1RightSurface(false, true, false);
            GetWalking1RightSurface(true, true, false);
            GetWalking2RightSurface(false, false, false);
            GetWalking2RightSurface(true, false, false);
            GetWalking2RightSurface(false, true, false);
            GetWalking2RightSurface(true, true, false);
            GetWalking1LeftSurface(false, false,false);
            GetWalking1LeftSurface(true, false, false);
            GetWalking1LeftSurface(false, true, false);
            GetWalking1LeftSurface(true, true, false);
            GetWalking2LeftSurface(false, false, false);
            GetWalking2LeftSurface(true, false, false);
            GetWalking2LeftSurface(false, true, false);
            GetWalking2LeftSurface(true, true, false);

            GetStandingRightSurface(false, false, false);
            GetStandingRightSurface(false, true, false);
            GetStandingRightSurface(true, false, false);
            GetStandingRightSurface(true, true, false);
            GetStandingLeftSurface(false, false, false);
            GetStandingLeftSurface(false, true, false);
            GetStandingLeftSurface(true, false, false);
            GetStandingLeftSurface(true, true, false);

            GetCrouchedLeftSurface(false, false, false);
            GetCrouchedLeftSurface(false, true, false);
            GetCrouchedLeftSurface(true, false, false);
            GetCrouchedLeftSurface(true, true, false);
            GetCrouchedRightSurface(false, false, false);
            GetCrouchedRightSurface(false, true, false);
            GetCrouchedRightSurface(true, false, false);
            GetCrouchedRightSurface(true, true, false);

            GetCrouchedHitRightSurface(false);
            GetCrouchedHitRightSurface(true);
            GetCrouchedHitLeftSurface(false);
            GetCrouchedHitLeftSurface(true);

            GetHitRightSurface(false);
            GetHitRightSurface(true);
            GetHitLeftSurface(false);
            GetHitLeftSurface(true);

            GetAttackFrame1RightSurface(false, false);
            GetAttackFrame1RightSurface(false, true);
            GetAttackFrame1RightSurface(true, false);
            GetAttackFrame1RightSurface(true, true);
            GetAttackFrame2RightSurface(false, false);
            GetAttackFrame2RightSurface(false, true);
            GetAttackFrame2RightSurface(true, false);
            GetAttackFrame2RightSurface(true, true);
            GetAttackFrame1LeftSurface(false, false);
            GetAttackFrame1LeftSurface(false, true);
            GetAttackFrame1LeftSurface(true, false);
            GetAttackFrame1LeftSurface(true, true);
            GetAttackFrame2LeftSurface(false, false);
            GetAttackFrame2LeftSurface(false, true);
            GetAttackFrame2LeftSurface(true, false);
            GetAttackFrame2LeftSurface(true, true);

            GetCrouchedAttackFrame1RightSurface(false, false);
            GetCrouchedAttackFrame1RightSurface(true, false);
            GetCrouchedAttackFrame1RightSurface(false, true);
            GetCrouchedAttackFrame1RightSurface(true, true);
            GetCrouchedAttackFrame1LeftSurface(false, false);
            GetCrouchedAttackFrame1LeftSurface(true, false);
            GetCrouchedAttackFrame1LeftSurface(false, true);
            GetCrouchedAttackFrame1LeftSurface(true, true);
            GetCrouchedAttackFrame2RightSurface(false, false);
            GetCrouchedAttackFrame2RightSurface(true, false);
            GetCrouchedAttackFrame2RightSurface(false, true);
            GetCrouchedAttackFrame2RightSurface(true, true);
            GetCrouchedAttackFrame2LeftSurface(false, false);
            GetCrouchedAttackFrame2LeftSurface(true, false);
            GetCrouchedAttackFrame2LeftSurface(false, true);
            GetCrouchedAttackFrame2LeftSurface(true, true);

            GetKickFrame1RightSurface(false, false);
            GetKickFrame1RightSurface(true, false);
            GetKickFrame1RightSurface(false, true);
            GetKickFrame1RightSurface(true, true);
            GetKickFrame2RightSurface(false, false);
            GetKickFrame2RightSurface(true, false);
            GetKickFrame2RightSurface(false, true);
            GetKickFrame2RightSurface(true, true);
            GetKickFrame1LeftSurface(false, false);
            GetKickFrame1LeftSurface(true, false);
            GetKickFrame1LeftSurface(false, true);
            GetKickFrame1LeftSurface(true, true);
            GetKickFrame2LeftSurface(false, false);
            GetKickFrame2LeftSurface(true, false);
            GetKickFrame2LeftSurface(false, true);
            GetKickFrame2LeftSurface(true, true);

            GetFlyRightSurface(false);
            GetFlyRightSurface(true);
            GetFlyLeftSurface(false);
            GetFlyLeftSurface(true);

            GetWalking1RightSurfaceTiny(false);
            GetWalking1RightSurfaceTiny(true);
            GetWalking2RightSurfaceTiny(false);
            GetWalking2RightSurfaceTiny(true);
            GetWalking1LeftSurfaceTiny(false);
            GetWalking1LeftSurfaceTiny(true);
            GetWalking2LeftSurfaceTiny(false);
            GetWalking2LeftSurfaceTiny(true);

            GetKickFrame1RightSurfaceTiny(false);
            GetKickFrame1RightSurfaceTiny(true);
            GetKickFrame2RightSurfaceTiny(false);
            GetKickFrame2RightSurfaceTiny(true);
            GetKickFrame1LeftSurfaceTiny(false);
            GetKickFrame1LeftSurfaceTiny(true);
            GetKickFrame2LeftSurfaceTiny(false);
            GetKickFrame2LeftSurfaceTiny(true);

            GetAttackFrame1RightSurfaceTiny(false);
            GetAttackFrame1RightSurfaceTiny(true);
            GetAttackFrame2RightSurfaceTiny(false);
            GetAttackFrame2RightSurfaceTiny(true);
            GetAttackFrame1LeftSurfaceTiny(false);
            GetAttackFrame1LeftSurfaceTiny(true);
            GetAttackFrame2LeftSurfaceTiny(false);
            GetAttackFrame2LeftSurfaceTiny(true);

            GetHitRightSurfaceTiny(false);
            GetHitRightSurfaceTiny(true);
            GetHitLeftSurfaceTiny(false);
            GetHitLeftSurfaceTiny(true);

            GetStandingRightSurfaceTiny(false);
            GetStandingRightSurfaceTiny(true);
            GetStandingLeftSurfaceTiny(false);
            GetStandingLeftSurfaceTiny(true);
            #endregion
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return walking1NinjaDopedRightSurface;
                else
                    return walking1NinjaRightSurface;
            }
            else if (isDoped && isRasta)
                return GetWalking1RightSurfaceRastaDoped();
            else if (isRasta)
                return GetWalking1RightSurfaceRasta();
            else if (isDoped)
                return GetWalking1RightSurfaceDoped();

            if (walking1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/walk1.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/walk1.png");
                else
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/walk1.png");
            }
                
            return walking1RightSurface;
        }

        private Surface GetWalking1RightSurfaceRastaDoped()
        {
            if (walking1RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaWalk1doped.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaWalk1doped.png");
                else
                    walking1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaWalk1doped.png");
            }

            return walking1RightSurfaceRastaDoped;
        }

        private Surface GetWalking1RightSurfaceRasta()
        {
            if (walking1RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaWalk1.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaWalk1.png");
                else
                    walking1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaWalk1.png");
            }
            return walking1RightSurfaceRasta;
        }

        private Surface GetWalking1LeftSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return walking1NinjaDopedLeftSurface;
                else
                    return walking1NinjaLeftSurface;
            }
            else if (isDoped && isRasta)
                return GetWalking1LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetWalking1LeftSurfaceRasta();
            else if (isDoped)
                return GetWalking1LeftSurfaceDoped();

            if (walking1LeftSurface == null)
                walking1LeftSurface = GetWalking1RightSurface(false, isRasta, false).CreateFlippedHorizontalSurface();

            return walking1LeftSurface;
        }

        private Surface GetWalking1LeftSurfaceRastaDoped()
        {
            if (walking1LeftSurfaceRastaDoped == null)
                walking1LeftSurfaceRastaDoped = GetWalking1RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return walking1LeftSurfaceRastaDoped;
        }

        private Surface GetWalking1LeftSurfaceRasta()
        {
            if (walking1LeftSurfaceRasta == null)
                walking1LeftSurfaceRasta = GetWalking1RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return walking1LeftSurfaceRasta;
        }

        private Surface GetWalking2LeftSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return walking2NinjaDopedLeftSurface;
                else
                    return walking2NinjaLeftSurface;
            }
            if (isDoped && isRasta)
                return GetWalking2LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetWalking2LeftSurfaceRasta();
            else if (isDoped)
                return GetWalking2LeftSurfaceDoped();

            if (walking2LeftSurface == null)
                walking2LeftSurface = GetWalking2RightSurface(false, isRasta, false).CreateFlippedHorizontalSurface();

            return walking2LeftSurface;
        }

        private Surface GetWalking2LeftSurfaceRastaDoped()
        {
            if (walking2LeftSurfaceRastaDoped == null)
                walking2LeftSurfaceRastaDoped = GetWalking2RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return walking2LeftSurfaceRastaDoped;
        }

        private Surface GetWalking2LeftSurfaceRasta()
        {
            if (walking2LeftSurfaceRasta == null)
                walking2LeftSurfaceRasta = GetWalking2RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return walking2LeftSurfaceRasta;
        }

        private Surface GetWalking2RightSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return walking2NinjaDopedRightSurface;
                else
                    return walking2NinjaRightSurface;
            }
            else if (isDoped && isRasta)
                return GetWalking2RightSurfaceRastaDoped();
            else if (isRasta)
                return GetWalking2RightSurfaceRasta();
            else if (isDoped)
                return GetWalking2RightSurfaceDoped();

            if (walking2RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/walk2.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/walk2.png");
                else
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/walk2.png");
            }

            return walking2RightSurface;
        }

        private Surface GetWalking2RightSurfaceRastaDoped()
        {
            if (walking2RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaWalk2doped.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaWalk2doped.png");
                else
                    walking2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaWalk2doped.png");
            }

            return walking2RightSurfaceRastaDoped;
        }

        private Surface GetWalking2RightSurfaceRasta()
        {
            if (walking2RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaWalk2.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaWalk2.png");
                else
                    walking2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaWalk2.png");
            }

            return walking2RightSurfaceRasta;
        }

        private Surface GetStandingLeftSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return standingNinjaDopedLeftSurface;
                else
                    return standingNinjaLeftSurface;
            }
            else if (isDoped && isRasta)
                return GetStandingLeftSurfaceDopedRasta();
            else if (isRasta)
                return GetStandingLeftSurfaceRasta();
            else if (isDoped)
                return GetStandingLeftSurfaceDoped();

            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface(false, isRasta, false).CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetStandingLeftSurfaceDopedRasta()
        {
            if (standingLeftSurfaceRastaDoped == null)
                standingLeftSurfaceRastaDoped = GetStandingRightSurfaceDopedRasta().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceRastaDoped;
        }

        private Surface GetStandingLeftSurfaceRasta()
        {
            if (standingLeftSurfaceRasta == null)
                standingLeftSurfaceRasta = GetStandingRightSurfaceRasta().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceRasta;
        }

        private Surface GetCrouchedRightSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return crouchedNinjaDopedRight;
                else
                    return crouchedNinjaRight;
            }
            else if (isDoped && isRasta)
                return GetCrouchedRightSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedRightSurfaceRasta();
            else if (isDoped)
                return GetCrouchedRightSurfaceDoped();

            if (crouchedRightSurface == null)
            {
                if (Program.screenHeight > 720)
                    crouchedRightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouched.png");
                else if (Program.screenHeight > 480)
                    crouchedRightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/crouched.png");
                else
                    crouchedRightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/crouched.png");
            }

            return crouchedRightSurface;
        }

        private Surface GetCrouchedRightSurfaceRastaDoped()
        {
            if (crouchedRightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCroucheddoped.png");
                else if (Program.screenHeight > 480)
                    crouchedRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCroucheddoped.png");
                else
                    crouchedRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCroucheddoped.png");
            }

            return crouchedRightSurfaceRastaDoped;
        }

        private Surface GetCrouchedRightSurfaceRasta()
        {
            if (crouchedRightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    crouchedRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCrouched.png");
                else if (Program.screenHeight > 480)
                    crouchedRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCrouched.png");
                else
                    crouchedRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCrouched.png");
            }

            return crouchedRightSurfaceRasta;
        }

        private Surface GetCrouchedLeftSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return crouchedNinjaDopedLeft;
                else
                    return crouchedNinjaLeft;
            }
            else if (isDoped && isRasta)
                return GetCrouchedLeftSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedLeftSurfaceRasta();
            else if (isDoped)
                return GetCrouchedLeftSurfaceDoped();

            if (crouchedLeftSurface == null)
                crouchedLeftSurface = GetCrouchedRightSurface(false, isRasta, false).CreateFlippedHorizontalSurface();

            return crouchedLeftSurface;
        }

        private Surface GetCrouchedLeftSurfaceRastaDoped()
        {
            if (crouchedLeftSurfaceRastaDoped == null)
                crouchedLeftSurfaceRastaDoped = GetCrouchedRightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return crouchedLeftSurfaceRastaDoped;
        }

        private Surface GetCrouchedLeftSurfaceRasta()
        {
            if (crouchedLeftSurfaceRasta == null)
                crouchedLeftSurfaceRasta = GetCrouchedRightSurfaceRasta().CreateFlippedHorizontalSurface();

            return crouchedLeftSurfaceRasta;
        }

        private Surface GetCrouchedHitRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedHitRightSurfaceDoped();

            if (crouchedHitRightSurface == null)
            {
                if (Program.screenHeight > 720)
                    crouchedHitRightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedHit.png");
                else if (Program.screenHeight > 480)
                    crouchedHitRightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedHit.png");
                else
                    crouchedHitRightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedHit.png");
            }

            return crouchedHitRightSurface;
        }

        private Surface GetCrouchedHitLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedHitLeftSurfaceDoped();

            if (crouchedHitLeftSurface == null)
                crouchedHitLeftSurface = GetCrouchedHitRightSurface(false).CreateFlippedHorizontalSurface();

            return crouchedHitLeftSurface;
        }

        private Surface GetHitRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetHitRightSurfaceDoped();

            if (hitRightSurface == null)
            {
                if (Program.screenHeight > 720)
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/hit.png");
                else if (Program.screenHeight > 480)
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/hit.png");
                else
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/hit.png");
            }

            return hitRightSurface;
        }

        private Surface GetHitLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetHitLeftSurfaceDoped();

            if (hitLeftSurface == null)
                hitLeftSurface = GetHitRightSurface(false).CreateFlippedHorizontalSurface();

            return hitLeftSurface;
        }

        private Surface GetStandingRightSurface(bool isDoped, bool isRasta, bool isNinja)
        {
            if (isNinja)
            {
                if (isDoped)
                    return standingNinjaDopedRightSurface;
                else
                    return standingNinjaRightSurface;
            }
            else if (isDoped && isRasta)
                return GetStandingRightSurfaceDopedRasta();
            else if (isRasta)
                return GetStandingRightSurfaceRasta();
            else if (isDoped)
                return GetStandingRightSurfaceDoped();

            if (standingRightSurface == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/stand.png");
                else if (Program.screenHeight > 480)
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/stand.png");
                else
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/stand.png");
            }

            return standingRightSurface;
        }

        private Surface GetStandingRightSurfaceRasta()
        {
            if (standingRightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaStand.png");
                else if (Program.screenHeight > 480)
                    standingRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaStand.png");
                else
                    standingRightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaStand.png");
            }

            return standingRightSurfaceRasta;
        }

        private Surface GetStandingRightSurfaceDopedRasta()
        {
            if (standingRightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaStanddoped.png");
                else if (Program.screenHeight > 480)
                    standingRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaStanddoped.png");
                else
                    standingRightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaStanddoped.png");
            }

            return standingRightSurfaceRastaDoped;
        }

        private Surface GetAttackFrame2RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetAttackFrame2RightSurfaceRastaDoped();
            else if (isRasta)
                return GetAttackFrame2RightSurfaceRasta();
            else if (isDoped)
                return GetAttackFrame2RightSurfaceDoped();

            if (attackFrame2RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/punch2.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/punch2.png");
                else
                    attackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/punch2.png");
            }

            return attackFrame2RightSurface;
        }

        private Surface GetAttackFrame2RightSurfaceRastaDoped()
        {
            if (attackFrame2RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaPunch2doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaPunch2doped.png");
                else
                    attackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaPunch2doped.png");
            }

            return attackFrame2RightSurfaceRastaDoped;
        }

        private Surface GetAttackFrame2RightSurfaceRasta()
        {
            if (attackFrame2RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaPunch2.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaPunch2.png");
                else
                    attackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaPunch2.png");
            }

            return attackFrame2RightSurfaceRasta;
        }

        private Surface GetAttackFrame1RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetAttackFrame1RightSurfaceRastaDoped();
            else if (isRasta)
                return GetAttackFrame1RightSurfaceRasta();
            else if (isDoped)
                return GetAttackFrame1RightSurfaceDoped();

            if (attackFrame1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/punch1.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/punch1.png");
                else
                    attackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/punch1.png");
            }

            return attackFrame1RightSurface;
        }

        private Surface GetAttackFrame1RightSurfaceRastaDoped()
        {
            if (attackFrame1RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaPunch1doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaPunch1doped.png");
                else
                    attackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaPunch1doped.png");
            }

            return attackFrame1RightSurfaceRastaDoped;
        }

        private Surface GetAttackFrame1RightSurfaceRasta()
        {
            if (attackFrame1RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaPunch1.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaPunch1.png");
                else
                    attackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaPunch1.png");
            }

            return attackFrame1RightSurfaceRasta;
        }

        private Surface GetCrouchedAttackFrame1RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetCrouchedAttackFrame1RightSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedAttackFrame1RightSurfaceRasta();
            else if (isDoped)
                return GetCrouchedAttackFrame1RightSurfaceDoped();

            if (crouchedAttackFrame1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedPunch1.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedPunch1.png");
                else
                    crouchedAttackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedPunch1.png");
            }

            return crouchedAttackFrame1RightSurface;
        }

        private Surface GetCrouchedAttackFrame1RightSurfaceRastaDoped()
        {
            if (crouchedAttackFrame1RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCrouchedPunch1doped.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCrouchedPunch1doped.png");
                else
                    crouchedAttackFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCrouchedPunch1doped.png");
            }

            return crouchedAttackFrame1RightSurfaceRastaDoped;
        }

        private Surface GetCrouchedAttackFrame1RightSurfaceRasta()
        {
            if (crouchedAttackFrame1RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCrouchedPunch1.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCrouchedPunch1.png");
                else
                    crouchedAttackFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCrouchedPunch1.png");
            }

            return crouchedAttackFrame1RightSurfaceRasta;
        }

        private Surface GetCrouchedAttackFrame2RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetCrouchedAttackFrame2RightSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedAttackFrame2RightSurfaceRasta();
            if (isDoped)
                return GetCrouchedAttackFrame2RightSurfaceDoped();

            if (crouchedAttackFrame2RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedPunch2.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedPunch2.png");
                else
                    crouchedAttackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedPunch2.png");
            }

            return crouchedAttackFrame2RightSurface;
        }

        private Surface GetCrouchedAttackFrame2RightSurfaceRastaDoped()
        {
            if (crouchedAttackFrame2RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCrouchedPunch2doped.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCrouchedPunch2doped.png");
                else
                    crouchedAttackFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCrouchedPunch2doped.png");
            }

            return crouchedAttackFrame2RightSurfaceRastaDoped;
        }

        private Surface GetCrouchedAttackFrame2RightSurfaceRasta()
        {
            if (crouchedAttackFrame2RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaCrouchedPunch2.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaCrouchedPunch2.png");
                else
                    crouchedAttackFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaCrouchedPunch2.png");
            }

            return crouchedAttackFrame2RightSurfaceRasta;
        }

        private Surface GetAttackFrame2LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetAttackFrame2LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetAttackFrame2LeftSurfaceRasta();
            else if (isDoped)
                return GetAttackFrame2LeftSurfaceDoped();

            if (attackFrame2LeftSurface == null)
                attackFrame2LeftSurface = GetAttackFrame2RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurface;
        }

        private Surface GetAttackFrame2LeftSurfaceRastaDoped()
        {
            if (attackFrame2LeftSurfaceRastaDoped == null)
                attackFrame2LeftSurfaceRastaDoped = GetAttackFrame2RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceRastaDoped;
        }

        private Surface GetAttackFrame2LeftSurfaceRasta()
        {
            if (attackFrame2LeftSurfaceRasta == null)
                attackFrame2LeftSurfaceRasta = GetAttackFrame2RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceRasta;
        }

        private Surface GetAttackFrame1LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetAttackFrame1LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetAttackFrame1LeftSurfaceRasta();
            else if (isDoped)
                return GetAttackFrame1LeftSurfaceDoped();

            if (attackFrame1LeftSurface == null)
                attackFrame1LeftSurface = GetAttackFrame1RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurface;
        }

        private Surface GetAttackFrame1LeftSurfaceRastaDoped()
        {
            if (attackFrame1LeftSurfaceRastaDoped == null)
                attackFrame1LeftSurfaceRastaDoped = GetAttackFrame1RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceRastaDoped;
        }

        private Surface GetAttackFrame1LeftSurfaceRasta()
        {
            if (attackFrame1LeftSurfaceRasta == null)
                attackFrame1LeftSurfaceRasta = GetAttackFrame1RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceRasta;
        }

        private Surface GetCrouchedAttackFrame2LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetCrouchedAttackFrame2LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedAttackFrame2LeftSurfaceRasta();
            else if (isDoped)
                return GetCrouchedAttackFrame2LeftSurfaceDoped();

            if (crouchedAttackFrame2LeftSurface == null)
                crouchedAttackFrame2LeftSurface = GetCrouchedAttackFrame2RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurface;
        }

        private Surface GetCrouchedAttackFrame2LeftSurfaceRastaDoped()
        {
            if (crouchedAttackFrame2LeftSurfaceRastaDoped == null)
                crouchedAttackFrame2LeftSurfaceRastaDoped = GetCrouchedAttackFrame2RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurfaceRastaDoped;
        }

        private Surface GetCrouchedAttackFrame2LeftSurfaceRasta()
        {
            if (crouchedAttackFrame2LeftSurfaceRasta == null)
                crouchedAttackFrame2LeftSurfaceRasta = GetCrouchedAttackFrame2RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurfaceRasta;
        }

        private Surface GetCrouchedAttackFrame1LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetCrouchedAttackFrame1LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetCrouchedAttackFrame1LeftSurfaceRasta();
            else if (isDoped)
                return GetCrouchedAttackFrame1LeftSurfaceDoped();

            if (crouchedAttackFrame1LeftSurface == null)
                crouchedAttackFrame1LeftSurface = GetCrouchedAttackFrame1RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurface;
        }

        private Surface GetCrouchedAttackFrame1LeftSurfaceRastaDoped()
        {
            if (crouchedAttackFrame1LeftSurfaceRastaDoped == null)
                crouchedAttackFrame1LeftSurfaceRastaDoped = GetCrouchedAttackFrame1RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurfaceRastaDoped;
        }

        private Surface GetCrouchedAttackFrame1LeftSurfaceRasta()
        {
            if (crouchedAttackFrame1LeftSurfaceRasta == null)
                crouchedAttackFrame1LeftSurfaceRasta = GetCrouchedAttackFrame1RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurfaceRasta;
        }

        private Surface GetKickFrame2LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetKickFrame2LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetKickFrame2LeftSurfaceRasta();
            else if (isDoped)
                return GetKickFrame2LeftSurfaceDoped();

            if (kickFrame2LeftSurface == null)
                kickFrame2LeftSurface = GetKickFrame2RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurface;
        }

        private Surface GetKickFrame2LeftSurfaceRastaDoped()
        {
            if (kickFrame2LeftSurfaceRastaDoped == null)
                kickFrame2LeftSurfaceRastaDoped = GetKickFrame2RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceRastaDoped;
        }

        private Surface GetKickFrame2LeftSurfaceRasta()
        {
            if (kickFrame2LeftSurfaceRasta == null)
                kickFrame2LeftSurfaceRasta = GetKickFrame2RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceRasta;
        }

        private Surface GetKickFrame2RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetKickFrame2RightSurfaceRastaDoped();
            else if (isRasta)
                return GetKickFrame2RightSurfaceRasta();
            else if (isDoped)
                return GetKickFrame2RightSurfaceDoped();

            if (kickFrame2RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/kick2.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/kick2.png");
                else
                    kickFrame2RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/kick2.png");
            }

            return kickFrame2RightSurface;
        }

        private Surface GetKickFrame2RightSurfaceRastaDoped()
        {
            if (kickFrame2RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaKick2doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaKick2doped.png");
                else
                    kickFrame2RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaKick2doped.png");
            }

            return kickFrame2RightSurfaceRastaDoped;
        }

        private Surface GetKickFrame2RightSurfaceRasta()
        {
            if (kickFrame2RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaKick2.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaKick2.png");
                else
                    kickFrame2RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaKick2.png");
            }

            return kickFrame2RightSurfaceRasta;
        }

        private Surface GetKickFrame1LeftSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetKickFrame1LeftSurfaceRastaDoped();
            else if (isRasta)
                return GetKickFrame1LeftSurfaceRasta();
            else if (isDoped)
                return GetKickFrame1LeftSurfaceDoped();

            if (kickFrame1LeftSurface == null)
                kickFrame1LeftSurface = GetKickFrame1RightSurface(false, isRasta).CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurface;
        }

        private Surface GetKickFrame1LeftSurfaceRastaDoped()
        {
            if (kickFrame1LeftSurfaceRastaDoped == null)
                kickFrame1LeftSurfaceRastaDoped = GetKickFrame1RightSurfaceRastaDoped().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceRastaDoped;
        }

        private Surface GetKickFrame1LeftSurfaceRasta()
        {
            if (kickFrame1LeftSurfaceRasta == null)
                kickFrame1LeftSurfaceRasta = GetKickFrame1RightSurfaceRasta().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceRasta;
        }

        private Surface GetKickFrame1RightSurface(bool isDoped, bool isRasta)
        {
            if (isDoped && isRasta)
                return GetKickFrame1RightSurfaceRastaDoped();
            else if (isRasta)
                return GetKickFrame1RightSurfaceRasta();
            else if (isDoped)
                return GetKickFrame1RightSurfaceDoped();

            if (kickFrame1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/kick1.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/kick1.png");
                else
                    kickFrame1RightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/kick1.png");
            }

            return kickFrame1RightSurface;
        }

        private Surface GetKickFrame1RightSurfaceRastaDoped()
        {
            if (kickFrame1RightSurfaceRastaDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaKick1doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaKick1doped.png");
                else
                    kickFrame1RightSurfaceRastaDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaKick1doped.png");
            }

            return kickFrame1RightSurfaceRastaDoped;
        }

        private Surface GetKickFrame1RightSurfaceRasta()
        {
            if (kickFrame1RightSurfaceRasta == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaKick1.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaKick1.png");
                else
                    kickFrame1RightSurfaceRasta = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaKick1.png");
            }

            return kickFrame1RightSurfaceRasta;
        }

        private Surface GetWalking2LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetWalking2LeftSurfaceTinyDoped();

            if (walking2LeftSurfaceTiny == null)
                walking2LeftSurfaceTiny = GetWalking2RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();
            return walking2LeftSurfaceTiny;
        }

        private Surface GetWalking2LeftSurfaceTinyDoped()
        {
            if (walking2LeftSurfaceTinyDoped == null)
                walking2LeftSurfaceTinyDoped = GetWalking2RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();
            return walking2LeftSurfaceTinyDoped;
        }

        private Surface GetWalking2RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetWalking2RightSurfaceTinyDoped();

            if (walking2RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyWalk2.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyWalk2.png");
                else
                    walking2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyWalk2.png");
            }
            return walking2RightSurfaceTiny;
        }

        private Surface GetWalking2RightSurfaceTinyDoped()
        {
            if (walking2RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyWalk2doped.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyWalk2doped.png");
                else
                    walking2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyWalk2doped.png");
            }
            return walking2RightSurfaceTinyDoped;
        }

        private Surface GetWalking1LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetWalking1LeftSurfaceTinyDoped();

            if (walking1LeftSurfaceTiny == null)
                walking1LeftSurfaceTiny = GetWalking1RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();
            return walking1LeftSurfaceTiny;
        }

        private Surface GetWalking1LeftSurfaceTinyDoped()
        {
            if (walking1LeftSurfaceTinyDoped == null)
                walking1LeftSurfaceTinyDoped = GetWalking1RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();
            return walking1LeftSurfaceTinyDoped;
        }

        private Surface GetWalking1RightSurfaceTinyDoped()
        {
            if (walking1RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyWalk1doped.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyWalk1doped.png");
                else
                    walking1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyWalk1doped.png");
            }
            return walking1RightSurfaceTinyDoped;
        }

        private Surface GetWalking1RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetWalking1RightSurfaceTinyDoped();

            if (walking1RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyWalk1.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyWalk1.png");
                else
                    walking1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyWalk1.png");
            }
            return walking1RightSurfaceTiny;
        }

        private Surface GetWalking1RightSurfaceDoped()
        {
            if (walking1RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/walk1doped.png");
                else if (Program.screenHeight > 480)
                    walking1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/walk1doped.png");
                else
                    walking1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/walk1doped.png");
            }
            return walking1RightSurfaceDoped;
        }

        private Surface GetWalking1LeftSurfaceDoped()
        {
            if (walking1LeftSurfaceDoped == null)
                walking1LeftSurfaceDoped = GetWalking1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return walking1LeftSurfaceDoped;
        }

        private Surface GetWalking2LeftSurfaceDoped()
        {
            if (walking2LeftSurfaceDoped == null)
                walking2LeftSurfaceDoped = GetWalking2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return walking2LeftSurfaceDoped;
        }

        private Surface GetWalking2RightSurfaceDoped()
        {
            if (walking2RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    walking2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/walk2doped.png");
                else if (Program.screenHeight > 480)
                    walking2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/walk2doped.png");
                else
                    walking2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/walk2doped.png");
            }

            return walking2RightSurfaceDoped;
        }

        private Surface GetStandingLeftSurfaceDoped()
        {
            if (standingLeftSurfaceDoped == null)
                standingLeftSurfaceDoped = GetStandingRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceDoped;
        }

        private Surface GetCrouchedRightSurfaceDoped()
        {
            if (crouchedRightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/croucheddoped.png");
                else if (Program.screenHeight > 480)
                    crouchedRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/croucheddoped.png");
                else
                    crouchedRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/croucheddoped.png");
            }

            return crouchedRightSurfaceDoped;
        }

        private Surface GetCrouchedLeftSurfaceDoped()
        {
            if (crouchedLeftSurfaceDoped == null)
                crouchedLeftSurfaceDoped = GetCrouchedRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedLeftSurfaceDoped;
        }

        private Surface GetCrouchedHitRightSurfaceDoped()
        {
            if (crouchedHitRightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedHitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedHitdoped.png");
                else if (Program.screenHeight > 480)
                    crouchedHitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedHitdoped.png");
                else
                    crouchedHitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedHitdoped.png");
            }

            return crouchedHitRightSurfaceDoped;
        }

        private Surface GetCrouchedHitLeftSurfaceDoped()
        {
            if (crouchedHitLeftSurfaceDoped == null)
                crouchedHitLeftSurfaceDoped = GetCrouchedHitRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedHitLeftSurfaceDoped;
        }

        private Surface GetHitRightSurfaceDoped()
        {
            if (hitRightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    hitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/hitdoped.png");
                else if (Program.screenHeight > 480)
                    hitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/hitdoped.png");
                else
                    hitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/hitdoped.png");
            }

            return hitRightSurfaceDoped;
        }

        private Surface GetHitLeftSurfaceDoped()
        {
            if (hitLeftSurfaceDoped == null)
                hitLeftSurfaceDoped = GetHitRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return hitLeftSurfaceDoped;
        }

        private Surface GetStandingRightSurfaceDoped()
        {
            if (standingRightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/standdoped.png");
                else if (Program.screenHeight > 480)
                    standingRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/standdoped.png");
                else
                    standingRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/standdoped.png");
            }

            return standingRightSurfaceDoped;
        }

        private Surface GetAttackFrame2RightSurfaceDoped()
        {
            if (attackFrame2RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/punch2doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/punch2doped.png");
                else
                    attackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/punch2doped.png");
            }

            return attackFrame2RightSurfaceDoped;
        }

        private Surface GetAttackFrame1RightSurfaceDoped()
        {
            if (attackFrame1RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/punch1doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/punch1doped.png");
                else
                    attackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/punch1doped.png");
            }

            return attackFrame1RightSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame1RightSurfaceDoped()
        {
            if (crouchedAttackFrame1RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedPunch1doped.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedPunch1doped.png");
                else
                    crouchedAttackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedPunch1doped.png");
            }

            return crouchedAttackFrame1RightSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame2RightSurfaceDoped()
        {
            if (crouchedAttackFrame2RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    crouchedAttackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/crouchedPunch2doped.png");
                else if (Program.screenHeight > 480)
                    crouchedAttackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/crouchedPunch2doped.png");
                else
                    crouchedAttackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/crouchedPunch2doped.png");
            }

            return crouchedAttackFrame2RightSurfaceDoped;
        }

        private Surface GetAttackFrame2LeftSurfaceDoped()
        {
            if (attackFrame2LeftSurfaceDoped == null)
                attackFrame2LeftSurfaceDoped = GetAttackFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceDoped;
        }

        private Surface GetAttackFrame1LeftSurfaceDoped()
        {
            if (attackFrame1LeftSurfaceDoped == null)
                attackFrame1LeftSurfaceDoped = GetAttackFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame2LeftSurfaceDoped()
        {
            if (crouchedAttackFrame2LeftSurfaceDoped == null)
                crouchedAttackFrame2LeftSurfaceDoped = GetCrouchedAttackFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame1LeftSurfaceDoped()
        {
            if (crouchedAttackFrame1LeftSurfaceDoped == null)
                crouchedAttackFrame1LeftSurfaceDoped = GetCrouchedAttackFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurfaceDoped;
        }

        private Surface GetKickFrame2LeftSurfaceDoped()
        {
            if (kickFrame2LeftSurfaceDoped == null)
                kickFrame2LeftSurfaceDoped = GetKickFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceDoped;
        }

        private Surface GetKickFrame2RightSurfaceDoped()
        {
            if (kickFrame2RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/kick2doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/kick2doped.png");
                else
                    kickFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/kick2doped.png");
            }

            return kickFrame2RightSurfaceDoped;
        }

        private Surface GetKickFrame1LeftSurfaceDoped()
        {
            if (kickFrame1LeftSurfaceDoped == null)
                kickFrame1LeftSurfaceDoped = GetKickFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceDoped;
        }

        private Surface GetKickFrame1RightSurfaceDoped()
        {
            if (kickFrame1RightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/kick1doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/kick1doped.png");
                else
                    kickFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/kick1doped.png");
            }

            return kickFrame1RightSurfaceDoped;
        }

        private Surface GetKickFrame1LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetKickFrame1LeftSurfaceTinyDoped();

            if (kickFrame1LeftSurfaceTiny == null)
                kickFrame1LeftSurfaceTiny = GetKickFrame1RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceTiny;
        }

        private Surface GetKickFrame1LeftSurfaceTinyDoped()
        {
            if (kickFrame1LeftSurfaceTinyDoped == null)
                kickFrame1LeftSurfaceTinyDoped = GetKickFrame1RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceTinyDoped;
        }

        private Surface GetKickFrame2LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetKickFrame2LeftSurfaceTinyDoped();

            if (kickFrame2LeftSurfaceTiny == null)
                kickFrame2LeftSurfaceTiny = GetKickFrame2RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceTiny;
        }

        private Surface GetKickFrame2LeftSurfaceTinyDoped()
        {
            if (kickFrame2LeftSurfaceTinyDoped == null)
                kickFrame2LeftSurfaceTinyDoped = GetKickFrame2RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceTinyDoped;
        }

        private Surface GetStandingRightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetStandingRightSurfaceTinyDoped();

            if (standingRightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyStand.png");
                else if (Program.screenHeight > 480)
                    standingRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyStand.png");
                else
                    standingRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyStand.png");
            }

            return standingRightSurfaceTiny;
        }

        private Surface GetStandingRightSurfaceTinyDoped()
        {
            if (standingRightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    standingRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyStanddoped.png");
                else if (Program.screenHeight > 480)
                    standingRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyStanddoped.png");
                else
                    standingRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyStanddoped.png");
            }

            return standingRightSurfaceTinyDoped;
        }

        private Surface GetStandingLeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetStandingLeftSurfaceTinyDoped();

            if (standingLeftSurfaceTiny == null)
                standingLeftSurfaceTiny = GetStandingRightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return standingLeftSurfaceTiny;
        }

        private Surface GetStandingLeftSurfaceTinyDoped()
        {
            if (standingLeftSurfaceTinyDoped == null)
                standingLeftSurfaceTinyDoped = GetStandingRightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceTinyDoped;
        }

        private Surface GetKickFrame1RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetKickFrame1RightSurfaceTinyDoped();

            if (kickFrame1RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyKick1.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyKick1.png");
                else
                    kickFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyKick1.png");
            }

            return kickFrame1RightSurfaceTiny;
        }

        private Surface GetKickFrame1RightSurfaceTinyDoped()
        {
            if (kickFrame1RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyKick1doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyKick1doped.png");
                else
                    kickFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyKick1doped.png");
            }

            return kickFrame1RightSurfaceTinyDoped;
        }

        private Surface GetKickFrame2RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetKickFrame2RightSurfaceTinyDoped();

            if (kickFrame2RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyKick2.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyKick2.png");
                else
                    kickFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyKick2.png");
            }

            return kickFrame2RightSurfaceTiny;
        }

        private Surface GetKickFrame2RightSurfaceTinyDoped()
        {
            if (kickFrame2RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    kickFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyKick2doped.png");
                else if (Program.screenHeight > 480)
                    kickFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyKick2doped.png");
                else
                    kickFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyKick2doped.png");
            }

            return kickFrame2RightSurfaceTinyDoped;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetStandingRightSurfaceTiny(false).CreateFlippedVerticalSurface();

            return deadSurface;
        }

        private Surface GetAttackFrame1LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetAttackFrame1LeftSurfaceTinyDoped();

            if (attackFrame1LeftSurfaceTiny == null)
                attackFrame1LeftSurfaceTiny = GetAttackFrame1RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceTiny;
        }

        private Surface GetAttackFrame1LeftSurfaceTinyDoped()
        {
            if (attackFrame1LeftSurfaceTinyDoped == null)
                attackFrame1LeftSurfaceTinyDoped = GetAttackFrame1RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceTinyDoped;
        }

        private Surface GetAttackFrame2LeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetAttackFrame2LeftSurfaceTinyDoped();

            if (attackFrame2LeftSurfaceTiny == null)
                attackFrame2LeftSurfaceTiny = GetAttackFrame2RightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceTiny;
        }

        private Surface GetAttackFrame2LeftSurfaceTinyDoped()
        {
            if (attackFrame2LeftSurfaceTinyDoped == null)
                attackFrame2LeftSurfaceTinyDoped = GetAttackFrame2RightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceTinyDoped;
        }

        private Surface GetAttackFrame1RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetAttackFrame1RightSurfaceTinyDoped();

            if (attackFrame1RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyPunch1.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyPunch1.png");
                else
                    attackFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyPunch1.png");
            }

            return attackFrame1RightSurfaceTiny;
        }

        private Surface GetAttackFrame1RightSurfaceTinyDoped()
        {
            if (attackFrame1RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyPunch1doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyPunch1doped.png");
                else
                    attackFrame1RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyPunch1doped.png");
            }

            return attackFrame1RightSurfaceTinyDoped;
        }

        private Surface GetAttackFrame2RightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetAttackFrame2RightSurfaceTinyDoped();

            if (attackFrame2RightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyPunch2.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyPunch2.png");
                else
                    attackFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyPunch2.png");
            }

            return attackFrame2RightSurfaceTiny;
        }

        private Surface GetAttackFrame2RightSurfaceTinyDoped()
        {
            if (attackFrame2RightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    attackFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyPunch2doped.png");
                else if (Program.screenHeight > 480)
                    attackFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyPunch2doped.png");
                else
                    attackFrame2RightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyPunch2doped.png");
            }

            return attackFrame2RightSurfaceTinyDoped;
        }

        private Surface GetHitLeftSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetHitLeftSurfaceTinyDoped();

            if (hitLeftSurfaceTiny == null)
                hitLeftSurfaceTiny = GetHitRightSurfaceTiny(isShowDopedColor).CreateFlippedHorizontalSurface();

            return hitLeftSurfaceTiny;
        }

        private Surface GetHitLeftSurfaceTinyDoped()
        {
            if (hitLeftSurfaceTinyDoped == null)
                hitLeftSurfaceTinyDoped = GetHitRightSurfaceTinyDoped().CreateFlippedHorizontalSurface();

            return hitLeftSurfaceTinyDoped;
        }

        private Surface GetHitRightSurfaceTiny(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetHitRightSurfaceTinyDoped();

            if (hitRightSurfaceTiny == null)
            {
                if (Program.screenHeight > 720)
                    hitRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyHit.png");
                else if (Program.screenHeight > 480)
                    hitRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyHit.png");
                else
                    hitRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyHit.png");
            }

            return hitRightSurfaceTiny;
        }

        private Surface GetHitRightSurfaceTinyDoped()
        {
            if (hitRightSurfaceTinyDoped == null)
            {
                if (Program.screenHeight > 720)
                    hitRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/tinyHitdoped.png");
                else if (Program.screenHeight > 480)
                    hitRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/tinyHitdoped.png");
                else
                    hitRightSurfaceTinyDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/tinyHitdoped.png");
            }

            return hitRightSurfaceTinyDoped;
        }

        private Surface GetFlyLeftSurface(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetFlyLeftSurfaceDoped();

            if (flyLeftSurface == null)
                flyLeftSurface = GetFlyRightSurface(isShowDopedColor).CreateFlippedHorizontalSurface();
            return flyLeftSurface;
        }

        private Surface GetFlyLeftSurfaceDoped()
        {
            if (flyLeftSurfaceDoped == null)
                flyLeftSurfaceDoped = GetFlyRightSurfaceDoped().CreateFlippedHorizontalSurface();
            return flyLeftSurfaceDoped;
        }

        private Surface GetFlyRightSurface(bool isShowDopedColor)
        {
            if (isShowDopedColor)
                return GetFlyRightSurfaceDoped();

            if (flyRightSurface == null)
            {
                if (Program.screenHeight > 720)
                    flyRightSurface = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaFly.png");
                else if (Program.screenHeight > 480)
                    flyRightSurface = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaFly.png");
                else
                    flyRightSurface = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaFly.png");
            }
            return flyRightSurface;
        }

        private Surface GetFlyRightSurfaceDoped()
        {
            if (flyRightSurfaceDoped == null)
            {
                if (Program.screenHeight > 720)
                    flyRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/1080/abrahman/rastaFlydoped.png");
                else if (Program.screenHeight > 480)
                    flyRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/720/abrahman/rastaFlydoped.png");
                else
                    flyRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/480/abrahman/rastaFlydoped.png");
            }
            return flyRightSurfaceDoped;
        }

        private Surface GetCurrentSurfaceWithBeaver(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;

            if (!IsAlive)
                return GetDeadSurface();

            bool isShowTiny = IsTiny;

            if (changingSizeAnimationCycle.IsFired)
                isShowTiny = changingSizeAnimationCycle.CurrentValue % 4 <= 2;

            if (isShowTiny)
            {
                if (IsTryingToWalkRight)
                    xOffset = -0.15;
                else
                    xOffset = 0.15;
            }


            bool isShowDopedColor;
            if (powerUpAnimationCycle.IsFired)
            {
                isShowDopedColor = ((int)(powerUpAnimationCycle.CurrentValue) % 4 >= 2);
            }
            else if (invincibilityCycle.IsFired)
            {
                isShowDopedColor = ((int)(invincibilityCycle.CurrentValue) % 4 >= 2);
            }
            else
            {
                isShowDopedColor = isDoped;
            }

            Surface standSurfaceRight;
            Surface standSurfaceLeft;
            Surface walkSurfaceRight;
            Surface walkSurfaceLeft;
            Surface attackSurfaceRight;
            Surface attackSurfaceLeft;
            Surface crouchedSurfaceRight;
            Surface crouchedSurfaceLeft;
            Surface hitSurfaceRight;
            Surface hitSurfaceLeft;

            if (isShowTiny)
            {
                if (isShowDopedColor)
                {
                    standSurfaceRight = beaverStandTinyDopedRight;
                    standSurfaceLeft = beaverStandTinyDopedLeft;
                    walkSurfaceRight = beaverWalkTinyDopedRight;
                    walkSurfaceLeft = beaverWalkTinyDopedLeft;
                    attackSurfaceRight = beaverAttackTinyDopedRight;
                    attackSurfaceLeft = beaverAttackTinyDopedLeft;
                    crouchedSurfaceRight = beaverCrouchedTinyDopedRight;
                    crouchedSurfaceLeft = beaverCrouchedTinyDopedLeft;
                    hitSurfaceRight = beaverHitTinyDopedRight;
                    hitSurfaceLeft = beaverHitTinyDopedLeft;
                }
                else
                {
                    standSurfaceRight = beaverStandTinyRight;
                    standSurfaceLeft = beaverStandTinyLeft;
                    walkSurfaceRight = beaverWalkTinyRight;
                    walkSurfaceLeft = beaverWalkTinyLeft;
                    attackSurfaceRight = beaverAttackTinyRight;
                    attackSurfaceLeft = beaverAttackTinyLeft;
                    crouchedSurfaceRight = beaverCrouchedTinyRight;
                    crouchedSurfaceLeft = beaverCrouchedTinyLeft;
                    hitSurfaceRight = beaverHitTinyRight;
                    hitSurfaceLeft = beaverHitTinyLeft;
                }
            }
            else
            {
                if (isRasta)
                {
                    if (isShowDopedColor)
                    {
                        standSurfaceRight = beaverStandTallRastaDopedRight;
                        standSurfaceLeft = beaverStandTallRastaDopedLeft;
                        walkSurfaceRight = beaverWalkTallRastaDopedRight;
                        walkSurfaceLeft = beaverWalkTallRastaDopedLeft;
                        attackSurfaceRight = beaverAttackTallRastaDopedRight;
                        attackSurfaceLeft = beaverAttackTallRastaDopedLeft;
                        crouchedSurfaceRight = beaverCrouchedTallRastaDopedRight;
                        crouchedSurfaceLeft = beaverCrouchedTallRastaDopedLeft;
                        hitSurfaceRight = beaverHitTallRastaDopedRight;
                        hitSurfaceLeft = beaverHitTallRastaDopedLeft;
                    }
                    else
                    {
                        standSurfaceRight = beaverStandTallRastaRight;
                        standSurfaceLeft = beaverStandTallRastaLeft;
                        walkSurfaceRight = beaverWalkTallRastaRight;
                        walkSurfaceLeft = beaverWalkTallRastaLeft;
                        attackSurfaceRight = beaverAttackTallRastaRight;
                        attackSurfaceLeft = beaverAttackTallRastaLeft;
                        crouchedSurfaceRight = beaverCrouchedTallRastaRight;
                        crouchedSurfaceLeft = beaverCrouchedTallRastaLeft;
                        hitSurfaceRight = beaverHitTallRastaRight;
                        hitSurfaceLeft = beaverHitTallRastaLeft;
                    }
                }
                else if (isNinja)
                {
                    if (isShowDopedColor)
                    {
                        standSurfaceRight = beaverStandTallNinjaDopedRight;
                        standSurfaceLeft = beaverStandTallNinjaDopedLeft;
                        walkSurfaceRight = beaverWalkTallNinjaDopedRight;
                        walkSurfaceLeft = beaverWalkTallNinjaDopedLeft;
                        attackSurfaceRight = beaverAttackTallNinjaDopedRight;
                        attackSurfaceLeft = beaverAttackTallNinjaDopedLeft;
                        crouchedSurfaceRight = beaverCrouchedTallNinjaDopedRight;
                        crouchedSurfaceLeft = beaverCrouchedTallNinjaDopedLeft;
                        hitSurfaceRight = beaverHitTallNinjaDopedRight;
                        hitSurfaceLeft = beaverHitTallNinjaDopedLeft;
                    }
                    else
                    {
                        standSurfaceRight = beaverStandTallNinjaRight;
                        standSurfaceLeft = beaverStandTallNinjaLeft;
                        walkSurfaceRight = beaverWalkTallNinjaRight;
                        walkSurfaceLeft = beaverWalkTallNinjaLeft;
                        attackSurfaceRight = beaverAttackTallNinjaRight;
                        attackSurfaceLeft = beaverAttackTallNinjaLeft;
                        crouchedSurfaceRight = beaverCrouchedTallNinjaRight;
                        crouchedSurfaceLeft = beaverCrouchedTallNinjaLeft;
                        hitSurfaceRight = beaverHitTallNinjaRight;
                        hitSurfaceLeft = beaverHitTallNinjaLeft;
                    }
                }
                else
                {
                    if (isShowDopedColor)
                    {
                        standSurfaceRight = beaverStandTallDopedRight;
                        standSurfaceLeft = beaverStandTallDopedLeft;
                        walkSurfaceRight = beaverWalkTallDopedRight;
                        walkSurfaceLeft = beaverWalkTallDopedLeft;
                        attackSurfaceRight = beaverAttackTallDopedRight;
                        attackSurfaceLeft = beaverAttackTallDopedLeft;
                        crouchedSurfaceRight = beaverCrouchedTallDopedRight;
                        crouchedSurfaceLeft = beaverCrouchedTallDopedLeft;
                        hitSurfaceRight = beaverHitTallDopedRight;
                        hitSurfaceLeft = beaverHitTallDopedLeft;
                    }
                    else
                    {
                        standSurfaceRight = beaverStandTallRight;
                        standSurfaceLeft = beaverStandTallLeft;
                        walkSurfaceRight = beaverWalkTallRight;
                        walkSurfaceLeft = beaverWalkTallLeft;
                        attackSurfaceRight = beaverAttackTallRight;
                        attackSurfaceLeft = beaverAttackTallLeft;
                        crouchedSurfaceRight = beaverCrouchedTallRight;
                        crouchedSurfaceLeft = beaverCrouchedTallLeft;
                        hitSurfaceRight = beaverHitTallRight;
                        hitSurfaceLeft = beaverHitTallLeft;
                    }
                }
            }

            Surface standSurface;
            Surface walkSurface;
            Surface attackSurface;
            Surface crouchedSurface;
            Surface hitSurface;

            if (IsTryingToWalkRight)
            {
                standSurface = standSurfaceRight;
                walkSurface = walkSurfaceRight;
                attackSurface = attackSurfaceRight;
                crouchedSurface = crouchedSurfaceRight;
                hitSurface = hitSurfaceRight;
            }
            else
            {
                standSurface = standSurfaceLeft;
                walkSurface = walkSurfaceLeft;
                attackSurface = attackSurfaceLeft;
                crouchedSurface = crouchedSurfaceLeft;
                hitSurface = hitSurfaceLeft;
            }

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

            if (AttackingCycle.IsFired)
            {
                if (IsTryingToWalkRight)
                    xOffset += 0.5;
                else
                    xOffset -= 0.5;

                return attackSurface;
            }

            if (IsCrouch)
            {
                return crouchedSurface;
            }

            if (isRasta && CurrentJumpAcceleration < 0 && IsTryingToJump)
            {
                if (IsTryingToWalkRight)
                {
                    if (isShowDopedColor)
                    {
                        return beaverStandTallRastaDopedFlyRight;
                    }
                    else
                    {
                        return beaverStandTallRastaFlyRight;
                    }
                }
                else
                {
                    if (isShowDopedColor)
                    {
                        return beaverStandTallRastaDopedFlyLeft;
                    }
                    else
                    {
                        return beaverStandTallRastaFlyLeft;
                    }
                }
            }


            if (cycleDivision == 1 || cycleDivision == 3 || IGround == null || CurrentJumpAcceleration != 0)
            {
                return walkSurface;
            }

            if (HitCycle.IsFired)
            {
                return hitSurface;
            }
            
            return standSurface;
        }

        private Surface GetNinjaFlipSurface(bool isShowDopedColor, out double xOffset)
        {
            int ninjaFlipRotationId = ninjaFlipCycle.GetCycleDivision(8);
            if (IsTryingToWalkRight)
            {
                xOffset = -0.1;
                if (isShowDopedColor)
                {
                    switch (ninjaFlipRotationId)
                    {
                        case 0:
                            return ninjaFlipDopedRight1;
                        case 1:
                            return ninjaFlipDopedRight2;
                        case 2:
                            return ninjaFlipDopedRight3;
                        case 3:
                            return ninjaFlipDopedRight4;
                        case 4:
                            return ninjaFlipDopedRight5;
                        case 5:
                            return ninjaFlipDopedRight6;
                        case 6:
                            return ninjaFlipDopedRight7;
                        default:
                            return ninjaFlipDopedRight8;
                    }
                }
                else
                {
                    switch (ninjaFlipRotationId)
                    {
                        case 0:
                            return ninjaFlipRight1;
                        case 1:
                            return ninjaFlipRight2;
                        case 2:
                            return ninjaFlipRight3;
                        case 3:
                            return ninjaFlipRight4;
                        case 4:
                            return ninjaFlipRight5;
                        case 5:
                            return ninjaFlipRight6;
                        case 6:
                            return ninjaFlipRight7;
                        default:
                            return ninjaFlipRight8;
                    }
                }
            }
            else
            {
                xOffset = 0.1;
                if (isShowDopedColor)
                {
                    switch (ninjaFlipRotationId)
                    {
                        case 0:
                            return ninjaFlipDopedLeft1;
                        case 1:
                            return ninjaFlipDopedLeft2;
                        case 2:
                            return ninjaFlipDopedLeft3;
                        case 3:
                            return ninjaFlipDopedLeft4;
                        case 4:
                            return ninjaFlipDopedLeft5;
                        case 5:
                            return ninjaFlipDopedLeft6;
                        case 6:
                            return ninjaFlipDopedLeft7;
                        default:
                            return ninjaFlipDopedLeft8;
                    }
                }
                else
                {
                    switch (ninjaFlipRotationId)
                    {
                        case 0:
                            return ninjaFlipLeft1;
                        case 1:
                            return ninjaFlipLeft2;
                        case 2:
                            return ninjaFlipLeft3;
                        case 3:
                            return ninjaFlipLeft4;
                        case 4:
                            return ninjaFlipLeft5;
                        case 5:
                            return ninjaFlipLeft6;
                        case 6:
                            return ninjaFlipLeft7;
                        default:
                            return ninjaFlipLeft8;
                    }
                }
            }
        }

        private Surface GetNunchakuSurface(bool isShowDopedColor, bool isCrouched, bool isTryingToWalkRight, out double xOffset)
        {
            int cycleFrame = nunchakuCycle.GetCycleDivision(8.0);

            //xOffset = 0.03125;

            if (isCrouched)
            {
                if (isTryingToWalkRight)
                {
                    if (isShowDopedColor)
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = 0.09375;
                                return ninjaDopedCrouchedNunchaku1Right;
                            case 1:
                                xOffset = 0.109375;
                                return ninjaDopedCrouchedNunchaku2Right;
                            case 2:
                                xOffset = 0.109375;
                                return ninjaDopedCrouchedNunchaku3Right;
                            case 3:
                                xOffset = 0.109375;
                                return ninjaDopedCrouchedNunchaku4Right;
                            case 4:
                                xOffset = 0.09375;
                                return ninjaDopedCrouchedNunchaku5Right;
                            case 5:
                                xOffset = 0.03125;
                                return ninjaDopedCrouchedNunchaku6Right;
                            case 6:
                                xOffset = 0.03125;
                                return ninjaDopedCrouchedNunchaku7Right;
                            default:
                                xOffset = 0.03125;
                                return ninjaDopedCrouchedNunchaku8Right;
                        }
                    }
                    else
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = 0.09375;
                                return ninjaCrouchedNunchaku1Right;
                            case 1:
                                xOffset = 0.109375;
                                return ninjaCrouchedNunchaku2Right;
                            case 2:
                                xOffset = 0.109375;
                                return ninjaCrouchedNunchaku3Right;
                            case 3:
                                xOffset = 0.109375;
                                return ninjaCrouchedNunchaku4Right;
                            case 4:
                                xOffset = 0.09375;
                                return ninjaCrouchedNunchaku5Right;
                            case 5:
                                xOffset = 0.03125;
                                return ninjaCrouchedNunchaku6Right;
                            case 6:
                                xOffset = 0.03125;
                                return ninjaCrouchedNunchaku7Right;
                            default:
                                xOffset = 0.03125;
                                return ninjaCrouchedNunchaku8Right;
                        }
                    }
                }
                else
                {
                    if (isShowDopedColor)
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = -0.09375;
                                return ninjaDopedCrouchedNunchaku1Left;
                            case 1:
                                xOffset = -0.109375;
                                return ninjaDopedCrouchedNunchaku2Left;
                            case 2:
                                xOffset = -0.109375;
                                return ninjaDopedCrouchedNunchaku3Left;
                            case 3:
                                xOffset = -0.109375;
                                return ninjaDopedCrouchedNunchaku4Left;
                            case 4:
                                xOffset = -0.09375;
                                return ninjaDopedCrouchedNunchaku5Left;
                            case 5:
                                xOffset = -0.03125;
                                return ninjaDopedCrouchedNunchaku6Left;
                            case 6:
                                xOffset = -0.03125;
                                return ninjaDopedCrouchedNunchaku7Left;
                            default:
                                xOffset = -0.03125;
                                return ninjaDopedCrouchedNunchaku8Left;
                        }
                    }
                    else
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = -0.09375;
                                return ninjaCrouchedNunchaku1Left;
                            case 1:
                                xOffset = -0.109375;
                                return ninjaCrouchedNunchaku2Left;
                            case 2:
                                xOffset = -0.109375;
                                return ninjaCrouchedNunchaku3Left;
                            case 3:
                                xOffset = -0.109375;
                                return ninjaCrouchedNunchaku4Left;
                            case 4:
                                xOffset = -0.09375;
                                return ninjaCrouchedNunchaku5Left;
                            case 5:
                                xOffset = -0.03125;
                                return ninjaCrouchedNunchaku6Left;
                            case 6:
                                xOffset = -0.03125;
                                return ninjaCrouchedNunchaku7Left;
                            default:
                                xOffset = -0.03125;
                                return ninjaCrouchedNunchaku8Left;
                        }
                    }
                }
            }
            else
            {
                if (isTryingToWalkRight)
                {
                    if (isShowDopedColor)
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = 0.078125;
                                return ninjaDopedNunchaku1Right;
                            case 1:
                                xOffset = 0.125;
                                return ninjaDopedNunchaku2Right;
                            case 2:
                                xOffset = 0.125;
                                return ninjaDopedNunchaku3Right;
                            case 3:
                                xOffset = 0.125;
                                return ninjaDopedNunchaku4Right;
                            case 4:
                                xOffset = 0.078125;
                                return ninjaDopedNunchaku5Right;
                            case 5:
                                xOffset = 0.0625;
                                return ninjaDopedNunchaku6Right;
                            case 6:
                                xOffset = 0.0625;
                                return ninjaDopedNunchaku7Right;
                            default:
                                xOffset = 0.0625;
                                return ninjaDopedNunchaku8Right;
                        }
                    }
                    else
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = 0.078125;
                                return ninjaNunchaku1Right;
                            case 1:
                                xOffset = 0.125;
                                return ninjaNunchaku2Right;
                            case 2:
                                xOffset = 0.125;
                                return ninjaNunchaku3Right;
                            case 3:
                                xOffset = 0.125;
                                return ninjaNunchaku4Right;
                            case 4:
                                xOffset = 0.078125;
                                return ninjaNunchaku5Right;
                            case 5:
                                xOffset = 0.0625;
                                return ninjaNunchaku6Right;
                            case 6:
                                xOffset = 0.0625;
                                return ninjaNunchaku7Right;
                            default:
                                xOffset = 0.0625;
                                return ninjaNunchaku8Right;
                        }
                    }
                }
                else
                {
                    if (isShowDopedColor)
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = -0.078125;
                                return ninjaDopedNunchaku1Left;
                            case 1:
                                xOffset = -0.125;
                                return ninjaDopedNunchaku2Left;
                            case 2:
                                xOffset = -0.125;
                                return ninjaDopedNunchaku3Left;
                            case 3:
                                xOffset = -0.125;
                                return ninjaDopedNunchaku4Left;
                            case 4:
                                xOffset = -0.078125;
                                return ninjaDopedNunchaku5Left;
                            case 5:
                                xOffset = -0.0625;
                                return ninjaDopedNunchaku6Left;
                            case 6:
                                xOffset = -0.0625;
                                return ninjaDopedNunchaku7Left;
                            default:
                                xOffset = -0.0625;
                                return ninjaDopedNunchaku8Left;
                        }
                    }
                    else
                    {
                        switch (cycleFrame)
                        {
                            case 0:
                                xOffset = -0.078125;
                                return ninjaNunchaku1Left;
                            case 1:
                                xOffset = -0.125;
                                return ninjaNunchaku2Left;
                            case 2:
                                xOffset = -0.125;
                                return ninjaNunchaku3Left;
                            case 3:
                                xOffset = -0.125;
                                return ninjaNunchaku4Left;
                            case 4:
                                xOffset = -0.078125;
                                return ninjaNunchaku5Left;
                            case 5:
                                xOffset = -0.0625;
                                return ninjaNunchaku6Left;
                            case 6:
                                xOffset = -0.0625;
                                return ninjaNunchaku7Left;
                            default:
                                xOffset = -0.0625;
                                return ninjaNunchaku8Left;
                        }
                    }
                }
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Reset health and powerups
        /// </summary>
        internal void ResetHealthAndPowerUps()
        {
            IsTiny = true;
            isRasta = false;
            isDoped = false;
            isBeaver = false;
            isNinja = false;
            Health = 0.5;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Whether sprite is affected by gravity
        /// </summary>
        /// <returns>Whether sprite is affected by gravity</returns>
        protected override bool BuildIsAffectedByGravity()
        {
            return true;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }
        
        protected override bool BuildIsCrossGrounds()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToPunch()
        {
            return true;
        }

        protected override int BuildZIndex()
        {
            return 2;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        /// <summary>
        /// Build width
        /// </summary>
        /// <returns>width</returns>
        protected override double BuildWidth(Random random)
        {
            return 0.7;
        }

        /// <summary>
        /// Build height
        /// </summary>
        /// <returns>height</returns>
        protected override double BuildHeight(Random random)
        {
            return 1.9;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.45;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.016;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.60;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 10;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildBounciness()
        {
            return 1.0;
        }

        protected override double BuildMaxFallingSpeed()
        {
            return double.PositiveInfinity;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            if (isBeaver)
                return GetCurrentSurfaceWithBeaver(out xOffset, out yOffset);

            xOffset = 0;
            yOffset = 0;
            //If currently attacking
            int attackCycleDivision = AttackingCycle.GetCycleDivision(8);

            if (!IsAlive)
                return GetDeadSurface();

            bool isShowDopedColor;
            if (powerUpAnimationCycle.IsFired)
            {
                isShowDopedColor = ((int)(powerUpAnimationCycle.CurrentValue) % 4 >= 2);
            }
            else if (invincibilityCycle.IsFired)
            {
                isShowDopedColor = ((int)(invincibilityCycle.CurrentValue) % 4 >= 2);
            }
            else
            {
                isShowDopedColor = isDoped;
            }

            bool isShowTiny = IsTiny;

            if (changingSizeAnimationCycle.IsFired)
                isShowTiny = changingSizeAnimationCycle.CurrentValue % 4 <= 2;


            
            if (ThrowBallCycle.IsFired && !isNinja)
            {
                if (IsCrouch)
                {
                    if (IsTryingToWalkRight)
                    {
                        xOffset = 0.2;
                        return GetCrouchedAttackFrame1RightSurface(isShowDopedColor, isRasta);
                    }
                    else
                    {
                        xOffset = -0.2;
                        return GetCrouchedAttackFrame1LeftSurface(isShowDopedColor, isRasta);
                    }
                }
                else
                {
                    if (IsTryingToWalkRight)
                    {
                        xOffset = 0.2;
                        return GetAttackFrame1RightSurface(isShowDopedColor, isRasta);
                    }
                    else
                    {
                        xOffset = -0.2;
                        return GetAttackFrame1LeftSurface(isShowDopedColor, isRasta);
                    }
                }
            }
            else if (AttackingCycle.IsFired)
            {
                #region Attacking (punch / kick)
                if (IsCrouch)
                {
                    #region Crouched
                    if (isNinja)
                    {
                        #region Ninja
                        if (isTryUseNunchaku)
                        {
                            #region Nunchaku
                            return GetNunchakuSurface(isShowDopedColor, true, IsTryingToWalkRight, out xOffset);
                            #endregion
                        }
                        else
                        {
                            #region Not nunchaku
                            if (IsTryingToWalkRight)
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = -0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched1Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched1Right;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = 0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched2Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched2Right;
                                    }
                                }
                                else
                                {
                                    xOffset = 0.7;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched3Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched3Right;
                                    }
                                }
                            }
                            else
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = 0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched1Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched1Left;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = -0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched2Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched2Left;
                                    }
                                }
                                else
                                {
                                    xOffset = -0.7;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaCrouched3Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaCrouched3Left;
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not Ninja
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = 0.6;
                                return GetCrouchedAttackFrame2RightSurface(isShowDopedColor, isRasta);
                            }
                            else
                            {
                                xOffset = 0.2;
                                return GetCrouchedAttackFrame1RightSurface(isShowDopedColor, isRasta);
                            }
                        }
                        else
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = -0.6;
                                return GetCrouchedAttackFrame2LeftSurface(isShowDopedColor, isRasta);
                            }
                            else
                            {
                                xOffset = -0.2;
                                return GetCrouchedAttackFrame1LeftSurface(isShowDopedColor, isRasta);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (IGround == null)
                {
                    #region In air
                    if (isNinja)
                    {
                        #region Ninja
                        if (IsTryingToWalkRight)
                        {
                            if (CurrentJumpAcceleration <= 0)
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = 0.7;
                                    yOffset = 0.5;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump3Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump3Right;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = 0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump2Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump2Right;
                                    }
                                }
                                else
                                {
                                    xOffset = -0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump1Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump1Right;
                                    }
                                }
                            }
                            else
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = -0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump1Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump1Right;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = 0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump2Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump2Right;
                                    }
                                }
                                else
                                {
                                    xOffset = 0.7;
                                    yOffset = 0.5;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump3Right;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump3Right;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (CurrentJumpAcceleration <= 0)
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = -0.7;
                                    yOffset = 0.5;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump3Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump3Left;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = -0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump2Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump2Left;
                                    }
                                }
                                else
                                {
                                    xOffset = 0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump1Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump1Left;
                                    }
                                }
                            }
                            else
                            {
                                if (attackCycleDivision >= 6)
                                {
                                    xOffset = 0.4;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump1Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump1Left;
                                    }
                                }
                                else if (attackCycleDivision >= 3)
                                {
                                    xOffset = -0.44;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump2Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump2Left;
                                    }
                                }
                                else
                                {
                                    xOffset = -0.7;
                                    yOffset = 0.5;
                                    if (isShowDopedColor)
                                    {
                                        return ninjaDopedKatanaJump3Left;
                                    }
                                    else
                                    {
                                        return ninjaKatanaJump3Left;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not Ninja
                        if (isShowTiny)
                        {
                            #region Tiny
                            if (IsTryingToWalkRight)
                            {
                                if (attackCycleDivision < 4)
                                {
                                    xOffset = 0.35;
                                    yOffset = 0.1;
                                    return GetKickFrame2RightSurfaceTiny(isShowDopedColor);
                                }
                                else
                                {
                                    xOffset = -0.2;
                                    yOffset = 0.0;
                                    return GetKickFrame1RightSurfaceTiny(isShowDopedColor);
                                }
                            }
                            else
                            {
                                if (attackCycleDivision < 4)
                                {
                                    xOffset = -0.35;
                                    yOffset = 0.1;
                                    return GetKickFrame2LeftSurfaceTiny(isShowDopedColor);
                                }
                                else
                                {
                                    xOffset = 0.2;
                                    yOffset = 0.0;
                                    return GetKickFrame1LeftSurfaceTiny(isShowDopedColor);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Not tiny
                            if (IsTryingToWalkRight)
                            {
                                if (attackCycleDivision < 4)
                                {
                                    xOffset = 0.35;
                                    yOffset = 0.1;
                                    return GetKickFrame2RightSurface(isShowDopedColor, isRasta);
                                }
                                else
                                {
                                    xOffset = -0.2;
                                    yOffset = 0.0;
                                    return GetKickFrame1RightSurface(isShowDopedColor, isRasta);
                                }
                            }
                            else
                            {
                                if (attackCycleDivision < 4)
                                {
                                    xOffset = -0.35;
                                    yOffset = 0.1;
                                    return GetKickFrame2LeftSurface(isShowDopedColor, isRasta);
                                }
                                else
                                {
                                    xOffset = 0.2;
                                    yOffset = 0.0;
                                    return GetKickFrame1LeftSurface(isShowDopedColor, isRasta);
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Standing
                    if (isShowTiny)
                    {
                        #region Tiny
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = 0.6;
                                return GetAttackFrame2RightSurfaceTiny(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = 0.2;
                                return GetAttackFrame1RightSurfaceTiny(isShowDopedColor);
                            }
                        }
                        else
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = -0.6;
                                return GetAttackFrame2LeftSurfaceTiny(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = -0.2;
                                return GetAttackFrame1LeftSurfaceTiny(isShowDopedColor);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not tiny
                        if (isNinja)
                        {
                            #region Ninja
                            if (isTryUseNunchaku)
                            {
                                #region Nunchaku
                                return GetNunchakuSurface(isShowDopedColor, false, IsTryingToWalkRight, out xOffset);
                                #endregion
                            }
                            else
                            {
                                #region Not using nunchakus
                                if (IsTryingToWalkRight)
                                {
                                    if (attackCycleDivision >= 6)
                                    {
                                        xOffset = -0.4;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand1Right;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand1Right;
                                        }
                                    }
                                    else if (attackCycleDivision >= 3)
                                    {
                                        xOffset = 0.44;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand2Right;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand2Right;
                                        }
                                    }
                                    else
                                    {
                                        xOffset = 0.7;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand3Right;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand3Right;
                                        }
                                    }
                                }
                                else
                                {
                                    if (attackCycleDivision >= 6)
                                    {
                                        xOffset = 0.4;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand1Left;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand1Left;
                                        }
                                    }
                                    else if (attackCycleDivision >= 3)
                                    {
                                        xOffset = -0.44;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand2Left;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand2Left;
                                        }
                                    }
                                    else
                                    {
                                        xOffset = -0.7;
                                        if (isShowDopedColor)
                                        {
                                            return ninjaDopedKatanaStand3Left;
                                        }
                                        else
                                        {
                                            return ninjaKatanaStand3Left;
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Not Ninja
                            if (IsTryingToWalkRight)
                            {
                                if (attackCycleDivision >= 4)
                                {
                                    xOffset = 0.6;
                                    return GetAttackFrame2RightSurface(isShowDopedColor, isRasta);
                                }
                                else
                                {
                                    xOffset = 0.2;
                                    return GetAttackFrame1RightSurface(isShowDopedColor, isRasta);
                                }
                            }
                            else
                            {
                                if (attackCycleDivision >= 4)
                                {
                                    xOffset = -0.6;
                                    return GetAttackFrame2LeftSurface(isShowDopedColor, isRasta);
                                }
                                else
                                {
                                    xOffset = -0.2;
                                    return GetAttackFrame1LeftSurface(isShowDopedColor, isRasta);
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }

            #region Crouched and not attacking
            if (IsCrouch)
            {
                if (HitCycle.IsFired && !isRasta)
                {
                    if (isNinja)
                    {
                        if (isShowDopedColor)
                        {
                            if (IsTryingToWalkRight)
                                return ninjaDopedCrouchedHitRight;
                            else
                                return ninjaDopedCrouchedHitLeft;
                        }
                        else
                        {
                            if (IsTryingToWalkRight)
                                return ninjaCrouchedHitRight;
                            else
                                return ninjaCrouchedHitLeft;
                        }
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return GetCrouchedHitRightSurface(isShowDopedColor);
                        else
                            return GetCrouchedHitLeftSurface(isShowDopedColor);
                    }
                }
                else
                {
                    if (IsTryingToWalkRight)
                    {
                        if (isRasta && IsTryingToJump && CurrentJumpAcceleration < 0)
                        {
                            xOffset = -0.1;
                            if (isShowDopedColor)
                                return rastaFlyCrouchedDopedRight;
                            else
                                return rastaFlyCrouchedRight;
                        }
                        return GetCrouchedRightSurface(isShowDopedColor, isRasta,isNinja);
                    }
                    else
                    {
                        if (isRasta && IsTryingToJump && CurrentJumpAcceleration < 0)
                        {
                            xOffset = 0.1;
                            if (isShowDopedColor)
                                return rastaFlyCrouchedDopedLeft;
                            else
                                return rastaFlyCrouchedLeft;
                        }
                        return GetCrouchedLeftSurface(isShowDopedColor, isRasta, isNinja);
                    }
                }
            }
            #endregion

            if (CurrentJumpAcceleration != 0)
            {
                #region Jumping or falling while being hit
                if (HitCycle.IsFired && !isRasta)
                {
                    if (isNinja)
                    {
                        if (isShowDopedColor)
                        {
                            if (IsTryingToWalkRight)
                                return ninjaDopedHitRight;
                            else
                                return ninjaDopedHitLeft;
                        }
                        else
                        {
                            if (IsTryingToWalkRight)
                                return ninjaHitRight;
                            else
                                return ninjaHitLeft;
                        }
                    }
                    else if (isShowTiny)
                    {
                        #region Tiny
                        if (IsTryingToWalkRight)
                            return GetHitRightSurfaceTiny(isShowDopedColor);
                        else
                            return GetHitLeftSurfaceTiny(isShowDopedColor);
                        #endregion
                    }
                    else
                    {
                        #region Not tiny
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface(isShowDopedColor);
                        else
                            return GetHitLeftSurface(isShowDopedColor);
                        #endregion
                    }
                }
                #endregion

                #region Jumping or falling
                if (isShowTiny)
                {
                    #region Tiny
                    if (IsTryingToWalkRight)
                        return GetWalking1RightSurfaceTiny(isShowDopedColor);
                    else
                        return GetWalking1LeftSurfaceTiny(isShowDopedColor);
                    #endregion
                }
                else
                {
                    #region Not tiny
                    if (isNinja)
                    {
                        #region Jumping or falling AS ninja
                        return GetNinjaFlipSurface(isShowDopedColor, out xOffset);
                        #endregion
                    }
                    else
                    {
                        #region Jumping or falling NOT as ninja
                        if (IsTryingToWalkRight)
                        {
                            if (isRasta && IsTryingToJump && CurrentJumpAcceleration < 0) //falling as rasta
                            {
                                xOffset = -0.33;
                                return GetFlyRightSurface(isShowDopedColor);
                            }
                            else
                                return GetWalking1RightSurface(isShowDopedColor, isRasta, isNinja);
                        }
                        else
                        {
                            if (isRasta && IsTryingToJump && CurrentJumpAcceleration < 0) //falling as rasta
                            {
                                xOffset = 0.33;
                                return GetFlyLeftSurface(isShowDopedColor);
                            }
                            else
                                return GetWalking1LeftSurface(isShowDopedColor, isRasta, isNinja);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                #region Walking
                if (isShowTiny)
                {
                    #region Tiny
                    if (cycleDivision == 1)
                    {
                        if (HitCycle.IsFired && !isRasta && !isNinja)
                        {
                            if (IsTryingToWalkRight)
                                return GetHitRightSurfaceTiny(isShowDopedColor);
                            else
                                return GetHitLeftSurfaceTiny(isShowDopedColor);
                        }

                        if (IsTryingToWalkRight)
                            return GetWalking1RightSurfaceTiny(isShowDopedColor);
                        else
                            return GetWalking1LeftSurfaceTiny(isShowDopedColor);
                    }
                    else if (cycleDivision == 3)
                    {
                        if (HitCycle.IsFired && !isRasta && !isNinja)
                        {
                            if (IsTryingToWalkRight)
                                return GetHitRightSurfaceTiny(isShowDopedColor);
                            else
                                return GetHitLeftSurfaceTiny(isShowDopedColor);
                        }

                        if (IsTryingToWalkRight)
                            return GetWalking2RightSurfaceTiny(isShowDopedColor);
                        else
                            return GetWalking2LeftSurfaceTiny(isShowDopedColor);
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return GetStandingRightSurfaceTiny(isShowDopedColor);
                        else
                            return GetStandingLeftSurfaceTiny(isShowDopedColor);
                    }
                    #endregion
                }
                else
                {
                    #region Not tiny
                    if (cycleDivision == 1)
                    {
                        if (HitCycle.IsFired && !isRasta && !isNinja)
                        {
                            if (IsTryingToWalkRight)
                                return GetHitRightSurface(isShowDopedColor);
                            else
                                return GetHitLeftSurface(isShowDopedColor);
                        }

                        if (IsTryingToWalkRight)
                            return GetWalking1RightSurface(isShowDopedColor, isRasta, isNinja);
                        else
                            return GetWalking1LeftSurface(isShowDopedColor, isRasta, isNinja);
                    }
                    else if (cycleDivision == 3)
                    {
                        if (HitCycle.IsFired && !isRasta && !isNinja)
                        {
                            if (IsTryingToWalkRight)
                                return GetHitRightSurface(isShowDopedColor);
                            else
                                return GetHitLeftSurface(isShowDopedColor);
                        }

                        if (IsTryingToWalkRight)
                            return GetWalking2RightSurface(isShowDopedColor, isRasta, isNinja);
                        else
                            return GetWalking2LeftSurface(isShowDopedColor, isRasta, isNinja);
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return GetStandingRightSurface(isShowDopedColor, isRasta, isNinja);
                        else
                            return GetStandingLeftSurface(isShowDopedColor, isRasta, isNinja);
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Standing
                if (isShowTiny)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking1RightSurfaceTiny(isShowDopedColor);
                    else
                        return GetWalking1LeftSurfaceTiny(isShowDopedColor);
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface(isShowDopedColor, isRasta, isNinja);
                    else
                        return GetStandingLeftSurface(isShowDopedColor, isRasta, isNinja);
                }
                #endregion
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether sprite can throw fire balls
        /// </summary>
        public bool IsDoped
        {
            get { return isDoped; }
            set { isDoped = value; }
        }

        /// <summary>
        /// Whether sprite can fly
        /// </summary>
        public bool IsRasta
        {
            get { return isRasta; }
            set { isRasta = value; }
        }

        /// <summary>
        /// Whether sprite is on beaver
        /// </summary>
        public bool IsBeaver
        {
            get { return isBeaver; }
            set { isBeaver = value; }
        }

        /// <summary>
        /// Whether sprite is currently a ninja
        /// </summary>
        public bool IsNinja
        {
            get { return isNinja; }
            set { isNinja = value; }
        }

        /// <summary>
        /// Whether sprite is currently trying to throw a fire ball
        /// </summary>
        public bool IsTryThrowingBall
        {
            get { return isTryThrowingBall; }
            set { isTryThrowingBall = value; }
        }

        /// <summary>
        /// Whether sprite is currently using nunchakus
        /// </summary>
        public bool IsTryUseNunchaku
        {
            get { return isTryUseNunchaku; }
            set { isTryUseNunchaku = value; }
        }

        /// <summary>
        /// Whether sprite is trying to throw a ninja's rope
        /// </summary>
        public bool IsTryThrowNinjaRope
        {
            get { return isTryThrowNinjaRope; }
            set { isTryThrowNinjaRope = value; }
        }

        /// <summary>
        /// Health when starting a level or after death
        /// </summary>
        public double DefaultHealth
        {
            get { return defaultHealth; }
        }

        /// <summary>
        /// When sprite is currently moving from one pipe to another (destination pipe)
        /// This value is normally null
        /// </summary>
        public PipeSprite DestinationPipe
        {
            get { return destinationPipe; }
            set { destinationPipe = value; }
        }

        /// <summary>
        /// Power up animation cycle
        /// </summary>
        public Cycle PowerUpAnimationCycle
        {
            get { return powerUpAnimationCycle; }
        }

        /// <summary>
        /// Change size animation cycle
        /// </summary>
        public Cycle ChangingSizeAnimationCycle
        {
            get { return changingSizeAnimationCycle; }
        }

        /// <summary>
        /// Fired when throw fire ball
        /// </summary>
        public Cycle ThrowBallCycle
        {
            get { return throwBallCycle; }
        }

        /// <summary>
        /// Invincibility cycle
        /// </summary>
        public Cycle InvincibilityCycle
        {
            get { return invincibilityCycle; }
        }

        /// <summary>
        /// Time for which the player won't be able to use a vortex because he came from one
        /// </summary>
        public Cycle FromVortexCycle
        {
            get { return fromVortexCycle; }
        }

        /// <summary>
        /// Ninja flip cycle
        /// </summary>
        public Cycle NinjaFlipCycle
        {
            get { return ninjaFlipCycle; }
        }

        /// <summary>
        /// Nunchaku spin cycle
        /// </summary>
        public Cycle NunchakuCycle
        {
            get { return nunchakuCycle; }
        }

        /// <summary>
        /// Latest beaver that was left voluntarily by ninja
        /// </summary>
        public BeaverSprite LatestNinjaBeaver
        {
            get { return latestNinjaBeaver; }
            set { latestNinjaBeaver = value; }
        }
        #endregion
    }
}