using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variable for updating the slider UI
    public Slider eatSlider;
    [SerializeField] private float maxEatTime = 10f;
    [SerializeField] private float currentEatTime = 0.0f;
    [SerializeField] private bool canEat = false;

    [SerializeField] private GameObject portals;
    [SerializeField] private GameObject teleporterOne;
    [SerializeField] private GameObject teleporterTwo;
    [SerializeField] private GameObject last;
    GameObject GM;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip deathSound;

    private bool canTeleport = true;
    public bool eatPower = false;
    [SerializeField] private bool isDouble = false;

    public Vector3 startPos;

    // Sets up the player with initial values
    void Start()
    {
        startPos = transform.position;
        GM = GameObject.FindGameObjectWithTag("GM");
        teleporterOne = portals.transform.GetChild(0).gameObject;
        teleporterTwo = portals.transform.GetChild(1).gameObject;
        Reset();
    }

    // Updates the eat functionality and slider
    void Update()
    {

        //Checks if the player can eat and if not begins to fill the bar
        if (currentEatTime >= maxEatTime)
        {
            canEat = true;
        }
        else
        {
            canEat = false;
            currentEatTime += Time.deltaTime;
            currentEatTime = Mathf.Clamp(currentEatTime, 0.0f, maxEatTime);
        }
        if (eatPower)
            eatSlider.value = 1;
        else
            eatSlider.value = currentEatTime / maxEatTime;
    }

    // Checks collisions and conducts various functions according to the collision detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Clone")
        {
            if (canEat || eatPower)
            {
                collision.GetComponent<EnemyNavigation>().Die();
                currentEatTime = 0f;
            }
            else
            {
                GM.GetComponent<Reset>().ResetEverything();
                GM.GetComponent<GameOver>().decrementLives();
                if (PlayerPrefs.GetInt("SoundEffects", 1) == 1)
                    source.PlayOneShot(deathSound);
            }
        }
        else if (collision.tag == "GhostEnemy")
        {
            GM.GetComponent<Reset>().ResetEverything();
            GM.GetComponent<GameOver>().decrementLives();
            if(PlayerPrefs.GetInt("SoundEffects", 1) == 1)
                source.PlayOneShot(deathSound);
        }
        else if (collision.tag == "Point" && isDouble)
        {
            GM.GetComponent<Score>().score += 100;
        }
        else if (collision.tag == "Portal")
        {
            if (canTeleport)
            {
                canTeleport = false;
                if (collision.gameObject == teleporterOne)
                {
                    transform.position = teleporterTwo.transform.position;
                    last = teleporterTwo;
                }
                else
                {
                    transform.position = teleporterOne.transform.position;
                    last = teleporterOne;
                }
                this.GetComponent<CharacterMovement>().target = transform.position;
            }
        }
    }
    // Prevents the player from sling-shotting between teleporters
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Portal")
        {
            if(last == collision.gameObject)
                canTeleport = true;
        }
    }
    //Resets the player object to starting position
    public void Reset()
    {
        transform.position = startPos;
        currentEatTime = 0;
        this.GetComponent<CharacterMovement>().target = this.transform.position;
    }
    public IEnumerator EatPowerDown()
    {
        yield return new WaitForSeconds(5.0f);

        canEat = false;
        eatPower = false;
        currentEatTime = 10f;
    }

    // All the following functions are used for power up functionality
    public void EatPowerUp()
    {
        canEat = true;
        eatPower = true;

        StartCoroutine(EatPowerDown());
    }

    public void SpeedBoostPowerUp()
    {
        this.GetComponent<CharacterMovement>().speed *= 1.5f;

        StartCoroutine(SpeedBoostPowerDown());
    }

    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);

        this.GetComponent<CharacterMovement>().speed /= 1.5f;
    }

    public void DoublePointsPowerUp()
    {
        isDouble = true;

        StartCoroutine(DoublePointsPowerDown());
    }

    public IEnumerator DoublePointsPowerDown()
    {
        yield return new WaitForSeconds(5.0f);

        isDouble = false;
    }

}
