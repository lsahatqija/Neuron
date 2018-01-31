using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShapeMotor : MonoBehaviour {

    private CharacterController controller;
    public static ShapeContainerManager Instance { set; get; }
    Material mat;
    public Material squareMat, circleMat, triangleMat, pentagonMat, hexagonMat, starMat;

    public GameObject pauseButton;
    public DeathMenu deathMenu;

    private int shape;      // 0 = square, 1 = circle, 2 = triangle, 3 = pentagon, 4 = hexagon, 5 = star
    public Text shapeText;
    private int score = 0;
    public Text scoreText;
    private int gatesPassed = 0;
    private int gatesPerLevel = 15;
    private int[] shapeTable = new int[3];

    private float speed = 4.0f;
    private int desiredLane = 1;    // 0 = left, 1 = middle, 2 = right
    private const float LANE_DISTANCE = 2.0f;
    private bool bCanMoveLanes = true;

    private bool isAlive = true, isPaused = false;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        ChangeShape();
        scoreText.text = "Score: " + score.ToString();
        pauseButton.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive && !isPaused)
        {
            if (gatesPassed >= gatesPerLevel)
            {
                EndGame();
            }
            // Gather the input on which lane we should be
            if (Swipe.Instance.SwipeLeft)
            {
                MoveLane(false);
            }
            if (Swipe.Instance.SwipeRight)
            {
                MoveLane(true);
            }

            // Calculate where we should be
            Vector3 targetPosition = transform.position.y * Vector3.down;        // always move down
            if (desiredLane == 0)
                targetPosition += Vector3.left * LANE_DISTANCE;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * LANE_DISTANCE;

            // Let's calculate our move vector
            Vector3 moveVector = Vector3.zero;

            moveVector.x = (targetPosition - transform.position).x * speed * 5;
            moveVector.y = -speed;

            // Move spaceship
            if (!bCanMoveLanes)
                moveVector.x = 0;

            controller.Move(moveVector * Time.deltaTime);
        }
    }

    private void MoveLane(bool goingRight)
    {
        if (bCanMoveLanes)
        {
            desiredLane += (goingRight) ? 1 : -1;
            desiredLane = Mathf.Clamp(desiredLane, 0, 2);
        }
    }

    public void ChangeShape()
    {
        shapeTable = ShapeContainerManager.Instance.GetShapeTable();
        mat = GetComponent<Renderer>().material;

        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                shape = shapeTable[0];
                break;
            case 1:
                shape = shapeTable[1];
                break;
            case 2:
                shape = shapeTable[2];
                break;
            default:
                break;
        }

        switch (shape)
        {
            case 0:
                GetComponent<Renderer>().material = squareMat;
                shapeText.text = "Katror";
                break;
            case 1:
                GetComponent<Renderer>().material = circleMat;
                shapeText.text = "Rreth";
                break;
            case 2:
                GetComponent<Renderer>().material = triangleMat;
                shapeText.text = "Trekëndësh";
                break;
            case 3:
                GetComponent<Renderer>().material = pentagonMat;
                shapeText.text = "Pesëkëndësh";
                break;
            case 4:
                GetComponent<Renderer>().material = hexagonMat;
                shapeText.text = "Gjashtëkëndësh";
                break;
            case 5:
                GetComponent<Renderer>().material = starMat;
                shapeText.text = "Yll";
                break;
            default:
                Debug.Log("nothing lol");
                break;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShapeGate"){
            if (other.gameObject.GetComponent<ShapeContainerMotor>().GetShape() == shape)
            {
                score++;
            }
            scoreText.text = "Score: " + score.ToString();
            bCanMoveLanes = false;
        } else if (other.gameObject.tag == "ShapeGateManager")
        {
            this.transform.position = new Vector3(0, 6, 0);
            desiredLane = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ShapeGate")
        {
            ChangeShape();
            bCanMoveLanes = true;
            gatesPassed++;
        }
    }

    private void EndGame()
    {
        isAlive = false;
        pauseButton.SetActive(false);
        AdManager.AdInstance.ShowAd();
        deathMenu.ToggleEndMenu(score);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            deathMenu.ToggleEndMenu(score);
            isPaused = true;
        }
        else
        {
            deathMenu.UnToggleEndMenu();
            isPaused = false;
        }
    }
}
