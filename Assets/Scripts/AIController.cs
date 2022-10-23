using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject[] myPrefabs;
    public static List<GameObject> instances = new List<GameObject>();
    public static Vector3 position;//z = 45
    public static int sectionCount = 0;
    public Rigidbody rb;
    public float multiplier = 2.5f;// 2.5 best value

    void Start()
    {
        position.z = 45f;
        position.y = -0.5f;
    }

    void Update()
    {
        if (transform.position.z >= position.z)
        {
            Debug.Log("spawn");
            position.z += 110 + GetDistance();
            position.y -= 20;
            instances.Add(Instantiate(myPrefabs[Random.Range(0, myPrefabs.Length)], position, Quaternion.identity));
            sectionCount++;
        }
    }

    float GetDistance()
    {
        float dist = rb.velocity[2];//takes velocity in z, the direction of the ball
        return dist * multiplier;
    }
}
