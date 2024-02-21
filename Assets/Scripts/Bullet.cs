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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit " + collision.gameObject.name);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    //Trigger box attached to the camera will move with the player + dispose of stray bullets
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Main Camera")
        {
            Destroy(gameObject);
        }
    }
}
