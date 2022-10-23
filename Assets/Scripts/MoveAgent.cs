using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveAgent : Agent
{
    //[SerializeField] private Transform targetTransform;
    //GameObject[] prefabs = GetComponent<Controller>().myPrefabs;
    public GameObject prefab;
    //public GameObject player;
    GameObject instance;
    Vector3 placement;
    public float steering = 20f;
    public float acceleration = 10f;


    public override void OnEpisodeBegin()
    {
        //GameObject.Find("Camera").transform.rotation = FollowRay.fixedRotation;
        AIdeathTrigger.alive = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        placement.z = 45f;
        placement.y = -0.5f;
        placement.x = 0f;
        transform.position = Vector3.zero;
        instance = Instantiate(prefab, placement, Quaternion.identity);     
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        //sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.ContinuousActions[0]);
        float moveX = actions.ContinuousActions[0];
        

        GetComponent<Rigidbody>().AddForce(steering * moveX, 0, 0, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(0, 0, acceleration, ForceMode.Impulse);
        //GetComponent<Rigidbody>().AddForce(steering * Input.GetAxis("Horizontal"), 0, 0, ForceMode.Impulse);
    }

    public void Update()
    {
        /*if (transform.position.x > 4 || transform.position.x < -4)
        {
            Debug.Log("OutOfBounds");
            Destroy(instance);
            foreach (GameObject section in AIController.instances)
            {
                Destroy(section);
            }
            AIController.instances.Clear();
            AIController.sectionCount = 0;
            AIController.position = Vector3.zero;
            AIController.position.y = -0.5f;
            AIController.position.z = 45f;
            SetReward(-2f);
            EndEpisode();
        }
        if (transform.position.y < -30 + (-20 * AIController.sectionCount))
        {
            Debug.Log("OutOfBounds");
            Destroy(instance);
            foreach (GameObject section in AIController.instances)
            {
                Destroy(section);
            }
            AIController.instances.Clear();
            AIController.sectionCount = 0;
            AIController.position = Vector3.zero;
            AIController.position.y = -0.5f;
            AIController.position.z = 45f;
            SetReward(-2f);
            EndEpisode();
        }*/

        if (transform.position.x > 3.65 || transform.position.x < -3.5)
        {
            SetReward(-100f);
        }

        if (!AIdeathTrigger.alive)
        {
            //deathTrigger.alive = true;
            Debug.Log("Obstacle");
            Destroy(instance);
            foreach (GameObject section in AIController.instances)
            {
                Destroy(section);
            }
            AIController.instances.Clear();
            AIController.sectionCount = 0;
            AIController.position = Vector3.zero;
            AIController.position.y = -0.5f;
            AIController.position.z = 45f;
            SetReward(-10f);
            Debug.Log(transform.position.z);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("Obstacle");
            SetReward(-100f);
        }
        if (other.gameObject.tag == "Ramp")
        {
            Debug.Log("Ramp");
            SetReward(+20f);
        }
        if (other.gameObject.tag == "speedUp")
        {
            Debug.Log("speedUp");
            SetReward(+20f);
        }
    }
}
