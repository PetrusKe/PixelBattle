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

        private Color fullHPColor = new Color(0f, 1f, 0f, 0.8f);
        private Color zeroHPColor = new Color(1f, 0f, 0f, 0.6f);

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
        private bool isRun;
        private bool isLightAttack;
        private bool isGround;

        public CharacterState()
        {
            isRun = false;
            isLightAttack = false;
            isGround = true;
        }



        public bool IsRun
        {
            get
            {
                return isRun;
            }

            set
            {
                if (isRun == value)
                    return;
                isRun = value;
            }
        }

        public bool IsLightAttack
        {
            get
            {
                return isLightAttack;
            }

            set
            {
                if (isLightAttack == value)
                    return;
                isLightAttack = value;
            }
        }

        public bool IsGround
        {
            get
            {
                return isGround;
            }

            set
            {
                if (isGround == value)
                    return;
                isGround = value;
            }
        }
    }

    public class CharacterAnimation
    {
        public int idleAnim { get; private set; }
        public int runAnim { get; private set; }
        public int lightAttackAnim { get; private set; }

        public string lightAttack { get; private set; }
        public string run { get; private set; }

        public CharacterAnimation()
        {
            idleAnim = Animator.StringToHash("Base Layer.Idle");
            runAnim = Animator.StringToHash("Base Layer.Run");
            lightAttackAnim = Animator.StringToHash("Base Layer.LightAttack");

            lightAttack = "lightAttack";
            run = "run";
        }
    }
}
