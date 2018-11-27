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
                string s = inputProfiles[i].Axis[(int)axis];
                if (s == "None")
                    continue;
                value += Input.GetAxisRaw("Joy" + (i + 1) + "_" + s);
            }

            value /= count;

            return value;
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
                value = Input.GetKey("Joystick" + (i + 1) + s);
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
                value = Input.GetKeyDown("Joystick" + (i + 1) + s);
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
                value = Input.GetKeyUp("Joystick" + (i + 1) + s);
            }

            return value;
        }
    }
}
