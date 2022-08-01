using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    [SerializeField] private FireTrap _fireTrap;
    [SerializeField] private float _delay;

    private bool _isActive;

    private void Awake()
    {
        _isActive = true;
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            Invoke(nameof(Deactivate), _delay);
        }
        else
        {
            Invoke(nameof(Activate), _delay);
        }   
    }

    private void Activate()
    {
        _fireTrap.gameObject.SetActive(true);
        _isActive = true;
    }

    private void Deactivate()
    {
        _fireTrap.gameObject.SetActive(false);
        _isActive = false;
    }
}