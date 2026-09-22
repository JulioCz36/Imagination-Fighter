using UnityEngine;

[CreateAssetMenu(fileName = "NewComboData", menuName = "Sistema de Combate/Datos de Combo")]
public class ComboData : ScriptableObject
{
    [Header("Identificadores")]
    public string nombreDelAtaque;

    [Header("Secuencias de Botones")]
    [Tooltip("El string exacto que debe detectar (ej: ArribaDerechaH)")]
    public string secuenciaRequerida;

    [Header("Configuración del Animator")]
    [Tooltip("El nombre del parámetro Bool en tu Animator (ej: combos)")]
    public string parametroBool;

    [Tooltip("El número de animación dentro de ese árbol (ej: 0)")]
    public float numeroDeAnimacion;
}
