using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public Vector2 push_direction;
    public LayerMask interactables;
    public float intensity = 1f;

    public Transform self;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray_data = Physics2D.Raycast(self.position, push_direction, 5f, interactables);

        if (ray_data.collider != null)
        {
            if (ray_data.collider.gameObject.GetComponent<Rigidbody2D>())
            {
                ray_data.collider.gameObject.GetComponent<Rigidbody2D>().velocity += (push_direction * intensity);
            }
        }

    }
}
