using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteUpdater : MonoBehaviour
{
    Weapon currentWeapon;
    SpriteRenderer currentSprite;
    Animator weaponAnimation;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = PlayerEquipment.instance.currentWeapon;
        weaponAnimation = GetComponent<Animator>();
        currentSprite = GetComponent<SpriteRenderer>();
        if(currentWeapon != null){
            currentSprite.sprite = currentWeapon.item_Sprite_worldSpace;
        }
    }

    public void updateWeapon(Weapon currentWeapon){
        this.currentWeapon = currentWeapon;
        if(this.currentWeapon != null){
            currentSprite.sprite = currentWeapon.item_Sprite_worldSpace;
        }
    }
}
