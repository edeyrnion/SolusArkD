using System.Collections.Generic;

namespace Matthias
{
    public partial class InputProfile
    {
        private void Logitech_F710_Init()
        {
            methods.Add("Controller (Wireless Gamepad F710)", Logitech_F710);  // methods.Add("NAME_OF_DEVICE", NAME_OF_METHOD);
        }

        public void Logitech_F710()
        {
            name = "Logitech F710";

            axis = new Dictionary<int, string>
        {
            { 0, "AxisX" }, // Left Stick X
            { 1, "AxisY" }, // Left Stick Y
            { 2, "Axis4" }, // Right Stick X
            { 3, "Axis5" }, // Richt Stick Y
            { 4, "Axis3" }, // Left Trigger
            { 5, "Axis6" }, // Right Trigger
            { 6, "Axis6" }, // DPad X
            { 7, "Axis7" }, // DPad Y
        };

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
