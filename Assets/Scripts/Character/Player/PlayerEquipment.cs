using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    #region Singleton

	public static PlayerEquipment instance;

	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Equipment found!");
			return;
		}

		instance = this;
	}

	#endregion
    public Weapon currentWeapon;
    private RangeWeapon rangeattack;
    private MeleeWeapon meleeAttack;
    private WeaponSpriteUpdater weaponAnimator;

    private 
    // Start is called before the first frame update
    void Start()
    {
        rangeattack = GetComponent<RangeWeapon>();
        meleeAttack = GetComponent<MeleeWeapon>();
        weaponAnimator = GetComponentInChildren<WeaponSpriteUpdater>();
    }

    public void ChangeWeapon(Item newItem){
        if(currentWeapon){
            Inventory.instance.Remove(newItem);
            Inventory.instance.Add((Item)currentWeapon);
        }
        currentWeapon = (Weapon)newItem;
        //Need to update Attack Scripts and weapon animator
        rangeattack.updateWeapon(currentWeapon);
        meleeAttack.updateWeapon(currentWeapon);
        weaponAnimator.updateWeapon(currentWeapon);
    }
    public Weapon getCurrentWeapon(){
        return currentWeapon;
    }
}
