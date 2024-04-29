using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    // •Ï”éŒ¾
    // -------------------------------------------------
        public Slider HpSlider;

        Character character;
    // ------------------------------------------------- 
    // ------------------------------------------------- 

    private void Start()
    {
        //Slider‚ğ–ƒ^ƒ“‚É‚·‚éB
        HpSlider.value = 1;

        character = this.gameObject.GetComponent<Character>();
    }

    private void Update()
    {
        _hpDown();
    }

    private void _hpDown()
    {
        //Å‘åHP‚É‚¨‚¯‚éŒ»İ‚ÌHP‚ğSlider‚É”½‰fB
        HpSlider.value = (float)character.status.GetHp() / (float)character.status.MaxHp;
    }

}
