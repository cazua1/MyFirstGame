using UnityEngine;

public class ElectricTrap : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Electrify();
        }
    }
}
