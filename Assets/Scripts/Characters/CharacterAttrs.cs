using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public struct LightAttackAttrs
    {
        public string name { get; set; }
        public float power { get; set; }
        public LightAttackAttrs(float power = 0.0f)
        {
            this.name = "LightAttack";
            this.power = power;
        }
    }

    public class CharacterAttrs : MonoBehaviour
    {
        public float walkSpeed;
        public float maxHP;
        public LightAttackAttrs lightAttackAttrs;

        public CharacterAttrs()
        {
            walkSpeed = 0.0f;
            maxHP = 0.0f;
            lightAttackAttrs = new LightAttackAttrs();
        }
    }

    public class MagicGirlAttrs : CharacterAttrs
    {
        public MagicGirlAttrs()
        {
            walkSpeed = 6.0f;
            maxHP = 100.0f;
            lightAttackAttrs = new LightAttackAttrs(5.0f);
        }
    }

    public class SnowBoyAttrs : CharacterAttrs
    {
        public SnowBoyAttrs()
        {
            walkSpeed = 5.0f;
            maxHP = 120.0f;
            lightAttackAttrs = new LightAttackAttrs(7.0f);
        }
    }

}
