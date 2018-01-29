using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceshipMotor : MonoBehaviour {

    public static ColorGateSpawner Instance { set; get; }

    public AudioClip scoreSound;
    public AudioClip livesLostSound;
    private AudioSource source;
    public DeathMenu deathMenu;

    private CharacterController controller;
    public ColorGateObject colorGate;
    private int spaceshipColor = 1;
    Material mat;

    private int lives = 3;
    private int score = 0;
    private bool isAlive = true;
    public Text livesText;
    public Text scoreText;
    public Text colorText;
    int[] colorTable = new int[3];

    // Movement
    private float speed = 4.0f;
    private int desiredLane = 1;    // 0 = left, 1 = middle, 2 = right
    private const float LANE_DISTANCE = 2.0f;
    private bool bCanMoveLanes = true;
    private int TriggerCounter = 0;
    private float Acceleration = 0.0f;
    private float AccelerationPerGate = 0.075f;

    private void Awake()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //ColorGateSpawner.Instance.SpawnCubes(0);
        ChangeSpaceShipColor();
        source.Play();
    }

    private void Update()
    {
        if (isAlive)
        {
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
            Vector3 targetPosition = transform.position.y * Vector3.up;        // always move up
            if (desiredLane == 0)
                targetPosition += Vector3.left * LANE_DISTANCE;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * LANE_DISTANCE;

            // Let's calculate our move vector
            Vector3 moveVector = Vector3.zero;

            moveVector.x = (targetPosition - transform.position).x * speed * 10;
            moveVector.y = Mathf.Clamp(speed + Acceleration, speed, 12.0f);

            // Move spaceship
            if (!bCanMoveLanes)
                moveVector.x = 0;

            controller.Move(moveVector * Time.deltaTime);

        }

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Winbox":
                if (PlayerPrefs.GetInt("PlayerScore") < score)
                {
                    PlayerPrefs.SetInt("PlayerScore", score);
                }
                EndGame();
                break;
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        bCanMoveLanes = false;
        if (hit.gameObject.tag == "ColorGate")
        {
            TriggerCounter++;
            int i = hit.gameObject.GetComponent<ColorGateObject>().GetColor();

            if (i == spaceshipColor)
            {
                // cross through and add score
                score++;
                scoreText.text = "Pikë: " + score.ToString();
                source.PlayOneShot(scoreSound, 0.5f);
            }
            else
            {
                // death or lives--
                lives--;
                livesText.text = "Jetë: " + lives.ToString();
                source.PlayOneShot(livesLostSound, 0.5f);
                if (lives <= 0)
                {
                    EndGame();
                }
            }

        }
        if (TriggerCounter == 1)
            ColorGateSpawner.Instance.SpawnCubes((int)transform.position.y);
    }

    private void OnTriggerExit(Collider other)
    {
        bCanMoveLanes = true;
        ChangeSpaceShipColor();
        TriggerCounter = 0;
        Acceleration += AccelerationPerGate;
        if (Acceleration >= 10.0f)
        {
            Acceleration += 0.05f;
        }
    }

    private void ChangeSpaceShipColor()
    {
        mat = GetComponent<Renderer>().material;
        colorTable = ColorGateSpawner.Instance.GetColorTable();

        int i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                spaceshipColor = colorTable[0];
                break;
            case 2:
                spaceshipColor = colorTable[1];
                break;
            case 3:
                spaceshipColor = colorTable[2];
                break;
            default:
                break;
        }
        
        switch (spaceshipColor)
        {
            case 1:
                mat.color = Color.red;
                colorText.text = "Kuq";
                colorText.color = Color.red;
                break;
            case 2:
                mat.color = Color.blue;
                colorText.text = "Kaltër";
                colorText.color = Color.blue;
                break;
            case 3:
                mat.color = Color.green;
                colorText.text = "Gjelbër";
                colorText.color = Color.green;
                break;
            case 4:
                mat.color = Color.yellow;
                colorText.text = "Verdhë";
                colorText.color = Color.yellow;
                break;
            case 5:
                mat.color = Color.magenta;
                colorText.text = "Rozë";
                colorText.color = Color.magenta;
                break;
            case 6:
                mat.color = Color.white;
                colorText.text = "Bardhë";
                colorText.color = Color.white;
                break;
            case 7:
                mat.color = Color.gray;
                colorText.text = "Hiri";
                colorText.color = Color.gray;
                break;
            case 8:
                mat.color = new Color(1.0f, 0.56f, 0);  //orange
                colorText.text = "Portokall";
                colorText.color = new Color(1.0f, 0.56f, 0);
                break;
            case 9:
                mat.color = new Color(0.33f, 0.24f, 0);     //brown
                colorText.text = "Kaftë";
                colorText.color = new Color(0.33f, 0.24f, 0);
                break;
            case 10:
                mat.color = new Color(0.46f, 0.0f, 0.8f);      //purple
                colorText.text = "Vjollcë";
                colorText.color = new Color(0.46f, 0.0f, 0.8f);
                break;
            default:
                break;
        }
    }

    private void EndGame()
    {
        isAlive = false;
        deathMenu.ToggleEndMenu(score);
    }
}
