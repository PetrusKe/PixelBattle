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

        // FIXME(Petrus): cancel this param future, and fix Inp
        public int controllerId;

        private void Start()
        {
            character = GetComponent<Character>();
            inputController = InputController.getInstance();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            PositionAction();
            AttackAction();
        }


        private void PositionAction()
        {
            character.CheckGround();
            if (inputController.GetKeyDown("jump_" + controllerId))
            {
                character.Jump();
                return;
            }
            Debug.Log("Horizontal_" + controllerId);

            float h = inputController.GetAxis("Horizontal_" + controllerId);
            float v = inputController.GetAxis("Vertical_" + controllerId);
            character.Move(h, v);

            
        }



        public void AttackAction()
        {
            character.CheckCoolTime();

            if (inputController.GetKeyDown("dash_" + controllerId))
                character.Dash();

            if (inputController.GetKeyDown("lightAttack_"+controllerId))
                character.LightAttack();

            if (inputController.GetKeyDown("hardAttack_" + controllerId))
                character.HardAttack();
        }
    }
}