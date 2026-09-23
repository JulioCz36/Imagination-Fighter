using UnityEngine;

public class ManejadorEntrenamiento : MonoBehaviour
{
    [Header("Controladores de la Escena")]
    [SerializeField] private PlayerInputHandler controladorP1;
    [SerializeField] private UIController controladorUI;

    [Header("Canales de Vida para Inyectar")]
    [SerializeField] private FloatEventChannel canalVidaP1;
    [SerializeField] private FloatEventChannel canalVidaP2;

    [Header("Puntos de Aparición (Spawn)")]
    [SerializeField] private Transform posicionSpawnP1;
    [SerializeField] private Transform posicionSpawnP2;

    [Header("Datos de los Personajes")]
    [SerializeField] private PersonajeSeleccionadoSO datosP1;
    [SerializeField] private PersonajeSeleccionadoSO datosP2;

    private void Start()
    {
        GenerarEntrenamiento();
    }

    private void GenerarEntrenamiento()
    {
        Entity entidadP1 = null;
        Entity entidadP2 = null;

        // P1
        if (datosP1 != null && datosP1.prefabDelPersonaje != null)
        {
            GameObject p1GO = Instantiate(datosP1.prefabDelPersonaje, posicionSpawnP1.position, Quaternion.identity);
            p1GO.name = "Jugador_P1";
            entidadP1 = p1GO.GetComponent<Entity>();

            if (entidadP1 != null)
            {
              
                entidadP1.InicializarEntidad(canalVidaP1);

                if (controladorP1 != null)
                {
                    controladorP1.InicializarCerebro(entidadP1);
                }
            }
        }

        // P2
        if (datosP2 != null && datosP2.prefabDelPersonaje != null)
        {
            GameObject p2GO = Instantiate(datosP2.prefabDelPersonaje, posicionSpawnP2.position, Quaternion.Euler(0, 180, 0));
            p2GO.name = "Jugador_P2";
            entidadP2 = p2GO.GetComponent<Entity>();

            if (entidadP2 != null)
            {
         
                entidadP2.InicializarEntidad(canalVidaP2);
            }
        }

        // LE PASAMOS LOS NOMBRES DEL PREFAB DIRECTO A LA UI
        if (controladorUI != null)
        {
            string nomP1 = (entidadP1 != null) ? entidadP1.ObtenerNombre() : "P1";
            string nomP2 = (entidadP2 != null) ? entidadP2.ObtenerNombre() : "P2";
            controladorUI.EstablecerNombresEnPantalla(nomP1, nomP2);
        }
    }
}
