using UnityEngine;

public class FollowPlayerTarget : MonoBehaviour {
    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

     void FixedUpdate() {
        transform.position = player.position;
    }

}