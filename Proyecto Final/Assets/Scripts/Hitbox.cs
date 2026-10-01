using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [HideInInspector] public float danioActual;
    [HideInInspector] public Entity duenoDeLaHitbox; 

    private void Awake()
    {
        duenoDeLaHitbox = GetComponentInParent<Entity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox hurtboxEnemiga = collision.GetComponent<Hurtbox>();

        if (hurtboxEnemiga != null)
        {
            
            Entity entidadEnemiga = collision.GetComponentInParent<Entity>();
            if (entidadEnemiga == duenoDeLaHitbox) return;


            Vector2 puntoDeImpacto = collision.ClosestPoint(transform.position);

            hurtboxEnemiga.RecibirImpacto(danioActual, puntoDeImpacto);
        }
    }
}

