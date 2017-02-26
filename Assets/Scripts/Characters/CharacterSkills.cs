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
        public float speed { get; set; }
        public float coolTime { get; set; }
        public float waitTime { get; set; }
    }

    public struct Dash
    {
        public string name { get; set; }
        public float distance { get; set; }
        public float speed { get; set; }
        public float coolTime { get; set; }
        public float waitTime { get; set; }
        public iTween.EaseType moveWay { get; set; }
    }

    public class CharacterSkills
    {
        public LightAttackAttrs lightAttackAttrs;
        public Dash dashAttrs;

        public CharacterSkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 0f;
            lightAttackAttrs.lifeTime = 0f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 0f;
            lightAttackAttrs.coolTime = 0f;
            lightAttackAttrs.waitTime = -1f;

            dashAttrs.name = "Dash";
            dashAttrs.distance = 0f;
            dashAttrs.speed = 0f;
            dashAttrs.coolTime = 2f;
            dashAttrs.waitTime = -1f;
            dashAttrs.moveWay = iTween.EaseType.linear;
        }
    }

    public class MagicGirlSkills : CharacterSkills
    {
        public MagicGirlSkills() : base()
        {

            lightAttackAttrs.power = 5.0f;
            lightAttackAttrs.lifeTime = 10.0f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 15f;
            lightAttackAttrs.coolTime = 0.7f;

            dashAttrs.distance = 5f;
            dashAttrs.speed = 25f;
            dashAttrs.moveWay = iTween.EaseType.easeInElastic;
        }
    }

    public class SnowBoySkills : CharacterSkills
    {
        public SnowBoySkills() : base()
        {
            lightAttackAttrs.power = 2.0f;

            dashAttrs.distance = 5f;
            dashAttrs.speed = 20f;
            dashAttrs.moveWay = iTween.EaseType.linear;
        }
    }
}
