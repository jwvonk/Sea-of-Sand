using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BoatPushScript : MonoBehaviour
{

    //public Transform _playerTransform;
    public Transform _fluidTransform;
    public Transform _playerTransform;
    public CharacterController _controller;
    public float pushSpeed = 10;
    public float pushDist = 10;
    public float riseSpeed = 50;
    public float riseDist = 10;
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _fluidTransform = GameObject.FindGameObjectWithTag("Fluid").GetComponent<Transform>();
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Fluid").GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(raise());
        StartCoroutine(push());
    }

    private IEnumerator push()
    {
        Vector3 impact = transform.position;
        impact.y = 0;

        Vector3 direction = _playerTransform.position - impact;
        direction.y = 0;

        direction.Normalize();

        var dist = Vector3.Distance(new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z), impact);

        while (dist < pushDist)
        {
            var move = pushSpeed * Time.deltaTime;
            if (dist + move > pushDist)
            {
                _controller.Move(direction * (pushDist - dist));
            }
            else
            {
                _controller.Move(direction * move);
            }
            dist += move;
            yield return null;
        }
        Destroy(gameObject);



        //Vector3 dest = impact + (direction * pushDist);


        //while (Vector3.Distance(new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z), dest) > 0)

        //{
        //    Vector3 postPush = _playerTransform.position + direction * pushSpeed * Time.deltaTime;
        //    postPush.y = 0;
        //    if (Vector3.Distance(postPush, impact) > pushDist)
        //    {
        //        _playerTransform.position = new Vector3(dest.x, _playerTransform.position.y, dest.z);
        //    }
        //    else
        //    {
        //        _playerTransform.position = new Vector3(postPush.x, _playerTransform.position.y, postPush.z);

        //    }
        //    yield return null;
        //}
        //Destroy(gameObject);


    }

    private IEnumerator raise()
    {
        float targetLevel = _fluidTransform.position.y + riseDist;
        while (_fluidTransform.position.y < targetLevel)
        {
            if (_fluidTransform.position.y + (riseSpeed * Time.deltaTime) < targetLevel)
            {
                _playerTransform.position += new Vector3(0f, riseSpeed, 0f) * Time.deltaTime;
                _fluidTransform.position += new Vector3(0f, riseSpeed, 0f) * Time.deltaTime;
            }
            else
            {
                _playerTransform.position = new Vector3(_playerTransform.position.x, targetLevel, _playerTransform.position.z);
                _fluidTransform.position = new Vector3(_fluidTransform.position.x, targetLevel, _fluidTransform.position.z);
            }
               
            yield return null;
        }
    }
}
