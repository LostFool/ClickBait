//Devon Wenskunas
//Oct 2017
//Clicker One

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Contains the resource types available and their current value
//Updates the resource overlay with the current values

public enum rescType { Empty, Food, Wood, Stone, Metal, Peon, Research, Stone_Blocks, Tools, Weapons, Clothes, Home };

public class resourceManager : MonoBehaviour
{

    rescType _Type;

    public GameObject upgradeManager_GO;
    upgradeManager upgradeMan;


    public float foodCount { get; set; } //oldCount[0]
    public float woodCount { get; set; }
    public float stoneCount { get; set; }
    public float metalCount { get; set; }
    public float peonCount { get; set; }
    public float researchCount { get; set; }
    public float blockCount { get; set; }
    public float toolCount { get; set; }
    public float weaponCount { get; set; }
    public float clothesCount { get; set; }
    public float homeCount { get; set; }

    //Array to store old count for each of the above, stores last count before updating.
    //Here for now, in case we need to refrence old count later
    //In same order as above
    private float[] oldCount = new float[11];


    private void Awake()
    {
        foodCount = 1;
        woodCount = 1;

        //Set upgradeMan to live upgradeManager Game Objects script (For getting actual values stored during game)
        upgradeMan = upgradeManager_GO.GetComponent<upgradeManager>();
    }

    //Public method to increase resources, based on resource type
    public void updateCount(rescType type, sourceType source)
    {
        if (type == rescType.Food)
        {
            oldCount[0] = foodCount;

            foodCount = incCount(rescType.Food, foodCount, source);
        }
    }

    //Public method 2 to increase resources, based on job
    public void updateCount(jobType type, sourceType source, float multiplier)
    {
        if (type == jobType.Farmer)
        {
            oldCount[0] = foodCount;

            foodCount = incCount(rescType.Food, foodCount, source, multiplier);
        }
    }

    //Private method for resourceManager to increase resource
    private float incCount(rescType Type, float Count, sourceType source, float multiplier = 1)
    {
        float newCount = 0, modifier;

        //Apply appropriate modifier
        modifier = upgradeMan.getModifier(Type, source);

        //Apply increase
        newCount = (modifier * multiplier) + Count;

        return newCount;
    }
}
