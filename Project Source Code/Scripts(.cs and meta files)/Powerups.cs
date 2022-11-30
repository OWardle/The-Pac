using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{

    [SerializeField] private int powerupID = 0;

    // Applies a random power up to the player then destroys the object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player P = collision.GetComponent<Player>();
            powerupID = Random.Range(0, 3);

            if (P != null)
            {
                if (powerupID == 0)
                {
                    P.EatPowerUp();
                }
                else if (powerupID == 1)
                {
                    P.SpeedBoostPowerUp();
                }
                else if (powerupID == 2)
                {
                    P.DoublePointsPowerUp();
                }

            }

            Destroy(this.gameObject);
        }
    }
}