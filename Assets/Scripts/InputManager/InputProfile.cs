using System.Collections.Generic;
using System;

public partial class InputProfile : IInputProfile
{

    public string Name => name;
    public IReadOnlyDictionary<int, string> Axis => axis;
    public IReadOnlyDictionary<int, string> Buttons => buttons;

    private string name;
    private Dictionary<int, string> axis;
    private Dictionary<int, string> buttons;

    public InputProfile()
    {
        DefaultProfile();
    }

    public InputProfile(Action profileName)
    {
        profileName();
    }
}
