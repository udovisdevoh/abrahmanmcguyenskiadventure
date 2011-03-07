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
        /// <summary>
        /// List of available joysticks
        /// </summary>
        private List<Joystick> joystickList;

        /// <summary>
        /// Default joystick to get axes value from
        /// </summary>
        private Joystick defaultJoystickForRealAxes = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Build joystick manager
        /// </summary>
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
        /// <summary>
        /// List of joysticks
        /// </summary>
        /// <returns>List of joysticks</returns>
        public IEnumerator<Joystick> GetEnumerator()
        {
            return joystickList.GetEnumerator();
        }

        /// <summary>
        /// List of joysticks
        /// </summary>
        /// <returns>List of joysticks</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return joystickList.GetEnumerator();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set input states in user input object fro joystick axes state
        /// </summary>
        /// <param name="userInput">user input object</param>
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
        /// <summary>
        /// Default joystick to get axes value from
        /// </summary>
        public Joystick DefaultJoystickForRealAxes
        {
            get { return defaultJoystickForRealAxes; }
            set { defaultJoystickForRealAxes = value; }
        }

        /// <summary>
        /// Joystick at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Joystick at index</returns>
        public Joystick this[byte index]
        {
            get { return joystickList[index]; }
        }
        #endregion
    }
}
