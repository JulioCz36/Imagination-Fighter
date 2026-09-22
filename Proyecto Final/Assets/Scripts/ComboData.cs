using UnityEngine;

[CreateAssetMenu(fileName = "NewComboData", menuName = "Sistema de Combate/Datos de Combo")]
public class ComboData : ScriptableObject
{
    [Header("Identificadores")]
    public string nombreDelAtaque;

    [Header("Secuencias de Botones")]
    public string secuenciaRequerida;

    [Header("Configuración del Animator")]
    public string parametroBool;

    [Tooltip("El número de animación")]
    public float numeroDeAnimacion;
}
