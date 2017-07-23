using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeController : MonoBehaviour
{

    GameController mainGameController = null;

    // Use this for initialization
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
        //Debug.Log("Escaped");
        if (mainGameController != null)
        {
            if (other.CompareTag(SelectionCodes.GameTags.Player.ToString()))
            {
                
                mainGameController.FinishedGame();
            }
        }

    }
}
