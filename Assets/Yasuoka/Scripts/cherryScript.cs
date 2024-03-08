using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherryScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Death"))
        {
            Destroy(this.gameObject);
        }
    }
}
