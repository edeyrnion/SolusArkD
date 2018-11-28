using System;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    public class InputManager
    {
        private List<InputProfile> inputProfiles;

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
            float value = 0f;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                var inputProfile = inputProfiles[i];
                string s = inputProfile.Axis[(int)axis];
                if (s == "None")
                    continue;
                if (inputProfile.IsInverted[(int)axis])
                {
                    if (s.Contains("-"))
                    {
                        value -= Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);
                    }
                    else if (s.Contains("+"))
                    {
                        value -= Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);
                    }
                    else
                    {
                        value -= Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);
                    }
                }
                else
                {
                    value += Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);
                }
            }

            value /= count;

            return value;
        }

        public static float ConvertRange(
            int originalStart, int originalEnd, // original range
            int newStart, int newEnd, // desired range
            float value) // value to convert
        {
            float scale = (newEnd - newStart) / (originalEnd - originalStart);
            return (newStart + ((value - originalStart) * scale));
        }



        public bool GetButton(GamepadButton btn)
        {
            bool value = false;
            int count = inputProfiles.Count;

            for (int i = 0; i < count; i++)
            {
                if (value)
                    break;
                string s = inputProfiles[i].Buttons[(int)btn];
                if (s == "None")
                    continue;
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
                string s = inputProfiles[i].Buttons[(int)btn];
                if (s == "None")
                    continue;
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
                string s = inputProfiles[i].Buttons[(int)btn];
                if (s == "None")
                    continue;
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
