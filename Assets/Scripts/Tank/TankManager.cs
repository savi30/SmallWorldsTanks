using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;

[Serializable]
public class TankManager
{

    public Material playerMaterial;                             // This is the color this tank will be tinted.
    [HideInInspector] public int playerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string coloredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector] public GameObject instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector] public int wins;                    // The number of wins this player has so far.


    private TankMovement movement;
    private TankShooting shooting;                        // Reference to tank's shooting script, used to disable and enable control.
    private GameObject canvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


    public void Setup()
    {
        // Get references to the components.
        movement = instance.GetComponent<TankMovement>();
        shooting = instance.GetComponent<TankShooting>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;


        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material = playerMaterial;
        }
    }


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl()
    {
        movement.enabled = false;
        shooting.enabled = false;

        canvasGameObject.SetActive(false);
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl()
    {
        movement.enabled = true;
        shooting.enabled = true;

        canvasGameObject.SetActive(true);
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset()
    {
        instance.SetActive(false);
        instance.SetActive(true);
    }
}