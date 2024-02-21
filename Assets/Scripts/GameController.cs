using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;

    [SerializeField] private Player _player;

    private void Awake()
    {
        _instance = this;
    }



}
