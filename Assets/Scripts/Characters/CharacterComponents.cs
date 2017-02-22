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

        private Color fullHPColor = new Color(0f, 1f, 0f, 0.6f);
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

        Animator anim;

        public CharacterState(Animator anim)
        {
            this.anim = anim;
        }

        public bool isMove()
        {
            return anim.GetBool("run");
        }

        public bool isAttacking()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
                return true;

            return false;
        }
    }
}
