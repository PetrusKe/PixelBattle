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
            Debug.Log("Horizontal_" + controllerId);
            float v = inputController.GetAxis("Vertical_" + controllerId);
            if (h != 0 || v != 0)
            {
                character.Walk(h, v);
                character.Turn(h, v);
                character.ChangeAnim("walk", true);
            }
            else
            {
                character.ChangeAnim("walk", false);
            }
        }

        private void AttrsUpdateAction()
        {
            float amount = 0f;
            character.ChangeHP(amount);
        }

        public void AttackAction()
        {
            if(inputController.GetKeyDown("lightAttack_"+controllerId))
            {
                // 设置 动画连击
                character.LightAttack();
                character.ChangeAnim("attack1A");
            }
        }
    }
}