using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages touching powerups
    /// </summary>
    internal class PowerUpManager
    {

        /// <summary>
        /// Sprite touches peyote
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="peyoteSprite">peyote sprite</param>
        internal void UpdateTouchPeyote(PlayerSprite playerSprite, PeyoteSprite peyoteSprite)
        {
            SoundManager.PlayPowerUpSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.Health = playerSprite.MaxHealth;
            playerSprite.IsTiny = false;
            playerSprite.IsRasta = false;
            playerSprite.IsDoped = true;
            playerSprite.IsBodhi = false;
            playerSprite.IsNinja = false;
            peyoteSprite.IsAlive = false;
            peyoteSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Touch rasta hat
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="rastaHatSprite">rasta hat</param>
        internal void UpdateTouchRastaHat(PlayerSprite playerSprite, RastaHatSprite rastaHatSprite)
        {
            SoundManager.PlayReggaeSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.Health = playerSprite.MaxHealth;
            playerSprite.IsTiny = false;
            playerSprite.IsDoped = false;
            playerSprite.IsRasta = true;
            playerSprite.IsNinja = false;
            playerSprite.IsBodhi = false;
            rastaHatSprite.IsAlive = false;
            rastaHatSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        internal void UpdateTouchBuddha(PlayerSprite playerSprite, BuddhaSprite buddhaSprite)
        {
            SoundManager.PlayEnlightenmentSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.Health = playerSprite.MaxHealth;
            playerSprite.IsTiny = false;
            playerSprite.IsDoped = false;
            playerSprite.IsRasta = false;
            playerSprite.IsNinja = false;
            playerSprite.IsBodhi = true;
            buddhaSprite.IsAlive = false;
            buddhaSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Touch ninja bandana
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="rastaHatSprite">rasta hat</param>
        internal void UpdateTouchBandana(PlayerSprite playerSprite, BandanaSprite bandanaSprite)
        {
            SoundManager.PlayGongSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.Health = playerSprite.MaxHealth;
            playerSprite.IsTiny = false;
            playerSprite.IsDoped = false;
            playerSprite.IsRasta = false;
            playerSprite.IsBodhi = false;
            playerSprite.IsNinja = true;
            bandanaSprite.IsAlive = false;
            bandanaSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches music note
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="musicNoteSprite">music note</param>
        internal void UpdateTouchMusicNote(PlayerSprite playerSprite, MusicNoteSprite musicNoteSprite, AbstractGameMode gameMode)
        {
            if (gameMode.IsTransformToBodhiWhenGetsEnoughMusicNote && playerSprite.IsBodhi)
                return;

            SoundManager.PlayCoinSound();
            musicNoteSprite.IsAlive = false;
            playerSprite.MusicNoteCount++;

            if (gameMode.IsTransformToBodhiWhenGetsEnoughMusicNote && playerSprite.MusicNoteCount >= Program.musicNoteCountForBodhi)
            {
                SoundManager.PlayEnlightenmentSound();
                playerSprite.PowerUpAnimationCycle.Fire();
                if (playerSprite.IsTiny)
                    playerSprite.ChangingSizeAnimationCycle.Fire();
                playerSprite.Health = playerSprite.MaxHealth;
                playerSprite.IsTiny = false;
                playerSprite.IsDoped = false;
                playerSprite.IsRasta = false;
                playerSprite.IsNinja = false;
                playerSprite.IsBodhi = true;

                playerSprite.MusicNoteCount = 0;
            }
            else if (gameMode.IsNoteGivesFullHealthMax99)
            {
                if (playerSprite.MusicNoteCount > 99)
                    playerSprite.MusicNoteCount = 99;
                playerSprite.Health = playerSprite.MaxHealth;
            }

            musicNoteSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches whisky and becomes invincible
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="whiskySprite">whisky</param>
        internal void UpdateTouchWhisky(PlayerSprite playerSprite, WhiskySprite whiskySprite)
        {
            SoundManager.PlayPowerUpSound();
            playerSprite.InvincibilityCycle.Fire();
            playerSprite.HitCycle.StopAndReset();
            whiskySprite.IsAlive = false;
            whiskySprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }
    }
}
