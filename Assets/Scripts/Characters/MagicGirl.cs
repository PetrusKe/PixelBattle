using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public class MagicGirl : Character
    {
        public override void LightAttack()
        {
            base.LightAttack();

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1A_Hand"))
            {
                ChangeAnim("attack1B");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1B_Hand"))
                ChangeAnim("attack1A");

            ChangeAnim("attack1");
        }

    }
}

