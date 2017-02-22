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
            AttackAction();
            PositionAction();
            AttrsUpdateAction();
        }

        private void PositionAction()
        {
            float h = inputController.GetAxis("Horizontal_" + controllerId);
            float v = inputController.GetAxis("Vertical_" + controllerId);
            if (h != 0 || v != 0)
            {
                character.Move(h, v);
                character.Turn(h, v);
                character.ChangeAnim("run", true);
            }
            else
            {
                character.ChangeAnim("run", false);
            }
        }

        private void AttrsUpdateAction()
        {
            
        }

        public void AttackAction()
        {
            if (character.state.isAttacking())
                return;
            if(inputController.GetKeyDown("lightAttack_"+controllerId))
            {
                character.LightAttack();
            }
        }
    }
}