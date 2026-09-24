using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement rellenoVidaP1;
    private VisualElement rellenoVidaP2;
    private Label textoNombreP1;
    private Label textoNombreP2;

    private FloatEventChannel canalActivoP1;
    private FloatEventChannel canalActivoP2;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        rellenoVidaP1 = root.Q<VisualElement>("RellenoVidaP1");
        rellenoVidaP2 = root.Q<VisualElement>("RellenoVidaP2");
        textoNombreP1 = root.Q<Label>("NombreTextoP1");
        textoNombreP2 = root.Q<Label>("NombreTextoP2");

        ActualizarBarraPorcentaje(rellenoVidaP1, 1f);
        ActualizarBarraPorcentaje(rellenoVidaP2, 1f);
    }


    public void ConfigurarUI(Entity jugador1, Entity jugador2)
    {
    
        if (jugador1 != null)
        {
            if (textoNombreP1 != null) textoNombreP1.text = jugador1.ObtenerNombre();

            DesuscribirCanalP1();

            canalActivoP1 = jugador1.CanalVidaAsignado;
            if (canalActivoP1 != null) canalActivoP1.OnEventRaised += OnVidaP1Modificada;
        }

        if (jugador2 != null)
        {
            if (textoNombreP2 != null) textoNombreP2.text = jugador2.ObtenerNombre();

            DesuscribirCanalP2();

            canalActivoP2 = jugador2.CanalVidaAsignado;
            if (canalActivoP2 != null) canalActivoP2.OnEventRaised += OnVidaP2Modificada;
        }
    }

    private void OnDisable()
    {
        DesuscribirCanalP1();
        DesuscribirCanalP2();
    }

    private void DesuscribirCanalP1()
    {
        if (canalActivoP1 != null) canalActivoP1.OnEventRaised -= OnVidaP1Modificada;
    }

    private void DesuscribirCanalP2()
    {
        if (canalActivoP2 != null) canalActivoP2.OnEventRaised -= OnVidaP2Modificada;
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

