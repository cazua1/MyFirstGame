using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SectionslBuilder _builder;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pauseScreen;    

    private Camera _camera;
    private bool _isPaused = false;
      
    private void Awake()
    {
        _camera = Camera.main;
        _pauseScreen.SetActive(false);
    }

    private void OnEnable()
    {
        _player.GameRestart += OnGameRestart;
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnDisable()
    {
        _player.GameRestart -= OnGameRestart;
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
    }

    private void OnGUI()
    {
        if (_isPaused)
            GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        _isPaused = !hasFocus;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        _isPaused = pauseStatus;
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;       
        _pauseScreen.SetActive(true);
    }

    private void OnGameRestart()
    {
        _player.ResetPlayer();
        _builder.ResetSection();   
        _camera.GetComponent<PlayerTracker>().ReserPosition();
    }
}
