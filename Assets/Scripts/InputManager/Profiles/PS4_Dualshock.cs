using System.Collections.Generic;

namespace Matthias
{
    public partial class InputProfile
    {
        private void PS4_DualshockInit()
        {
            methods.Add("Wireless Controller", PS4_Dualshock);  // methods.Add("NAME_OF_DEVICE", NAME_OF_METHOD);
        }

        public void PS4_Dualshock()
        {
            name = "PS4 Dualshock";

            axis = new Dictionary<int, string>
            {
                { 0, "AxisX" }, // Left Stick X
                { 1, "AxisY" }, // Left Stick Y
                { 2, "Axis3" }, // Right Stick X
                { 3, "Axis6" }, // Richt Stick Y
                { 4, "Axis4" }, // Left Trigger
                { 5, "Axis5" }, // Right Trigger
                { 6, "Axis7" }, // DPad X
                { 7, "Axis8" }, // DPad Y
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
                { 0, "button 0" }, // Action Left
                { 1, "button 1" }, // Action Bottom
                { 2, "button 2" }, // Action Right
                { 3, "button 3" }, // Action Top
                { 4, "button 4" }, // Left Bumper
                { 5, "button 5" }, // Right Bumper
                { 6, "button 6" }, // Left Trigger
                { 7, "button 7" }, // Right Trigger
                { 8, "button 9" }, // Select
                { 9, "button 8" }, // Start
                { 10, "button 10" }, // Left Stick
                { 11, "button 11" }, // Right Stick
                { 12, "button 12" }, // Guide
                { 13, "button 13" }, // Touchpad
                { 16, "None" }, // DPad Left
                { 15, "None" }, // DPad Down
                { 17, "None" }, // DPad Right
                { 14, "None" }, // DPad Up
            };
        }
    }
}
