using UnityEngine;
using UnityEngine.UI;
public class UICanvas : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _currentScore = 0;
    private int _bestScore = 0;

    public void SetScore(int score)
    {
        _currentScore = score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
        _scoreText.text = string.Format("{0} (High Score {1})", _currentScore, _bestScore);
    }
}
