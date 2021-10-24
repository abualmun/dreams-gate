using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Singletons
    Player player;
    Spawner spawner;

    public int partsToWin;
    public float levelTime;

    void Start() {
        player = Player.player;
        spawner = Spawner.spawner;
    }


    void Update() {
        levelTime -= Time.deltaTime;

        // if time is over
        if (levelTime <= 0) {
            spawner.EndGame();
        }

        if (player.isDead) {
            LoseGame();
        }

        if (player.reachedEnd) {
            if (player.collectedParts >= partsToWin) {
                WinGame();
            } else {
                LoseGame();
            }
        }
    }

    void WinGame() {
        print("Win!!");
    }

    void LoseGame() {
        print("Lose :(");
    }
}
