using System.Collections.Generic;
using System;
using System.Reflection;

public partial class InputProfile
{

    public string Name => name;
    public IReadOnlyDictionary<int, string> Axis => axis;
    public IReadOnlyDictionary<int, string> Buttons => buttons;

    private string name = "";
    private Dictionary<int, string> axis = new Dictionary<int, string>();
    private Dictionary<int, string> buttons = new Dictionary<int, string>();

    private Dictionary<string, Action> methods = new Dictionary<string, Action>();

    partial void Blub();

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
            { 6, "Axis6" }, // DPad X
            { 7, "Axis7" }, // DPad Y
        };

        buttons = new Dictionary<int, string>
        {
            { 0, "JoystickButton2" }, // Action Left
            { 1, "JoystickButton0" }, // Action Bottom
            { 2, "JoystickButton1" }, // Action Right
            { 3, "JoystickButton3" }, // Action Top
            { 4, "JoystickButton4" }, // Left Bumper
            { 5, "JoystickButton5" }, // Right Bumper
            { 6, "None" }, // Left Trigger
            { 7, "None" }, // Right Trigger
            { 8, "JoystickButton6" }, // Select
            { 9, "JoystickButton7" }, // Start
            { 10, "JoystickButton8" }, // Left Stick
            { 11, "JoystickButton9" }, // Right Stick
            { 12, "None" }, // Guide
            { 13, "None" }, // Touchpad
            { 16, "None" }, // DPad Left
            { 15, "None" }, // DPad Down
            { 17, "None" }, // DPad Right
            { 14, "None" }, // DPad Up
        };
    }
}
