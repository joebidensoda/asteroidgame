using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public movemen player;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int lives = 3;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        if (asteroid.size < 0.75f) {
            this.score += 100;
        } else if (asteroid.size < 1.0f) {
            this.score += 50;
        } else {
            this.score += 25;
        }
    }
    public void PlayerDied()
    {
        this.lives--;

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
        

        
    }
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("ignore collisions");
        this.player.gameObject.SetActive(true);
        
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
         this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
       
    }
}