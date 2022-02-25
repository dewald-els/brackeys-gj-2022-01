using System.Linq;
using System.Collections.Generic;
using UnityEngine;

enum PigState
{
    Patrol,
    ChaseTruffle,
    InFence,
    SleepInFence
}

public class PigController : MonoBehaviour
{
    [SerializeField] private PigState state = PigState.Patrol;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private int currentPoint = 0;

    [Header("Truffle Movemement")]
    [SerializeField] private float moveToTruffleSpeed = 3.0f;
    private Truffle truffle;

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D smellRadius;
    [SerializeField] private LayerMask truffleSmellLayerMask;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D fenceSleepZone;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        fenceSleepZone = GameObject.FindGameObjectWithTag("FenceSleepZone").GetComponent<BoxCollider2D>();
    }

    private void FindNearbyTruffles()
    {
        if (state == PigState.ChaseTruffle || state == PigState.InFence) return; // Already chasing a truffle.
        
        // Get all the truffels the pig is overlapping with.
        Collider2D[] truffleSmellRadius = Physics2D.OverlapCircleAll(smellRadius.bounds.center, smellRadius.radius, truffleSmellLayerMask);

        if (truffleSmellRadius.Length > 0) // At least 1 truffle found
        {
            // If Pig is overlapping, set moving to truffle.
            state = PigState.ChaseTruffle;
            // Find the parent truffle object on the smell radius object.
            truffle = truffleSmellRadius.First().transform.parent.GetComponent<Truffle>();
        }
    }

    private void Update()
    {
        FindNearbyTruffles();
    }

    private void FixedUpdate()
    {

        if (state == PigState.InFence)
        {
            MoveToSleepZone();
        }

        if (state == PigState.SleepInFence)
        {
            return;
        }


        if (state == PigState.ChaseTruffle)
        {
            MoveToTruffle();
        }
        
        if (state == PigState.Patrol)
        {
            Patrol();
        }
    }

    private void MoveToSleepZone()
    {
        rb.position = Vector2.MoveTowards(rb.position, fenceSleepZone.transform.position, speed * Time.fixedDeltaTime);
    }

    private void MoveToTruffle()
    {
        state = PigState.ChaseTruffle;
        rb.position = Vector2.MoveTowards(rb.position, truffle.transform.position, moveToTruffleSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, truffle.transform.position) < 0.6f)
        {
            truffle.Eat();
            truffle = null;

            if (!state.Equals(PigState.InFence))
            {
                state = PigState.Patrol;
            }
        }
    }

    private void Patrol()
    {
        state = PigState.Patrol;
        Transform goal = waypoints[currentPoint].transform;
        if (goal.position.x > rb.position.x)
        {
            sprite.flipX = true;
        }
        else if (goal.position.x < rb.position.x)
        {
            sprite.flipX = false;
        }

        rb.position = Vector2.MoveTowards(rb.position, goal.position, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, goal.position) < 0.6f)
        {
            FindNextWaypoint();
        }
    }

    private void FindNextWaypoint()
    {
        currentPoint++;
        if (currentPoint == waypoints.Length)
        {
            currentPoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.CompareTag("Finish"))
        {
            state = PigState.InFence;
        }

        if (collision.CompareTag("SleepInFence"))
        {
            state = PigState.SleepInFence;
        }
    }

}
