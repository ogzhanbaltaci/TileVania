using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    [SerializeField] AudioClip bulletPickupSFX;
    bool wasCollected = false;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().BulletPickup();
            AudioSource.PlayClipAtPoint(bulletPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }   
    }
}
