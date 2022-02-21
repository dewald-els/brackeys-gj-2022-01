using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{

    [Header("Waypoints")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2.0f;
    private int currentPoint = 0;

    [Header("Truffle Movemement")]
    [SerializeField] private float moveToTruffleSpeed = 3.0f;
    private bool isMovingToTruffle = false;
    private Truffle truffle;

    [SerializeField] private List<Truffle> truffles = new List<Truffle>();

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D smellRadius;
    [SerializeField] private LayerMask truffleSmellLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerEvents.instance.OnPlayerPlacedTruffle += TruffleAdded;
    }

    private void TruffleAdded(Truffle truffle)
    {
        if (truffle == null) return;

        truffles.Add(truffle);
    }

    private void FindNearbyTruffles()
    {
        if (isMovingToTruffle == true) return;
        // Get all the truffels the pig is overlapping with.
        Collider2D[] truffleSmellRadius = Physics2D.OverlapCircleAll(smellRadius.bounds.center, smellRadius.radius, truffleSmellLayerMask);

        if (truffleSmellRadius.Length > 0)
        {
            // If Pig is overlapping, set moving to truffle.
            isMovingToTruffle = true;
            // Find the truffle object on the smell radius object.
            truffle = truffleSmellRadius.First().transform.parent.GetComponent<Truffle>();
        }
    }

    private void Update()
    {
        FindNearbyTruffles();
    }

    private void FixedUpdate()
    {
        if (!isMovingToTruffle)
        {
            Patrol();
        }
        else
        {
            MoveToTruffle();
        }
    }

    private void MoveToTruffle()
    {
        rb.position = Vector2.MoveTowards(rb.position, truffle.transform.position, moveToTruffleSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, truffle.transform.position) < 0.6f)
        {
            isMovingToTruffle = false;
            truffle.Eat();
            truffle = null;
        }
    }

    private void Patrol()
    {
        Transform goal = waypoints[currentPoint].transform;
        rb.position = Vector2.MoveTowards(rb.position, goal.position, speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, goal.position) < 0.6f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        currentPoint++;
        if (currentPoint == waypoints.Length)
        {
            currentPoint = 0;
        }
    }

}
