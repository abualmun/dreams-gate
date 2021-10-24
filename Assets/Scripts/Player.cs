using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Singltons
    static public Player player;

    [Header("Movement")]
    public float rotSpeed;
    float speed;
    float h;
    float v;
    float radius;

    [Header("Objects Assignment")]
    public Transform body;
    Rigidbody rb;

    [Header("Game Status")]
    public int collectedParts = 0;
    public bool isDead = false;
    public bool reachedEnd = false;

    void Awake() {
        player = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        speed = ValueManager.valueManager.speed;
        radius = ValueManager.valueManager.radius;
        body.transform.localPosition = Vector3.down * radius;
    }

    void Update() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        rb.position += transform.forward * speed * Time.deltaTime;

        var newRot = transform.rotation * Quaternion.Euler(Vector3.forward * rotSpeed * h * Time.deltaTime);
        var angle = (newRot.eulerAngles.z + 90) % 360;

        // clamp rotation
        rb.rotation = Quaternion.Euler(
            newRot.eulerAngles.x,
            newRot.eulerAngles.y,
            (angle < 180) ? newRot.eulerAngles.z : (angle > 270) ? -90 : 90
        );
    }

    void OnCollisionEnter(Collision other) {
        GetComponentInChildren<Renderer>().material.color = Color.red; // TODO: remove me

        switch (other.collider.tag) {
            case "EndGame":
                reachedEnd = true;
                break;
            default:
                isDead = true;
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
        collectedParts++;
    }
}
