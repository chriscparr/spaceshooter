using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float _speed = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void Shoot(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }
}
