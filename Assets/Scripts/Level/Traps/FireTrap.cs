using UnityEngine;

public class FireTrap : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Burn();            
        }
    }     
}
