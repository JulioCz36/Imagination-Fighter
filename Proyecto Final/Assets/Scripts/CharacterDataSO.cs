using UnityEngine;

[CreateAssetMenu(fileName = "NuevoCharacterData", menuName = "Sistema de Combate/Character Data UI")]
public class CharacterDataSO : ScriptableObject
{
    [Header("Datos de Identidad Base")]
    public string playerName;

    [Header("Estadísticas de Combate")]
    public float vidaMaxima = 100f;
    public float imaginacionMaxima = 100f;
    public float poderMaximo = 3f;
}

