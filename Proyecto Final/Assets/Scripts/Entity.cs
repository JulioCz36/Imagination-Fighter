using UnityEngine;

public class Entity : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Datos fijos del Personaje")]
    [SerializeField] private CharacterDataSO datosBase;

    [Header("Estadísticas en Tiempo Real")]
    public float vidaActual;

    private FloatEventChannel canalVidaAsignado;

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
    }

    public void InicializarEntidad(FloatEventChannel canalParaEstaEntidad)
    {
        canalVidaAsignado = canalParaEstaEntidad;

        if (datosBase != null)
        {
            vidaActual = datosBase.vidaMaxima;
        }
        else
        {
            vidaActual = 100f;
        }

        if (canalVidaAsignado != null)
            canalVidaAsignado.RaiseEvent(1f);
    }

    public void RecibirDanio(float cantidad)
    {
        float vidaMax = ObtenerVidaMaxima();

        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMax);

        float porcentaje = vidaActual / vidaMax;

        // LE MANDAMOS EL DATO DIRECTO AL CANAL ASIGNADO
        if (canalVidaAsignado != null)
        {
            canalVidaAsignado.RaiseEvent(porcentaje);
        }
    }

    public string ObtenerNombre() => datosBase != null ? datosBase.playerName : "Desconocido";
    public float ObtenerVidaMaxima() => datosBase != null ? datosBase.vidaMaxima : 100f;
    public void DarOrdenMovimiento(Vector2 direccion) => comandoMovimiento = direccion;

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

    public void VerificarCombosEspeciales()
    {
        if (Atacando || listaDeCombos == null) return;

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
