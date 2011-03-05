using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Input;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Manage joysticks
    /// </summary>
    internal class JoystickManager : IEnumerable<Joystick>
    {
        #region Parts
        private List<Joystick> joystickList;
        #endregion

        #region Constructor
        public JoystickManager()
        {
            #warning Must allow user to setup joystick config
            #warning Must work with digital DPad
            joystickList = new List<Joystick>();

            for (int i = 0; i < Joysticks.NumberOfJoysticks; i++)
                joystickList.Add(Joysticks.OpenJoystick(i));
        }
        #endregion

        #region IEnumerable<Joystick> Members
        public IEnumerator<Joystick> GetEnumerator()
        {
            return joystickList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return joystickList.GetEnumerator();
        }
        #endregion

        internal void SetInputStateFromAxis(UserInput userInput)
        {
            if (joystickList.Count > 0)
            {
                double horizontalAxisPosition = joystickList[0].GetAxisPosition(JoystickAxis.Horizontal);
                double verticalAxisPosition = joystickList[0].GetAxisPosition(JoystickAxis.Vertical);

                if (horizontalAxisPosition > 0.9)
                {
                    userInput.isPressRight = true;
                    userInput.isPressLeft = false;
                }
                else if (horizontalAxisPosition < 0.1)
                {
                    userInput.isPressLeft = true;
                    userInput.isPressRight = false;
                }
                else
                {
                    userInput.isPressLeft = false;
                    userInput.isPressRight = false;
                }

                if (verticalAxisPosition > 0.9)
                {
                    userInput.isPressUp = false;
                    userInput.isPressDown = true;
                }
                else if (verticalAxisPosition < 0.1)
                {
                    userInput.isPressDown = false;
                    userInput.isPressUp = true;
                }
                else
                {
                    userInput.isPressDown = false;
                    userInput.isPressUp = false;
                }
            }
        }
    }
}
