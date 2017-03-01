using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameParicles;

namespace GameCharacters
{
    public class MagicGirl : Character
    {
        public override bool LightAttack()
        {
            if (!base.LightAttack())
                return false;
            if (skills.lightAttackAttrs.waitTime >= 0)
                return false;
            if (weaponCenter.Length == 0)
                return false;

            state.IsLightAttack = true;
            ChangeAnim(anim.lightAttack);

            LightAttackAttrs skillAttrs = skills.lightAttackAttrs;
            GameObject magicBall = Instantiate(extendObj.lightAttackParticle, weaponCenter[0].position, weaponCenter[0].rotation) as GameObject;
            MagicBallParicles paricles = magicBall.transform.GetComponent<MagicBallParicles>();

            paricles.owner = gameObject;
            paricles.lifeTime = skillAttrs.lifeTime;
            paricles.power = skillAttrs.power;
            paricles.force = skillAttrs.force;
            paricles.speed = skillAttrs.speed;
            paricles.radius = skillAttrs.radius;
            skills.lightAttackAttrs.waitTime = 0;
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

            HardAttackAttrs skillAttrs = skills.hardAttackAttrs;
            skills.hardAttackAttrs.waitTime = 0;
            return true;
        }

        public override bool Dash()
        {
            if (!base.Dash())
                return false;

            state.IsDash = true;

            if (extendObj.dashParticle != null)
            {
                Vector3 pos = transform.position - new Vector3(0f, 0.5f, 0f);
                GameObject particle = Instantiate(extendObj.dashParticle, pos, Quaternion.Euler(new Vector3(-90f, 0f, -90f))) as GameObject;
                Destroy(particle, 1f);
            }

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

            if (skills.hardAttackAttrs.waitTime < skills.hardAttackAttrs.coolTime
                && skills.hardAttackAttrs.waitTime >= 0)
                skills.hardAttackAttrs.waitTime += Time.deltaTime;
            else if (skills.hardAttackAttrs.waitTime >= skills.hardAttackAttrs.coolTime)
                skills.hardAttackAttrs.waitTime = -1;
        }

    }
}

