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
            if (weaponCenter.Length == 0)
                return;

            state.IsLightAttack = true;         // may bug
            ChangeAnim(anim.lightAttack);
            LightAttackAttrs skillAttrs = skills.lightAttackAttrs;

            foreach (Transform hit in weaponCenter)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.position, skillAttrs.radius, 1 << LayerMask.NameToLayer("Character"));
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.name == this.gameObject.name)
                        continue;
                    Character other = collider.gameObject.GetComponent<Character>();
                    other.TakeDamage(skillAttrs.power);
                }           
            }
        }

        public override void HardAttack()
        {
            base.HardAttack();
            if (skills.hardAttackAttrs.waitTime >= 0)
                return;
            if (weaponCenter.Length == 0)
                return;

            state.IsHardAttack = true;
            ChangeAnim(anim.hardAttack);
            HardAttackAttrs skillAttrs = skills.hardAttackAttrs;
            
            foreach(Transform hit in weaponCenter)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.position, skillAttrs.radius, 1 << LayerMask.NameToLayer("Character"));
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.name == this.gameObject.name)
                        continue;
                    Character other = collider.gameObject.GetComponent<Character>();
                    other.TakeDamage(skillAttrs.power);
                }
            }
            skills.hardAttackAttrs.waitTime = 0;
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

            if (skills.hardAttackAttrs.waitTime < skills.hardAttackAttrs.coolTime
                && skills.hardAttackAttrs.waitTime >= 0)
                skills.hardAttackAttrs.waitTime += Time.deltaTime;
            else if (skills.hardAttackAttrs.waitTime >= skills.hardAttackAttrs.coolTime)
                skills.hardAttackAttrs.waitTime = -1;
        }

    }
}

