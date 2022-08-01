using System.Collections;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _bestResalt;
    [SerializeField] private TMP_Text _currentResalt;
    [SerializeField] private CanvasGroup _canvasGroup;

    private const string DistanceKey = "DistanceKey";

    private Coroutine _changeAlphaCoroutine;
    private float _bestDistance = 0;
        
    private void OnEnable()
    {
        _player.DistanceChanged += OnDistanceChanged;
    }

    private void OnDisable()
    {
        _player.DistanceChanged -= OnDistanceChanged;
    }

    private void Start()
    {
        _bestDistance = PlayerPrefs.GetFloat(DistanceKey);
        _bestResalt.text = _bestDistance.ToString() + "m";        
    }

    private void OnDistanceChanged(float distance)
    {
        ShowResult(distance);
        RecordBestResult(distance);
    }

    private void ShowResult(float distance)
    {        
        _currentResalt.text = distance.ToString() + "m";
        _canvasGroup.alpha = 1;

        if (_changeAlphaCoroutine != null)
            StopCoroutine(_changeAlphaCoroutine);

        _changeAlphaCoroutine = StartCoroutine(ChangeAlpha(0f, 0.01f));       
    }

    private void RecordBestResult(float distance)
    {
        if (_bestDistance < distance)
        {
            _bestDistance = distance;
            _bestResalt.text = _bestDistance.ToString() + "m";
        }
        PlayerPrefs.SetFloat(DistanceKey, _bestDistance);
    }

    private IEnumerator ChangeAlpha (float targetAlphaValue, float delay)
    {
        var hideDelay = new WaitForSeconds(delay);
        float changeStep = 0.01f;

        while(_canvasGroup.alpha != targetAlphaValue)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, targetAlphaValue, changeStep);
            yield return hideDelay;
        }
    }
}
