using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [Header("Referencia al Setup de la Escena")]
    [SerializeField] private MatchSetup setup;

    [Header("Opciones de Entrenamiento")]
    public bool p2SeDefiende;
    public bool vidaInfinita;

    private System.Collections.IEnumerator Start() //funcionara?
    {
        
        yield return new WaitForEndOfFrame();

        Entity p1 = setup.Jugador1;
        Entity p2 = setup.Jugador2;

        AplicarOpcionesDeEntrenamiento(p1, p2);
    }

    private void Update()
    {
        if (vidaInfinita)
        {
            if (setup.Jugador1 != null) setup.Jugador1.vidaActual = setup.Jugador1.ObtenerVidaMaxima();
            if (setup.Jugador2 != null) setup.Jugador2.vidaActual = setup.Jugador2.ObtenerVidaMaxima();
        }
    }

    private void AplicarOpcionesDeEntrenamiento(Entity p1, Entity p2)
    {
        Debug.Log($"Configurando entrenamiento para: {p1?.ObtenerNombre()} vs {p2?.ObtenerNombre()}");
    }
}
