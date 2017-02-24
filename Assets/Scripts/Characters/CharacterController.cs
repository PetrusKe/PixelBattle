using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfig;
using GameUtils;


namespace GameCharacters
{
    public class CharacterController : MonoBehaviour
    {
        public Character character { get; private set; }
        InputController inputController;

        private int controllerId;

        private void Start()
        {
            character = GetComponent<Character>();
            inputController = new InputController();
            controllerId = InputController.ControllerId;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            PositionAction();
            AttackAction();
        }


        private void PositionAction()
        {
            float h = inputController.GetAxis("Horizontal_" + controllerId);
            float v = inputController.GetAxis("Vertical_" + controllerId);
            character.Move(h, v);
        }



        public void AttackAction()
        {
            if(inputController.GetKeyDown("lightAttack_"+controllerId))
            {
                character.LightAttack();
            }
        }

    }
}