using UnityEngine;

[RequireComponent(typeof(PlayerMover))]

public class PositionCorrectorOnWall : MonoBehaviour
{
    private const float PositionX = 0.3f;
    private const float PositionY = 1f;

    private PlayerMover _mover;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TopOfTheWall>())
        {
            if (_mover.MovesToTheRight)
            {
                transform.Translate(-PositionX, -PositionY, 0);
            }
            else
            {
                transform.Translate(PositionX, -PositionY, 0);
            }
        }
    }
}