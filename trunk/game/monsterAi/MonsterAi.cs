using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.ai
{
    class MonsterAi
    {
        internal void Update(MonsterSprite monster, PlayerSprite player, Level level, double timeDelta, Random random)
        {
            monster.IsTryingToWalk = false;

            monster.IsNeedToJumpAgain = false;

            if (monster.Ground == null)
                monster.IsTryingToJump = (random.Next(0, 3) == 0);
            else
                monster.IsTryingToJump = (random.Next(0, 40) == 0);


            bool isFleeMode = player.YPosition < monster.YPosition && (Math.Abs(monster.XPosition - player.XPosition) < player.Width);
                


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
