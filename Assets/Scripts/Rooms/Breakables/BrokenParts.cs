using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenParts : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    private Vector3 movementDirection;

    [SerializeField] float haltingFactor;

    [SerializeField] float lifeTime;
    [SerializeField] float fadeSpeed;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection.x = Random.Range ( -movementSpeed, movementSpeed );
        movementDirection.y = Random.Range ( -movementSpeed, movementSpeed );

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * Time.deltaTime;

        movementDirection = Vector3.Lerp ( movementDirection, Vector3.zero, haltingFactor * Time.deltaTime );
    
        lifeTime -= Time.deltaTime;

        if( lifeTime < 0)
        {
            sprite.color = new Color ( 
                sprite.color.r, 
                sprite.color.g, 
                sprite.color.b, 
                Mathf.MoveTowards( sprite.color.a, 0f, fadeSpeed * Time.deltaTime )
                );

            if ( sprite.color.a == 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
