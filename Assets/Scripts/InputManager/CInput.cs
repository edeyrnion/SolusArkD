using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    /// <summary>Interface into the Input system.</summary>
    public static class CInput
    {
        private static Dictionary<int, GamepadAxis> axisActions;
        private static Dictionary<int, GamepadButton> btnActions;

        private static InputManager inputManager;

        static CInput()
        {
            axisActions = new Dictionary<int, GamepadAxis>();
            btnActions = new Dictionary<int, GamepadButton>();

            inputManager = new InputManager();

            AddAction(Axis.MoveHorizontal, GamepadAxis.LStick_X, axisActions);
            AddAction(Axis.MoveVertical, GamepadAxis.LStick_Y, axisActions);
            AddAction(Axis.CaneraHorizontal, GamepadAxis.RStick_X, axisActions);
            AddAction(Axis.CaneraVertical, GamepadAxis.RStick_Y, axisActions);

            AddAction(Button.Jump, GamepadButton.Action_Bottom, btnActions);
        }

        private static void AddAction(Axis action, GamepadAxis button, Dictionary<int, GamepadAxis> actions)
        {
            actions.Add((int)action, button);
        }

        private static void AddAction(Button action, GamepadButton button, Dictionary<int, GamepadButton> actions)
        {
            actions.Add((int)action, button);
        }

        /// <summary>Returns the value of the virtual axis identified by axisName.</summary>
        /// <param name="axisName">The name of the axis.</param>
        /// <returns>The value of the axis.</returns>
        public static float GetAxis(Axis axisName)
        {
            float value = inputManager.GetAxis(axisActions[(int)axisName]);
            return value;
        }

        /// <summary>Returns true while the virtual button identified by buttonName is held down.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been pressed and not released.</returns>
        public static bool GetButton(Button buttonName)
        {
            bool value = inputManager.GetButton(btnActions[(int)buttonName]);
            return value;
        }

        /// <summary>Returns true during the frame the user pressed down the virtual button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been pressed.</returns>
        public static bool GetButtonDown(Button buttonName)
        {
            bool value = inputManager.GetButtonDown(btnActions[(int)buttonName]);
            return value;
        }

        /// <summary>Returns true the first frame the user releases the virtual button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been released.</returns>
        public static bool GetButtonUp(Button buttonName)
        {
            bool value = inputManager.GetButtonUp(btnActions[(int)buttonName]);
            return value;
        }

        /// <summary>Returns true while the key identified by key is held down.</summary>
        /// <param name="key">The name of the key.</param>
        /// <returns>True when an key has been pressed and not released.</returns>
        public static bool GetKey(KeyCode key)
        {
            bool value = Input.GetKey(key);
            return value;
        }

        /// <summary>Returns true during the frame the user pressed down the key identified by key.</summary>
        /// <param name="key">The name of the key.</param>
        /// <returns>True when an key has been pressed.</returns>
        public static bool GetKeyDown(KeyCode key)
        {
            bool value = Input.GetKeyDown(key);
            return value;
        }

        /// <summary>Returns true the first frame the user releases the key identified by key.</summary>
        /// <param name="key">The name of the key.</param>
        /// <returns>True when an key has been released.</returns>
        public static bool GetKeyUp(KeyCode key)
        {
            bool value = Input.GetKeyUp(key);
            return value;
        }
    }
}
