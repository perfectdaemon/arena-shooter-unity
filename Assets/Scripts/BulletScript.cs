using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{
    public GameObject BloodParticles;

    void OnEnable()
    {        
        Invoke("Destroy", BulletManager.current.BulletMaxTime);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        transform.position += transform.up.normalized * BulletManager.current.BulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Enemy")
        {
            var obj = Instantiate<GameObject>(BloodParticles);
            obj.transform.position = transform.position;
        }
        Debug.Log(tag);
        Destroy();
    }
}
