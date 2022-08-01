using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private SectionslBuilder _builder;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerTracker _camera;
    
    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnRestartButtonClick()
    {
        _builder.ResetLevel();
        _player.ResetPlayer();
        _camera.ReserPosition();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnPlayButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();        
    }
}
