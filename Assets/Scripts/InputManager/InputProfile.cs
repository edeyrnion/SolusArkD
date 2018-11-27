using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

namespace Matthias
{
    public partial class InputProfile
    {
        private Dictionary<string, Action> methods = new Dictionary<string, Action>();

        public string Name => name;
        public IReadOnlyDictionary<int, string> Axis => axis;
        public IReadOnlyDictionary<int, string> Buttons => buttons;

        private string name = "";
        private Dictionary<int, string> axis;
        private Dictionary<int, string> buttons;

        private Dictionary<int, AnalogAxis> axis2;
        private struct AnalogAxis
        {
            public string Name { get; }
            public bool IsInverted { get; }

            public AnalogAxis(string name, bool isInverted)
            {
                Name = name;
                IsInverted = isInverted;
            }
        }

        public InputProfile(string deviceName)
        {
            MethodInfo[] methodInfo = typeof(InputProfile).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < methodInfo.Length; i++)
            {
                methodInfo[i].Invoke(this, null);
            }

            if (methods.ContainsKey(deviceName))
            {
                methods[deviceName].Invoke();
            }
            else
            {
                DefaultProfile();
                Debug.Log("Unknown device. Using default profile.");
            }
        }

        private void DefaultProfileInit()
        {
            methods.Add("", DefaultProfile);
        }

        public void DefaultProfile()
        {
            name = "Default";

            axis = new Dictionary<int, string>
            {
                { 0, "AxisX" }, // Left Stick X
                { 1, "AxisY" }, // Left Stick Y
                { 2, "Axis4" }, // Right Stick X
                { 3, "Axis5" }, // Richt Stick Y
                { 4, "Axis3" }, // Left Trigger
                { 5, "Axis6" }, // Right Trigger
                { 6, "Axis8" }, // DPad X
                { 7, "Axis7" }, // DPad Y
            };

            axis2 = new Dictionary<int, AnalogAxis>(8)
            {
                { 0, new AnalogAxis("AxisX", false) },
                { 1, new AnalogAxis("AxisY", false) },
                { 2, new AnalogAxis("Axis4", false) },
                { 3, new AnalogAxis("Axis5", false) },
                { 4, new AnalogAxis("Axis3", false) },
                { 5, new AnalogAxis("Axis6", false) },
                { 6, new AnalogAxis("Axis8", false) },
                { 7, new AnalogAxis("Axis7", false) },
            };

            axis2[0] = new AnalogAxis();

            buttons = new Dictionary<int, string>
            {
                { 0, "Button2" }, // Action Left
                { 1, "Button0" }, // Action Bottom
                { 2, "Button1" }, // Action Right
                { 3, "Button3" }, // Action Top
                { 4, "Button4" }, // Left Bumper
                { 5, "Button5" }, // Right Bumper
                { 6, "None" }, // Left Trigger
                { 7, "None" }, // Right Trigger
                { 8, "Button6" }, // Select
                { 9, "Button7" }, // Start
                { 10, "Button8" }, // Left Stick
                { 11, "Button9" }, // Right Stick
                { 12, "None" }, // Guide
                { 13, "None" }, // Touchpad
                { 16, "None" }, // DPad Left
                { 15, "None" }, // DPad Down
                { 17, "None" }, // DPad Right
                { 14, "None" }, // DPad Up
            };
        }
    }
}
