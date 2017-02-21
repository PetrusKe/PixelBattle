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
        Animator anim;
        Rigidbody playerRigidbody;

        // Character components
        CharacterAttrs characterAttrs;
        CharacterHP characterHP;
        CharacterState characterState;

        public string characterName;

        public void Start()
        {
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();

            // Init character components
            string className = "GameCharacters." + characterName + "Attrs";
            Console.Write(className);
            Assembly assembly = Assembly.GetExecutingAssembly();
            characterAttrs = (CharacterAttrs)assembly.CreateInstance(className);

            // set HP. need a check
            Slider HPSlider = transform.Find("Canvas/HealthSlider").GetComponent<Slider>();
            Image fillImage = transform.Find("Canvas/HealthSlider/Fill Area/Fill").GetComponent<Image>();
            characterHP = new CharacterHP(characterAttrs.maxHP, HPSlider, fillImage);
            characterHP.OnEnable();

        }

        public void Walk(float h, float v)
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

        public void LightAttack()
        {
            
        }

        public void HardAttack()
        {

        }


        public void Death()
        {
            
        }

        public void ChangeHP(float amount=0)
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
    }

}
