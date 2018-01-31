using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeContainerManager : MonoBehaviour {

    public static ShapeContainerManager Instance { set; get; }

    private Vector3 spawnPoint = new Vector3(0, 15.0f, 0);
    public GameObject shapeContainer1;
    public GameObject shapeContainer2;
    public GameObject shapeContainer3;
    private Vector3 spawnOffsetLeft = new Vector3(-2, 0, 0);
    private Vector3 spawnOffsetCenter = new Vector3(0, 0, 0);
    private Vector3 spawnOffsetRight = new Vector3(2, 0, 0);

    int shape1, shape2, shape3;
    int[] shapeTable = new int[3];
    private int shapeCount = 0;

    // Use this for initialization
    void Awake () {
        Instance = this;
        SetShapes();
	}

    public void SetShapes()
    {
        shape1 = shapeContainer1.gameObject.GetComponent<ShapeContainerMotor>().ChangeShape();
        while (shape1 == shape2 || shape2 == shape3 || shape1 == shape3)
        {
            shape2 = shapeContainer2.gameObject.GetComponent<ShapeContainerMotor>().ChangeShape();
            shape3 = shapeContainer3.gameObject.GetComponent<ShapeContainerMotor>().ChangeShape();
        }

        // Prepare shape array to send to shapemotor
        shapeTable[0] = shape1;
        shapeTable[1] = shape2;
        shapeTable[2] = shape3;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetShapes();
    }

    public int[] GetShapeTable()
    {
        return shapeTable;
    }
}
