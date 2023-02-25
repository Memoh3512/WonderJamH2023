using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrasManager : MonoBehaviour
{
    private bool canMove = true;
    private Camera camera;
    private Transform rotation_base;
    public Transform bras_target_transform;
    public PolygonCollider2D bounding_collider;
    // Start is called before the first frame update

    private void Awake()
    {
        bras_target_transform.position = bounding_collider.ClosestPoint(bras_target_transform.position);
    }
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 world_mouse_pos = camera.ScreenToWorldPoint(Input.mousePosition);
            world_mouse_pos.z = 0;

            float speed = 5f;
            float step = speed * Time.deltaTime;
            Vector2 closet_point = bounding_collider.ClosestPoint(world_mouse_pos);
            bras_target_transform.position = Vector3.Lerp(bras_target_transform.position, closet_point, step);

            //TODO rotation fix
            //var position0 = actual_hand_transform.position;
            //var position1 = rotation_base.position;
        
            /*bras_target_transform.eulerAngles = new Vector3(0,0,(float)(Mathf.Rad2Deg*Math.Atan2(
                position0.y-position1.y,
                Math.Abs(position0.x-position1.x)
                )));*/
            //bras_target_transform.rotation = Quaternion.LookRotation(bras_target_transform.position - rotation_base.position);
            
        }
    }
}
