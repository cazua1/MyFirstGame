using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private float _positionX;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DOMoveX(_positionX, _duration).SetLoops(-1);       
    }
}
