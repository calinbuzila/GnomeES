using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    GameController mainGameController = null;
    public GameObject enemyParent;
    private bool wasTriggered = false;

    // Use this for initialization
    void Awake()
    {
        //var gObj = GameObject.FindObjectOfType<GameController>();
        //var transformGObj = tt.GetComponent<Transform>();
        //Debug.Log(transformGObj.position.x + "___" + transformGObj.position.z);
    }

    void Start()
    {
        //mainGameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // if the sight box collider intersects with the player
    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(SelectionCodes.GameTags.Player.ToString()) && !wasTriggered)
        {
            //Debug.Log("TRIGGERED");
            if (enemyParent != null)
            {
                wasTriggered = true;
                var enemyController = enemyParent.GetComponent<enemyController>();
                enemyController.playerInSightArea = true;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(SelectionCodes.GameTags.Player.ToString()) && !wasTriggered)
        {
            //Debug.Log("TRIGGERED");
            if (enemyParent != null)
            {
                wasTriggered = false;
                var enemyController = enemyParent.GetComponent<enemyController>();
                enemyController.playerInSightArea = false;
            }
        }
    }
}
