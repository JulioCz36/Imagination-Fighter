using UnityEngine;

public class ManejadorDePelea : MonoBehaviour
{
    [Header("Datos de Selección")]
    [SerializeField] private PersonajeSeleccionadoSO datosP1;
    [SerializeField] private PersonajeSeleccionadoSO datosP2;

    [Header("Puntos de Aparición")]
    [SerializeField] private Transform puntoSpawnP1;
    [SerializeField] private Transform puntoSpawnP2;

    [Header("Configuración de la Partida")]
    [SerializeField] private bool esContraCPU = true;

    private void Start()
    {
        GenerarLuchadores();
    }

    private void GenerarLuchadores()
    {

        if (datosP1 != null && datosP1.prefabDelPersonaje != null)
        {
            GameObject p1GO = Instantiate(datosP1.prefabDelPersonaje, puntoSpawnP1.position, Quaternion.identity);

            //p1GO.AddComponent<PlayerInputHandler>();
        }

        if (datosP2 != null && datosP2.prefabDelPersonaje != null)
        {
            GameObject p2GO = Instantiate(datosP2.prefabDelPersonaje, puntoSpawnP2.position, Quaternion.Euler(0, 180, 0));

            if (esContraCPU)
            {
                //p2GO.AddComponent<SimpleEnemyAI>();
            }
            else
            {
                // p2GO.AddComponent<Player2InputHandler>(); 
            }
        }
    }
}
