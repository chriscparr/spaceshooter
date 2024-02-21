using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _scoreText;
    private int _currentScore = 0;
    private int _bestScore = 0;

    // Update is called once per frame
    void Update()
    {
        //Only scroll when player is progressing right
        if (_player.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(_player.transform.position.x, 0, -20);
        }

        _currentScore = Mathf.FloorToInt(_player.transform.position.x);
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
        _scoreText.text = string.Format("{0} (High Score {1})", _currentScore, _bestScore);

    }
}
