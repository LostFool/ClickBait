//Devon Wenskunas
//Oct 2017
//Clicker One
using UnityEngine;
using System;
using System.Timers;
using UnityEngine.EventSystems;

//Handles when buttons are pressed
//Handles peon's assigned to tasks
//      Peon buttons should be assign, remove and each one should feed the jobType to 
//          a method that handles what to do for each type
public enum jobType { empty, Farmer }

public class buttonController : MonoBehaviour
{

    public GameObject resourceManager_GO;
    public GameObject upgradeManger_GO;
    public GameObject mainMenuPanel, rescPanel, craftPanel, resrchPanel, peonPanel, footerPanel;

    private GameObject tempObject;
    private GameObject currPanel;
    private GameObject[] menuList;

    Timer peonTimer;
    resourceManager resourceMan;
    upgradeManager upgradeMan;
    peonManager peonMan;

    bool temp, buttonActive = true;


    public void Awake()
    {
        //load in current upgrade gameobject (contains progress)
        resourceMan = resourceManager_GO.GetComponent<resourceManager>();
        upgradeMan = upgradeManger_GO.GetComponent<upgradeManager>();
        peonMan = new peonManager(5, 5);
        setPeonTimer();
        menuList = new GameObject[]
         {
            rescPanel, craftPanel, resrchPanel, peonPanel
         };
    }

    //Universal method for Menu Buttons
    //Checks for which button was pressed by string passed
    //Activates and deactivates appropriate panels
    public void menuButtons( string buttonName)
    {
        switch (buttonName)
        {
            case "resource":
                mainMenuPanel.SetActive(false);
                footerPanel.SetActive(true);
                rescPanel.SetActive(true);
                break;
            case "research":
                mainMenuPanel.SetActive(false);
                footerPanel.SetActive(true);
                resrchPanel.SetActive(true);
                break;
            case "peon":
                mainMenuPanel.SetActive(false);
                footerPanel.SetActive(true);
                peonPanel.SetActive(true);
                break;
            case "craft":
                mainMenuPanel.SetActive(false);
                footerPanel.SetActive(true);
                craftPanel.SetActive(true);
                break;
            case "main":
                GetCurrPanel().SetActive(false);
                footerPanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                break;
        }
    }

    //Prototype for peon assignment
    public void addPeonButton(string job)
    {
        //Use string Job and tempJob later to display current tasked per job type
        //
        //Pass string Job to StringToJobType Method, to use for comparison

        //jobType tempJob;
        //tempJob = StringToJobType(job);

        if (checkUntaskedPeon())
        {
            if (job == "Farmer") peonMan.incFarmers();
        }
        else Debug.Log("No Peon Available to Task");
    }

    public void updatePeonButton()
    {

        peonTimer.Enabled = buttonActive;
        if (buttonActive)
            buttonActive = false;
        else if (!buttonActive)
            buttonActive = true;
    }

    //Prototype for removing peon
    public void removePeonButton()
    {

    }

    //protoype for gathering button
    public void clickMeButton()
    {
        resourceMan.updateCount(rescType.Food, sourceType.Button);
    }

    //prototype for Upgrade purchase button
    //Disapears after clicked
    public void purchaseButton()
    {
        upgradeMan.purchase(upgradeName.HarvestingI, true);
        tempObject = GameObject.Find("Purchase Button");
        tempObject.SetActive(false);
        Debug.Log(tempObject.ToString() + " :: disabled");
    }

    //Method for finding which menu Panel is active
    private GameObject GetCurrPanel()
    {
        for (int i = 0; i < menuList.Length; i++)
        {
            if (menuList[i].activeInHierarchy) currPanel = menuList[i];
        }
        return currPanel;
    }

    //Peon assignment Handler
    private bool checkUntaskedPeon()
    {
        bool peonAvailable = false;

        if (peonMan.peonUntasked > 0)
        {
            peonAvailable = true;
        }

        return peonAvailable;
    }

    private jobType StringToJobType(string stringJob)
    {
        jobType passMe = new jobType();
        if (stringJob.Equals("Farmer"))
        {
            passMe = jobType.Farmer;
        }
        if (passMe == jobType.empty)
        {
            Debug.LogError("No jobType found for StringToJobType " + passMe.ToString());
        }
        return passMe;
    }

    //Runs every second to gather resources Peons are assigned to
    private void runPeons()
    {
        jobType[] types = jobTypeArray();

        for (int i = 1; i < types.Length; i++)
        {
            resourceMan.updateCount(types[i], sourceType.Peon, peonMan.taskedToJob(types[i]));
        }

    }

    private jobType[] jobTypeArray()
    {
        jobType[] types;
        string[] stringTypes;

        stringTypes = Enum.GetNames(typeof(jobType));
        types = new jobType[stringTypes.Length];

        for (int i = 1; i < stringTypes.Length; i++)
        {
            types[i] = StringToJobType(stringTypes[i]);
        }

        return types;
    }

    //Runs every second and subtracts appropriate amount of food for number of peons that exist
    private void feedPeons()
    {

    }

    private void setPeonTimer()
    {
        peonTimer = new Timer(1000);
        peonTimer.Elapsed += PeonTimer_Elapsed;
        peonTimer.AutoReset = true;
    }

    private void PeonTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        runPeons();
    }
}
