using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour {
    static public ValueManager valueManager;

    public float speed;
    public float radius;

    void Awake() {
        valueManager = this;
    }
}
