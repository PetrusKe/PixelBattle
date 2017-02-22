using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public struct LightAttackAttrs
    {
        public string name { get; set; }
        public float power { get; set; }
        public bool isAct { get; set; }
    }

    public class CharacterSkills
    {
        public LightAttackAttrs lightAttackAttrs;

        public CharacterSkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 0f;
            lightAttackAttrs.isAct = false;
        }
            
        public virtual void lightAttackHurt() { }

    }

    public class MagicGirlSkills : CharacterSkills
    {
        public MagicGirlSkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 5.0f;
            lightAttackAttrs.isAct = false;

        }

        public override void lightAttackHurt()
        {
            base.lightAttackHurt();

        }
    }

    public class SnowBoySkills : CharacterSkills
    {
        public SnowBoySkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 7.0f;
            lightAttackAttrs.isAct = false;
            
        }
    }
}
