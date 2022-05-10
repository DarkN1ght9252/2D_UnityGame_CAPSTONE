using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    #region Init
    public Item_Range_Weapon rangeWeapon;
    public Transform gunBarrel;
    public Transform crouchedGunBarrel;
    public Animator _anim;
    private Animator _weaponAnim;
    private float weaponCoolingDownCounter = 0;
    private bool coolingdown = false;
    bool crouched;
    #endregion
    
   void Start() {
        Weapon currentWeapon = PlayerEquipment.instance.getCurrentWeapon();
        if(currentWeapon.weapon_type == Weapon.Weapon_type.Range){
            rangeWeapon = (Item_Range_Weapon)currentWeapon;
        }else{
            rangeWeapon = null;
        }
        _anim = GetComponent<Animator>();
        _weaponAnim = GetComponentInChildren<Animator>();
        crouched = GetComponent<PlayerMovement>().getIsCrouched();
        gunBarrel.localPosition = new Vector3(rangeWeapon.nonCrouchedXZ[0],rangeWeapon.nonCrouchedXZ[1],rangeWeapon.nonCrouchedXZ[2]);
        crouchedGunBarrel.localPosition = new Vector3(rangeWeapon.crouchedXYZ[0],rangeWeapon.crouchedXYZ[1],rangeWeapon.crouchedXYZ[2]); 
    }
    // Update is called once per frame
    void Update()
    {
        if(rangeWeapon != null && Input.GetButtonDown("Fire1") && !coolingdown){
            fireWeapon();
        }else{
            weaponCoolingDownCounter -= Time.deltaTime;
            if (weaponCoolingDownCounter <= 0) {
                coolingdown = false;
            }
        }
    }

    private void fireWeapon(){
            Debug.Log("Fired Range Weapon" + rangeWeapon.name);

            GameObject bullet;
            
            if ( _anim.GetBool("crouched") ){
                Debug.Log("Crouch Fire");
                bullet  = Instantiate(rangeWeapon.bulletPreFab,crouchedGunBarrel.position , crouchedGunBarrel.rotation);
            }else{
                Debug.Log("Normal Fire");
                bullet = Instantiate(rangeWeapon.bulletPreFab,gunBarrel.position,gunBarrel.rotation);
            }
            bullet.GetComponent<Bullet>().SetTransform(transform);
            coolingdown = true;
            weaponCoolingDownCounter = rangeWeapon.weaponCoolDown;
    }

    public void updateWeapon(Weapon currentWeapon){
        if(currentWeapon.weapon_type == Weapon.Weapon_type.Range){
            rangeWeapon = (Item_Range_Weapon) currentWeapon;
        }else{
            rangeWeapon = null;
        }
    }
}