using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose;
    [SerializeField] bool closeDoorOnPlayerEnter, openDoorsAfterEnemiesDeath;
    [SerializeField] List<Collider2D> enemies = new List<Collider2D>();

    private Collider2D roomCollider;
    private ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        roomCollider = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        roomCollider.OverlapCollider(contactFilter, enemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (closeDoorOnPlayerEnter)
            {
                foreach(GameObject door in doorsToClose)
                {
                    door.SetActive(true);
                }
            }
        }
    }

}
