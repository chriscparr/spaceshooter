using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IGameEntity
{ 
    [SerializeField] protected Bullet bulletPrefab;
    protected float _moveSpeed = 10;

    protected int bulletInterval => 100;
    protected int bulletCountdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

        float horizontalInput = Input.GetAxis("Horizontal");
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis("Vertical");
        //Get the value of the Vertical input axis.

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed * Time.deltaTime);
    }

    protected void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull = Instantiate<Bullet>(bulletPrefab);
            bull.transform.position = transform.position + Vector3.right;
            bull.Shoot(Vector3.right, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }

    public void TakeDamage(int damage)
    {
        //take some damage
    }
}
