using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public string Combo;
    public float Cronometro;
    public float Tiempo = 0.5f;
    public bool Atacando;
    public Animator animator;

    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private Vector2 lastMoveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Combo += "H";
        Cronometro = Tiempo;

        if (!Atacando)
        {
            Atacando = true;
            animator.SetBool("attack", true);
            animator.SetFloat("basicosN", 0);
        }
    }

    public void Sistema_Combos()
    {
        if (Atacando) return;

        if (moveInput.y > 0.5f && lastMoveInput.y <= 0.5f)
        {
            Combo += "Arriba";
            Cronometro = Tiempo;
        }
        if (moveInput.y < -0.5f && lastMoveInput.y >= -0.5f)
        {
            Combo += "Abajo";
            Cronometro = Tiempo;
        }
        if (moveInput.x < -0.5f && lastMoveInput.x >= -0.5f)
        {
            Combo += "Izquierda";
            Cronometro = Tiempo;
        }
        if (moveInput.x > 0.5f && lastMoveInput.x <= 0.5f)
        {
            Combo += "Derecha";
            Cronometro = Tiempo;
        }
        lastMoveInput = moveInput;
    }

    public void AtaquesBasicos()
    {
        if (!Atacando)
        {
            switch (Combo)
            {
                case string a when a.Contains("HH"):
                    animator.SetBool("attack", false);
                    animator.SetBool("combosAttack", true);
                    animator.SetBool("combos", false);
                    animator.SetFloat("basicosCombosN", 0);
                    Combo = "";
                    Cronometro = 0;
                    Atacando = true;
                    break;
            }
        }
    }

    public void AtaquesEspeciales()
    {

        if (!Atacando)
        {
            switch (Combo)
            {
                case string a when a.Contains("ArribaDerechaH"):
                    animator.SetBool("attack", false);
                    animator.SetBool("combosAttack", false);
                    animator.SetBool("combos", true);
                    animator.SetFloat("specialesN", 0);
                    Combo = "";
                    Cronometro = 0;
                    Atacando = true;
                    break;
            }
        }
    }

    public void FinalAttack()
    {
        animator.SetBool("combos", false);
        animator.SetBool("attack", false);
        animator.SetBool("combosAttack", false);
        Atacando = false;
    }

    private void FixedUpdate()
    {
        if (Atacando)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void Update()
    {
        Sistema_Combos();

        if (Cronometro <= 0)
        {
            Combo = "";
            Cronometro = 0;
        }
        else
        {
            Cronometro -= 1 * Time.deltaTime;
        }

        AtaquesBasicos();
        AtaquesEspeciales();
    }

}
