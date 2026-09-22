using UnityEngine;

public class Entity : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Canal de Eventos")]
    [SerializeField] private FloatEventChannel canalVida;

    [Header("Estadísticas")]
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Combos de este Personaje")]
    public ComboData[] listaDeCombos;

    [Header("Variables de Control")]
    public string Combo;
    public float Cronometro;
    public float Tiempo = 0.5f;
    public bool Atacando;

    [SerializeField] private float moveSpeed = 5f;
    private Vector2 comandoMovimiento;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vidaActual = vidaMaxima;
    }

    private void Start()
    {
        if (canalVida != null) canalVida.RaiseEvent(vidaActual / vidaMaxima);
    }

    public void DarOrdenMovimiento(Vector2 direccion)
    {
        comandoMovimiento = direccion;
    }

    public void EjecutarAtaque()
    {
        Combo += "H";
        Cronometro = Tiempo;

        if (!Atacando)
        {
            Atacando = true;
            animator.SetBool("attack", true);
            animator.SetFloat("basicosN", 0);
            Combo = "";
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (canalVida != null) canalVida.RaiseEvent(vidaActual / vidaMaxima);
    }

    public void VerificarCombosEspeciales()
    {
        if (Atacando || listaDeCombos == null) return;

        // Recorremos la lista
        foreach (ComboData comboItem in listaDeCombos)
        {
            if (Combo.Contains(comboItem.secuenciaRequerida))
            {
                animator.SetBool("attack", false);
                animator.SetBool("combosAttack", false);

                animator.SetBool(comboItem.parametroBool, true);

                if (comboItem.parametroBool == "combos") animator.SetFloat("specialesN", comboItem.numeroDeAnimacion);
                if (comboItem.parametroBool == "combosAttack") animator.SetFloat("basicosCombosN", comboItem.numeroDeAnimacion);

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
        rb.linearVelocity = new Vector2(comandoMovimiento.x * moveSpeed, rb.linearVelocity.y);
    }
}

