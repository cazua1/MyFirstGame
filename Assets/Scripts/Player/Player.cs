using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bangEffect;
        
    private Animator _animator;
    private PlayerMover _mover;
    private float _distance = 0;

    public float Distance => _distance;
    
    public event UnityAction GameRestart;
    public event UnityAction<float> DistanceChanged;

    private void Awake()
    {        
        _animator = GetComponent<Animator>();
        _mover = GetComponent<PlayerMover>();        
    }

    private void FixedUpdate()
    {
        CalculatedDistance();       
    }

    public void CalculatedDistance()
    {
        float distance = Vector2.Distance(_mover.StartPosition.transform.position, transform.position);        
        float result = (float)Math.Round(distance, 1);

        if (_distance < result)
            _distance = result;        
    }

    public void ResetPlayer()
    {
        float delay = 1f;
        _mover.ResetPosition();        
        Invoke(nameof(Enable), delay);        
    }     

    public void Explode()
    {
        Instantiate(_bangEffect, transform.position, transform.rotation);
        _animator.Play(PlayerAnimator.States.Disappear);       
    }

    public void Burn()
    {
        _animator.Play(PlayerAnimator.States.Burn);        
    }

    public void Electrify()
    {
        _animator.Play(PlayerAnimator.States.Electrify);        
    }

    public void Die()
    {        
        gameObject.SetActive(false);        
        GameRestart?.Invoke();
        DistanceChanged?.Invoke(_distance);
        _distance = 0;        
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }
}
