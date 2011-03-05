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

        private bool isUseAxes = false;

        private Joystick defaultJoystick = null;
        #endregion

        #region Constructor
        public JoystickManager()
        {
            #warning Must allow user to setup input (keyboard / joystick) config
            joystickList = new List<Joystick>();

            for (int i = 0; i < Joysticks.NumberOfJoysticks; i++)
                joystickList.Add(Joysticks.OpenJoystick(i));

            if (joystickList.Count > 0)
                defaultJoystick = joystickList[0];
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
            if (defaultJoystick != null)
            {
                double horizontalAxisPosition = defaultJoystick.GetAxisPosition(JoystickAxis.Horizontal);
                double verticalAxisPosition = defaultJoystick.GetAxisPosition(JoystickAxis.Vertical);

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
        public bool IsUseAxes
        {
            get { return isUseAxes; }
            set{isUseAxes = value;}
        }

        public Joystick DefaultJoystick
        {
            get { return defaultJoystick; }
            set { defaultJoystick = value; }
        }

        public Joystick this[byte index]
        {
            get { return joystickList[index]; }
        }
        #endregion
    }
}
