using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public bool dropsItems;
    [SerializeField] GameObject[] itemsDrops;
    [SerializeField] float dropChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        float a = Random.value;
        
        if ( !dropsItems || a < dropChance ) return;

        int randomItemNumber = Random.Range( 0, itemsDrops.Length );

        Instantiate( itemsDrops[randomItemNumber], transform.position, transform.rotation );
    }
}
