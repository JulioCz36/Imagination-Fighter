using UnityEngine;
using UnityEngine.InputSystem;

public class MatchSetup : MonoBehaviour
{
    [Header("Controladores del Sistema")]
    [SerializeField] private PlayerInputHandler controladorP1;
    [SerializeField] private UIController controladorUI;

    [Header("Canales de Vida para Inyectar")]
    [SerializeField] private FloatEventChannel canalVidaP1;
    [SerializeField] private FloatEventChannel canalVidaP2;

    [Header("Puntos de Aparición (Spawn)")]
    [SerializeField] private Transform posicionSpawnP1;
    [SerializeField] private Transform posicionSpawnP2;

    [Header("Datos de los Personajes Seleccionados")]
    [SerializeField] private PersonajeSeleccionadoSO datosP1;
    [SerializeField] private PersonajeSeleccionadoSO datosP2;

    public Entity Jugador1 { get; private set; }
    public Entity Jugador2 { get; private set; }

    [Header("Lógica del Reloj de la Partida")]
    [SerializeField] private float tiempoMaximoPartida = 99f;
    private float tiempoRestante;
    private bool tiempoPausadoInternamente = false;
    private bool partidaTerminada = false;

    [Header("Ajustes de Input de Pausa")]
    [SerializeField] private float tiempoRequeridoSubMenu = 1.5f;
    private bool juegoPausadoPorBoton = false;
    private bool subMenuAbierto = false;

    private void Start()
    {
        tiempoRestante = tiempoMaximoPartida;
        IniciarCombate();
    }

    private void Update()
    {
        if (partidaTerminada) return;

        ManejarReloj();
        VerificarMuertesPorVida();
    }

    private void IniciarCombate()
    {
      
        if (datosP1 != null && datosP1.prefabDelPersonaje != null)
        {
            GameObject p1GO = Instantiate(datosP1.prefabDelPersonaje, posicionSpawnP1.position, Quaternion.identity);
            p1GO.name = "Jugador_P1";
            Jugador1 = p1GO.GetComponent<Entity>();

            if (Jugador1 != null)
            {
                Jugador1.InicializarEntidad(canalVidaP1);
                if (controladorP1 != null) controladorP1.InicializarCerebro(Jugador1);
            }
        }

        if (datosP2 != null && datosP2.prefabDelPersonaje != null)
        {
            GameObject p2GO = Instantiate(datosP2.prefabDelPersonaje, posicionSpawnP2.position, Quaternion.Euler(0, 180, 0));
            p2GO.name = "Jugador_P2";
            Jugador2 = p2GO.GetComponent<Entity>();

            if (Jugador2 != null) Jugador2.InicializarEntidad(canalVidaP2);
        }

        if (controladorUI != null) controladorUI.ConfigurarUI(Jugador1, Jugador2);
    }

    private void ManejarReloj()
    {
       
        if (tiempoPausadoInternamente || juegoPausadoPorBoton) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (controladorUI != null)
                controladorUI.ActualizarTextoReloj(Mathf.CeilToInt(tiempoRestante));

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinalizarPartida("TIEMPO EXPIRED");
            }
        }
    }

    private void VerificarMuertesPorVida()
    {
        if (Jugador1 != null && Jugador1.vidaActual <= 0)
        {
            FinalizarPartida("P2_GANADOR");
        }
        else if (Jugador2 != null && Jugador2.vidaActual <= 0)
        {
            FinalizarPartida("P1_GANADOR");
        }
    }

    public void AlternarPausaJuego()
    {
        juegoPausadoPorBoton = !juegoPausadoPorBoton;

        Time.timeScale = juegoPausadoPorBoton ? 0f : 1f;

        if (controladorUI != null)
        {
            controladorUI.MostrarMenuPausa(juegoPausadoPorBoton);
        }

 
        if (!juegoPausadoPorBoton)
        {
            subMenuAbierto = false;
        }
    }

    public void ConfigurarPausaDelTiempo(bool pausar) => tiempoPausadoInternamente = pausar;

    public void SetPausaJuego(bool pausar)
    {
        juegoPausadoPorBoton = pausar;
        Time.timeScale = pausar ? 0f : 1f;

        if (controladorUI != null)
        {
            controladorUI.MostrarMenuPausa(pausar);
        }

        if (!pausar)
        {
            subMenuAbierto = false;
        }
    }

    private void FinalizarPartida(string motivo)
    {
        partidaTerminada = true;
        Time.timeScale = 0f;

        Debug.Log($"--- FIN DE LA PARTIDA --- Motivo: {motivo}");

        if (motivo == "TIEMPO EXPIRED" && Jugador1 != null && Jugador2 != null)
        {
            if (Jugador1.vidaActual > Jugador2.vidaActual) Debug.Log($"Ganador por tiempo: {Jugador1.ObtenerNombre()}");
            else if (Jugador2.vidaActual > Jugador1.vidaActual) Debug.Log($"Ganador por tiempo: {Jugador2.ObtenerNombre()}");
            else Debug.Log("¡Empate por tiempo!");
        }
        else
        {
            Debug.Log($"Fin del match: {motivo}");
        }
    }
}
