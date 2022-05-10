using UnityEngine;
using System.Collections;

public interface ICollider
{
    void OnCollisionEnter2D(Collision2D other);
}
