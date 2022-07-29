using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Explode();
        }
    }
}
