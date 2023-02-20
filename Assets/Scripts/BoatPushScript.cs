using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatPushScript : MonoBehaviour
{

    public Transform _playerTransform;
    public float pushSpeed = 10;
    public float pushDist = 10;
    private bool fallen = false;
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(push());
    }

    private IEnumerator push()
    {
        Debug.Log("Function Called");
        Vector3 impact = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 direction = _playerTransform.position - transform.position;
        direction.Normalize();
        while (Vector3.Distance(_playerTransform.position, impact) < pushDist)
        {
            _playerTransform.position += (direction * pushSpeed) * Time.deltaTime;
            yield return null;
        }

    }
}
