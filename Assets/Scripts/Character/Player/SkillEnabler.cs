using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        bool enabledDoubleJump = true;
        other.GetComponent<PlayerMovement>().SetDoubleJumpAbility(enabledDoubleJump);
        Destroy(this.gameObject);
    }
}
