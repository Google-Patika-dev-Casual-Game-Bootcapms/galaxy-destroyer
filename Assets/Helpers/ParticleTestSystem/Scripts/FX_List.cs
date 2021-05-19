using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_List : MonoBehaviour
{
    public List<GameObject> allFX;
    public GameObject dropdown;

    void Start()
    {
        allFX = new List<GameObject>(Resources.LoadAll<GameObject>("FX"));
        UI_Dropdown_Handler dropdown_Handler = dropdown.GetComponent<UI_Dropdown_Handler>();
        dropdown_Handler.initiate_From_FX_List();
        dropdown_Handler.populate_UI(allFX);
    }


    void Update()
    {
        
    }
}
