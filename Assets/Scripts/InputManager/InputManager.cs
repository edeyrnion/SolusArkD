using System.Collections.Generic;
using UnityEngine;

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
            inputProfiles.Add(p);
        }
    }

    public float GetAxis()
    {
        
        return 0f;
    }
}
