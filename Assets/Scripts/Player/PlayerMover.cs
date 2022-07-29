using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpLenght;    
    [SerializeField] private float _jumpÑoefficient;    
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private ParticleSystem _jumpEffect;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;    
    private Animator _animator;
    private bool _faceRight;
    private int _jumpCounter;

    public Transform StartPosition => _startPosition;
    public bool FaceRight => _faceRight;
   
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();        
    }

    private void Start()
    {
        ResetPosition();
        _rigidbody2D.gravityScale = 1.5f;
        _jumpCounter = 0;
        _faceRight = true;        
    }

    private void Update()
    {       
        Flip();
        Jump();        
        WallJump();
        DoubleJump();

        if (IsGrounded())
        {
            _animator.SetBool(PlayerAnimator.States.Idle, true);
        }
        else
        {
            _animator.SetBool(PlayerAnimator.States.Idle, false);
        }        
    }

    public void ResetPosition()
    {
        transform.position = _startPosition.position;        
        _rigidbody2D.velocity = Vector2.zero;
        _faceRight = true;
    }    

    private void Jump(float x, float y)
    {
        _rigidbody2D.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        PlayJumpEffect();
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() && _faceRight && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            Jump(_jumpLenght, _jumpHeight);
            _animator.SetTrigger(PlayerAnimator.States.Jump);
            _jumpCounter++;
        }
        else if (Input.GetMouseButtonDown(0) && IsGrounded() && _faceRight == false && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            Jump(-_jumpLenght, _jumpHeight);
            _animator.SetTrigger(PlayerAnimator.States.Jump);
            _jumpCounter++;
        }

        if (Input.GetMouseButtonUp(0) && _rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x / _jumpÑoefficient, _rigidbody2D.velocity.y / _jumpÑoefficient);
        }
    }

    private void WallJump()
    {
        if (OnWall())
        {
            _rigidbody2D.gravityScale = 2.5f;
            _rigidbody2D.velocity = Vector2.zero;
            _jumpCounter = 0;
            _animator.SetBool(PlayerAnimator.States.Slide, true);

            if (Input.GetMouseButtonDown(0) && _faceRight && EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                ResetVelocityOfFall();
                Jump(-_jumpLenght, _jumpHeight);
                _animator.SetTrigger(PlayerAnimator.States.Jump);
                _jumpCounter++;
            }
            else if (Input.GetMouseButtonDown(0) && _faceRight == false && EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                ResetVelocityOfFall();
                Jump(_jumpLenght, _jumpHeight);
                _animator.SetTrigger(PlayerAnimator.States.Jump);
                _jumpCounter++;
            }
        }
        else
        {
            _rigidbody2D.gravityScale = 1.5f;
            _animator.SetBool(PlayerAnimator.States.Slide, false);
        }
    }

    private void DoubleJump()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() == false && OnWall() == false && _faceRight && _jumpCounter == 1 && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            ResetVelocityOfFall();
            Jump(-_jumpLenght, _jumpHeight);
            _animator.SetTrigger(PlayerAnimator.States.Salto);
            _jumpCounter++;
        }
        else if (Input.GetMouseButtonDown(0) && IsGrounded() == false && OnWall() == false && _faceRight == false && _jumpCounter == 1 && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            ResetVelocityOfFall();
            Jump(_jumpLenght, _jumpHeight);
            _animator.SetTrigger(PlayerAnimator.States.Salto);
            _jumpCounter++;
        }
    }

    private void Flip()
    {
        if (_rigidbody2D.velocity.x > 0.01)
        {
            transform.localScale = new Vector2(1, 1);
            _faceRight = true;            
        }
                               
        else if (_rigidbody2D.velocity.x < -0.01)
        {
            transform.localScale = new Vector2(-1, 1);
            _faceRight = false;            
        }                   
    }

    private void PlayJumpEffect()
    {
        _jumpEffect.Play();
    }

    private void ResetVelocityOfFall()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
    }
    
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, _wallLayer);
        return raycastHit.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
        return raycastHit.collider != null;
    }
}
