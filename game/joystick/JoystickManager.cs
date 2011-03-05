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
    }
}
