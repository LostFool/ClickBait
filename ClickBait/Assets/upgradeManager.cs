//Devon Wenskunas
//Oct 2017
//Clicker One

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//upgradeManager handles available upgrades and toggles if bought or not
//attached to GameObject so that it is live and available to access for buttonManager and mathManager
//
//Upgrades available :: Farming I/II, Hunting I/II, Forestry I/II, Mining I/II, Research I/II, Blacksmithing I/II, Tailoring I/II, Masonry I/II, Construction I/II
//Upgrades open some resources and improve efficiency of gathering and usage
//
//Modifier must be less than a whole number, unless wanting increase over 100%


public enum upgradeName { defMods, HarvestingI, HarvestingII };
public enum sourceType { Button, Peon, Gift };

public class upgradeManager : MonoBehaviour
{
    Dictionary<upgradeName, Upgrade> upgradeLibrary;

    private Upgrade tempUpgrade;

    public upgradeManager() { }

    private void Main()
    {
        //Set defaults for upgradeLibrary
        upgradeLibrary = new Dictionary<upgradeName, Upgrade>
        {
        {upgradeName.defMods, new Upgrade{ peonModifier = 1f, buttonModifier = 1f, isEnabled = true} },
        {upgradeName.HarvestingI, new Upgrade{ peonModifier = 1.25f, buttonModifier = 2f, isEnabled = false} }
        };
    }

    //Get appropriate value for current resource and its highest unlocked upgrade modifier
    //sourceType determines which modifier to return
    public float getModifier(rescType type, sourceType source)
    {

        //set tempUpgrade to the default for modifiers. Is always set default so that modifiers from other selections do not carry over
        tempUpgrade = upgradeLibrary[upgradeName.defMods];

        switch (type)
        {
            case rescType.Food:
                //Check for highest available upgrade
                if (Check(upgradeName.HarvestingI) == true) tempUpgrade = upgradeLibrary[upgradeName.HarvestingI];
                else if (Check(upgradeName.HarvestingII) == true) tempUpgrade = upgradeLibrary[upgradeName.HarvestingII];

                //check to use button or peon modifier and return
                if (source == sourceType.Button) return tempUpgrade.buttonModifier;
                else return tempUpgrade.peonModifier;

        }
        Debug.LogError("No rescType of type " + type.ToString() + " returning def button modifier");
        return tempUpgrade.buttonModifier;
    }

    //Handle upgrade purchasing
    //If purchased, then turn on the upgrade modifier specified by upgradeName
    public void purchase(upgradeName upgrdName, bool purchased = false)
    {
        if (purchased == true)
        {
            switch (upgrdName)
            {
                case upgradeName.HarvestingI:
                    upgradeLibrary.TryGetValue(upgradeName.HarvestingI, out tempUpgrade);
                    tempUpgrade.isEnabled = true;
                    upgradeLibrary[upgradeName.HarvestingI] = tempUpgrade;

                    break;
                case upgradeName.HarvestingII:
                    //Shut off lower tier upgrade
                    tempUpgrade = upgradeLibrary[upgradeName.HarvestingI];
                    tempUpgrade.isEnabled = true;
                    upgradeLibrary[upgradeName.HarvestingI] = tempUpgrade;
                    //Turn on new upgrade
                    tempUpgrade = upgradeLibrary[upgradeName.HarvestingII];
                    tempUpgrade.isEnabled = true;
                    upgradeLibrary[upgradeName.HarvestingII] = tempUpgrade;
                    break;
                default:
                    Debug.LogError("No upgradeName in upgradeTree");
                    break;
            }
        }
        else Debug.LogError("Cannot upgradeName " + upgrdName.ToString() + " not purchaseable");
    }

    //Check if upgrade is purchased
    public bool Check(upgradeName name)
    {
        bool unlocked = false;

        switch (name)
        {
            case upgradeName.HarvestingI:
                //Grab and return the state of unlock
                unlocked = upgradeLibrary[upgradeName.HarvestingI].isEnabled;
                break;
            case upgradeName.HarvestingII:
                //Grab and return the state of unlock
                unlocked = upgradeLibrary[upgradeName.HarvestingI].isEnabled;
                break;
            default: break;
        }
        return unlocked;
    }

}

public class Upgrade
{
    public float peonModifier { get; set; }
    public float buttonModifier { get; set; }
    public bool isEnabled { get; set; }

}
