using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatPushScript : MonoBehaviour
{

    // public CharacterController _controller;
    // public GameObject _player;
    public Transform transform;
    // public Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        // _player = GameObject.FindGameObjectWithTag("Player");
        // _controller = _player.GetComponent<CharacterController>();
        // _direction = transform.position - _player.transform.position;
        transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionStay (Collision collision)
    {
        Debug.Log("Collision Detcted");
        // _controller.Move(_direction.normalized * Time.deltaTime);
        // _controller.Move(Vector3.left * Time.deltaTime);
        transform.position = transform.position + (Vector3.left);
    }
}