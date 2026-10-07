using UnityEngine;
using UnityEngine.UIElements;
using System;

public class BtnAjusteTrainig : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    [SerializeField] private float tiempoRequerido = 1.5f;

    private Button botonAjustes;
    private VisualElement progresoCircular;

    private float tiempoMantenido = 0f;
    private bool estaPresionando = false;

    public event Action OnProgresoCompletado;

    public void Inicializar(VisualElement root)
    {
        botonAjustes = root.Q<Button>("btn_ajustes");
        progresoCircular = root.Q<VisualElement>("progreso_circular");

        if (botonAjustes != null)
        {
            botonAjustes.RegisterCallback<PointerDownEvent>(OnPointerDown);
            botonAjustes.RegisterCallback<PointerUpEvent>(OnPointerUp);
            botonAjustes.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
        }

        ActualizarVisualizacionProgreso(0f);
    }

    private void OnDestroy()
    {
        if (botonAjustes != null)
        {
            botonAjustes.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            botonAjustes.UnregisterCallback<PointerUpEvent>(OnPointerUp);
            botonAjustes.UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);
        }
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        estaPresionando = true;
        tiempoMantenido = 0f;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        ResetearProgreso();
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        ResetearProgreso();
    }

    private void ResetearProgreso()
    {
        estaPresionando = false;
    }

    void Update()
    {
        bool teclaEscapePresionada = UnityEngine.InputSystem.Keyboard.current != null &&
                                     UnityEngine.InputSystem.Keyboard.current.escapeKey.isPressed;

        if (estaPresionando || teclaEscapePresionada)
        {
            tiempoMantenido += Time.unscaledDeltaTime;
            float porcentaje = Mathf.Clamp01(tiempoMantenido / tiempoRequerido);

            ActualizarVisualizacionProgreso(porcentaje);

            if (tiempoMantenido >= tiempoRequerido)
            {
                estaPresionando = false;
                tiempoMantenido = 0f;
                ActualizarVisualizacionProgreso(0f);

                OnProgresoCompletado?.Invoke();
            }
        }
        else if (!estaPresionando && tiempoMantenido > 0)
        {
            tiempoMantenido -= Time.unscaledDeltaTime * 2f;
            float porcentaje = Mathf.Clamp01(tiempoMantenido / tiempoRequerido);
            ActualizarVisualizacionProgreso(porcentaje);
        }
    }

    private void ActualizarVisualizacionProgreso(float porcentaje)
    {
        if (progresoCircular != null)
        {
            progresoCircular.style.scale = new Scale(new Vector3(porcentaje, porcentaje, 1f));
            progresoCircular.style.opacity = porcentaje;
        }
    }
}
