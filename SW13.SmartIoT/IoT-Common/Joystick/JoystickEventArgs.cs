using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Common
{
    public class JoystickEventArgs : EventArgs
    {

        #region constructor & destructor
        public JoystickEventArgs(JoystickButton button)
        {
            Button = button;
        }
        #endregion


        #region properties
        public JoystickButton Button { get; }
        #endregion

    }
}
