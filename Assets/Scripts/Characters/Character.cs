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
        protected CharacterAttrs attrs;
        protected CharacterHP characterHP;
        protected CharacterSkills skills;
        public CharacterState state;
        protected CharacterAnimation anim;
        protected CharacterExtendObj extendObj;
        protected Transform[] weaponCenter;
        public string characterName;

        private float distToGround;

        public void Start()
        {
            animator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
            extendObj = GetComponent<CharacterExtendObj>();
            // Init character components
            Assembly assembly = Assembly.GetExecutingAssembly();
            string className = "GameCharacters." + characterName + "Attrs";
            attrs = (CharacterAttrs)assembly.CreateInstance(className);

            className = "GameCharacters." + characterName + "Skills";
            skills = (CharacterSkills)assembly.CreateInstance(className);

            // set HP. need a check
            Slider HPSlider = transform.Find("Canvas/HealthSlider").GetComponent<Slider>();
            Image fillImage = transform.Find("Canvas/HealthSlider/Fill Area/Fill").GetComponent<Image>();
            characterHP = new CharacterHP(attrs.maxHP, HPSlider, fillImage);
            characterHP.OnEnable();

            state = new CharacterState();
            anim = new CharacterAnimation();

            // get weapon Collider
            GetWeaponsCenter();

            distToGround = GetComponent<Collider>().bounds.extents.y;
        }

        protected virtual void GetWeaponsCenter() { }

        public virtual void CheckCoolTime()
        {
            if (skills.dashAttrs.waitTime < skills.dashAttrs.coolTime
                && skills.dashAttrs.waitTime >= 0)
                skills.dashAttrs.waitTime += Time.deltaTime;
            else if (skills.dashAttrs.waitTime >= skills.dashAttrs.coolTime)
                skills.dashAttrs.waitTime = -1;
        }

        public void Move(float h, float v)
        {
            if (!state.IsDash)
            {
                if ((h != 0 || v != 0) && attrs.runSpeed > 0f)
                {
                    Vector3 movement = new Vector3(h, 0f, v);
                    movement = movement.normalized * attrs.runSpeed * Time.deltaTime;
                    state.IsRun = true;
                    if (state.IsJump)
                    {
                        state.IsRun = false;
                        movement *= 0.7f;
                    }
                    else if(state.IsAttack)
                    {
                        movement *= 0.5f;
                    }

                    ChangeAnim(anim.run, state.IsRun);
                    playerRigidbody.MovePosition(transform.position + movement);
                    Turn(h, v);
                    return;
                }
            }

            state.IsRun = false;
            ChangeAnim(anim.run, state.IsRun);
        }

        public void Turn(float h, float v)
        {
            Vector3 rotation = new Vector3(h, 0f, v);
            if (rotation.magnitude > 0.1f)
                playerRigidbody.MoveRotation(Quaternion.LookRotation(-rotation));
        }

        public void CheckGround()
        {
            state.IsJump = !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f, 1 << LayerMask.NameToLayer("Ground"));
            //ChangeAnim(anim.jump, state.IsJump);
            //state.IsGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        }


        public void Jump()
        {
            if (state.IsJump || state.IsDash)
                return;
            state.IsRun = false;
            playerRigidbody.velocity = new Vector3(0f, attrs.jumpSpeed, 0f);
            //Vector3 height = transform.TransformDirection(Vector3.up).normalized * 500f;
            //playerRigidbody.AddForce(height);
            //Hashtable args = new Hashtable();
            //Vector3 height = transform.TransformDirection(Vector3.up).normalized * 1.3f;
            //args.Add("speed", 5f);
            //args.Add("easeype", iTween.EaseType.easeOutQuad);
            //args.Add("position", transform.position + height);
            //iTween.MoveTo(gameObject, args);

        }

        public virtual void LightAttack()
        {
            if (state.IsJump || state.IsDash || state.IsLightAttack)
                return;
        }

        public virtual void HardAttack()
        {
            if (state.IsJump || state.IsDash || state.IsHardAttack)
                return;
        }


        public virtual void Dash()
        {
            if (state.IsJump || !state.IsRun || state.IsAttack)
                return;

            if (skills.dashAttrs.waitTime >= 0)
                return;

            state.IsDash = true;
            //dash here
            Vector3 movement = -transform.TransformDirection(Vector3.forward).normalized;
            movement = movement * skills.dashAttrs.distance;
            Hashtable args = new Hashtable();
            args.Add("speed", skills.dashAttrs.speed);
            args.Add("easetype", skills.dashAttrs.moveWay);
            args.Add("onstart", "ChangeDashAnim");
            args.Add("onstartparams", true);
            args.Add("oncomplete", "ChangeDashAnim");
            args.Add("oncompleteparams", false);
            args.Add("position", transform.position + movement);

            iTween.MoveTo(gameObject, args);
            skills.dashAttrs.waitTime = 0;
            state.IsDash = false;
        }

        public void TakeDamage(float damage)
        {
            if (characterHP.Change(-damage) == 0)
            {
                // player daed
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

        public void ChangeAnim(string paramName, bool value) { animator.SetBool(paramName, value); }

        public void ChangeAnim(string paramName, int value) { animator.SetInteger(paramName, value); }

        public void ChangeAnim(string paramName, float value) { animator.SetFloat(paramName, value); }

        public void ChangeAnim(string paramName) { animator.SetTrigger(paramName); }

        // for special anim change happen in dish action
        public void ChangeDashAnim(bool v) { animator.SetBool(anim.dash, v); }

        public void AnimEnd()
        {
            int nameHash = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

            if (nameHash == anim.lightAttackAnim)
                state.IsLightAttack = false;
            else if (nameHash == anim.hardAttackAnim)
                state.IsHardAttack = false;
        }
    }

}
