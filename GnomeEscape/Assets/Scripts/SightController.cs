using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    GameController mainGameController = null;

    // Use this for initialization
    void Awake()
    {
        //var gObj = GameObject.FindObjectOfType<GameController>();
        //var transformGObj = tt.GetComponent<Transform>();
        //Debug.Log(transformGObj.position.x + "___" + transformGObj.position.z);
    }

    void Start()
    {
        mainGameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // if the sight box collider intersects with the player
    void OnTriggerEnter(Collider other)
    {
        if (mainGameController != null)
        {
            if (other.CompareTag(SelectionCodes.GameTags.Player.ToString()))
            {
                mainGameController = GameObject.FindObjectOfType<GameController>();
                mainGameController.CaughtAndStopGame();
            }
        }

    }
}
