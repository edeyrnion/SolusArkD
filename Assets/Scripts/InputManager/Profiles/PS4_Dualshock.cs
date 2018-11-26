using System.Collections.Generic;

public partial class InputProfile
{
    private void PS4_DualshockInit()
    {
        methods.Add("PS4 Dualshock", PS4_Dualshock);  // methods.Add("NAME_OF_DEVICE", NAME_OF_METHOD);
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
            { 0, "JoystickButton0" }, // Action Left
            { 1, "JoystickButton1" }, // Action Bottom
            { 2, "JoystickButton2" }, // Action Right
            { 3, "JoystickButton3" }, // Action Top
            { 4, "JoystickButton4" }, // Left Bumper
            { 5, "JoystickButton5" }, // Right Bumper
            { 6, "JoystickButton6" }, // Left Trigger
            { 7, "JoystickButton7" }, // Right Trigger
            { 8, "JoystickButton8" }, // Select
            { 9, "JoystickButton9" }, // Start
            { 10, "JoystickButton10" }, // Left Stick
            { 11, "JoystickButton11" }, // Right Stick
            { 12, "JoystickButton12" }, // Guide
            { 13, "JoystickButton13" }, // Touchpad
            { 16, "None" }, // DPad Left
            { 15, "None" }, // DPad Down
            { 17, "None" }, // DPad Right
            { 14, "None" }, // DPad Up
        };
    }
}
