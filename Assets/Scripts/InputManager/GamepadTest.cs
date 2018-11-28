using System;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    public class GamepadTest : MonoBehaviour
    {
        private Dictionary<GamepadButton, Renderer> buttons;
        private Dictionary<GamepadAxis, Transform> axis;

        private Dictionary<Renderer, Color> btnColor;
        private Dictionary<Transform, Vector3> axisPos;

        private IReadOnlyDictionary<int, GamepadButton> btnActions;
        private IReadOnlyDictionary<int, GamepadAxis> axisActions;

        private void Start()
        {
            btnActions = CInput.BtnActions;
            axisActions = CInput.AxisActions;

            buttons = new Dictionary<GamepadButton, Renderer>();
            axis = new Dictionary<GamepadAxis, Transform>();

            btnColor = new Dictionary<Renderer, Color>();
            axisPos = new Dictionary<Transform, Vector3>();

            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);

                GamepadButton btn = GetButtonEnum(child.name);
                Renderer renderer = child.GetComponent<Renderer>();

                buttons.Add(btn, renderer);
                btnColor.Add(renderer, renderer.material.color);
            }

            var trans8 = transform.GetChild(8);
            axis.Add(GamepadAxis.LStick_X, trans8);
            axisPos.Add(trans8, trans8.position);

            var trans8a = transform.GetChild(8);
            axis.Add(GamepadAxis.LStick_Y, trans8a);

            var trans9 = transform.GetChild(9);
            axis.Add(GamepadAxis.RStick_X, trans9);
            axisPos.Add(trans9, trans9.position);

            var trans9a = transform.GetChild(9);
            axis.Add(GamepadAxis.RStick_Y, trans9a);

            var trans15 = transform.GetChild(15);
            axis.Add(GamepadAxis.LTrigger, trans15);
            axisPos.Add(trans15, trans15.position);

            var trans14 = transform.GetChild(14);
            axis.Add(GamepadAxis.RTrigger, trans14);
            axisPos.Add(trans14, trans14.position);

            var trans4 = transform.GetChild(4);
            axis.Add(GamepadAxis.DPad_X, trans4);
            axisPos.Add(trans4, trans4.position);

            var trans6 = transform.GetChild(6);
            axis.Add(GamepadAxis.DPad_Y, trans6);
            axisPos.Add(trans6, trans6.position);

        }

        private void Update()
        {
            var buttonsReleased = CInput.GetAllButtonsUp();
            int btnRCount = buttonsReleased.Count;
            for (int i = 0; i < btnRCount; i++)
            {
                if (buttons.ContainsKey(buttonsReleased[i]))
                {
                    buttons[buttonsReleased[i]].material.color = btnColor[buttons[buttonsReleased[i]]];
                }
            }

            var buttonsPressed = CInput.GetAllButtons();
            int btnPCount = buttonsPressed.Count;
            for (int i = 0; i < btnPCount; i++)
            {
                if (buttons.ContainsKey(buttonsPressed[i]))
                {
                    buttons[buttonsPressed[i]].material.color = new Color(0f, 1f, 0f);
                }
            }

            var axisPushed = CInput.GetAllAxis();

            float valueLX = axisPushed[GamepadAxis.LStick_X];
            float valueLY = axisPushed[GamepadAxis.LStick_Y];
            axis[GamepadAxis.LStick_X].position = axisPos[axis[GamepadAxis.LStick_X]] + new Vector3(valueLX * 0.5f, 0, valueLY * 0.5f);

            float valueRX = axisPushed[GamepadAxis.RStick_X];
            float valueRY = axisPushed[GamepadAxis.RStick_Y];
            axis[GamepadAxis.RStick_X].position = axisPos[axis[GamepadAxis.RStick_X]] + new Vector3(valueRX * 0.5f, 0, valueRY * 0.5f);

            float valueLT = axisPushed[GamepadAxis.LTrigger];
            axis[GamepadAxis.LTrigger].position = axisPos[axis[GamepadAxis.LTrigger]] + new Vector3(-valueLT * 0.5f, 0, 0);

            float valueRT = axisPushed[GamepadAxis.RTrigger];
            axis[GamepadAxis.RTrigger].position = axisPos[axis[GamepadAxis.RTrigger]] + new Vector3(valueRT * 0.5f, 0, 0);

            float valueDL = axisPushed[GamepadAxis.DPad_X];
            if (valueDL <= 0f)
            {
                axis[GamepadAxis.DPad_X].position = axisPos[axis[GamepadAxis.DPad_X]] + new Vector3(valueDL * 0.3f, 0, 0);
            }
            else
            {
            }

            float valueDT = axisPushed[GamepadAxis.DPad_Y];

            if (valueDT >= 0f)
            {
                axis[GamepadAxis.DPad_Y].position = axisPos[axis[GamepadAxis.DPad_Y]] + new Vector3(0, 0, valueDT * 0.3f);
            }
            else
            {
            }

        }

        private GamepadButton GetButtonEnum(string name)
        {
            return (GamepadButton)Enum.Parse(typeof(GamepadButton), name);
        }

        private GamepadAxis GetAxisEnum(string name)
        {
            return (GamepadAxis)Enum.Parse(typeof(GamepadAxis), name);
        }
    }
}
