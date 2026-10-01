using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [Header("Configuración de la Zona")]
    public string zonaDelCuerpo;

    private Entity entidadPadre;

    private void Awake()
    {
        entidadPadre = GetComponentInParent<Entity>();
    }

    public void RecibirImpacto(float danio, Vector2 puntoImpacto)
    {
        if (entidadPadre != null)
        {
            entidadPadre.RecibirDanio(danio);

            // en un futuro cercano llamo a SFX xd
        }
    }
}

