using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dropdown_Handler : MonoBehaviour
{
    public Dropdown dropdown;
    public List<GameObject> the_FX;
    public GameObject gun_Point;
    public GameObject fx_Pointer;
    public List<GameObject> allFX;

    void Start()
    {
        allFX = new List<GameObject>(Resources.LoadAll<GameObject>("FX"));
        initiate_From_FX_List();
        populate_UI(allFX);
    }

    public void initiate_From_FX_List()
    {
        dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        dropdown.options.Add(new Dropdown.OptionData() { text = fx_Pointer.gameObject.name });
        the_FX.Add(fx_Pointer.gameObject);
        fx_Pointer = Instantiate(fx_Pointer, gun_Point.transform);
    }


    public void populate_UI(List<GameObject> fx)
    {
        foreach (var effect in fx)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = effect.gameObject.name });
            the_FX.Add(effect.gameObject);
        }
    }

    public void dropdown_Value_Changed(int k)
    {
        Destroy(fx_Pointer.gameObject);
        Debug.Log(the_FX[k]);
        fx_Pointer = Instantiate(the_FX[k], gun_Point.transform);
    }
}
