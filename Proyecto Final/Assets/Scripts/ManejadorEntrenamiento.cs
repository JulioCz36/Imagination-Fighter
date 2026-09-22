using UnityEngine;

public class ManejadorEntrenamiento : MonoBehaviour
{
    [Header("Controladores de la Escena")]
    [SerializeField] private PlayerInputHandler controladorP1;

    [Header("Puntos de Aparición (Spawn)")]
    [SerializeField] private Transform posicionSpawnP1;
    [SerializeField] private Transform posicionSpawnP2;

    [Header("Datos de los Personajes")]
    [Tooltip("archivo Seleccion_P1")]
    [SerializeField] private PersonajeSeleccionadoSO datosP1;
    [Tooltip("archivo Seleccion_P2")]
    [SerializeField] private PersonajeSeleccionadoSO datosP2;

    private Entity entidadP1;
    private Entity entidadP2;

    private void Start()
    {
        GenerarEntrenamiento();
    }

    private void GenerarEntrenamiento()
    {
        // P1
        if (datosP1 != null && datosP1.prefabDelPersonaje != null)
        {
            GameObject p1GO = Instantiate(datosP1.prefabDelPersonaje, posicionSpawnP1.position, Quaternion.identity);
            p1GO.name = "Jugador_P1";
            entidadP1 = p1GO.GetComponent<Entity>();

            if (controladorP1 != null && entidadP1 != null)
            {
                controladorP1.InicializarCerebro(entidadP1);
            }
            else
            {
                Debug.LogError("Error");
            }
        }
        else
        {
            Debug.LogWarning("Falta asignar el Prefab del P1");
        }

        // P2
        if (datosP2 != null && datosP2.prefabDelPersonaje != null)
        {
            GameObject p2GO = Instantiate(datosP2.prefabDelPersonaje, posicionSpawnP2.position, Quaternion.Euler(0, 180, 0));
            p2GO.name = "Jugador_P2";
            entidadP2 = p2GO.GetComponent<Entity>();

            // por ahora no hace nada xd
        }
        else
        {
            Debug.LogWarning("Falta asignar el Prefab del P2");
        }
    }
}
