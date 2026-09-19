using UnityEngine;
using UnityEngine.InputSystem;

public class Player_1 : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        animator.SetTrigger("Attack");
    }
}
