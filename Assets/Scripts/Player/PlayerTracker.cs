using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _yOffset;
    private float _maxPositionY = 0;
    private Vector3 _target;    

    private void FixedUpdate()
    {
        float positionY = _player.transform.position.y;        

        if (_maxPositionY < positionY)
            _maxPositionY = positionY;

        Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _speed * Time.deltaTime);
        transform.position = currentPosition;
        _target = new Vector3(_player.transform.position.x, _maxPositionY, transform.position.z);       
    }   

    public void ReserPosition()
    {
        _maxPositionY = 0;
        Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _speed * Time.deltaTime);
        transform.position = currentPosition;
    }    
}
