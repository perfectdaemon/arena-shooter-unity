using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{   
    private Vector3 vDir;

    // Use this for initialization
    void Start()
    {

    }

    void Control()
    {
        vDir = Player.current.transform.position - transform.position;
        vDir.z = 0;
        vDir.Normalize();

        transform.up = vDir;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }
}
