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

        private Joystick defaultJoystickForRealAxes = null;
        #endregion

        #region Constructor
        public JoystickManager()
        {
            joystickList = new List<Joystick>();

            for (int i = 0; i < Joysticks.NumberOfJoysticks; i++)
                joystickList.Add(Joysticks.OpenJoystick(i));

            if (joystickList.Count > 0)
                defaultJoystickForRealAxes = joystickList[0];
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

        #region Public Methods
        internal void SetInputStateFromAxes(UserInput userInput)
        {
            if (defaultJoystickForRealAxes != null)
            {
                double horizontalAxisPosition = defaultJoystickForRealAxes.GetAxisPosition(JoystickAxis.Horizontal);
                double verticalAxisPosition = defaultJoystickForRealAxes.GetAxisPosition(JoystickAxis.Vertical);

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
        #endregion

        #region Properties
        public Joystick DefaultJoystickForRealAxes
        {
            get { return defaultJoystickForRealAxes; }
            set { defaultJoystickForRealAxes = value; }
        }

        public Joystick this[byte index]
        {
            get { return joystickList[index]; }
        }
        #endregion
    }
}
