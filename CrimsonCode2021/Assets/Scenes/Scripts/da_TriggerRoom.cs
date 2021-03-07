using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class da_TriggerRoom : MonoBehaviour
{
    public Transform followTransform;
    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x - width/2 <= followTransform.position.x && this.transform.position.x + width / 2 >= followTransform.position.x
            && this.transform.position.y - height / 2 <= followTransform.position.y && this.transform.position.y + height / 2 >= followTransform.position.y) {
            // Collision -- Trigger event
        }
    }
}
