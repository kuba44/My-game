using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenParts : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    private Vector3 movementDirection;

    [SerializeField] float haltingFactor;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection.x = Random.Range ( -movementSpeed, movementSpeed );
        movementDirection.y = Random.Range ( -movementSpeed, movementSpeed );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * Time.deltaTime;

        movementDirection = Vector3.Lerp ( movementDirection, Vector3.zero, haltingFactor * Time.deltaTime );
    }
}
