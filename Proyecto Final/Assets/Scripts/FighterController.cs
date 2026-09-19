using UnityEngine;

public class FighterController : MonoBehaviour
{
    private Animator animator;

    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;

        animator.SetTrigger("Attack");
    }

    public bool CanMove()
    {
        return !isAttacking;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
