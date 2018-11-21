using UnityEngine;

namespace Matthias
{
    public class ControllerCheck : MonoBehaviour
    {
        void Start()
        {
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                print(names[x].Length);
                print(names[x]);
                if (names[x].Length == 19)
                {
                    print("PS4 CONTROLLER IS CONNECTED");
                }
                if (names[x].Length == 33)
                {
                    print("XBOX ONE CONTROLLER IS CONNECTED");

                }
            }
        }
    }
}
