using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGateSpawner : MonoBehaviour {

    public static ColorGateSpawner Instance { set; get; }

    private Vector3 spawnPoint = new Vector3(0, 15.0f, 0);
    public GameObject cube;
    private GameObject gate1;
    private GameObject gate2;
    private GameObject gate3;
    private Vector3 spawnOffsetLeft = new Vector3(-2,0,0);
    private Vector3 spawnOffsetCenter = new Vector3(0, 0, 0);
    private Vector3 spawnOffsetRight = new Vector3(2, 0, 0);

    int color1, color2, color3;
    int[] colorTable = new int[3];
    private int gateCount = 0;

    public void Awake()
    {
        Instance = this;
        SpawnCubes(0);
    }

    public void SpawnCubes(int currentSpaceShipDistance)
    {
        // Spawn new cubes
        Vector3 spaceshipOffset = new Vector3(0, currentSpaceShipDistance, 0);
        gate1 = Instantiate(cube, spawnPoint + spawnOffsetLeft + spaceshipOffset, transform.rotation);
        gate2 = Instantiate(cube, spawnPoint + spawnOffsetCenter + spaceshipOffset, transform.rotation);
        gate3 = Instantiate(cube, spawnPoint + spawnOffsetRight + spaceshipOffset, transform.rotation);
    }

    public void SetColors()
    {
        color1 = gate1.gameObject.GetComponent<ColorGateObject>().GetColor();
        color2 = gate2.gameObject.GetComponent<ColorGateObject>().GetColor();
        color3 = gate3.gameObject.GetComponent<ColorGateObject>().GetColor();

        // Prepare color array to send to spaceship
        colorTable[0] = color1;
        colorTable[1] = color2;
        colorTable[2] = color3;
    }

    public int[] GetColorTable()
    {
        return colorTable;
    }

    public void GateCount()
    {
        // This is so we only call SetColors after all the gates have had time to initialize their color values
        gateCount++;
        if (gateCount == 3)
        {
            SetColors();
            gateCount = 0;
        }
    }
}
