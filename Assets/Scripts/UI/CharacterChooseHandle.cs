using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCharacters;
using GameUtils;


namespace GameUI
{
    public class CharacterChooseHandle : MonoBehaviour
    {

        private GameObject characters;

        private GLOBAL_SCREEN globalScreen;

        private void Awake()
        {
            globalScreen = GameObject.Find("UIController").GetComponent<GLOBAL_SCREEN>();
            Screen.SetResolution(globalScreen.newRes.width, globalScreen.newRes.height, globalScreen.full_screen);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void Enter()
        {

        }
    }

}
