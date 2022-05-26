using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose;
    private bool closeDoorOnPlayerEnter = true;

    // Start is called before the first frame update
    void Start()
    {
        
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
