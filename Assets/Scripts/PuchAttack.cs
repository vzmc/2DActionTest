using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Punchの動作
public class PuchAttack : MonoBehaviour
{
    private Rigidbody2D parentRig2d;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    private static int hashAttacking = Animator.StringToHash("Toko_Punching");
    private static int hashAttackStart = Animator.StringToHash("Toko_PunchStart");
    private AnimatorStateInfo stateInfo;
    private CharacterAction actionScript;

    void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        boxCollider2d.enabled = false;
        animator = GetComponentInParent<Animator>();
        parentRig2d = GetComponentInParent<Rigidbody2D>();
        actionScript = GetComponentInParent<CharacterAction>();
    }

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == hashAttacking)
        {
            boxCollider2d.enabled = true;
        }
        else if (stateInfo.shortNameHash == hashAttackStart)
        {
            parentRig2d.MovePosition((Vector2)transform.parent.position + (parentRig2d.velocity + Vector2.right * 10 * (int)actionScript.GetDirection()) * Time.deltaTime);
        }
        else
        {
            boxCollider2d.enabled = false;
        }
    }
}
