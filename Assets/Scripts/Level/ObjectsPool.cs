using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();
    private Camera _camera;

    protected void DisableObjectsInPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }  

    protected void ClearPool()
    {
        foreach (var item in _pool)        
            Destroy(item);
        
        _pool.Clear();
    }

    protected void Initialize(GameObject[] prefabs)
    {
        _camera = Camera.main;        

        for (int i = 0; i < prefabs.Length; i++)
        {            
            GameObject spawned = Instantiate(prefabs[i], _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);            
        }
        _pool = _pool.OrderBy(item => Guid.NewGuid()).ToList();
    }
    
    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
    
    protected void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector3(0, 0));
        int offsetY = 25;

        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                if (item.transform.position.y + offsetY < disablePoint.y)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}