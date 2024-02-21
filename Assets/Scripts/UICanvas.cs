using UnityEngine;
using UnityEngine.UI;
public class UICanvas : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _currentScore = 0;
    private int _bestScore = 0;

    protected void Start()
    {
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        _currentScore = Mathf.FloorToInt(playerPosition.x);
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
        _scoreText.text = string.Format("{0} (High Score {1})", _currentScore, _bestScore);
    }
}
