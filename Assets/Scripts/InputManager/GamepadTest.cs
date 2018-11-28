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
                AddInput(btn, child);
            }

            var trans8 = transform.GetChild(8);
            AddInput(GamepadAxis.LStick_X, trans8);
            AddInput(GamepadAxis.LStick_Y, trans8);

            var trans9 = transform.GetChild(9);
            AddInput(GamepadAxis.RStick_X, trans9);
            AddInput(GamepadAxis.RStick_Y, trans9);

            var trans15 = transform.GetChild(15);
            AddInput(GamepadAxis.LTrigger, trans15);

            var trans14 = transform.GetChild(14);
            AddInput(GamepadAxis.RTrigger, trans14);

            var trans4 = transform.GetChild(4);
            AddInput(GamepadAxis.DPad_X, trans4);

            var trans5 = transform.GetChild(5);
            AddInput(GamepadAxis.DPad_X, trans5);

            var trans6 = transform.GetChild(6);
            AddInput(GamepadAxis.DPad_Y, trans6);

            var trans7 = transform.GetChild(7);
            AddInput(GamepadAxis.DPad_Y, trans7);
        }

        private void Update()
        {
            var btnsReleased = CInput.GetAllButtonsUp();
            int btnRCount = btnsReleased.Count;
            for (int i = 0; i < btnRCount; i++)
            {
                var btnReleased = btnsReleased[i];
                if (buttons.ContainsKey(btnReleased))
                {
                    buttons[btnReleased].material.color = btnColor[buttons[btnReleased]];
                }
            }

            var btnsPressed = CInput.GetAllButtons();
            int btnPCount = btnsPressed.Count;
            for (int i = 0; i < btnPCount; i++)
            {
                var btnPressed = btnsPressed[i];
                if (buttons.ContainsKey(btnPressed))
                {
                    buttons[btnPressed].material.color = new Color(0f, 1f, 0f);
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
            if (valueDL >= 0f)
            {
                transform.GetChild(5).position = axisPos[transform.GetChild(5)] + new Vector3(valueDL * 0.3f, 0, 0);
            }

            float valueDT = axisPushed[GamepadAxis.DPad_Y];

            if (valueDT >= 0f)
            {
                axis[GamepadAxis.DPad_Y].position = axisPos[axis[GamepadAxis.DPad_Y]] + new Vector3(0, 0, valueDT * 0.3f);
            }
            if (valueDT <= 0f)
            {
                transform.GetChild(7).position = axisPos[transform.GetChild(7)] + new Vector3(0, 0, valueDT * 0.3f);
            }

        }

        private GamepadButton GetButtonEnum(string name)
        {
            return (GamepadButton)Enum.Parse(typeof(GamepadButton), name);
        }

        private void AddInput(GamepadButton btn, Transform trans)
        {
            var r = trans.GetComponent<Renderer>();
            buttons.Add(btn, r);
            btnColor.Add(r, r.material.color);
        }

        private void AddInput(GamepadAxis btn, Transform trans)
        {
            if (!axis.ContainsKey(btn))
            {
                axis.Add(btn, trans);
            }
            if (!axisPos.ContainsKey(trans))
            {
                axisPos.Add(trans, trans.position);
            }
        }
    }
}
