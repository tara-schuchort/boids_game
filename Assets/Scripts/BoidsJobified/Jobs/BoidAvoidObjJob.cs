using UnityEngine;
using Unity.Burst;
using UnityEngine.Jobs;
using Unity.Collections;


public struct BoidAvoidObjJob : IJobParallelForTransform {

    public NativeArray<Vector3> velocity;
    [ReadOnly] public NativeArray<bool> isHitObstacles;
    [ReadOnly] public NativeArray<Vector3> hitNormals;


    public void Execute(int i, TransformAccess transform) {

        // in case of disaster change these:
        int weight = 12;

    //   /// Vector3[] reflectedVector;
    //    Vector3[] oppositeVel;

         var reflectedVector = Vector3.zero;
           var oppositeVel = Vector3.zero;
        var avoidanceVector = Vector3.zero;
        var found = 0;
        var rotationSpeed = 2;

     //   Debug.DrawRay(transform.position, velocity[i] * 2, Color.yellow);  // surprisingly this works but the other stuff doesnt

        // at this point we know if boid is about to hit sth 
        if (isHitObstacles[i]) {  // if the ray cast from a boid hits sth -> avoid
                                  // avoidanceVector = Vector3.Reflect(velocity[i], hitNormals[i]);  // just making shit up at this point
                                  //Debug.DrawRay(transform.position, velocity[i] * 2, Color.yellow);
                                  // avoidanceVector = (-velocity[i]);
                                  // transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up,- velocity[i]);
                                  //  Debug.Log(isHitObstacles[i]);
                                  // velocity[i] = Vector3.Reflect(velocity[i], Vector3.right);
            found++;

            //   Debug.Log("velocity[i]");
            //   Debug.Log(velocity[i]);

            // calc new vector: 
            reflectedVector = Vector3.Reflect(velocity[i], hitNormals[i]); // doesnt do what i want it to
                                                                           // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidanceVector), rotationSpeed * deltaTime);


            //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidanceVector), rotationSpeed * deltaTime);
            //  Debug.Log("avoidanceVector");
            //   Debug.Log(avoidanceVector);
            //avoidanceVector = hitNormals[i];
            //  avoidanceVector *= weight;
            velocity[i] = velocity[i] * (-1); 


            velocity[i] += reflectedVector;
            velocity[i] *= 20;
            // velocity[i] = test;


            // Debug.Log("velocity[i] AFTER");
            // Debug.Log(velocity[i]);

            //setting vector pointing away from neighbor
            //  avoidanceMove += (Vector3)(agent.transform.position - closestPointofObstacle);



        }



        /*
        if (found > 0) {
            avoidanceVector /= found;  // normalizing
            avoidanceVector *= weight; // change for getting desired effect in flock behavior
            velocity[i] *= -1 ; // opposite direction
        }*/

    }

    /*
    // takes first ray that doesnt hit anything as direction
    // prob: physics baby 
    // sol: end my fucking life
    Vector3 ObstacleRays() {
        Vector3[] rayDirections = BoidHelper.directions;
        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(Transform.position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }
        return forward;
    }
    */










}