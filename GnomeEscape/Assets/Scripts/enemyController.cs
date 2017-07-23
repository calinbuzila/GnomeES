using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    // Use this for initialization

    public List<Transform> targets;
    public float rayDistance;
    public LayerMask LayerMaskToCollide;
    private NavMeshAgent navmeshAgent;

    public NavMeshAgent NavmeshAgent
    {
        get { return navmeshAgent; }
        set { navmeshAgent = value; }
    }

    // indexing starts from 0, which is the element current position, the target will be the second one, and then looping back.
    private int currentTarget = 1;
    private float errorRate = 5.0f;
    private static Transform initialPositionTransform;

    public Transform InitialPositionTransform
    {
        get { return initialPositionTransform; }
        set { initialPositionTransform = value; }
    }


    void Start()
    {
        //Debug.Log(targets.Length + "TARGETLEN");
        NavmeshAgent = this.GetComponent<NavMeshAgent>();
        if (targets.Capacity > 0)
        {
            GameObject newObjectOnInitialPosition = new GameObject();
            newObjectOnInitialPosition.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            InitialPositionTransform = newObjectOnInitialPosition.transform;
            targets.Capacity += 1;
            targets.Insert(0, InitialPositionTransform);
            //Debug.Log(InitialPositionTransform.position);
            navmeshAgent.SetDestination(new Vector3(this.targets[1].position.x, this.targets[1].position.y, this.targets[1].position.z));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        // substract a difference vector between destination and current enemy agent and compare it to te final stoppingdistance, which is usually 0 and an errorRate is added to improve calculations
        if (Vector3.Distance(navmeshAgent.destination, navmeshAgent.transform.position) < navmeshAgent.stoppingDistance + errorRate)
        {

            if (!navmeshAgent.hasPath)
            {
                if (currentTarget >= targets.Capacity - 1)
                {

                    currentTarget = 0;
                    if (currentTarget < targets.Capacity)
                    {
                        //Debug.Log("!!!!! " + currentTarget + "_____ " + targets.Capacity);
                        navmeshAgent.SetDestination(new Vector3(this.targets[currentTarget].position.x, this.targets[currentTarget].position.y, this.targets[currentTarget].position.z));
                    }
                }
                else
                {
                    currentTarget++;

                    if (currentTarget < targets.Capacity)
                    {
                        //Debug.Log("?????  " + currentTarget + "_____ " + targets.Capacity);
                        navmeshAgent.SetDestination(new Vector3(this.targets[currentTarget].position.x, this.targets[currentTarget].position.y, this.targets[currentTarget].position.z));
                    }

                }

            }
        }
        RayCastingDetection();

    }

    protected void RayCastingDetection()
    {
        RaycastHit target;
        var currentPositionOfEnemy = this.transform;
        Ray castedRay = new Ray(currentPositionOfEnemy.position, currentPositionOfEnemy.forward);
        if (Physics.Raycast(castedRay, out target, rayDistance, (int)LayerMaskToCollide))
        {
            Debug.Log(target.collider.gameObject);
            if (target.collider.tag == SelectionCodes.GameTags.Player.ToString())
            {

            }
        }
        //Debug.DrawRay(currentPositionOfEnemy.position, currentPositionOfEnemy.forward * rayDistance, Color.red);
    }
}
