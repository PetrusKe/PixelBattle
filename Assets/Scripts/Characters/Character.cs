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
        protected Animator animator;
        protected Rigidbody playerRigidbody;

        // Character components
        protected CharacterAttrs characterAttrs;
        protected CharacterHP characterHP;
        protected CharacterSkills characterSkills;
        protected CharacterState state;
        protected CharacterAnimation anim;
        protected CharacterExtendObj extendObj;
        protected Transform[] characterWeaponsCenter;
        
        public string characterName;

        public void Start()
        {
            animator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
            extendObj = GetComponent<CharacterExtendObj>();

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

            state = new CharacterState();
            anim = new CharacterAnimation();

            // get weapon Collider
            GetWeaponsCenter();
        }

        protected virtual void GetWeaponsCenter() { }

        public void Move(float h, float v)
        {
            if ((h != 0 || v != 0) && characterAttrs.walkSpeed > 0f)
            {
                ChangeAnim(anim.run, true);
                Vector3 movement = new Vector3(h, 0f, v);
                movement = movement.normalized * characterAttrs.walkSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + movement);
                Turn(h, v);
                return;
            }
            ChangeAnim(anim.run, false);
        }

        public void Turn(float h, float v)
        {
            Vector3 rotation = new Vector3(h, 0f, v);
            if (rotation.magnitude > 0.1f)
                playerRigidbody.MoveRotation(Quaternion.LookRotation(-rotation));
        }

        public virtual void LightAttack()
        {
            if (!state.IsGround)
                return;
            if (state.IsLightAttack < 0 || state.IsLightAttack >= 3)  // feture will support 3 combo
                state.IsLightAttack = 0;

            state.IsLightAttack++;

        }

        public void TakeDamage(float damage)
        {
            if (characterHP.Change(-damage) == 0)
            {
                // player Dad.
            }
            else
            {
                // player hurt
            }
        }

        public void Death()
        {

        }

        public float ChangeHP(float amount = 0)
        {
            return characterHP.Change(amount);
        }

        public void ChangeAnim(string paramName, bool value)
        {
            animator.SetBool(paramName, value);
        }

        public void ChangeAnim(string paramName, int value)
        {
            animator.SetInteger(paramName, value);
        }

        public void ChangeAnim(string paramName, float value)
        {
            animator.SetFloat(paramName, value);
        }
        public void ChangeAnim(string paramName)
        {
            animator.SetTrigger(paramName);
        }

    }

}
