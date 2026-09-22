using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider sliderVida;
    [SerializeField] private FloatEventChannel canalVida; 

    private void OnEnable()
    {
        if (canalVida != null) canalVida.OnEventRaised += ActualizarBarra;
    }

    private void OnDisable()
    {
        if (canalVida != null) canalVida.OnEventRaised -= ActualizarBarra;
    }

    private void ActualizarBarra(float porcentajeVida)
    {
        sliderVida.value = porcentajeVida;
    }
}
