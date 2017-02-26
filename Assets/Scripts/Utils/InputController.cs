using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using GameConfig;

namespace GameUtils
{
    public struct InputAxis
    {
        public string axisName;
        public KeyCode positiveButton;
        public KeyCode negativeButton;
        public InputAxis(string name, KeyCode pButton, KeyCode nButton)
        {
            axisName = name;
            positiveButton = pButton;
            negativeButton = nButton;
        }
    }

    public class InputController
    {

        private static InputController _instance = new InputController();

        private InputConfig config;

        private Dictionary<string, InputAxis> axisMap;

        private InputController()
        {
            config = InputConfig.getInstance();
            InputAxis xAxis_1 = new InputAxis("Horizontal_1", config.moveRight_1, config.moveLeft_1);
            InputAxis zAxis_1 = new InputAxis("Vertical_1", config.moveForward_1, config.moveBackward_1);
            InputAxis xAxis_2 = new InputAxis("Horizontal_2", config.moveRight_2, config.moveLeft_2);
            InputAxis zAxis_2 = new InputAxis("Vertical_2", config.moveForward_2, config.moveBackward_2);

            axisMap = new Dictionary<string, InputAxis>()
            {
                { "Horizontal_1", xAxis_1 },
                { "Vertical_1", zAxis_1 },
                { "Horizontal_2", xAxis_2 },
                { "Vertical_2", zAxis_2 },
            };
        }

        public static InputController getInstance()
        {
            return _instance;
        }

        public float GetAxis(string axisName)
        {
            if (axisMap.ContainsKey(axisName))
            {
                InputAxis axis = axisMap[axisName];
                if (Input.GetKey(axis.positiveButton))
                    return 1f;
                else if (Input.GetKey(axis.negativeButton))
                    return -1f;
                else
                    return 0f;
            }

            Debug.Log("No such axis.");
            throw new Exception();
        }

        public bool GetKeyDown(string action)
        {
            KeyCode key = (KeyCode)config.GetParam(config, action);
            return Input.GetKeyDown(key);
        }

        public bool GetKey(string action)
        {
            KeyCode key = (KeyCode)config.GetParam(config, action);
            return Input.GetKey(key);
        }
    }
}

