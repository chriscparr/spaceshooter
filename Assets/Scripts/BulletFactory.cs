using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : Factory
{
    public override IBullet GetBullet(Vector3 position)
    {
        GameObject go = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
        go.transform.position = position;
        return go.GetComponent<Bullet>();
    }
}
