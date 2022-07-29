using UnityEngine;

[RequireComponent(typeof(PlayerMover))]

public class PositionCorrectorOnWall : MonoBehaviour
{
    private PlayerMover _mover;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TopOfTheWall>())
        {
            if (_mover.FaceRight)
            {
                transform.Translate(-0.3f, -1f, 0);
            }
            else if (_mover.FaceRight == false)
            {
                transform.Translate(0.3f, -1f, 0);
            }
        }
    }
}
