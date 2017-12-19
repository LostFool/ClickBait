//Devon Wenskunas
//Oct 2017
//Clicker One
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//peonManager knows how many peons exist and how many are assigned what tasks (and as a total).
//Needs to be able to check house availability
internal class peonManager
{

    public int peonUntasked { get; protected set; }
    public int populationTotal { get; protected set; }
    public int peonTasked { get; protected set; }
    public int maxPopulation { get; protected set; }

    public int farmerPeons { get; protected set; }
    public int hunterPeons { get; protected set; }


    public peonManager(int maxPopulation, int currentPopulation)
    {
        this.maxPopulation = maxPopulation;
        this.populationTotal = currentPopulation;
        updateUntasked();
    }

    //Increase max Population by 1
    public void incMaxPop()
    {
        maxPopulation++;
        updateUntasked();
    }

    //Increase max Population by count total
    public void incMaxPop(int count)
    {
        maxPopulation += count;
        updateUntasked();
    }

    //Increase population by one
    public void incPopulation()
    {
        if (populationTotal < maxPopulation)
        {
            populationTotal++;
            if (populationTotal > maxPopulation) populationTotal = maxPopulation;
            updateUntasked();
        }
        else Debug.Log("Max Population reached at " + maxPopulation.ToString());
    }

    //Increase population by a number
    public void incPopulation(int count)
    {
        if (populationTotal < maxPopulation)
        {
            if (count > (maxPopulation - populationTotal)) count = maxPopulation - populationTotal;
            populationTotal = populationTotal + count;
            if (populationTotal > maxPopulation) populationTotal = maxPopulation;
            updateUntasked();
        }
        else Debug.Log("Max Population reached at " + maxPopulation.ToString());
    }

    //Decrease population by one
    public void decPopulation()
    {
        if (populationTotal > 0)
        {
            populationTotal--;
            if (populationTotal < 0) populationTotal = 0;
            updateUntasked();
            updateTasked();
        }
        else Debug.Log("No more Peons to remove from population");
    }

    //Decrease population by a number
    public void decPopulation(int count)
    {
        if (populationTotal > 0)
        {
            if (count > populationTotal) count = populationTotal;
            populationTotal -= count;
            if (populationTotal < 0) populationTotal = 0;
            updateUntasked();
            updateTasked();
        }
        else Debug.Log("No more Peons to remove from population");
    }

    //Increase Tasked
    public void incTasked()
    {
        if (peonUntasked > 0)
        {
            peonTasked++;
            updateUntasked();
        }
    }

    //Increase Tasked by x number
    public void incTasked(int count)
    {
        if (peonUntasked > 0)
        {
            if (count > peonUntasked) count = peonUntasked;
            peonTasked += count;
            updateUntasked();
        }
    }

    //Decrease Tasked
    public void decTasked()
    {
        if (peonTasked > 0)
        {
            peonTasked--;
            updateUntasked();
        }

    }

    //Decreased Tasked by x
    public void decTasked(int count)
    {
        if (peonTasked > 0)
        {
            if (count > peonTasked) count = peonTasked;
            peonTasked -= count;
            updateUntasked();
        }
    }

    //Increase amount of Farmers
    public void incFarmers()
    {
        if (peonUntasked > 0)
        {
            farmerPeons++;
            incTasked();
        }
    }
    
    //Increase amount of Farmers by x
    public void incFarmers(int count)
    {
        if (peonUntasked > 0)
        {
            if (count > peonUntasked) count = peonUntasked;
            farmerPeons += count;
            incTasked(count);
        }
    }

    //Get amount tasked to job
    public int taskedToJob(jobType type)
    {
        int temp = 0;

        switch (type)
        {
            case jobType.Farmer:
                return temp = farmerPeons;
            default:  return temp;
        }
    }

    //Methods to update tasked/untasked counts
    private void updateUntasked()
    {
        peonUntasked = populationTotal - peonTasked;
    }

    private void updateTasked()
    {
        if (peonTasked > populationTotal)
        {
            peonTasked = populationTotal;
        }
    }

}
