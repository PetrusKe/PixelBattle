using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{
    public class CharacterAttrs : MonoBehaviour
    {
        public float walkSpeed;
        public float maxHP;

        public CharacterAttrs()
        {
            walkSpeed = 0.0f;
            maxHP = 0.0f;
        }
    }

    public class MagicGirlAttrs : CharacterAttrs
    {
        public MagicGirlAttrs()
        {
            walkSpeed = 6.0f;
            maxHP = 100.0f;
        }
    }

    public class SnowBoyAttrs : CharacterAttrs
    {
        public SnowBoyAttrs()
        {
            walkSpeed = 5.0f;
            maxHP = 120.0f;
        }
    }

}
