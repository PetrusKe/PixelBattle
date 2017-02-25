using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public struct LightAttackAttrs
    {
        public string name { get; set; }
        public float power { get; set; }
        public float lifeTime { get; set; }
        public float force { get; set; }
        public float speed { get;  set; }
        public float coolTime { get;  set; }
        public float waitTime { get;  set; }
    }

    public class CharacterSkills
    {
        public LightAttackAttrs lightAttackAttrs;

        public CharacterSkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 0f;
            lightAttackAttrs.lifeTime = 0f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 0f;
            lightAttackAttrs.coolTime = 0f;
            lightAttackAttrs.waitTime = -1f;
        }
    }

    public class MagicGirlSkills : CharacterSkills
    {
        public MagicGirlSkills()
        {
            lightAttackAttrs.power = 5.0f;
            lightAttackAttrs.lifeTime = 10.0f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 15f;
            lightAttackAttrs.coolTime = 0.7f;
        }
    }

    public class SnowBoySkills : CharacterSkills
    {
        public SnowBoySkills()
        {
            lightAttackAttrs.power = 2.0f;
        }
    }
}
