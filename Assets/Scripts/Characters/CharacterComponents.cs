using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCharacters
{
    public class CharacterHP
    {
        public float maxHP { get; set; }

        private float minHP = 0;
        public float currentHP { get; private set; }

        private Slider HPSlider;
        private Image fillImage;

        private Color fullHPColor = new Color(0f, 1f, 0f, 1f);
        private Color zeroHPColor = new Color(1f, 0f, 0f, 1f);

        public CharacterHP(float maxHP, Slider HPSlider, Image fillImage)
        {
            this.maxHP = maxHP;
            this.HPSlider = HPSlider;
            this.fillImage = fillImage;
        }

        public void OnEnable()
        {
            currentHP = maxHP;
            SetHealthUI();
        }

        public float Change(float amount)
        {
            // Adjust the current HP
            currentHP += amount;

            if (currentHP > maxHP)
                currentHP = maxHP;
            else if (currentHP < minHP)
                currentHP = minHP;

            SetHealthUI();
            return currentHP;
        }

        private void SetHealthUI()
        {
            HPSlider.value = (currentHP / maxHP) * 100;
            fillImage.color = Color.Lerp(zeroHPColor, fullHPColor, currentHP / maxHP);
        }
    }

    public class CharacterState
    {
        private bool isJump;
        private bool isRun;
        private bool isAttack;

        private bool isLightAttack;
        private bool isHardAttack;
        private bool isDash;

        public CharacterState()
        {
            isJump = false;
            isRun = false;
            isAttack = false;

            isLightAttack = false;
            isHardAttack = false;
            isDash = false;

        }

        public bool IsJump
        {
            get { return isJump; }
            set
            {
                if (isJump != value)
                    isJump = value;
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            set
            {
                if (isRun != value)
                    isRun = value;
            }
        }

        public bool IsAttack
        {
            get
            {
                return isLightAttack || isHardAttack;           // need add each time add attack
            }
        }

        public bool IsLightAttack
        {
            get { return isLightAttack; }
            set
            {
                if (isLightAttack != value)
                    isLightAttack = value;
            }
        }

        public bool IsHardAttack
        {
            get { return isHardAttack; }
            set
            {
                if (isHardAttack != value)
                    isHardAttack = value;
            }
        }

        public bool IsDash
        {
            get { return isDash; }

            set
            {
                if(isDash != value)
                    isDash = value;
            }
        }
    }

    public class CharacterAnimation
    {
        public int idleAnim { get; private set; }
        public int runAnim { get; private set; }
        public int jumpAnim { get; private set; }
        public int dashAnim { get; private set; }
        public int lightAttackAnim { get; private set; }
        public int hardAttackAnim { get; private set; }

        public string run { get; private set; }
        public string jump { get; private set; }
        public string dash { get; private set; }
        public string lightAttack { get; private set; }
        public string hardAttack { get; private set; }

        public CharacterAnimation()
        {
            idleAnim = Animator.StringToHash("Base Layer.Idle");
            runAnim = Animator.StringToHash("Base Layer.Run");
            jumpAnim = Animator.StringToHash("Base Layer.Jump");
            dashAnim = Animator.StringToHash("Base Layer.Dash");
            lightAttackAnim = Animator.StringToHash("Base Layer.LightAttack");
            hardAttackAnim = Animator.StringToHash("Base Layer.HardAttack");

            run = "run";
            jump = "jump";
            dash = "dash";
            lightAttack = "lightAttack";
            hardAttack = "hardAttack";
        }
    }
}
