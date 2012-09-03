using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure
{
    class AdventureRpgGameMode : AbstractGameMode
    {
        private Cycle drawTextCycle = new Cycle(500, false, false, false);

        private Surface messageSurface = null;

        private Point messagePosition;

        #region Constructor
        public AdventureRpgGameMode(Surface surfaceToDrawLoadingProgress)
            : base(surfaceToDrawLoadingProgress)
        {
        }
        #endregion

        protected override double BuildHoleLengthMultiplicator()
        {
            return 1.0;
        }

        protected override double BuildGroundSurfaceLengthMultiplicator()
        {
            return 1.0;
        }

        protected override double BuildMonsterDensityMultiplicator()
        {
            return 1.0;
        }

        public override void HackPlayerSprite(PlayerSprite playerSprite)
        {
            switch (playerSprite.Level)
            {
                case 0:
                    playerSprite.IsTiny = true;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 1:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 2:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = true;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 3:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = true;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = false;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = true;
                    playerSprite.IsBodhi = false;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    playerSprite.IsTiny = false;
                    playerSprite.IsRasta = false;
                    playerSprite.IsDoped = false;
                    playerSprite.IsNinja = false;
                    playerSprite.IsBodhi = true;
                    break;
            }
        }

        protected override bool BuildIsMusicSpeedUp()
        {
            return false;
        }

        protected override bool BuildIsMushroomOverrideUpgrade()
        {
            return true;
        }

        protected override bool BuildIsShowHealthBar()
        {
            return true;
        }

        protected override bool BuildIsTransformToBodhiWhenGetsEnoughMusicNote()
        {
            return false;
        }

        protected override bool BuildIsShowExp()
        {
            return true;
        }

        public override void PerformDestroyMonsterExtraLogic(PlayerSprite playerSprite, MonsterSprite monsterSprite, int skillLevel)
        {
            playerSprite.Experience += (int)Math.Round((monsterSprite.MaxHealth + monsterSprite.AttackStrengthCollision) * (double)(skillLevel + 1) * 10.0);

            while (playerSprite.Experience >= GetExperienceNeededForLevel(playerSprite.Level + 1))
            {
                playerSprite.Level++;
                SoundManager.PlayEnlightenmentSound();
                if (playerSprite.IsTiny)
                    playerSprite.ChangingSizeAnimationCycle.Fire();
                else
                    playerSprite.PowerUpAnimationCycle.Fire();
                HackPlayerSprite(playerSprite);

                drawTextCycle.Fire();
                messageSurface = null;
            }
        }

        public override void CollisionRemoveSuitOrBecomeSmallOrDie(PlayerSprite playerSprite, IEvilSprite evilSprite)
        {
            SoundManager.PlayHit2Sound();
            ((PlayerSprite)playerSprite).KiBallChargeCycle.StopAndReset();
            SoundManager.StopKiChargingSound();
            playerSprite.CurrentDamageReceiving = evilSprite.AttackStrengthCollision * 0.4;
        }

        public override void UpdateTouchMushroom(PlayerSprite playerSprite, MushroomSprite mushroomSprite)
        {
            SoundManager.PlayPowerUpSound();

            playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.Health -= mushroomSprite.AttackStrengthCollision;
            mushroomSprite.IsAlive = false;
            mushroomSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        public override int GetExperienceNeededForLevel(int level)
        {
            return (int)Math.Round(Math.Pow((double)(level + 1), 1.9) * 50.0);
        }

        public override bool IsAllowShuriken(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 5 || playerSprite.Level == 3;
        }

        public override bool IsAllowNunchaku(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 4;
        }

        public override bool IsAllowPunchKick(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 0 || playerSprite.IsBeaver;
        }

        public override bool IsAllowThrowNinjaRope(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 6;
        }

        public override bool IsAllowBodhiAirJump(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 10;
        }

        public override bool IsAllowCharge(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 9;
        }

        public override bool IsAllowAngleAttack(PlayerSprite playerSprite)
        {
            return playerSprite.Level > 8;
        }

        public override void UpdateByFrame(double timeDelta, PlayerSprite playerSprite)
        {
            if (drawTextCycle.IsFired)
            {
                drawTextCycle.Increment(timeDelta);

                if (messageSurface == null)
                {
                    #region Draw text surface
                    Surface line1 = GameMenu.GetFontText("You have reached level " + (playerSprite.Level + 1) + ".");
                    Surface line2;

                    switch (playerSprite.Level)
                    {
                        case 1:
                            line2 = GameMenu.GetFontText("You became a big. You can now punch and kick.");
                            break;
                        case 2:
                            line2 = GameMenu.GetFontText("You became a rasta. Use your hair like a parachute.");
                            break;
                        case 3:
                            line2 = GameMenu.GetFontText("You're now doped on mescaline. Throw fireballs.");
                            break;
                        case 4:
                            line2 = GameMenu.GetFontText("You are now a ninja. Try using your cool sword.");
                            break;
                        case 5:
                            line2 = GameMenu.GetFontText("You can now use your nunchaku.");
                            break;
                        case 6:
                            line2 = GameMenu.GetFontText("You can now throw shurikens.");
                            break;
                        case 7:
                            line2 = GameMenu.GetFontText("You can throw ninja ropes.");
                            break;
                        case 8:
                            line2 = GameMenu.GetFontText("You reached enlightenment. Throw ki balls.");
                            break;
                        case 9:
                            line2 = GameMenu.GetFontText("Throw ki balls in each angle.");
                            break;
                        case 10:
                            line2 = GameMenu.GetFontText("You can now throw charged ki balls.");
                            break;
                        case 11:
                            line2 = GameMenu.GetFontText("You can now fly.");
                            break;
                        default:
                            line2 = null;
                            break;
                    }

                    if (line2 == null)
                        messageSurface = line1;
                    else
                    {
                        messageSurface = new Surface(Math.Max(line1.Width, line2.Width), line1.Height + line2.Height);
                        messageSurface.Blit(line1, new Point(0, 0));
                        messageSurface.Blit(line2, new Point(0, line1.Height));
                    }

                    messageSurface.Transparent = true;

                    messagePosition = new Point(Program.screenWidth / 2 - messageSurface.Width / 2, Program.screenHeight - messageSurface.Height);
                    #endregion
                }

                mainSurface.Blit(messageSurface, messagePosition); 
            }
        }
    }
}