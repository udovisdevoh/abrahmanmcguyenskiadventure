using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Represents user input
    /// </summary>
    internal class UserInput
    {
        public bool isPressUp = false;

        public bool isPressDown = false;

        public bool isPressLeft = false;

        public bool isPressRight = false;

        public bool isPressJump = false;

        public bool isPressAttack = false;

        public bool isPressLeaveBeaver = false;

        public bool isPressPageUp = false;

        public bool isPressPageDown = false;

        public int jumpButton = PersistentConfig.JumpButton;

        public int attackButton = PersistentConfig.AttackButton;

        public int leaveBeaverButton = PersistentConfig.LeaveBeaverButton;
    }
}
