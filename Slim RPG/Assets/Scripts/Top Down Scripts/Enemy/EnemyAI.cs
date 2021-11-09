using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace TopDown
{
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        public float speed = 200f;
        public float nextWaypointDistance = 3;

        private Path path;
        private int currentWaypoint = 0;
        bool reachedEndOfPath = false;

        [SerializeField]
        private float pathUpdateInterval = 0.25f;

        Seeker seeker;
        Rigidbody2D rb;
        EnemyScript enemyScript;

        // Start is called before the first frame update
        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            enemyScript = GetComponent<EnemyScript>();

            InvokeRepeating("UpdatePath", 0f, pathUpdateInterval); //repeatedly update the path towards the player
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target == null) //if player is dead or not assigned simply do not update
                return;

            if (path == null) //if path to target doesnt exist, skip
                return;

            if (currentWaypoint >= path.vectorPath.Count) //have we reached the end of the point?
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; //calculate direction "arrow" towards next waypoint
            Vector2 force = direction * speed * Time.deltaTime * rb.mass; //mass used so player cannot push enemy, delta time for consistency
            if (enemyScript.enemyType == EnemyType.Melee) //AI for melee enemies
            {
                if (Vector2.Distance(rb.position, target.position) < enemyScript.attackRange) //if we are within attack range, attack the player
                {
                    enemyScript.Attack(target);
                }
                else //if we are outside of attack range, move towards player
                {
                    rb.AddForce(force);
                }
            }

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]); //distance to the next waypoint along path

            if (distance < nextWaypointDistance) //check whether we have reached next waypoint
            {
                currentWaypoint++;
            }
        }

        void OnPathComplete(Path p) //when we are done with the path
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        void UpdatePath() //calculate a new path towards the target
        {
            if (seeker.IsDone() && target != null)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }
}
