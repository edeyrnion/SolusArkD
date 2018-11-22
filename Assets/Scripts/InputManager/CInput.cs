using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    /// <summary>Interface into the Input system.</summary>
    public static class CInput
    {
        private static Dictionary<int, string>[] _actions;
        private static int _numberOfPlayers = 1;

        private static string _jump = "Jump";

        static CInput()
        {
            _actions = new Dictionary<int, string>[_numberOfPlayers];

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                AddAction(Button.Jump, _jump, _actions[i]);
            }
        }

        private static void AddAction(Button action, string actionName, Dictionary<int, string> actions)
        {
            actions.Add((int)action, actionName);
        }

        /// <summary>Returns the value of the virtual axis identified by axisName.</summary>
        /// <param name="axisName">The name of the axis.</param>
        /// <param name="player">The number of the player. Default is 1.</param>
        /// <returns>The value of the axis.</returns>
        public static float GetAxis(Axis axisName, int player = 1)
        {
            if (player > 1 || player < _numberOfPlayers)
                return 0;
            float value = Input.GetAxisRaw(_actions[player - 1][(int)axisName]);
            return value;
        }

        /// <summary>Returns true while the virtual button identified by buttonName is held down.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <param name="player">The number of the player. Default is 1.</param>
        /// <returns>True when an button has been pressed and not released.</returns>
        public static bool GetButton(Button buttonName, int player = 1)
        {
            if (player > 1 || player < _numberOfPlayers)
                return false;
            bool value = Input.GetButton(_actions[player - 1][(int)buttonName]);
            return value;
        }

        /// <summary>Returns true during the frame the user pressed down the virtual button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <param name="player">The number of the player. Default is 1.</param>
        /// <returns>True when an button has been pressed.</returns>
        public static bool GetButtonDown(Button buttonName, int player = 1)
        {
            if (player > 1 || player < _numberOfPlayers)
                return false;
            bool value = Input.GetButtonDown(_actions[player - 1][(int)buttonName]);
            return value;
        }

        /// <summary>Returns true the first frame the user releases the virtual button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <param name="player">The number of the player. Default is 1.</param>
        /// <returns>True when an button has been released.</returns>
        public static bool GetButtonUp(Button buttonName, int player = 1)
        {
            if (player > 1 || player < _numberOfPlayers)
                return false;
            bool value = Input.GetButtonUp(_actions[player - 1][(int)buttonName]);
            return value;
        }

    }
}
