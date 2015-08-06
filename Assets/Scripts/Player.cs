using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public static Player current;

    private Vector2 vMove, vDir;

    public Transform Root, Body, BulletExtractor;
    public Camera MainCamera;

    [Range(1, 20)]
    public float CharacterSpeed;

    private Rigidbody2D rigidBody;   

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
        vMove.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        vMove.Normalize();        

        // Mouse look
        vDir = MainCamera.ScreenToWorldPoint(Input.mousePosition) - Root.position;        
        vDir.Normalize();

        // Apply movement and look
        this.rigidBody.MovePosition(this.rigidBody.position + vMove * CharacterSpeed * Time.deltaTime);        
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
