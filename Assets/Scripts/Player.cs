using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public static Player current;

    private Vector3 vMove, vDir;

    public Transform Root, Body, BulletExtractor;
    public Camera MainCamera;

    [Range(1, 20)]
    public float CharacterSpeed;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () 
    {
	
	}

    void Fire()
    {
        var bullet = BulletManager.current.GetBullet();                
        bullet.transform.position = BulletExtractor.position;
        bullet.transform.rotation = BulletExtractor.rotation;
        
        bullet.SetActive(true);
    }

    void Control()
    {
        // Move
        vMove.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        vMove.Normalize();        

        // Mouse look
        vDir = MainCamera.ScreenToWorldPoint(Input.mousePosition) - Root.position;
        vDir.z = 0;
        vDir.Normalize();

        // Apply movement and look
        Root.position += vMove * CharacterSpeed * Time.deltaTime;
        Body.up = vDir;

        // Check fire
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }
	
	// Update is called once per frame
	void Update () 
    {
        Control();

	}
}
