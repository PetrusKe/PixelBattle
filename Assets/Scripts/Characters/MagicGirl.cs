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
            if (characterSkills.lightAttackAttrs.waitTime >= 0)
                return;

            state.IsLightAttack = true;   // may bug
            ChangeAnim(anim.lightAttack);
            if (characterWeaponsCenter.Length == 0)
                return;

            LightAttackAttrs skillAttrs = characterSkills.lightAttackAttrs;
            GameObject magicBall = Instantiate(extendObj.lightAttackObj, characterWeaponsCenter[0].position, characterWeaponsCenter[0].rotation) as GameObject;
            MagicBallParicles paricles = magicBall.transform.GetComponent<MagicBallParicles>();

            paricles.owner = gameObject;
            paricles.lifeTime = skillAttrs.lifeTime;
            paricles.power = skillAttrs.power;
            paricles.force = skillAttrs.force;
            paricles.speed = skillAttrs.speed;

            characterSkills.lightAttackAttrs.waitTime = 0;
        }

        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
            Transform leftWeaponCenter = transform.Find("Body/ArmL/HandL/_holder/Weapon/HitCenter");
            characterWeaponsCenter = new Transform[1] { leftWeaponCenter };

        }

        public override void CheckCoolTime()
        {
            if (characterSkills.lightAttackAttrs.waitTime < characterSkills.lightAttackAttrs.coolTime
                && characterSkills.lightAttackAttrs.waitTime >= 0)
                characterSkills.lightAttackAttrs.waitTime += Time.deltaTime;
            else if (characterSkills.lightAttackAttrs.waitTime >= characterSkills.lightAttackAttrs.coolTime)
                characterSkills.lightAttackAttrs.waitTime = -1;
        }

    }
}

