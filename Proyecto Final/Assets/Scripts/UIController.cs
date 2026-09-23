using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [Header("Canales que escucha la UI")]
    [SerializeField] private FloatEventChannel canalVidaP1;
    [SerializeField] private FloatEventChannel canalVidaP2;

    private UIDocument uiDocument;
    private VisualElement rellenoVidaP1;
    private VisualElement rellenoVidaP2;
    private Label textoNombreP1;
    private Label textoNombreP2;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

    
        rellenoVidaP1 = root.Q<VisualElement>("RellenoVidaP1");
        rellenoVidaP2 = root.Q<VisualElement>("RellenoVidaP2");

        
        textoNombreP1 = root.Q<Label>("NombreTextoP1");
        textoNombreP2 = root.Q<Label>("NombreTextoP2");

        // Valores por defecto al arrancar
        ActualizarBarraPorcentaje(rellenoVidaP1, 1f);
        ActualizarBarraPorcentaje(rellenoVidaP2, 1f);
    }


    public void EstablecerNombresEnPantalla(string nombreP1, string nombreP2)
    {
        if (textoNombreP1 != null) textoNombreP1.text = nombreP1;
        if (textoNombreP2 != null) textoNombreP2.text = nombreP2;
    }

    private void OnEnable()
    {

        if (canalVidaP1 != null) canalVidaP1.OnEventRaised += OnVidaP1Modificada;
        if (canalVidaP2 != null) canalVidaP2.OnEventRaised += OnVidaP2Modificada;
    }

    private void OnDisable()
    {
        if (canalVidaP1 != null) canalVidaP1.OnEventRaised -= OnVidaP1Modificada;
        if (canalVidaP2 != null) canalVidaP2.OnEventRaised -= OnVidaP2Modificada;
    }

    private void OnVidaP1Modificada(float porcentaje) => ActualizarBarraPorcentaje(rellenoVidaP1, porcentaje);
    private void OnVidaP2Modificada(float porcentaje) => ActualizarBarraPorcentaje(rellenoVidaP2, porcentaje);

    private void ActualizarBarraPorcentaje(VisualElement elementoBarra, float porcentaje)
    {
        if (elementoBarra != null)
        {
            porcentaje = Mathf.Clamp01(porcentaje);
            elementoBarra.style.width = new Length(porcentaje * 100f, LengthUnit.Percent);
        }
    }
}
