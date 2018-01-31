using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeContainerMotor : MonoBehaviour {

    Material mat;
    public Material squareMat, circleMat, triangleMat, pentagonMat, hexagonMat, starMat;
    private int shape;      // 0 = square, 1 = circle, 2 = triangle, 3 = pentagon, 4 = hexagon, 5 = star

	// Use this for initialization
	void Start () {
        //ChangeShape();
    }

    public int ChangeShape()
    {
        mat = GetComponent<Renderer>().material;

        int i = Random.Range(0, 6);

        switch (i)
        {
            case 0:
                GetComponent<Renderer>().material = squareMat;
                break;
            case 1:
                GetComponent<Renderer>().material = circleMat;
                break;
            case 2:
                GetComponent<Renderer>().material = triangleMat;
                break;
            case 3:
                GetComponent<Renderer>().material = pentagonMat;
                break;
            case 4:
                GetComponent<Renderer>().material = hexagonMat;
                break;
            case 5:
                GetComponent<Renderer>().material = starMat;
                break;
            default:
                break;
        }
        shape = i;
        return i;
    }

    public int GetShape()
    {
        return shape;
    }
}
