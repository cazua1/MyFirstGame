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
    private bool _movesToTheRight;
    private int _jumpCounter;

    public Transform StartPosition => _startPosition;
    public bool MovesToTheRight => _movesToTheRight;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
   
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
        _movesToTheRight = true;        
    }

    private void Update()
    {       
        Flip();
        Jump();        
        WallJump();
        DoubleJump();

        if (IsGrounded())        
            _animator.SetBool(PlayerAnimator.States.Idle, true);        
        else        
            _animator.SetBool(PlayerAnimator.States.Idle, false);                
    }

    public void ResetPosition()
    {
        transform.position = _startPosition.position;        
        _rigidbody2D.velocity = Vector2.zero;
        _movesToTheRight = true;
    }    

    private void Jump(float x, float y)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        _animator.SetBool(PlayerAnimator.States.Jump, true);
        _jumpEffect.Play();
        _jumpCounter++;
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() && EventSystem.current.IsPointerOverGameObject(0) == false)        
            JumpForward();
        
        if (Input.GetMouseButtonUp(0) && _rigidbody2D.velocity.y > 0)        
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x / _jumpÑoefficient, _rigidbody2D.velocity.y / _jumpÑoefficient);        
    }    

    private void WallJump()
    {
        if (OnWall())
        {
            _rigidbody2D.gravityScale = 2.5f;
            _rigidbody2D.velocity = Vector2.zero;
            _jumpCounter = 0;
            _animator.SetBool(PlayerAnimator.States.Slide, true);

            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                JumpBack();
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
        if (Input.GetMouseButtonDown(0) && IsGrounded() == false && OnWall() == false && _jumpCounter == 1 && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            _animator.SetTrigger(PlayerAnimator.States.Salto);
            JumpBack();
        }        
    }    

    private void JumpForward()
    {
        if (_movesToTheRight)        
            Jump(_jumpLenght, _jumpHeight);
        else        
            Jump(-_jumpLenght, _jumpHeight);        
    }

    private void JumpBack()
    {
        if (_movesToTheRight)        
            Jump(-_jumpLenght, _jumpHeight);        
        else        
            Jump(_jumpLenght, _jumpHeight);        
    }

    private void Flip()
    {
        float minOffsetX = 0.01f;

        if (_rigidbody2D.velocity.x > minOffsetX)
        {
            transform.localScale = new Vector2(1, 1);
            _movesToTheRight = true;            
        }
                               
        else if (_rigidbody2D.velocity.x < -minOffsetX)
        {
            transform.localScale = new Vector2(-1, 1);
            _movesToTheRight = false;            
        }                   
    }    
    
    private bool OnWall()
    {
        float raycastDistance = 0.1f;
        float angle = 0f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, angle, new Vector2(transform.localScale.x, 0), raycastDistance, _wallLayer);
        return raycastHit.collider != null;
    }

    private bool IsGrounded()
    {
        float raycastDistance = 0.1f;
        float angle = 0f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, angle, Vector2.down, raycastDistance, _groundLayer);
        return raycastHit.collider != null;
    }
}