using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SectionslBuilder : ObjectsPool
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject[] _sectionsPrefabs;
    [SerializeField] private Section _startSection;

    private Section _previosSection;    

    private void Start()
    {
        Initialize(_sectionsPrefabs);        
        _previosSection = _startSection;
    }

    private void Update()
    {
        if (_player.position.y > _previosSection.BeginPoint.position.y)
        {
            if (TryGetObject(out GameObject section))
            {
                SetSection(section, _previosSection.EndPoint.transform.position);
                var currentSection = section.GetComponent<Section>();
                _previosSection = currentSection;
                DisableObjectAbroadScreen();
            }
        }
    }

    public void ResetLevel()
    {
        ClearPool();
        Initialize(_sectionsPrefabs);
        ResetSection();
    }

    public void ResetSection()
    {
        DisableObjectsInPool();
        _previosSection = _startSection;
    }

    private void SetSection(GameObject section, Vector3 installationPoint)
    {
        section.SetActive(true);
        section.transform.position = installationPoint;
    }
}