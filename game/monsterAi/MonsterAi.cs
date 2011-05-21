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
            if (monster.IsCanJump)
            {
                if (monster.CurrentWalkingSpeed < 0.01)
                    monster.IsTryingToJump = true;
                
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
            #endregion

            bool isFleeMode = player.YPosition < monster.YPosition && (Math.Abs(monster.XPosition - player.XPosition) < player.Width / 2.0);

            if (monster.XPosition < player.XPosition)
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


            //monster.IsRunning = isFleeMode;
        }
    }
}
