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
        public float radius { get; set; }
        public float coolTime { get; set; }
        public float waitTime { get; set; }
    }
    public struct HardAttackAttrs
    {
        public string name { get; set; }
        public float power { get; set; }
        public float lifeTime { get; set; }
        public float force { get; set; }
        public float speed { get; set; }
        public float radius { get; set; }
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
        public HardAttackAttrs hardAttackAttrs;
        public Dash dashAttrs;

        public CharacterSkills()
        {
            lightAttackAttrs.name = "LightAttack";
            lightAttackAttrs.power = 0f;
            lightAttackAttrs.lifeTime = 0f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 0f;
            lightAttackAttrs.radius = 0f;
            lightAttackAttrs.coolTime = 0f;
            lightAttackAttrs.waitTime = -1f;

            dashAttrs.name = "Dash";
            dashAttrs.distance = 0f;
            dashAttrs.speed = 0f;
            dashAttrs.coolTime = 2f;
            dashAttrs.waitTime = -1f;
            dashAttrs.moveWay = iTween.EaseType.linear;

            hardAttackAttrs.name = "HardAttack";
            hardAttackAttrs.power = 0f;
            hardAttackAttrs.force = 0f;
            hardAttackAttrs.speed = 0f;
            hardAttackAttrs.coolTime = 0f;
            hardAttackAttrs.waitTime = -1f;

        }
    }

    public class MagicGirlSkills : CharacterSkills
    {
        public MagicGirlSkills() : base()
        {

            lightAttackAttrs.power = 5f;
            lightAttackAttrs.lifeTime = 10f;
            lightAttackAttrs.force = 0f;
            lightAttackAttrs.speed = 15f;
            lightAttackAttrs.radius = 0.5f;
            lightAttackAttrs.coolTime = 0.7f;

            dashAttrs.distance = 5f;
            dashAttrs.speed = 25f;
            dashAttrs.moveWay = iTween.EaseType.easeInElastic;

            hardAttackAttrs.power = 8f;
            hardAttackAttrs.lifeTime = 10f;
            hardAttackAttrs.force = 0;
            hardAttackAttrs.speed = 15f;
            lightAttackAttrs.radius = 0.5f;
            hardAttackAttrs.coolTime = 1.5f;
        }
    }

    public class SnowBoySkills : CharacterSkills
    {
        public SnowBoySkills() : base()
        {
            lightAttackAttrs.power = 3f;
            lightAttackAttrs.radius = 0.3f;

            dashAttrs.distance = 5f;
            dashAttrs.speed = 20f;
            dashAttrs.moveWay = iTween.EaseType.linear;

            hardAttackAttrs.power = 6f;
            hardAttackAttrs.radius = 0.4f;
            hardAttackAttrs.coolTime = 1f;
        }
    }
}
