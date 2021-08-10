using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the player state
    /*This script should be on the empty player parent object. Functionally it should be able to determine
    which 'world' the player is in and switch its state accordingly. */
public enum ViewState { STATE_TOP, STATE_SIDE, STATE_ISOMETRIC }
public class StateManager : MonoBehaviour
{
    //init
    private ViewState vState_;
    public Camera[] camArr;
    public GameObject[] playerBodyArr;
    public Collider[] colArr;


    //Some function to determine where the player is -> which "world" the player is in.
        //**This function will determine the state of the player
    void CheckPlayerPos()
    {
        //iterate through colider list to check player position
        //this will def need to be changed to accommodate raycasts 
        foreach (Collider col in colArr)
        {
            if(col.bounds.Contains(gameObject.transform.position))
            {
                if (col.tag == "TOP_Colider")
                    vState_ = ViewState.STATE_TOP;
                else if (col.tag == "SIDE_Colider")
                    vState_ = ViewState.STATE_SIDE;
                else if (col.tag == "ISO_Colider")
                    vState_ = ViewState.STATE_ISOMETRIC;
                else 
                    Debug.Log("WHERE IS THE FUCKING PLAYER??");
            }
        }
    }


    void Update()
    {
        CheckPlayerPos();

        switch(vState_)
        {
            case ViewState.STATE_TOP:
                Debug.Log("TopDown");
                //Change camera if needed
                camArr[0].enabled = true;
                camArr[1].enabled = false;

                //Change player body
                break;
            case ViewState.STATE_SIDE:
                Debug.Log("SideScroller");
                //Change camera if needed
                camArr[0].enabled = true;
                camArr[1].enabled = false;

                //Change player body
                break;
            case ViewState.STATE_ISOMETRIC:
                Debug.Log("Isometric");
                //Change camera if needed
                camArr[1].enabled = true;
                camArr[0].enabled = false;

                //Change player body
                break;
        }
    }

    public ViewState GetCurState()
    {
        return vState_;
    }
}
