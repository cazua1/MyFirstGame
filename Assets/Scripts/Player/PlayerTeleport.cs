using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject _currentPortal;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal portal))
        {
            _currentPortal = portal.gameObject;
        }

        if (_currentPortal != null)
        {
            transform.position = _currentPortal.GetComponent<Portal>().GetDestination().position;                      
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal teleporter))
        {
            if (teleporter.gameObject == _currentPortal)
            {
                _currentPortal = null;                               
            }
        }
    }
}
