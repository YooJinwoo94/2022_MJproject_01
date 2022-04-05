using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneNormalAttack : MonoBehaviour
{
    [SerializeField]
    CharState charState;
    [SerializeField]
    BoxCollider2D weaponCollider;


    [SerializeField]
    Transform attackPos;
    [SerializeField]
    Vector2 size;
   // public LayerMask whatIsLayer;



    void Update()
    {
        if (charState.nowState == CharState.NowState.isAttack)
        {
            Collider2D[] hit = Physics2D.OverlapBoxAll(attackPos.position, size, 0);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, size);
    }
}
