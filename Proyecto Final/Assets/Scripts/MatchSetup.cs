using UnityEngine;

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

    private void Start()
    {
        IniciarCombate();
    }

    private void IniciarCombate()
    {
        // --- SPAWN JUGADOR 1 ---
        if (datosP1 != null && datosP1.prefabDelPersonaje != null)
        {
            GameObject p1GO = Instantiate(datosP1.prefabDelPersonaje, posicionSpawnP1.position, Quaternion.identity);
            p1GO.name = "Jugador_P1";
            Jugador1 = p1GO.GetComponent<Entity>();

            if (Jugador1 != null)
            {
                Jugador1.InicializarEntidad(canalVidaP1);

                if (controladorP1 != null)
                    controladorP1.InicializarCerebro(Jugador1);
            }
        }

        // --- SPAWN JUGADOR 2 ---
        if (datosP2 != null && datosP2.prefabDelPersonaje != null)
        {
            GameObject p2GO = Instantiate(datosP2.prefabDelPersonaje, posicionSpawnP2.position, Quaternion.Euler(0, 180, 0));
            p2GO.name = "Jugador_P2";
            Jugador2 = p2GO.GetComponent<Entity>();

            if (Jugador2 != null)
            {
                Jugador2.InicializarEntidad(canalVidaP2);
            }
        }

        if (controladorUI != null)
        {
            controladorUI.ConfigurarUI(Jugador1, Jugador2);
        }
    }
}
