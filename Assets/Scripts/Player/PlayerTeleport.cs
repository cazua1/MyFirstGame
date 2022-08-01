using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private Portal _currentPortal;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal portal))
        {
            _currentPortal = portal;
        }

        if (_currentPortal != null)
        {
            transform.position = portal.GetDestination().position;                      
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal portal))
        {
            if (portal == _currentPortal)
            {
                _currentPortal = null;                               
            }
        }
    }
}
