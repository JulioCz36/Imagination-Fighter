using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform camara;

    private float longitudSprite;
    private float posicionInicialX;

    private void Start()
    {
        posicionInicialX = transform.position.x;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            longitudSprite = spriteRenderer.bounds.size.x;
        }

        if (camara == null && Camera.main != null)
        {
            camara = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        if (camara == null) return;

        float distanciaRecorrida = camara.position.x * (1 - 0f);
        float movimientoTemporal = camara.position.x * 0f;

        transform.position = new Vector3(camara.position.x, transform.position.y, transform.position.z);

        if (movimientoTemporal > posicionInicialX + longitudSprite)
        {
            posicionInicialX += longitudSprite;
        }
        else if (movimientoTemporal < posicionInicialX - longitudSprite)
        {
            posicionInicialX -= longitudSprite;
        }
    }
}
