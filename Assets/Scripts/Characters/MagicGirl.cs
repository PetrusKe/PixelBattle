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
            ChangeAnim(anim.lightAttack);
            LightAttackAttrs skillAttrs = characterSkills.lightAttackAttrs;

            //if (characterWeaponsCenter.Length == 0)
            //    return;
            //foreach (Transform hit in characterWeaponsCenter)
            //{
            //    Collider[] colliders = Physics.OverlapSphere(hit.position, 0.3f, 1 << LayerMask.NameToLayer("Character"));
            //    foreach (Collider collider in colliders)
            //    {
            //        if (collider.gameObject.name == this.gameObject.name)
            //            continue;
            //        Character other = collider.gameObject.GetComponent<Character>();
            //        other.TakeDamage(skillAttrs.power);
            //    }
            //}
        }
        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
        }

    }
}

