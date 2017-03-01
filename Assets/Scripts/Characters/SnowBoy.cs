using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public class SnowBoy : Character
    {
        public override bool LightAttack()
        {
            if (!base.LightAttack())
                return false;
            if (weaponCenter.Length == 0)
                return false;

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
            return true;
        }

        public override bool HardAttack()
        {
            if (!base.HardAttack())
                return false;
            if (skills.hardAttackAttrs.waitTime >= 0)
                return false;
            if (weaponCenter.Length == 0)
                return false;

            state.IsHardAttack = true;
            ChangeAnim(anim.hardAttack);

            if(extendObj.hardAttackParticle!=null)
            {
                // Bug here
                GameObject particle = Instantiate(extendObj.hardAttackParticle, weaponCenter[0].position, transform.rotation, transform) as GameObject;
                Destroy(particle, 1f);
            }

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
            return true;
        }

        public override bool Dash()
        {
            if (!base.Dash())
                return false;

            state.IsDash = true;
            Vector3 movement = -transform.TransformDirection(Vector3.forward).normalized;
            movement = movement * skills.dashAttrs.distance;

            Hashtable args = new Hashtable();
            args.Add("speed", skills.dashAttrs.speed);
            args.Add("easetype", skills.dashAttrs.moveWay);
            args.Add("onstart", "ChangeDashAnim");
            args.Add("onstartparams", true);
            args.Add("oncomplete", "ChangeDashAnim");
            args.Add("oncompleteparams", false);
            args.Add("position", transform.position + movement);
            iTween.MoveTo(gameObject, args);
            skills.dashAttrs.waitTime = 0;
            state.IsDash = false;
            return true;
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

