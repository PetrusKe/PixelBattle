using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameParicles;

namespace GameCharacters
{
    public class MagicGirl : Character
    {
        public override void LightAttack()
        {
            base.LightAttack();
            if (skills.lightAttackAttrs.waitTime >= 0)
                return;

            state.IsLightAttack = true;   // may bug
            ChangeAnim(anim.lightAttack);
            if (weaponCenter.Length == 0)
                return;

            LightAttackAttrs skillAttrs = skills.lightAttackAttrs;
            GameObject magicBall = Instantiate(extendObj.lightAttackObj, weaponCenter[0].position, weaponCenter[0].rotation) as GameObject;
            MagicBallParicles paricles = magicBall.transform.GetComponent<MagicBallParicles>();

            paricles.owner = gameObject;
            paricles.lifeTime = skillAttrs.lifeTime;
            paricles.power = skillAttrs.power;
            paricles.force = skillAttrs.force;
            paricles.speed = skillAttrs.speed;

            skills.lightAttackAttrs.waitTime = 0;
        }

        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
            Transform leftWeaponCenter = transform.Find("Body/ArmL/HandL/_holder/Weapon/HitCenter");
            weaponCenter = new Transform[1] { leftWeaponCenter };

        }

        public override void CheckCoolTime()
        {
            base.CheckCoolTime();

            if (skills.lightAttackAttrs.waitTime < skills.lightAttackAttrs.coolTime
                && skills.lightAttackAttrs.waitTime >= 0)
                skills.lightAttackAttrs.waitTime += Time.deltaTime;
            else if (skills.lightAttackAttrs.waitTime >= skills.lightAttackAttrs.coolTime)
                skills.lightAttackAttrs.waitTime = -1;
        }

    }
}

