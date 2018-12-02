using System;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Inputmanager
{
    /// <summary>Interface into the Input system.</summary>
    public class ControllerInput
    {
        private List<ContollerProfile> controllerProfiles;

        public ControllerInput()
        {
            controllerProfiles = new List<ContollerProfile>();

            string[] controllerNames = Input.GetJoystickNames();

            int controllerCount = controllerNames.Length;
            for (int i = 0; i < controllerCount; i++)
            {
                string controllerName = controllerNames[i];
                Debug.Log("Found device: " + controllerName);

                ContollerProfile profile = new ContollerProfile(controllerName, i + 1);
                Debug.Log("Using profile: " + profile.Name);

                controllerProfiles.Add(profile);
            }
        }

        /// <summary>Returns the value of the axis identified by axisName.</summary>
        /// <param name="axisName">The name of the axis.</param>
        /// <returns>The value of the axis.</returns>
        public float GetAxis(ControllerAxis axisName)
        {
            float result = 0f;
            int count = controllerProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var axis = controllerProfiles[i].Axis;

                var temp = Axis.GetAxis(axis[(int)axisName]);

                if (Mathf.Abs(temp) > Mathf.Abs(result))
                {
                    result = temp;
                }
            }

            return result;
        }

        /// <summary>Returns true while the button identified by buttonName is held down.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been pressed and not released.</returns>
        public bool GetButton(ControllerButton buttonName)
        {
            bool result = false;
            int count = controllerProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var btn = controllerProfiles[i].Buttons;
                if (result = Button.GetButton(btn[(int)buttonName]))
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>Returns true during the frame the user pressed down the button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been pressed.</returns>
        public bool GetButtonDown(ControllerButton buttonName)
        {
            bool result = false;
            int count = controllerProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var btn = controllerProfiles[i].Buttons;
                if (result = Button.GetButtonDown(btn[(int)buttonName]))
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>Returns true the first frame the user releases the button identified by buttonName.</summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns>True when an button has been released.</returns>
        public bool GetButtonUp(ControllerButton buttonName)
        {
            bool result = false;
            int count = controllerProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var btn = controllerProfiles[i].Buttons;
                if (Button.GetButtonUp(btn[(int)buttonName]))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        //---------------------------------------------------------------------------------
        //  DEBUG STUFF
        //---------------------------------------------------------------------------------

        public List<ControllerButton> GetAllButtons()
        {
            var buttons = new List<ControllerButton>();
            int count = Enum.GetValues(typeof(ControllerButton)).Length;

            for (int i = 0; i < count; i++)
            {
                if (GetButton((ControllerButton)i))
                {
                    buttons.Add((ControllerButton)i);
                }
            }

            return buttons;
        }

        public List<ControllerButton> GetAllButtonsUp()
        {
            var buttons = new List<ControllerButton>();
            int count = Enum.GetValues(typeof(ControllerButton)).Length;

            for (int i = 0; i < count; i++)
            {
                if (GetButtonUp((ControllerButton)i))
                {
                    buttons.Add((ControllerButton)i);
                }
            }

            return buttons;
        }

        public List<ControllerButton> GetAllButtonsDown()
        {
            var buttons = new List<ControllerButton>();
            int count = Enum.GetValues(typeof(ControllerButton)).Length;

            for (int i = 0; i < count; i++)
            {
                if (GetButtonDown((ControllerButton)i))
                {
                    buttons.Add((ControllerButton)i);
                }
            }

            return buttons;
        }

        public Dictionary<ControllerAxis, float> GetAllAxis()
        {
            var axis = new Dictionary<ControllerAxis, float>();
            int count = Enum.GetValues(typeof(ControllerAxis)).Length;

            for (int i = 0; i < count; i++)
            {
                float value = GetAxis((ControllerAxis)i);
                axis.Add((ControllerAxis)i, value);
            }
            return axis;
        }
    }
}
