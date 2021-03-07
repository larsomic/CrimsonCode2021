using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Animator anim;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        anim.Play("player_idle");
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveDirection = new Vector2(0.0f, 0.0f);
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");
        

        if (horizontal > 0)
        {
            moveDirection.x = 1;
            direction = 1;
            anim.Play("player_walk");
        }
        else if (horizontal < 0)
        {
            moveDirection.x = -1;
            direction = -1; 
            anim.Play("player_walk");
        }
        else if (vertical > 0)
        {
            moveDirection.y = 1;
            anim.Play("player_idle_up");

        }
        else if (vertical < 0)
        {
            moveDirection.y = -1;
            anim.Play("player_idle_down");

        }
        transform.Translate(moveDirection * 5 * Time.deltaTime, Space.World);
        transform.localScale = new Vector3(direction*2, 2, 1);
    }
}
