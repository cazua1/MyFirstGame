using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private float _outputSpeedX;
    [SerializeField] private float _outputSpeedY;      

    public Transform GetDestination()
    {
        return _destination;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover player))
        {
            player.Rigidbody2D.velocity = new Vector2(0, 0);
            player.Rigidbody2D.AddForce(new Vector2(_outputSpeedX, _outputSpeedY), ForceMode2D.Impulse);
        }
    }    
}