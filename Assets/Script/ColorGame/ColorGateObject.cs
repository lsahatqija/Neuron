using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGateObject : MonoBehaviour {

    Material mat;
    private int color;

    // colors: red (1), blue (2), green (3)
    // to add later: yellow (4), magenta (5), white (6)
    // gray (7), orange (8), brown (9), purple (10)

    private void Start()
    {
        mat = GetComponent<Renderer>().material;

        int i = Random.Range(1, 11);
        switch (i)
        {
            case 1:
                mat.color = Color.red;
                color = 1;
                break;
            case 2:
                mat.color = Color.blue;
                color = 2;
                break;
            case 3:
                mat.color = Color.green;
                color = 3;
                break;
            case 4:
                mat.color = Color.yellow;
                color = 4;
                break;
            case 5:
                mat.color = Color.magenta;
                color = 5;
                break;
            case 6:
                mat.color = Color.white;
                color = 6;
                break;
            case 7:
                mat.color = Color.gray;
                color = 7;
                break;
            case 8:
                mat.color = new Color(1.0f, 0.56f, 0);  //orange
                color = 8;
                break;
            case 9:
                mat.color = new Color(0.33f, 0.24f, 0);     //brown
                color = 9;
                break;
            case 10:
                mat.color = new Color(0.46f, 0.0f, 0.8f);      //pruple
                color = 10;
                break;
            default:
                break;
        }

        ColorGateSpawner.Instance.GateCount();
    }

    public int GetColor()
    {
        return color;
    }

}
