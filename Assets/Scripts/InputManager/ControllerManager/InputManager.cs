using System;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    public class InputManager
    {
        // List of button mappings for all controllers found.
        private List<InputProfile> inputProfiles;

        // Virtual left + right trigger buttons for controllers that don't have them.
        private bool lTriggerPressed = false;
        private bool rTriggerPressed = false;

        // Get all controllers and load thier corresponding buttnon mappings.
        public InputManager()
        {
            inputProfiles = new List<InputProfile>();

            string[] names = Input.GetJoystickNames();

            for (int i = 0; i < names.Length; i++)
            {
                InputProfile p = new InputProfile(names[i]);
                Debug.Log("Found device: " + names[i]);
                Debug.Log("Using profile: " + p.Name);
                inputProfiles.Add(p);
            }
        }

        public float GetAxis(GamepadAxis axis)
        {
            float result = 0f;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var inputProfile = inputProfiles[i];
                string s = inputProfile.Axis[(int)axis];

                if (s == "None")
                {
                    if (axis == GamepadAxis.LTrigger)
                    {
                        string r = inputProfile.Buttons[(int)GamepadButton.LTrigger];
                        if (Input.GetKey("joystick " + (i + 1) + " " + r))
                        {
                            result = 1f;
                        }
                        continue;
                    }
                    else if (axis == GamepadAxis.RTrigger)
                    {
                        string r = inputProfile.Buttons[(int)GamepadButton.RTrigger];
                        if (Input.GetKey("joystick " + (i + 1) + " " + r))
                        {
                            result = 1f;
                        }
                        continue;
                    }
                    continue;
                }

                float value = Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);

                if (inputProfile.IsInverted[(int)axis])
                    value *= -1f;

                if (Mathf.Abs(value) > Mathf.Abs(result))
                {
                    result = value;
                }
            }

            if (axis == GamepadAxis.LTrigger || axis == GamepadAxis.RTrigger)
            {
                result = Mathf.Clamp01(result);
            }

            return result;
        }

        public bool GetButton(GamepadButton btn)
        {
            bool value = false;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                if (value)
                    break;

                var inputProfile = inputProfiles[i];
                string s = inputProfile.Buttons[(int)btn];
                if (s == "None")
                {
                    if (btn == GamepadButton.LTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.LTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        lTriggerPressed = value = Mathf.Abs(axisValue) > 0.1f;
                        continue;
                    }
                    else if (btn == GamepadButton.RTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.RTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        rTriggerPressed = value = Mathf.Abs(axisValue) > 0.1f;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                value = Input.GetKey("joystick " + (i + 1) + " " + s);
            }

            return value;
        }

        public bool GetButtonDown(GamepadButton btn)
        {
            bool value = false;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                if (value)
                    break;

                var inputProfile = inputProfiles[i];
                string s = inputProfile.Buttons[(int)btn];
                if (s == "None")
                {
                    if (btn == GamepadButton.LTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.LTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        if (Mathf.Abs(axisValue) > 0.1f && !lTriggerPressed)
                        {
                            value = true;
                        }
                        continue;
                    }
                    else if (btn == GamepadButton.RTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.RTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        if (Mathf.Abs(axisValue) > 0.1f && !rTriggerPressed)
                        {
                            value = true;
                        }
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                value = Input.GetKeyDown("joystick " + (i + 1) + " " + s);
            }

            return value;
        }

        public bool GetButtonUp(GamepadButton btn)
        {
            bool value = false;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                if (value)
                    break;

                var inputProfile = inputProfiles[i];
                string s = inputProfile.Buttons[(int)btn];
                if (s == "None")
                {
                    if (btn == GamepadButton.LTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.LTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        if (Mathf.Abs(axisValue) < 0.1f && lTriggerPressed)
                        {
                            value = true;
                        }
                        continue;
                    }
                    else if (btn == GamepadButton.RTrigger)
                    {
                        string r = inputProfile.Axis[(int)GamepadAxis.RTrigger];
                        float axisValue = Input.GetAxisRaw("Joy" + (i + 1) + "_" + r);
                        if (Mathf.Abs(axisValue) < 0.1f && rTriggerPressed)
                        {
                            value = true;
                        }
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                value = Input.GetKeyUp("joystick " + (i + 1) + " " + s);
            }

            return value;
        }

        public List<GamepadButton> GetAllButtons()
        {
            List<GamepadButton> buttons = new List<GamepadButton>();
            int count = Enum.GetValues(typeof(GamepadButton)).Length;

            for (int i = 0; i < count; i++)
            {
                if (GetButton((GamepadButton)i))
                {
                    buttons.Add((GamepadButton)i);
                }
            }

            return buttons;
        }

        public List<GamepadButton> GetAllButtonsUp()
        {
            List<GamepadButton> buttons = new List<GamepadButton>();
            int count = Enum.GetValues(typeof(GamepadButton)).Length;

            for (int i = 0; i < count; i++)
            {
                if (GetButtonUp((GamepadButton)i))
                {
                    buttons.Add((GamepadButton)i);
                }
            }

            return buttons;
        }

        public Dictionary<GamepadAxis, float> GetAllAxis()
        {
            Dictionary<GamepadAxis, float> axis = new Dictionary<GamepadAxis, float>();
            int count = Enum.GetValues(typeof(GamepadAxis)).Length;

            for (int i = 0; i < count; i++)
            {
                float value = GetAxis((GamepadAxis)i);
                axis.Add((GamepadAxis)i, value);
            }
            return axis;
        }
    }
}
