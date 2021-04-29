using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    CharacterController boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = transform.forward * v + transform.right * h;

        boss.SimpleMove(velocity);
    }
}
