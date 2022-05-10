using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    #region Init
    public Item_Melee_Weapon meleeWeapon;
    public Transform normalAttackPoint;
    public Transform crouchedAttackPoint;
    private float weaponCoolingDownCounter = 0;
    private bool coolingdown = false;
    bool isCrouched;
    public Animator _anim;
    private Animator _weaponAnim;
    public LayerMask EnemyLayer;
    PlayerMovement trackCrouch;
    
    #endregion

    void Start() {
        Weapon currentWeapon = PlayerEquipment.instance.getCurrentWeapon();
        if(currentWeapon.weapon_type == Weapon.Weapon_type.Melee){
            meleeWeapon = (Item_Melee_Weapon)currentWeapon;
        }else{
            meleeWeapon = null;
        }
        _anim = GetComponent<Animator>();
        _weaponAnim = GetComponentInChildren<Animator>();
        trackCrouch = GetComponent<PlayerMovement>();
        if(meleeWeapon != null){
            normalAttackPoint.localPosition = new Vector3(meleeWeapon.nonCrouchedXZ[0],meleeWeapon.nonCrouchedXZ[1],meleeWeapon.nonCrouchedXZ[2]);
            crouchedAttackPoint.localPosition = new Vector3(meleeWeapon.crouchedXYZ[0],meleeWeapon.crouchedXYZ[1],meleeWeapon.crouchedXYZ[2]); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if plaer is crouching eveyr frame
        isCrouched = trackCrouch.getIsCrouched();

        if(meleeWeapon != null && Input.GetButtonDown("Fire1") && !coolingdown){
            Debug.Log("Melee attack swing");
            _weaponAnim.SetTrigger("MeleeAttack");
            MeleeAttack();
        }else{
            weaponCoolingDownCounter -= Time.deltaTime;
            if (weaponCoolingDownCounter <= 0) {
                coolingdown = false;
            }
        }
    }

    void MeleeAttack(){
        //Play attack animation
        _anim.SetTrigger("MeleeAttack");
        _weaponAnim.SetTrigger("MeleeAttack");
        //Create 2d circle collider
        Collider2D[] enemiesToHit;
        
        if(!isCrouched){
             Debug.Log("(Melee)Crouch attack " + meleeWeapon.damage);
            enemiesToHit = Physics2D.OverlapCircleAll(normalAttackPoint.position, meleeWeapon.Range, EnemyLayer);
        }else{
             Debug.Log("(Melee)Normal attack " + meleeWeapon.damage);
            enemiesToHit = Physics2D.OverlapCircleAll(crouchedAttackPoint.position, meleeWeapon.Range, EnemyLayer);
        }
        foreach(Collider2D enemy in enemiesToHit){
            Debug.Log("(Melee)Enemy Was Hit" + meleeWeapon.damage);
            enemy.gameObject.GetComponent<Hostile>().Damage((int)Random.Range(3,meleeWeapon.damage));
        }

        coolingdown = true;
        weaponCoolingDownCounter = meleeWeapon.weaponCoolDown;
    }

    private void OnDrawGizmosSelected() {
        if(meleeWeapon == null){
            return;
        }

        if(!isCrouched){
            Gizmos.DrawWireSphere(normalAttackPoint.position,meleeWeapon.Range);  
        }else{
            Gizmos.DrawWireSphere(crouchedAttackPoint.position,meleeWeapon.Range);  
        }
          
    }

    public void updateWeapon(Weapon currentWeapon){
        if(currentWeapon.weapon_type == Weapon.Weapon_type.Melee){
             meleeWeapon = (Item_Melee_Weapon) currentWeapon;
        }else{
            meleeWeapon = null;
        }

    }
}
