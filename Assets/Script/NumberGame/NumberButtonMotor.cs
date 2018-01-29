using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButtonMotor : MonoBehaviour
{

    public int Number;
    public Text numberText;
    private bool isSelected = false;

    public void Start()
    {
        numberText.text = Number.ToString();
    }

    public int GetNumber()
    {
        return Number;
    }

    public void ChangeColorToSelected()
    {
        GetComponent<Image>().color = Color.red;

    }

    public void ChangeColorToUnselected()
    {
        GetComponent<Image>().color = Color.white;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
