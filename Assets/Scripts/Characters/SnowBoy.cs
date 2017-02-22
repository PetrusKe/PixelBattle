using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public class SnowBoy : Character
    {
        public override void LightAttack()
        {
            ChangeAnim("attack1");
        }

        protected override void GetWeaponsCenter()
        {
            base.GetWeaponsCenter();
            Transform leftWeaponCenter = transform.Find("Body/ArmL/HandL/_holder/Weapon/HitCenter");
            Transform rightWeaponCenter = transform.Find("Body/ArmR/HandR/_holder/Weapon/HitCenter");
            characterWeaponsCenter = new Transform[2] { leftWeaponCenter, rightWeaponCenter };
        }
    }
}

