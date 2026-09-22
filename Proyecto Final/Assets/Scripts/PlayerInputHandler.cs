using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Entity miEntidad;
    private Vector2 moveInput;
    private Vector2 lastMoveInput;

    public void InicializarCerebro(Entity entidadAControlar)
    {
        miEntidad = entidadAControlar;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (miEntidad != null)
        {
            miEntidad.DarOrdenMovimiento(moveInput);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (miEntidad != null)
        {
            miEntidad.EjecutarAtaque();
        }
    }

    private void Update()
    {
        if (miEntidad == null || miEntidad.Atacando) return;

        if (moveInput.y > 0.5f && lastMoveInput.y <= 0.5f) miEntidad.Combo += "Arriba";
        if (moveInput.y < -0.5f && lastMoveInput.y >= -0.5f) miEntidad.Combo += "Abajo";
        if (moveInput.x < -0.5f && lastMoveInput.x >= -0.5f) miEntidad.Combo += "Izquierda";
        if (moveInput.x > 0.5f && lastMoveInput.x <= 0.5f) miEntidad.Combo += "Derecha";

        lastMoveInput = moveInput;
    }
}

