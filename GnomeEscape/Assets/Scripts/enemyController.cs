using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{

    // Use this for initialization

	//targets are the waypoints where the enemies move
    public List<Transform> targets;
    public float rayDistance;
	// layer is set in the upper part of the inspector for the layers on which the raycast actions.
    public LayerMask LayerMaskToCollide;
    private NavMeshAgent navmeshAgent;
	// error rate used when raycasting
    public float rayOffset;
    public float lateralRayDistance;
    public bool isStationary = false;
	//playerinsight is used for the dynamic raycast when he triggers the sight box collider then thedynamic raycasts are cast
    public bool playerInSightArea = false;
    public GameObject player;

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
    GameController mainGameController = null;

	// the waypoints are set by their transform position in the scene, after finishing all the waypoints in the array, the enemy returns to the intial position and starts again passing through waypoints
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
            if (!isStationary)
            {
                navmeshAgent.SetDestination(new Vector3(this.targets[1].position.x, this.targets[1].position.y, this.targets[1].position.z));
            }
        }
        mainGameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isStationary)
        {
            // substract a difference vector between destination and current enemy agent and compare it to te final stoppingdistance, which is usually 0 and an errorRate is added to improve calculations
            if (Vector3.Distance(navmeshAgent.destination, navmeshAgent.transform.position) < navmeshAgent.stoppingDistance + errorRate)
            {
				//checking if the navmeshAgent component has waypoints attach has a destination one.
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
        }
        RayCastingDetection();
        DynamicRayCastToPlayer();

    }

    protected void RayCastingDetection()
    {
        RaycastHit target;
        var currentPositionOfEnemy = this.transform;
        Ray castedRay = new Ray(currentPositionOfEnemy.position, currentPositionOfEnemy.forward);
		// raycast in forward direction, if it hits an object in the scene it outputs it in target variable, cast only what is specified in the layermastToCollide
        if (Physics.Raycast(castedRay, out target, rayDistance, (int)LayerMaskToCollide))
        {
            if (target.collider.tag == SelectionCodes.GameTags.Player.ToString())
            {
                //Debug.Log("HITT");
                if (mainGameController != null)
                {
                    mainGameController.CaughtAndStopGame();
                }
            }
        }
		// raycast in 90 degree right direction, if it hits an object in the scene it outputs it in target variable, cast only what is specified in the layermastToCollide
        Ray castedRay2 = new Ray(currentPositionOfEnemy.position, currentPositionOfEnemy.right);
        if (Physics.Raycast(castedRay2, out target, lateralRayDistance, (int)LayerMaskToCollide))
        {
            if (target.collider.tag == SelectionCodes.GameTags.Player.ToString())
            {
                //Debug.Log("HITT+++");
                if (mainGameController != null)
                {
                    mainGameController.CaughtAndStopGame();
                }
            }
        }
		// raycast in 90 degree left direction, if it hits an object in the scene it outputs it in target variable, cast only what is specified in the layermastToCollide
        Ray castedRay3 = new Ray(currentPositionOfEnemy.position, -currentPositionOfEnemy.right);
        if (Physics.Raycast(castedRay3, out target, lateralRayDistance, (int)LayerMaskToCollide))
        {
            if (target.collider.tag == SelectionCodes.GameTags.Player.ToString())
            {
                //Debug.Log("HITT---");
                if (mainGameController != null)
                {
                    mainGameController.CaughtAndStopGame();
                }
            }
        }
			
        Debug.DrawRay(currentPositionOfEnemy.position, currentPositionOfEnemy.forward * rayDistance, Color.red);
        Debug.DrawRay(currentPositionOfEnemy.position, (currentPositionOfEnemy.right) * lateralRayDistance, Color.blue);
        Debug.DrawRay(currentPositionOfEnemy.position, (-currentPositionOfEnemy.right) * lateralRayDistance, Color.green);


    }

	// when the player steps in the in the box collider of the sight object this method triggers the dynamic raycast towards the player, if the first object hit is the player then the game ends.
	// the method is called from SIghtController.
    public void DynamicRayCastToPlayer()
    {
        if (player != null)
        {
            var playerPosition = this.player.GetComponent<Transform>().position;
            var currentPositionOfEnemy = this.transform;
            var direction = playerPosition - currentPositionOfEnemy.position;
            RaycastHit target;

            Ray castedRay = new Ray(currentPositionOfEnemy.position + Vector3.up / 2, direction.normalized);
            if (playerInSightArea)
            {
                if (Physics.Raycast(castedRay, out target, rayDistance, (int)LayerMaskToCollide))
                {
                    //Debug.Log("HITTDynamic" + target.collider.gameObject);
                    if (target.collider.tag == SelectionCodes.GameTags.Player.ToString())
                    {
                        //Debug.Log("HITTDynamic");
                        if (mainGameController != null)
                        {
                            mainGameController.CaughtAndStopGame();
                        }
                    }
                }
            }
        }

    }
}
