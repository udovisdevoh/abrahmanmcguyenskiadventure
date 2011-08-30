using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    enum WalkingDirection { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }

    /// <summary>
    /// Abrahman McGuyenski viewed on map
    /// </summary>
    internal class AbrahmanOnMap : MapSprite
    {
        #region Fields and parts
        private WalkingDirection walkingDirection;

        private bool isTiny;

        private bool isRasta;

        private bool isDoped;

        private bool isNinja;

        private bool isBeaver;

        private Cycle walkingCycle;
        #endregion

        #region Static fields and parts
        private static Surface standRight;

        private static Surface standRightTiny;

        private static Surface standRightDoped;

        private static Surface standRightRasta;

        private static Surface standRightNinja;

        private static Surface standLeft;

        private static Surface standLeftTiny;

        private static Surface standLeftDoped;

        private static Surface standLeftRasta;

        private static Surface standLeftNinja;

        private static Surface standUp;

        private static Surface standUpTiny;

        private static Surface standUpDoped;

        private static Surface standUpRasta;

        private static Surface standUpNinja;

        private static Surface standUpRight;

        private static Surface standUpRightTiny;

        private static Surface standUpRightDoped;

        private static Surface standUpRightRasta;

        private static Surface standUpRightNinja;

        private static Surface standUpLeft;

        private static Surface standUpLeftTiny;

        private static Surface standUpLeftDoped;

        private static Surface standUpLeftRasta;

        private static Surface standUpLeftNinja;

        private static Surface standDown;

        private static Surface standDownTiny;

        private static Surface standDownDoped;

        private static Surface standDownRasta;

        private static Surface standDownNinja;

        private static Surface standDownRight;

        private static Surface standDownRightTiny;

        private static Surface standDownRightDoped;

        private static Surface standDownRightRasta;

        private static Surface standDownRightNinja;

        private static Surface standDownLeft;

        private static Surface standDownLeftTiny;

        private static Surface standDownLeftDoped;

        private static Surface standDownLeftRasta;

        private static Surface standDownLeftNinja;

        private static Surface walk1Right;

        private static Surface walk1RightTiny;

        private static Surface walk1RightDoped;

        private static Surface walk1RightRasta;

        private static Surface walk1RightNinja;

        private static Surface walk1Left;

        private static Surface walk1LeftTiny;

        private static Surface walk1LeftDoped;

        private static Surface walk1LeftRasta;

        private static Surface walk1LeftNinja;

        private static Surface walk1Up;

        private static Surface walk1UpTiny;

        private static Surface walk1UpDoped;

        private static Surface walk1UpRasta;

        private static Surface walk1UpNinja;

        private static Surface walk1UpRight;

        private static Surface walk1UpRightTiny;

        private static Surface walk1UpRightDoped;

        private static Surface walk1UpRightRasta;

        private static Surface walk1UpRightNinja;

        private static Surface walk1UpLeft;

        private static Surface walk1UpLeftTiny;

        private static Surface walk1UpLeftDoped;

        private static Surface walk1UpLeftRasta;

        private static Surface walk1UpLeftNinja;

        private static Surface walk1Down;

        private static Surface walk1DownTiny;

        private static Surface walk1DownDoped;

        private static Surface walk1DownRasta;

        private static Surface walk1DownNinja;

        private static Surface walk1DownRight;

        private static Surface walk1DownRightTiny;

        private static Surface walk1DownRightDoped;

        private static Surface walk1DownRightRasta;

        private static Surface walk1DownRightNinja;

        private static Surface walk1DownLeft;

        private static Surface walk1DownLeftTiny;

        private static Surface walk1DownLeftDoped;

        private static Surface walk1DownLeftRasta;

        private static Surface walk1DownLeftNinja;

        private static Surface walk2Right;

        private static Surface walk2RightTiny;

        private static Surface walk2RightDoped;

        private static Surface walk2RightRasta;

        private static Surface walk2RightNinja;

        private static Surface walk2Left;

        private static Surface walk2LeftTiny;

        private static Surface walk2LeftDoped;

        private static Surface walk2LeftRasta;

        private static Surface walk2LeftNinja;

        private static Surface walk2Up;

        private static Surface walk2UpTiny;

        private static Surface walk2UpDoped;

        private static Surface walk2UpRasta;

        private static Surface walk2UpNinja;

        private static Surface walk2UpRight;

        private static Surface walk2UpRightTiny;

        private static Surface walk2UpRightDoped;

        private static Surface walk2UpRightRasta;

        private static Surface walk2UpRightNinja;

        private static Surface walk2UpLeft;

        private static Surface walk2UpLeftTiny;

        private static Surface walk2UpLeftDoped;

        private static Surface walk2UpLeftRasta;

        private static Surface walk2UpLeftNinja;

        private static Surface walk2Down;

        private static Surface walk2DownTiny;

        private static Surface walk2DownDoped;

        private static Surface walk2DownRasta;

        private static Surface walk2DownNinja;

        private static Surface walk2DownRight;

        private static Surface walk2DownRightTiny;

        private static Surface walk2DownRightDoped;

        private static Surface walk2DownRightRasta;

        private static Surface walk2DownRightNinja;

        private static Surface walk2DownLeft;

        private static Surface walk2DownLeftTiny;

        private static Surface walk2DownLeftDoped;

        private static Surface walk2DownLeftRasta;

        private static Surface walk2DownLeftNinja;

        private static Surface standRightBeaver;

        private static Surface standRightTinyBeaver;

        private static Surface standRightDopedBeaver;

        private static Surface standRightRastaBeaver;

        private static Surface standRightNinjaBeaver;

        private static Surface standLeftBeaver;

        private static Surface standLeftTinyBeaver;

        private static Surface standLeftDopedBeaver;

        private static Surface standLeftRastaBeaver;

        private static Surface standLeftNinjaBeaver;

        private static Surface standUpBeaver;

        private static Surface standUpTinyBeaver;

        private static Surface standUpDopedBeaver;

        private static Surface standUpRastaBeaver;

        private static Surface standUpNinjaBeaver;

        private static Surface standUpRightBeaver;

        private static Surface standUpRightTinyBeaver;

        private static Surface standUpRightDopedBeaver;

        private static Surface standUpRightRastaBeaver;

        private static Surface standUpRightNinjaBeaver;

        private static Surface standUpLeftBeaver;

        private static Surface standUpLeftTinyBeaver;

        private static Surface standUpLeftDopedBeaver;

        private static Surface standUpLeftRastaBeaver;

        private static Surface standUpLeftNinjaBeaver;

        private static Surface standDownBeaver;

        private static Surface standDownTinyBeaver;

        private static Surface standDownDopedBeaver;

        private static Surface standDownRastaBeaver;

        private static Surface standDownNinjaBeaver;

        private static Surface standDownRightBeaver;

        private static Surface standDownRightTinyBeaver;

        private static Surface standDownRightDopedBeaver;

        private static Surface standDownRightRastaBeaver;

        private static Surface standDownRightNinjaBeaver;

        private static Surface standDownLeftBeaver;

        private static Surface standDownLeftTinyBeaver;

        private static Surface standDownLeftDopedBeaver;

        private static Surface standDownLeftRastaBeaver;

        private static Surface standDownLeftNinjaBeaver;

        private static Surface walk1RightBeaver;

        private static Surface walk1RightTinyBeaver;

        private static Surface walk1RightDopedBeaver;

        private static Surface walk1RightRastaBeaver;

        private static Surface walk1RightNinjaBeaver;

        private static Surface walk1LeftBeaver;

        private static Surface walk1LeftTinyBeaver;

        private static Surface walk1LeftDopedBeaver;

        private static Surface walk1LeftRastaBeaver;

        private static Surface walk1LeftNinjaBeaver;

        private static Surface walk1UpBeaver;

        private static Surface walk1UpTinyBeaver;

        private static Surface walk1UpDopedBeaver;

        private static Surface walk1UpRastaBeaver;

        private static Surface walk1UpNinjaBeaver;

        private static Surface walk1UpRightBeaver;

        private static Surface walk1UpRightTinyBeaver;

        private static Surface walk1UpRightDopedBeaver;

        private static Surface walk1UpRightRastaBeaver;

        private static Surface walk1UpRightNinjaBeaver;

        private static Surface walk1UpLeftBeaver;

        private static Surface walk1UpLeftTinyBeaver;

        private static Surface walk1UpLeftDopedBeaver;

        private static Surface walk1UpLeftRastaBeaver;

        private static Surface walk1UpLeftNinjaBeaver;

        private static Surface walk1DownBeaver;

        private static Surface walk1DownTinyBeaver;

        private static Surface walk1DownDopedBeaver;

        private static Surface walk1DownRastaBeaver;

        private static Surface walk1DownNinjaBeaver;

        private static Surface walk1DownRightBeaver;

        private static Surface walk1DownRightTinyBeaver;

        private static Surface walk1DownRightDopedBeaver;

        private static Surface walk1DownRightRastaBeaver;

        private static Surface walk1DownRightNinjaBeaver;

        private static Surface walk1DownLeftBeaver;

        private static Surface walk1DownLeftTinyBeaver;

        private static Surface walk1DownLeftDopedBeaver;

        private static Surface walk1DownLeftRastaBeaver;

        private static Surface walk1DownLeftNinjaBeaver;

        private static Surface walk2RightBeaver;

        private static Surface walk2RightTinyBeaver;

        private static Surface walk2RightDopedBeaver;

        private static Surface walk2RightRastaBeaver;

        private static Surface walk2RightNinjaBeaver;

        private static Surface walk2LeftBeaver;

        private static Surface walk2LeftTinyBeaver;

        private static Surface walk2LeftDopedBeaver;

        private static Surface walk2LeftRastaBeaver;

        private static Surface walk2LeftNinjaBeaver;

        private static Surface walk2UpBeaver;

        private static Surface walk2UpTinyBeaver;

        private static Surface walk2UpDopedBeaver;

        private static Surface walk2UpRastaBeaver;

        private static Surface walk2UpNinjaBeaver;

        private static Surface walk2UpRightBeaver;

        private static Surface walk2UpRightTinyBeaver;

        private static Surface walk2UpRightDopedBeaver;

        private static Surface walk2UpRightRastaBeaver;

        private static Surface walk2UpRightNinjaBeaver;

        private static Surface walk2UpLeftBeaver;

        private static Surface walk2UpLeftTinyBeaver;

        private static Surface walk2UpLeftDopedBeaver;

        private static Surface walk2UpLeftRastaBeaver;

        private static Surface walk2UpLeftNinjaBeaver;

        private static Surface walk2DownBeaver;

        private static Surface walk2DownTinyBeaver;

        private static Surface walk2DownDopedBeaver;

        private static Surface walk2DownRastaBeaver;

        private static Surface walk2DownNinjaBeaver;

        private static Surface walk2DownRightBeaver;

        private static Surface walk2DownRightTinyBeaver;

        private static Surface walk2DownRightDopedBeaver;

        private static Surface walk2DownRightRastaBeaver;

        private static Surface walk2DownRightNinjaBeaver;

        private static Surface walk2DownLeftBeaver;

        private static Surface walk2DownLeftTinyBeaver;

        private static Surface walk2DownLeftDopedBeaver;

        private static Surface walk2DownLeftRastaBeaver;

        private static Surface walk2DownLeftNinjaBeaver;
        #endregion

        #region Constructor
        public AbrahmanOnMap(double xPosition, double yPosition, PlayerSprite playerSprite)
        {
            isTiny = playerSprite.IsTiny;
            isDoped = playerSprite.IsDoped;
            isRasta = playerSprite.IsRasta;
            isNinja = playerSprite.IsNinja;
            isBeaver = playerSprite.IsBeaver;

            XPosition = xPosition;
            YPosition = yPosition;
            walkingCycle = new Cycle(20, true);
            walkingDirection = WalkingDirection.Up;

            if (standUp == null)
            {
                standUp = BuildSpriteSurface("./assets/rendered/abrahman/MapStand.png");
                standUpRight = BuildSpriteSurface("./assets/rendered/abrahman/MapStand45.png");
                standRight = standUp.CreateRotatedSurface(270);
                standDownRight = standUpRight.CreateFlippedVerticalSurface();
                standDown = standUp.CreateFlippedVerticalSurface();
                standDownLeft = standDownRight.CreateFlippedHorizontalSurface();
                standLeft = standRight.CreateFlippedHorizontalSurface();
                standUpLeft = standDownLeft.CreateFlippedVerticalSurface();
                standUpBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaver.png");
                standUpRightBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaver45.png");
                standRightBeaver = standUpBeaver.CreateRotatedSurface(270);
                standDownRightBeaver = standUpRightBeaver.CreateFlippedVerticalSurface();
                standDownBeaver = standUpBeaver.CreateFlippedVerticalSurface();
                standDownLeftBeaver = standDownRightBeaver.CreateFlippedHorizontalSurface();
                standLeftBeaver = standRightBeaver.CreateCompatibleSurface();
                standUpLeftBeaver = standDownLeftBeaver.CreateFlippedVerticalSurface();
                walk1Up = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1.png");
                walk1UpRight = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk145.png");
                walk1Right = walk1Up.CreateRotatedSurface(270);
                walk1DownRight = walk1UpRight.CreateFlippedVerticalSurface();
                walk1Down = walk1Up.CreateFlippedVerticalSurface();
                walk1DownLeft = walk1DownRight.CreateFlippedHorizontalSurface();
                walk1Left = walk1Right.CreateFlippedHorizontalSurface();
                walk1UpLeft = walk1DownLeft.CreateFlippedVerticalSurface();
                walk1UpBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Beaver.png");
                walk1UpRightBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Beaver45.png");
                walk1RightBeaver = walk1UpBeaver.CreateRotatedSurface(270);
                walk1DownRightBeaver = walk1UpRightBeaver.CreateFlippedVerticalSurface();
                walk1DownBeaver = walk1UpBeaver.CreateFlippedVerticalSurface();
                walk1DownLeftBeaver = walk1DownRightBeaver.CreateFlippedHorizontalSurface();
                walk1LeftBeaver = walk1RightBeaver.CreateCompatibleSurface();
                walk1UpLeftBeaver = walk1DownLeftBeaver.CreateFlippedVerticalSurface();
                walk2Up = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2.png");
                walk2UpRight = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk245.png");
                walk2Right = walk2Up.CreateRotatedSurface(270);
                walk2DownRight = walk2UpRight.CreateFlippedVerticalSurface();
                walk2Down = walk2Up.CreateFlippedVerticalSurface();
                walk2DownLeft = walk2DownRight.CreateFlippedHorizontalSurface();
                walk2Left = walk2Right.CreateFlippedHorizontalSurface();
                walk2UpLeft = walk2DownLeft.CreateFlippedVerticalSurface();
                walk2UpBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Beaver.png");
                walk2UpRightBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Beaver45.png");
                walk2RightBeaver = walk2UpBeaver.CreateRotatedSurface(270);
                walk2DownRightBeaver = walk2UpRightBeaver.CreateFlippedVerticalSurface();
                walk2DownBeaver = walk2UpBeaver.CreateFlippedVerticalSurface();
                walk2DownLeftBeaver = walk2DownRightBeaver.CreateFlippedHorizontalSurface();
                walk2LeftBeaver = walk2RightBeaver.CreateCompatibleSurface();
                walk2UpLeftBeaver = walk2DownLeftBeaver.CreateFlippedVerticalSurface();

                standUpDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapStandDoped.png");
                standUpRightDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapStand45Doped.png");
                standRightDoped = standUpDoped.CreateRotatedSurface(270);
                standDownRightDoped = standUpRightDoped.CreateFlippedVerticalSurface();
                standDownDoped = standUpDoped.CreateFlippedVerticalSurface();
                standDownLeftDoped = standDownRightDoped.CreateFlippedHorizontalSurface();
                standLeftDoped = standRightDoped.CreateFlippedHorizontalSurface();
                standUpLeftDoped = standDownLeftDoped.CreateFlippedVerticalSurface();
                standUpDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverDoped.png");
                standUpRightDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaver45Doped.png");
                standRightDopedBeaver = standUpDopedBeaver.CreateRotatedSurface(270);
                standDownRightDopedBeaver = standUpRightDopedBeaver.CreateFlippedVerticalSurface();
                standDownDopedBeaver = standUpDopedBeaver.CreateFlippedVerticalSurface();
                standDownLeftDopedBeaver = standDownRightDopedBeaver.CreateFlippedHorizontalSurface();
                standLeftDopedBeaver = standRightDopedBeaver.CreateCompatibleSurface();
                standUpLeftDopedBeaver = standDownLeftDopedBeaver.CreateFlippedVerticalSurface();
                walk1UpDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Doped.png");
                walk1UpRightDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk145Doped.png");
                walk1RightDoped = walk1UpDoped.CreateRotatedSurface(270);
                walk1DownRightDoped = walk1UpRightDoped.CreateFlippedVerticalSurface();
                walk1DownDoped = walk1UpDoped.CreateFlippedVerticalSurface();
                walk1DownLeftDoped = walk1DownRightDoped.CreateFlippedHorizontalSurface();
                walk1LeftDoped = walk1RightDoped.CreateFlippedHorizontalSurface();
                walk1UpLeftDoped = walk1DownLeftDoped.CreateFlippedVerticalSurface();
                walk1UpDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverDoped.png");
                walk1UpRightDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Beaver45Doped.png");
                walk1RightDopedBeaver = walk1UpDopedBeaver.CreateRotatedSurface(270);
                walk1DownRightDopedBeaver = walk1UpRightDopedBeaver.CreateFlippedVerticalSurface();
                walk1DownDopedBeaver = walk1UpDopedBeaver.CreateFlippedVerticalSurface();
                walk1DownLeftDopedBeaver = walk1DownRightDopedBeaver.CreateFlippedHorizontalSurface();
                walk1LeftDopedBeaver = walk1RightDopedBeaver.CreateCompatibleSurface();
                walk1UpLeftDopedBeaver = walk1DownLeftDopedBeaver.CreateFlippedVerticalSurface();
                walk2UpDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Doped.png");
                walk2UpRightDoped = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk245Doped.png");
                walk2RightDoped = walk2UpDoped.CreateRotatedSurface(270);
                walk2DownRightDoped = walk2UpRightDoped.CreateFlippedVerticalSurface();
                walk2DownDoped = walk2UpDoped.CreateFlippedVerticalSurface();
                walk2DownLeftDoped = walk2DownRightDoped.CreateFlippedHorizontalSurface();
                walk2LeftDoped = walk2RightDoped.CreateFlippedHorizontalSurface();
                walk2UpLeftDoped = walk2DownLeftDoped.CreateFlippedVerticalSurface();
                walk2UpDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverDoped.png");
                walk2UpRightDopedBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Beaver45Doped.png");
                walk2RightDopedBeaver = walk2UpDopedBeaver.CreateRotatedSurface(270);
                walk2DownRightDopedBeaver = walk2UpRightDopedBeaver.CreateFlippedVerticalSurface();
                walk2DownDopedBeaver = walk2UpDopedBeaver.CreateFlippedVerticalSurface();
                walk2DownLeftDopedBeaver = walk2DownRightDopedBeaver.CreateFlippedHorizontalSurface();
                walk2LeftDopedBeaver = walk2RightDopedBeaver.CreateCompatibleSurface();
                walk2UpLeftDopedBeaver = walk2DownLeftDopedBeaver.CreateFlippedVerticalSurface();

                standUpRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapStandRasta.png");
                standUpRightRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapStandRasta45.png");
                standRightRasta = standUpRasta.CreateRotatedSurface(270);
                standDownRightRasta = standUpRightRasta.CreateFlippedVerticalSurface();
                standDownRasta = standUpRasta.CreateFlippedVerticalSurface();
                standDownLeftRasta = standDownRightRasta.CreateFlippedHorizontalSurface();
                standLeftRasta = standRightRasta.CreateFlippedHorizontalSurface();
                standUpLeftRasta = standDownLeftRasta.CreateFlippedVerticalSurface();
                standUpRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverRasta.png");
                standUpRightRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverRasta45.png");
                standRightRastaBeaver = standUpRastaBeaver.CreateRotatedSurface(270);
                standDownRightRastaBeaver = standUpRightRastaBeaver.CreateFlippedVerticalSurface();
                standDownRastaBeaver = standUpRastaBeaver.CreateFlippedVerticalSurface();
                standDownLeftRastaBeaver = standDownRightRastaBeaver.CreateFlippedHorizontalSurface();
                standLeftRastaBeaver = standRightRastaBeaver.CreateCompatibleSurface();
                standUpLeftRastaBeaver = standDownLeftRastaBeaver.CreateFlippedVerticalSurface();
                walk1UpRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Rasta.png");
                walk1UpRightRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Rasta45.png");
                walk1RightRasta = walk1UpRasta.CreateRotatedSurface(270);
                walk1DownRightRasta = walk1UpRightRasta.CreateFlippedVerticalSurface();
                walk1DownRasta = walk1UpRasta.CreateFlippedVerticalSurface();
                walk1DownLeftRasta = walk1DownRightRasta.CreateFlippedHorizontalSurface();
                walk1LeftRasta = walk1RightRasta.CreateFlippedHorizontalSurface();
                walk1UpLeftRasta = walk1DownLeftRasta.CreateFlippedVerticalSurface();
                walk1UpRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverRasta.png");
                walk1UpRightRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverRasta45.png");
                walk1RightRastaBeaver = walk1UpRastaBeaver.CreateRotatedSurface(270);
                walk1DownRightRastaBeaver = walk1UpRightRastaBeaver.CreateFlippedVerticalSurface();
                walk1DownRastaBeaver = walk1UpRastaBeaver.CreateFlippedVerticalSurface();
                walk1DownLeftRastaBeaver = walk1DownRightRastaBeaver.CreateFlippedHorizontalSurface();
                walk1LeftRastaBeaver = walk1RightRastaBeaver.CreateCompatibleSurface();
                walk1UpLeftRastaBeaver = walk1DownLeftRastaBeaver.CreateFlippedVerticalSurface();
                walk2UpRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Rasta.png");
                walk2UpRightRasta = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Rasta45.png");
                walk2RightRasta = walk2UpRasta.CreateRotatedSurface(270);
                walk2DownRightRasta = walk2UpRightRasta.CreateFlippedVerticalSurface();
                walk2DownRasta = walk2UpRasta.CreateFlippedVerticalSurface();
                walk2DownLeftRasta = walk2DownRightRasta.CreateFlippedHorizontalSurface();
                walk2LeftRasta = walk2RightRasta.CreateFlippedHorizontalSurface();
                walk2UpLeftRasta = walk2DownLeftRasta.CreateFlippedVerticalSurface();
                walk2UpRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverRasta.png");
                walk2UpRightRastaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverRasta45.png");
                walk2RightRastaBeaver = walk2UpRastaBeaver.CreateRotatedSurface(270);
                walk2DownRightRastaBeaver = walk2UpRightRastaBeaver.CreateFlippedVerticalSurface();
                walk2DownRastaBeaver = walk2UpRastaBeaver.CreateFlippedVerticalSurface();
                walk2DownLeftRastaBeaver = walk2DownRightRastaBeaver.CreateFlippedHorizontalSurface();
                walk2LeftRastaBeaver = walk2RightRastaBeaver.CreateCompatibleSurface();
                walk2UpLeftRastaBeaver = walk2DownLeftRastaBeaver.CreateFlippedVerticalSurface();

                standUpNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapStandNinja.png");
                standUpRightNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapStandNinja45.png");
                standRightNinja = standUpNinja.CreateRotatedSurface(270);
                standDownRightNinja = standUpRightNinja.CreateFlippedVerticalSurface();
                standDownNinja = standUpNinja.CreateFlippedVerticalSurface();
                standDownLeftNinja = standDownRightNinja.CreateFlippedHorizontalSurface();
                standLeftNinja = standRightNinja.CreateFlippedHorizontalSurface();
                standUpLeftNinja = standDownLeftNinja.CreateFlippedVerticalSurface();
                standUpNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverNinja.png");
                standUpRightNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverNinja45.png");
                standRightNinjaBeaver = standUpNinjaBeaver.CreateRotatedSurface(270);
                standDownRightNinjaBeaver = standUpRightNinjaBeaver.CreateFlippedVerticalSurface();
                standDownNinjaBeaver = standUpNinjaBeaver.CreateFlippedVerticalSurface();
                standDownLeftNinjaBeaver = standDownRightNinjaBeaver.CreateFlippedHorizontalSurface();
                standLeftNinjaBeaver = standRightNinjaBeaver.CreateCompatibleSurface();
                standUpLeftNinjaBeaver = standDownLeftNinjaBeaver.CreateFlippedVerticalSurface();
                walk1UpNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Ninja.png");
                walk1UpRightNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Ninja45.png");
                walk1RightNinja = walk1UpNinja.CreateRotatedSurface(270);
                walk1DownRightNinja = walk1UpRightNinja.CreateFlippedVerticalSurface();
                walk1DownNinja = walk1UpNinja.CreateFlippedVerticalSurface();
                walk1DownLeftNinja = walk1DownRightNinja.CreateFlippedHorizontalSurface();
                walk1LeftNinja = walk1RightNinja.CreateFlippedHorizontalSurface();
                walk1UpLeftNinja = walk1DownLeftNinja.CreateFlippedVerticalSurface();
                walk1UpNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverNinja.png");
                walk1UpRightNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverNinja45.png");
                walk1RightNinjaBeaver = walk1UpNinjaBeaver.CreateRotatedSurface(270);
                walk1DownRightNinjaBeaver = walk1UpRightNinjaBeaver.CreateFlippedVerticalSurface();
                walk1DownNinjaBeaver = walk1UpNinjaBeaver.CreateFlippedVerticalSurface();
                walk1DownLeftNinjaBeaver = walk1DownRightNinjaBeaver.CreateFlippedHorizontalSurface();
                walk1LeftNinjaBeaver = walk1RightNinjaBeaver.CreateCompatibleSurface();
                walk1UpLeftNinjaBeaver = walk1DownLeftNinjaBeaver.CreateFlippedVerticalSurface();
                walk2UpNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Ninja.png");
                walk2UpRightNinja = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Ninja45.png");
                walk2RightNinja = walk2UpNinja.CreateRotatedSurface(270);
                walk2DownRightNinja = walk2UpRightNinja.CreateFlippedVerticalSurface();
                walk2DownNinja = walk2UpNinja.CreateFlippedVerticalSurface();
                walk2DownLeftNinja = walk2DownRightNinja.CreateFlippedHorizontalSurface();
                walk2LeftNinja = walk2RightNinja.CreateFlippedHorizontalSurface();
                walk2UpLeftNinja = walk2DownLeftNinja.CreateFlippedVerticalSurface();
                walk2UpNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverNinja.png");
                walk2UpRightNinjaBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverNinja45.png");
                walk2RightNinjaBeaver = walk2UpNinjaBeaver.CreateRotatedSurface(270);
                walk2DownRightNinjaBeaver = walk2UpRightNinjaBeaver.CreateFlippedVerticalSurface();
                walk2DownNinjaBeaver = walk2UpNinjaBeaver.CreateFlippedVerticalSurface();
                walk2DownLeftNinjaBeaver = walk2DownRightNinjaBeaver.CreateFlippedHorizontalSurface();
                walk2LeftNinjaBeaver = walk2RightNinjaBeaver.CreateCompatibleSurface();
                walk2UpLeftNinjaBeaver = walk2DownLeftNinjaBeaver.CreateFlippedVerticalSurface();

                standUpTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapStandTiny.png");
                standUpRightTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapStandTiny45.png");
                standRightTiny = standUpTiny.CreateRotatedSurface(270);
                standDownRightTiny = standUpRightTiny.CreateFlippedVerticalSurface();
                standDownTiny = standUpTiny.CreateFlippedVerticalSurface();
                standDownLeftTiny = standDownRightTiny.CreateFlippedHorizontalSurface();
                standLeftTiny = standRightTiny.CreateFlippedHorizontalSurface();
                standUpLeftTiny = standDownLeftTiny.CreateFlippedVerticalSurface();
                standUpTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverTiny.png");
                standUpRightTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapStandBeaverTiny45.png");
                standRightTinyBeaver = standUpTinyBeaver.CreateRotatedSurface(270);
                standDownRightTinyBeaver = standUpRightTinyBeaver.CreateFlippedVerticalSurface();
                standDownTinyBeaver = standUpTinyBeaver.CreateFlippedVerticalSurface();
                standDownLeftTinyBeaver = standDownRightTinyBeaver.CreateFlippedHorizontalSurface();
                standLeftTinyBeaver = standRightTinyBeaver.CreateCompatibleSurface();
                standUpLeftTinyBeaver = standDownLeftTinyBeaver.CreateFlippedVerticalSurface();
                walk1UpTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Tiny.png");
                walk1UpRightTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1Tiny45.png");
                walk1RightTiny = walk1UpTiny.CreateRotatedSurface(270);
                walk1DownRightTiny = walk1UpRightTiny.CreateFlippedVerticalSurface();
                walk1DownTiny = walk1UpTiny.CreateFlippedVerticalSurface();
                walk1DownLeftTiny = walk1DownRightTiny.CreateFlippedHorizontalSurface();
                walk1LeftTiny = walk1RightTiny.CreateFlippedHorizontalSurface();
                walk1UpLeftTiny = walk1DownLeftTiny.CreateFlippedVerticalSurface();
                walk1UpTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverTiny.png");
                walk1UpRightTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk1BeaverTiny45.png");
                walk1RightTinyBeaver = walk1UpTinyBeaver.CreateRotatedSurface(270);
                walk1DownRightTinyBeaver = walk1UpRightTinyBeaver.CreateFlippedVerticalSurface();
                walk1DownTinyBeaver = walk1UpTinyBeaver.CreateFlippedVerticalSurface();
                walk1DownLeftTinyBeaver = walk1DownRightTinyBeaver.CreateFlippedHorizontalSurface();
                walk1LeftTinyBeaver = walk1RightTinyBeaver.CreateCompatibleSurface();
                walk1UpLeftTinyBeaver = walk1DownLeftTinyBeaver.CreateFlippedVerticalSurface();
                walk2UpTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Tiny.png");
                walk2UpRightTiny = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2Tiny45.png");
                walk2RightTiny = walk2UpTiny.CreateRotatedSurface(270);
                walk2DownRightTiny = walk2UpRightTiny.CreateFlippedVerticalSurface();
                walk2DownTiny = walk2UpTiny.CreateFlippedVerticalSurface();
                walk2DownLeftTiny = walk2DownRightTiny.CreateFlippedHorizontalSurface();
                walk2LeftTiny = walk2RightTiny.CreateFlippedHorizontalSurface();
                walk2UpLeftTiny = walk2DownLeftTiny.CreateFlippedVerticalSurface();
                walk2UpTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverTiny.png");
                walk2UpRightTinyBeaver = BuildSpriteSurface("./assets/rendered/abrahman/MapWalk2BeaverTiny45.png");
                walk2RightTinyBeaver = walk2UpTinyBeaver.CreateRotatedSurface(270);
                walk2DownRightTinyBeaver = walk2UpRightTinyBeaver.CreateFlippedVerticalSurface();
                walk2DownTinyBeaver = walk2UpTinyBeaver.CreateFlippedVerticalSurface();
                walk2DownLeftTinyBeaver = walk2DownRightTinyBeaver.CreateFlippedHorizontalSurface();
                walk2LeftTinyBeaver = walk2RightTinyBeaver.CreateCompatibleSurface();
                walk2UpLeftTinyBeaver = walk2DownLeftTinyBeaver.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Overrides
        internal override Surface GetSurface()
        {
            Surface currentStandUp, currentStandUpRight, currentStandRight, currentStandDownRight, currentStandDown, currentStandDownLeft, currentStandLeft, currentStandUpLeft;
            Surface currentWalk1Up, currentWalk1UpRight, currentWalk1Right, currentWalk1DownRight, currentWalk1Down, currentWalk1DownLeft, currentWalk1Left, currentWalk1UpLeft;
            Surface currentWalk2Up, currentWalk2UpRight, currentWalk2Right, currentWalk2DownRight, currentWalk2Down, currentWalk2DownLeft, currentWalk2Left, currentWalk2UpLeft;
            Surface up, upRight, right, downRight, down, downLeft, left, upLeft;

            if (isTiny)
            {
                if (isBeaver)
                {
                    currentStandUp = standUpTinyBeaver;
                    currentStandUpRight = standUpRightTinyBeaver;
                    currentStandRight = standRightTinyBeaver;
                    currentStandDownRight = standDownRightTinyBeaver;
                    currentStandDown = standDownTinyBeaver;
                    currentStandDownLeft = standDownLeftTinyBeaver;
                    currentStandLeft = standLeftTinyBeaver;
                    currentStandUpLeft = standUpLeftTinyBeaver;
                    currentWalk1Up = walk1UpTinyBeaver;
                    currentWalk1UpRight = walk1UpRightTinyBeaver;
                    currentWalk1Right = walk1RightTinyBeaver;
                    currentWalk1DownRight = walk1DownRightTinyBeaver;
                    currentWalk1Down = walk1DownTinyBeaver;
                    currentWalk1DownLeft = walk1DownLeftTinyBeaver;
                    currentWalk1Left = walk1LeftTinyBeaver;
                    currentWalk1UpLeft = walk1UpLeftTinyBeaver;
                    currentWalk2Up = walk2UpTinyBeaver;
                    currentWalk2UpRight = walk2UpRightTinyBeaver;
                    currentWalk2Right = walk2RightTinyBeaver;
                    currentWalk2DownRight = walk2DownRightTinyBeaver;
                    currentWalk2Down = walk2DownTinyBeaver;
                    currentWalk2DownLeft = walk2DownLeftTinyBeaver;
                    currentWalk2Left = walk2LeftTinyBeaver;
                    currentWalk2UpLeft = walk2UpLeftTinyBeaver;
                }
                else
                {
                    currentStandUp = standUpTiny;
                    currentStandUpRight = standUpRightTiny;
                    currentStandRight = standRightTiny;
                    currentStandDownRight = standDownRightTiny;
                    currentStandDown = standDownTiny;
                    currentStandDownLeft = standDownLeftTiny;
                    currentStandLeft = standLeftTiny;
                    currentStandUpLeft = standUpLeftTiny;
                    currentWalk1Up = walk1UpTiny;
                    currentWalk1UpRight = walk1UpRightTiny;
                    currentWalk1Right = walk1RightTiny;
                    currentWalk1DownRight = walk1DownRightTiny;
                    currentWalk1Down = walk1DownTiny;
                    currentWalk1DownLeft = walk1DownLeftTiny;
                    currentWalk1Left = walk1LeftTiny;
                    currentWalk1UpLeft = walk1UpLeftTiny;
                    currentWalk2Up = walk2UpTiny;
                    currentWalk2UpRight = walk2UpRightTiny;
                    currentWalk2Right = walk2RightTiny;
                    currentWalk2DownRight = walk2DownRightTiny;
                    currentWalk2Down = walk2DownTiny;
                    currentWalk2DownLeft = walk2DownLeftTiny;
                    currentWalk2Left = walk2LeftTiny;
                    currentWalk2UpLeft = walk2UpLeftTiny;
                }
            }
            else if (isDoped)
            {
                if (isBeaver)
                {
                    currentStandUp = standUpDopedBeaver;
                    currentStandUpRight = standUpRightDopedBeaver;
                    currentStandRight = standRightDopedBeaver;
                    currentStandDownRight = standDownRightDopedBeaver;
                    currentStandDown = standDownDopedBeaver;
                    currentStandDownLeft = standDownLeftDopedBeaver;
                    currentStandLeft = standLeftDopedBeaver;
                    currentStandUpLeft = standUpLeftDopedBeaver;
                    currentWalk1Up = walk1UpDopedBeaver;
                    currentWalk1UpRight = walk1UpRightDopedBeaver;
                    currentWalk1Right = walk1RightDopedBeaver;
                    currentWalk1DownRight = walk1DownRightDopedBeaver;
                    currentWalk1Down = walk1DownDopedBeaver;
                    currentWalk1DownLeft = walk1DownLeftDopedBeaver;
                    currentWalk1Left = walk1LeftDopedBeaver;
                    currentWalk1UpLeft = walk1UpLeftDopedBeaver;
                    currentWalk2Up = walk2UpDopedBeaver;
                    currentWalk2UpRight = walk2UpRightDopedBeaver;
                    currentWalk2Right = walk2RightDopedBeaver;
                    currentWalk2DownRight = walk2DownRightDopedBeaver;
                    currentWalk2Down = walk2DownDopedBeaver;
                    currentWalk2DownLeft = walk2DownLeftDopedBeaver;
                    currentWalk2Left = walk2LeftDopedBeaver;
                    currentWalk2UpLeft = walk2UpLeftDopedBeaver;
                }
                else
                {
                    currentStandUp = standUpDoped;
                    currentStandUpRight = standUpRightDoped;
                    currentStandRight = standRightDoped;
                    currentStandDownRight = standDownRightDoped;
                    currentStandDown = standDownDoped;
                    currentStandDownLeft = standDownLeftDoped;
                    currentStandLeft = standLeftDoped;
                    currentStandUpLeft = standUpLeftDoped;
                    currentWalk1Up = walk1UpDoped;
                    currentWalk1UpRight = walk1UpRightDoped;
                    currentWalk1Right = walk1RightDoped;
                    currentWalk1DownRight = walk1DownRightDoped;
                    currentWalk1Down = walk1DownDoped;
                    currentWalk1DownLeft = walk1DownLeftDoped;
                    currentWalk1Left = walk1LeftDoped;
                    currentWalk1UpLeft = walk1UpLeftDoped;
                    currentWalk2Up = walk2UpDoped;
                    currentWalk2UpRight = walk2UpRightDoped;
                    currentWalk2Right = walk2RightDoped;
                    currentWalk2DownRight = walk2DownRightDoped;
                    currentWalk2Down = walk2DownDoped;
                    currentWalk2DownLeft = walk2DownLeftDoped;
                    currentWalk2Left = walk2LeftDoped;
                    currentWalk2UpLeft = walk2UpLeftDoped;
                }
            }
            else if (isRasta)
            {
                if (isBeaver)
                {
                    currentStandUp = standUpRastaBeaver;
                    currentStandUpRight = standUpRightRastaBeaver;
                    currentStandRight = standRightRastaBeaver;
                    currentStandDownRight = standDownRightRastaBeaver;
                    currentStandDown = standDownRastaBeaver;
                    currentStandDownLeft = standDownLeftRastaBeaver;
                    currentStandLeft = standLeftRastaBeaver;
                    currentStandUpLeft = standUpLeftRastaBeaver;
                    currentWalk1Up = walk1UpRastaBeaver;
                    currentWalk1UpRight = walk1UpRightRastaBeaver;
                    currentWalk1Right = walk1RightRastaBeaver;
                    currentWalk1DownRight = walk1DownRightRastaBeaver;
                    currentWalk1Down = walk1DownRastaBeaver;
                    currentWalk1DownLeft = walk1DownLeftRastaBeaver;
                    currentWalk1Left = walk1LeftRastaBeaver;
                    currentWalk1UpLeft = walk1UpLeftRastaBeaver;
                    currentWalk2Up = walk2UpRastaBeaver;
                    currentWalk2UpRight = walk2UpRightRastaBeaver;
                    currentWalk2Right = walk2RightRastaBeaver;
                    currentWalk2DownRight = walk2DownRightRastaBeaver;
                    currentWalk2Down = walk2DownRastaBeaver;
                    currentWalk2DownLeft = walk2DownLeftRastaBeaver;
                    currentWalk2Left = walk2LeftRastaBeaver;
                    currentWalk2UpLeft = walk2UpLeftRastaBeaver;
                }
                else
                {
                    currentStandUp = standUpRasta;
                    currentStandUpRight = standUpRightRasta;
                    currentStandRight = standRightRasta;
                    currentStandDownRight = standDownRightRasta;
                    currentStandDown = standDownRasta;
                    currentStandDownLeft = standDownLeftRasta;
                    currentStandLeft = standLeftRasta;
                    currentStandUpLeft = standUpLeftRasta;
                    currentWalk1Up = walk1UpRasta;
                    currentWalk1UpRight = walk1UpRightRasta;
                    currentWalk1Right = walk1RightRasta;
                    currentWalk1DownRight = walk1DownRightRasta;
                    currentWalk1Down = walk1DownRasta;
                    currentWalk1DownLeft = walk1DownLeftRasta;
                    currentWalk1Left = walk1LeftRasta;
                    currentWalk1UpLeft = walk1UpLeftRasta;
                    currentWalk2Up = walk2UpRasta;
                    currentWalk2UpRight = walk2UpRightRasta;
                    currentWalk2Right = walk2RightRasta;
                    currentWalk2DownRight = walk2DownRightRasta;
                    currentWalk2Down = walk2DownRasta;
                    currentWalk2DownLeft = walk2DownLeftRasta;
                    currentWalk2Left = walk2LeftRasta;
                    currentWalk2UpLeft = walk2UpLeftRasta;
                }
            }
            else if (isNinja)
            {
                if (isBeaver)
                {
                    currentStandUp = standUpNinjaBeaver;
                    currentStandUpRight = standUpRightNinjaBeaver;
                    currentStandRight = standRightNinjaBeaver;
                    currentStandDownRight = standDownRightNinjaBeaver;
                    currentStandDown = standDownNinjaBeaver;
                    currentStandDownLeft = standDownLeftNinjaBeaver;
                    currentStandLeft = standLeftNinjaBeaver;
                    currentStandUpLeft = standUpLeftNinjaBeaver;
                    currentWalk1Up = walk1UpNinjaBeaver;
                    currentWalk1UpRight = walk1UpRightNinjaBeaver;
                    currentWalk1Right = walk1RightNinjaBeaver;
                    currentWalk1DownRight = walk1DownRightNinjaBeaver;
                    currentWalk1Down = walk1DownNinjaBeaver;
                    currentWalk1DownLeft = walk1DownLeftNinjaBeaver;
                    currentWalk1Left = walk1LeftNinjaBeaver;
                    currentWalk1UpLeft = walk1UpLeftNinjaBeaver;
                    currentWalk2Up = walk2UpNinjaBeaver;
                    currentWalk2UpRight = walk2UpRightNinjaBeaver;
                    currentWalk2Right = walk2RightNinjaBeaver;
                    currentWalk2DownRight = walk2DownRightNinjaBeaver;
                    currentWalk2Down = walk2DownNinjaBeaver;
                    currentWalk2DownLeft = walk2DownLeftNinjaBeaver;
                    currentWalk2Left = walk2LeftNinjaBeaver;
                    currentWalk2UpLeft = walk2UpLeftNinjaBeaver;
                }
                else
                {
                    currentStandUp = standUpNinja;
                    currentStandUpRight = standUpRightNinja;
                    currentStandRight = standRightNinja;
                    currentStandDownRight = standDownRightNinja;
                    currentStandDown = standDownNinja;
                    currentStandDownLeft = standDownLeftNinja;
                    currentStandLeft = standLeftNinja;
                    currentStandUpLeft = standUpLeftNinja;
                    currentWalk1Up = walk1UpNinja;
                    currentWalk1UpRight = walk1UpRightNinja;
                    currentWalk1Right = walk1RightNinja;
                    currentWalk1DownRight = walk1DownRightNinja;
                    currentWalk1Down = walk1DownNinja;
                    currentWalk1DownLeft = walk1DownLeftNinja;
                    currentWalk1Left = walk1LeftNinja;
                    currentWalk1UpLeft = walk1UpLeftNinja;
                    currentWalk2Up = walk2UpNinja;
                    currentWalk2UpRight = walk2UpRightNinja;
                    currentWalk2Right = walk2RightNinja;
                    currentWalk2DownRight = walk2DownRightNinja;
                    currentWalk2Down = walk2DownNinja;
                    currentWalk2DownLeft = walk2DownLeftNinja;
                    currentWalk2Left = walk2LeftNinja;
                    currentWalk2UpLeft = walk2UpLeftNinja;
                }
            }
            else
            {
                if (isBeaver)
                {
                    currentStandUp = standUpBeaver;
                    currentStandUpRight = standUpRightBeaver;
                    currentStandRight = standRightBeaver;
                    currentStandDownRight = standDownRightBeaver;
                    currentStandDown = standDownBeaver;
                    currentStandDownLeft = standDownLeftBeaver;
                    currentStandLeft = standLeftBeaver;
                    currentStandUpLeft = standUpLeftBeaver;
                    currentWalk1Up = walk1UpBeaver;
                    currentWalk1UpRight = walk1UpRightBeaver;
                    currentWalk1Right = walk1RightBeaver;
                    currentWalk1DownRight = walk1DownRightBeaver;
                    currentWalk1Down = walk1DownBeaver;
                    currentWalk1DownLeft = walk1DownLeftBeaver;
                    currentWalk1Left = walk1LeftBeaver;
                    currentWalk1UpLeft = walk1UpLeftBeaver;
                    currentWalk2Up = walk2UpBeaver;
                    currentWalk2UpRight = walk2UpRightBeaver;
                    currentWalk2Right = walk2RightBeaver;
                    currentWalk2DownRight = walk2DownRightBeaver;
                    currentWalk2Down = walk2DownBeaver;
                    currentWalk2DownLeft = walk2DownLeftBeaver;
                    currentWalk2Left = walk2LeftBeaver;
                    currentWalk2UpLeft = walk2UpLeftBeaver;
                }
                else
                {
                    currentStandUp = standUp;
                    currentStandUpRight = standUpRight;
                    currentStandRight = standRight;
                    currentStandDownRight = standDownRight;
                    currentStandDown = standDown;
                    currentStandDownLeft = standDownLeft;
                    currentStandLeft = standLeft;
                    currentStandUpLeft = standUpLeft;
                    currentWalk1Up = walk1Up;
                    currentWalk1UpRight = walk1UpRight;
                    currentWalk1Right = walk1Right;
                    currentWalk1DownRight = walk1DownRight;
                    currentWalk1Down = walk1Down;
                    currentWalk1DownLeft = walk1DownLeft;
                    currentWalk1Left = walk1Left;
                    currentWalk1UpLeft = walk1UpLeft;
                    currentWalk2Up = walk2Up;
                    currentWalk2UpRight = walk2UpRight;
                    currentWalk2Right = walk2Right;
                    currentWalk2DownRight = walk2DownRight;
                    currentWalk2Down = walk2Down;
                    currentWalk2DownLeft = walk2DownLeft;
                    currentWalk2Left = walk2Left;
                    currentWalk2UpLeft = walk2UpLeft;
                }
            }

            int walkingCycleDivision = walkingCycle.GetCycleDivision(4);
            switch (walkingCycleDivision)
            {
                case 1:
                    up = currentWalk1Up;
                    upRight = currentWalk1UpRight;
                    right = currentWalk1Right;
                    downRight = currentWalk1DownRight;
                    down = currentWalk1Down;
                    downLeft = currentWalk1DownLeft;
                    left = currentWalk1Left;
                    upLeft = currentWalk1UpLeft;
                    break;
                case 3:
                    up = currentWalk2Up;
                    upRight = currentWalk2UpRight;
                    right = currentWalk2Right;
                    downRight = currentWalk2DownRight;
                    down = currentWalk2Down;
                    downLeft = currentWalk2DownLeft;
                    left = currentWalk2Left;
                    upLeft = currentWalk2UpLeft;
                    break;
                default:
                    up = currentStandUp;
                    upRight = currentStandUpRight;
                    right = currentStandRight;
                    downRight = currentStandDownRight;
                    down = currentStandDown;
                    downLeft = currentStandDownLeft;
                    left = currentStandLeft;
                    upLeft = currentStandUpLeft;
                    break;
            }

            switch (walkingDirection)
            {
                case WalkingDirection.Up:
                    return up;
                case WalkingDirection.UpRight:
                    return upRight;
                case WalkingDirection.Right:
                    return right;
                case WalkingDirection.DownRight:
                    return downRight;
                case WalkingDirection.Down:
                    return down;
                case WalkingDirection.DownLeft:
                    return downLeft;
                case WalkingDirection.Left:
                    return left;
                default:
                    return upLeft;
            }
        }
        #endregion
    }
}