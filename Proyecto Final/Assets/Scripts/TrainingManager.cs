using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [Header("Referencia al Setup de la Escena")]
    [SerializeField] private MatchSetup setup;

    [Header("Opciones de Entrenamiento")]
    public bool vidaInfinita;
    public bool tiempoPausado;
    public bool p2SeDefiende;

    private void Start()
    {
        if (setup != null)
        {
            setup.ConfigurarPausaDelTiempo(tiempoPausado);
        }
    }

    private void Update()
    {
        ManejarVidaInfinita();
    }

    private void ManejarVidaInfinita()
    {
        if (!vidaInfinita || setup == null) return;

        if (setup.Jugador1 != null && setup.Jugador1.vidaActual < setup.Jugador1.ObtenerVidaMaxima())
        {
            setup.Jugador1.vidaActual = setup.Jugador1.ObtenerVidaMaxima();
            setup.Jugador1.CanalVidaAsignado?.RaiseEvent(1f);
        }

        if (setup.Jugador2 != null && setup.Jugador2.vidaActual < setup.Jugador2.ObtenerVidaMaxima())
        {
            setup.Jugador2.vidaActual = setup.Jugador2.ObtenerVidaMaxima();
            setup.Jugador2.CanalVidaAsignado?.RaiseEvent(1f);
        }
    }

    public void CambiarEstadoTiempo(bool pausar)
    {
        tiempoPausado = pausar;
        if (setup != null)
        {
            setup.ConfigurarPausaDelTiempo(tiempoPausado);
        }
    }
}
