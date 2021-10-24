using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    // Singltons
    static public Spawner spawner;

    [Header("Object to Spawn")]
    public GameObject car;
    public GameObject part;
    public GameObject endGame;

    public float timeBetween;
    public int numOfParts; // max number of parts
    float spawnRadius;
    float newTime;

    void Awake() {
        spawner = this;
    }

    void Start() {
        spawnRadius = ValueManager.valueManager.radius;
        part.GetComponent<Renderer>().material.color = Color.green;
    }


    void Update() {
        transform.Translate(Vector3.forward * ValueManager.valueManager.speed * Time.deltaTime);

        if (Time.time > newTime) {
            newTime = Time.time + timeBetween;

            var randomAngle = -Random.Range(0, Mathf.PI);
            var newPoint = new Vector3(
                spawnRadius * Mathf.Cos(randomAngle),
                spawnRadius * Mathf.Sin(randomAngle)
            );

            var newPart = Random.value > 0.8f;

            if (newPart) numOfParts--;

            Instantiate(
                newPart ? part : car,
                transform.position + newPoint,
                Quaternion.Euler(Vector3.up * 90)
            );
        }
    }

    public void EndGame() {
        Instantiate(endGame, transform.position, Quaternion.identity);
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
