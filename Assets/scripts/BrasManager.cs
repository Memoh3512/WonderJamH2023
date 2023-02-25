using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrasManager : MonoBehaviour
{
    private bool canMove = true;
    private Camera camera;
    private Transform rotation_base;
    private Transform bras_target_transform;
    public GameObject bras_target;
    public GameObject rotation_base_obj;
    public Transform actual_hand_transform;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rotation_base = camera.GetComponent<Transform>();
        bras_target_transform = bras_target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            bras_target_transform.position = pos;
            
            
            //TODO rotation fix
            var position0 = actual_hand_transform.position;
            var position1 = rotation_base.position;
            
            /*bras_target_transform.eulerAngles = new Vector3(0,0,(float)(Mathf.Rad2Deg*Math.Atan2(
                position0.y-position1.y,
                Math.Abs(position0.x-position1.x)
                )));*/
            //bras_target_transform.rotation = Quaternion.LookRotation(bras_target_transform.position - rotation_base.position);

        }
    }
}
