using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public class SnowBoy : Character
    {
        public override void LightAttack()
        {
            base.LightAttack();

            state.IsLightAttack = true;         // may bug
            ChangeAnim(anim.lightAttack);
            LightAttackAttrs skillAttrs = skills.lightAttackAttrs;

            if (weaponCenter.Length == 0)
                return;

            foreach (Transform hit in weaponCenter)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.position, 0.3f, 1 << LayerMask.NameToLayer("Character"));
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.name == this.gameObject.name)
                        continue;
                    Character other = collider.gameObject.GetComponent<Character>();
                    other.TakeDamage(skillAttrs.power);
                }           
            }
        }

        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
            Transform leftWeaponCenter = transform.Find("Body/ArmL/HandL/_holder/Weapon/HitCenter");
            Transform rightWeaponCenter = transform.Find("Body/ArmR/HandR/_holder/Weapon/HitCenter");
            weaponCenter = new Transform[2] { leftWeaponCenter, rightWeaponCenter };
        }

        public override void CheckCoolTime()
        {
            base.CheckCoolTime();
        }

    }
}

