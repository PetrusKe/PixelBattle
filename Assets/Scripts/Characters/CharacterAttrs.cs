using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharacters
{

    public class CharacterAttrs
    {
        public float runSpeed;
        public float jumpSpeed;
        public float maxHP;

        public CharacterAttrs()
        {
            runSpeed = 0.0f;
            jumpSpeed = 0.0f;
            maxHP = 0.0f;
        }
    }

    public class MagicGirlAttrs : CharacterAttrs
    {
        public MagicGirlAttrs()
        {
            runSpeed = 6.0f;
            jumpSpeed = 5.0f;
            maxHP = 100.0f;
            
        }
    }

    public class SnowBoyAttrs : CharacterAttrs
    {
        public SnowBoyAttrs()
        {
            runSpeed = 5.0f;
            jumpSpeed = 5.0f;
            maxHP = 120.0f;
        }
    }

}
