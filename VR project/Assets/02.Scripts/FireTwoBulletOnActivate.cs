using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class FireTwoBulletOnActivate : MonoBehaviour
{
    public GameObject bullet_1;
    public GameObject bullet_2;
    public Transform spawnPoint_1;
    public Transform spawnPoint_2;
    public float fireSpeed;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireTwoBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireTwoBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet_1 = Instantiate(bullet_1);
        spawnedBullet_1.transform.position = spawnPoint_1.position;
        spawnedBullet_1.GetComponent<Rigidbody>().velocity = spawnPoint_1.forward * fireSpeed;
        
        GameObject spawnedBullet_2 = Instantiate(bullet_2);
        spawnedBullet_2.transform.position = spawnPoint_2.position;
        spawnedBullet_2.GetComponent<Rigidbody>().velocity = spawnPoint_2.forward * fireSpeed;
        
        Destroy(spawnedBullet_1, 5);
        Destroy(spawnedBullet_2, 5);
    }

}
