using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private Player _player;



    // Update is called once per frame
    void Update()
    {
        //Only scroll when player is progressing right
        if (_player.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(_player.transform.position.x, 0, -20);
        }
    }
}
