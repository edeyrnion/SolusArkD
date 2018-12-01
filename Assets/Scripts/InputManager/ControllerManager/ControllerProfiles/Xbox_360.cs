﻿using System.Collections.Generic;

namespace Matthias
{
    public partial class InputProfile
    {
        private void Xbox_360Init()
        {
            methods.Add("Controller (Xbox 360 For Windows)", Xbox_One);  // methods.Add("NAME_OF_DEVICE", NAME_OF_METHOD);
            methods.Add("Controller (Xbox 360 Wireless Receiver For Windows)", Xbox_One);  // methods.Add("NAME_OF_DEVICE", NAME_OF_METHOD);
        }

        public void Xbox_360()
        {
            name = "Xbox 360";

            axis = new Dictionary<int, string>
            {
                { 0, "AxisX" }, // Left Stick X
                { 1, "AxisY" }, // Left Stick Y
                { 2, "Axis4" }, // Right Stick X
                { 3, "Axis5" }, // Richt Stick Y
                { 4, "Axis9" }, // Left Trigger
                { 5, "Axis10" }, // Right Trigger
                { 6, "Axis6" }, // DPad X
                { 7, "Axis7" }, // DPad Y
            };

            isInverted = new Dictionary<int, bool>
            {
                { 0, false },
                { 1, true },
                { 2, false },
                { 3, true },
                { 4, false },
                { 5, false },
                { 6, false },
                { 7, false },
            };

            buttons = new Dictionary<int, string>
            {
                { 0, "button 2" }, // Action Left
                { 1, "button 0" }, // Action Bottom
                { 2, "button 1" }, // Action Right
                { 3, "button 3" }, // Action Top
                { 4, "button 4" }, // Left Bumper
                { 5, "button 5" }, // Right Bumper
                { 6, "None" }, // Left Trigger
                { 7, "None" }, // Right Trigger
                { 8, "button 7" }, // Select
                { 9, "button 6" }, // Start
                { 10, "button 8" }, // Left Stick
                { 11, "button 9" }, // Right Stick
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