using UnityEngine;

public class MatchCamera : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private MatchSetup matchSetup;

    [Header("Movimiento")]
    [SerializeField] private float suavizado = 5f;
    [SerializeField] private float yOffset = 1.8f; 

    [Header("Zoom")]
    [SerializeField] private float zoomMinimo = 5.98f;
    [SerializeField] private float zoomMaximo = 8f;
    [SerializeField] private float factorZoom = 0.4f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        if (cam != null)
        {
            cam.orthographicSize = zoomMinimo;
        }
    }

    private void LateUpdate()
    {
        if (matchSetup == null || matchSetup.Jugador1 == null || matchSetup.Jugador2 == null) return;

        Transform p1 = matchSetup.Jugador1.transform;
        Transform p2 = matchSetup.Jugador2.transform;

        float distanciaEntreJugadores = Mathf.Abs(p1.position.x - p2.position.x);
        float zoomDeseado = zoomMinimo + (distanciaEntreJugadores * factorZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(zoomDeseado, zoomMinimo, zoomMaximo), suavizado * Time.deltaTime);

        Vector3 puntoMedio = (p1.position + p2.position) / 2f;
        float xDestino = puntoMedio.x;
        float yDestino = puntoMedio.y + yOffset;

        float mitadAnchoCamara = cam.orthographicSize * cam.aspect;

        Vector3 posicionDestino = new Vector3(xDestino, yDestino, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicionDestino, suavizado * Time.deltaTime);

        LimitarJugadoresDentroDePantalla(p1, p2);
    }

    private void LimitarJugadoresDentroDePantalla(Transform p1, Transform p2)
    {
        float mitadAnchoCamara = cam.orthographicSize * cam.aspect;
        float bordeIzquierdoCamara = transform.position.x - mitadAnchoCamara;
        float bordeDerechoCamara = transform.position.x + mitadAnchoCamara;

        float margen = 0.8f;

        p1.position = new Vector3(Mathf.Clamp(p1.position.x, bordeIzquierdoCamara + margen, bordeDerechoCamara - margen), p1.position.y, p1.position.z);
        p2.position = new Vector3(Mathf.Clamp(p2.position.x, bordeIzquierdoCamara + margen, bordeDerechoCamara - margen), p2.position.y, p2.position.z);
    }
}
