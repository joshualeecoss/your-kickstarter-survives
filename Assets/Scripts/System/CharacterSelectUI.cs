using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    Animator animator;
    Sprite characterSprite;

    public Image portrait;
    public Image weaponImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI weaponName;


    public void UpdateStats()
    {
        portrait.enabled = true;
        weaponImage.enabled = true;

        animator = portrait.GetComponent<Animator>();
        characterSprite = portrait.GetComponent<Sprite>();
        characterSprite = characterData.CharacterSprite;
        portrait.sprite = characterSprite;
        animator.runtimeAnimatorController = characterData.Animator;
        animator.SetBool("on_ui", true);

        characterName.SetText(characterData.CharacterName);
        description.SetText(characterData.Description);

        weaponName.SetText("Starting Weapon: " + characterData.StartingWeapon.GetComponent<WeaponController>().weaponData.Name);
        weaponImage.sprite = characterData.StartingWeapon.GetComponent<WeaponController>().weaponData.Icon;
    }

}
