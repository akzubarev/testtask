using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameController.completelevel();
        other.attachedRigidbody.simulated=false;
    }
}
