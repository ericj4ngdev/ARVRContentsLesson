using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameObject BulletPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject BulletObject = Instantiate(BulletPrefab);
            Bullet bullet = BulletObject.GetComponent<Bullet>();

            Vector3 ShootVector = new Vector3(0.2f, 0.2f, 0);

            bullet.ShootVector = ShootVector.normalized;
        }
    }
}