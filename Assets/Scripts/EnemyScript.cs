using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{   
    private Vector3 vDir;
    private float rotate;

    // Use this for initialization
    void Start()
    {

    }

    void Control()
    {
        vDir = Player.current.transform.position - transform.position;
        vDir.z = 0;
        vDir.Normalize();


        rotate = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg - 90;

        GetComponent<Rigidbody2D>().MoveRotation(rotate);

        //transform.up = vDir;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }
}
