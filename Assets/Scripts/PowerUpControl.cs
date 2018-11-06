using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour
{
    public PowerUp powerUp;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var scriptPlayer = other.gameObject.GetComponent<Player>();
            GetComponent<BoxCollider2D>().enabled = false;
            scriptPlayer.totalLifes += powerUp.extraLife;
            scriptPlayer.lifes += powerUp.lifeRegen;
            scriptPlayer.damage += powerUp.damage;
            scriptPlayer.fireRate += powerUp.fireRate;
            scriptPlayer.shotSpeed += powerUp.shotSpeed;
            scriptPlayer.speed += powerUp.speed;
            scriptPlayer.luck += powerUp.luck;
            scriptPlayer.bombs += powerUp.bombs;
            scriptPlayer.money += powerUp.money;
            Destroy(gameObject);
        }
    }
}
