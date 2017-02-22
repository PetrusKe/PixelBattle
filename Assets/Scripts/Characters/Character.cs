using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using GameConfig;
using GameUtils;
using UnityEngine.UI;

namespace GameCharacters
{
    public class Character : MonoBehaviour
    {
        protected Animator anim;
        protected Rigidbody playerRigidbody;

        // Character components
        protected CharacterAttrs characterAttrs;
        protected CharacterHP characterHP;
        protected CharacterSkills characterSkills;
        protected CharacterState characterState;

        protected Transform[] characterWeaponsCenter;

        

        public string characterName;

        public void Start()
        {
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();

            // Init character components
            Assembly assembly = Assembly.GetExecutingAssembly();
            string className = "GameCharacters." + characterName + "Attrs";
            characterAttrs = (CharacterAttrs)assembly.CreateInstance(className);

            className = "GameCharacters." + characterName + "Skills";
            characterSkills = (CharacterSkills)assembly.CreateInstance(className);

            // set HP. need a check
            Slider HPSlider = transform.Find("Canvas/HealthSlider").GetComponent<Slider>();
            Image fillImage = transform.Find("Canvas/HealthSlider/Fill Area/Fill").GetComponent<Image>();
            characterHP = new CharacterHP(characterAttrs.maxHP, HPSlider, fillImage);
            characterHP.OnEnable();

            characterState = new CharacterState(anim);

            // get weapon Collider
            GetWeaponsCenter();
        }

        protected virtual void GetWeaponsCenter() { }

        public void Move(float h, float v)
        {
            if (characterAttrs.walkSpeed > 0f)
            {
                Vector3 movement = new Vector3(h, 0f, v);
                movement = movement.normalized * characterAttrs.walkSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + movement);
            }
        }

        public void Turn(float h, float v)
        {
            Vector3 rotation = new Vector3(h, 0f, v);
            if (rotation.magnitude > 0.1f)
                playerRigidbody.MoveRotation(Quaternion.LookRotation(-rotation));
        }

        public virtual void LightAttack()
        {
            if (characterState.IsAttacking())
                return;
        }

        public void TakeDamage(float damage)
        {
            if (characterHP.Change(-damage) == 0)
            {
                // player dead.
            } else
            {
                // player hurt
            }
        }

        public void Death()
        {

        }

        public void ChangeHP(float amount = 0)
        {
            characterHP.Change(amount);
        }

        public void ChangeAnim(string paramName, bool value)
        {
            anim.SetBool(paramName, value);
        }

        public void ChangeAnim(string paramName, int value)
        {
            anim.SetInteger(paramName, value);
        }

        public void ChangeAnim(string paramName, float value)
        {
            anim.SetFloat(paramName, value);
        }
        public void ChangeAnim(string paramName)
        {
            anim.SetTrigger(paramName);
        }

        public void AttackIsStart()
        {
            
        }

        public void AttackIsEnd()
        {

        }
    }

}
