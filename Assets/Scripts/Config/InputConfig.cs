using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameConfig
{
    public class InputConfig : BaseConfig
    {
        private string[] inputParamsMap =
        {
            "moveForward_1",
            "moveBackWard_1",
            "moveRight_1",
            "moveLeft_1",
            "lightAttack_1",
            "hardAttack_1",
            "dash_1",
            "jump_1",

            "moveForward_2",
            "moveBackWard_2",
            "moveRight_2",
            "moveLeft_2",
            "lightAttack_2",
            "hardAttack_2",
            "dash_2",
            "jump_2",
        };

        protected override string[] ParamsMap { get { return inputParamsMap; } }

        public KeyCode moveForward_1 { get; private set; }
        public KeyCode moveBackward_1 { get; private set; }
        public KeyCode moveLeft_1 { get; private set; }
        public KeyCode moveRight_1 { get; private set; }
        public KeyCode lightAttack_1 { get; private set; }
        public KeyCode hardAttack_1 { get; private set; }
        public KeyCode dash_1 { get; private set; }
        public KeyCode jump_1 { get; private set; }

        public KeyCode moveForward_2 { get; private set; }
        public KeyCode moveBackward_2 { get; private set; }
        public KeyCode moveLeft_2 { get; private set; }
        public KeyCode moveRight_2 { get; private set; }
        public KeyCode lightAttack_2 { get; private set; }
        public KeyCode hardAttack_2 { get; private set; }
        public KeyCode dash_2 { get; private set; }
        public KeyCode jump_2 { get; private set; }

        private static InputConfig _instance = new InputConfig();
        public static InputConfig getInstance() { return _instance; }

        private InputConfig()
        {
            moveForward_1 = (KeyCode)119;
            moveBackward_1 = (KeyCode)115;
            moveRight_1 = (KeyCode)100;
            moveLeft_1 = (KeyCode)97;
            lightAttack_1 = (KeyCode)106;
            hardAttack_1 = (KeyCode)107;
            dash_1 = (KeyCode)304;
            jump_1 = (KeyCode)117;

            moveForward_2 = (KeyCode)273;
            moveBackward_2 = (KeyCode)274;
            moveRight_2 = (KeyCode)275;
            moveLeft_2 = (KeyCode)276;
            lightAttack_2 = (KeyCode)257;
            hardAttack_2 = (KeyCode)258;
            dash_2 = (KeyCode)271;
            jump_2 = (KeyCode)259;
        }
    }
}