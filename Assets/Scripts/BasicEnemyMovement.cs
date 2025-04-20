using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] bool chargeAttack;
    int framesSinceLastCharge = 0;
    int wait = 120;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Determine which direction to rotate towards
        Vector3 targetDirection = player.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 5.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (wait > 0 == false)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            if (transform.position.y < 0)
            {
                float randX = Random.Range(68.0f, 102.0f);
                float randZ = Random.Range(62.0f, 89.0f);
                transform.position = new Vector3(randX, 5.0f, randZ);
                wait = 150;
            }
            if (chargeAttack)
            {
                if (framesSinceLastCharge >= 120)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                    }
                }
                else
                {
                    framesSinceLastCharge += 1;
                }
            }
        } else
        {
            wait -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            transform.position = new Vector3(0, -10, 0);
            Destroy(other.gameObject);
        }
    }
}
