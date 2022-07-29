using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    [SerializeField] private Transform _beginPoint;
    [SerializeField] private Transform _endPoint;    

    public Transform BeginPoint => _beginPoint;
    public Transform EndPoint => _endPoint;   
}
