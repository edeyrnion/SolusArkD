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

            buttons = new Dictionary<int, string>
        {
            { 0, "Button0" }, // Action Left
            { 1, "Button1" }, // Action Bottom
            { 2, "Button2" }, // Action Right
            { 3, "Button3" }, // Action Top
            { 4, "Button4" }, // Left Bumper
            { 5, "Button5" }, // Right Bumper
            { 6, "Button6" }, // Left Trigger
            { 7, "Button7" }, // Right Trigger
            { 8, "Button8" }, // Select
            { 9, "Button9" }, // Start
            { 10, "Button10" }, // Left Stick
            { 11, "Button11" }, // Right Stick
            { 12, "Button12" }, // Guide
            { 13, "Button13" }, // Touchpad
            { 16, "None" }, // DPad Left
            { 15, "None" }, // DPad Down
            { 17, "None" }, // DPad Right
            { 14, "None" }, // DPad Up
        };
        }
    }
}
