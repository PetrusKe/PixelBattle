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

            ChangeAnim(anim.lightAttack);
            LightAttackAttrs skillAttrs = characterSkills.lightAttackAttrs;
            if (characterWeaponsCenter.Length == 0)
                return;
            GameObject magicBall = Instantiate(extendObj.lightAttackObj, characterWeaponsCenter[0].position, characterWeaponsCenter[0].rotation) as GameObject;
            MagicBallParicles paricles = magicBall.transform.GetComponent<MagicBallParicles>();

            paricles.owner = gameObject;
            paricles.lifeTime = skillAttrs.lifeTime;
            paricles.power = skillAttrs.power;
            paricles.force = skillAttrs.force;
            paricles.speed = skillAttrs.speed;

            
        }

        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
            Transform leftWeaponCenter = transform.Find("Body/ArmL/HandL/_holder/Weapon/HitCenter");
            characterWeaponsCenter = new Transform[1] { leftWeaponCenter };

        }

    }
}

