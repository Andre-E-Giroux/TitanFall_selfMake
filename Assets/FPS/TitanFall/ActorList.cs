using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorList : MonoBehaviour
{
    public List<Transform> actors = new List<Transform>();

   

    public void AddTransformToActors(Transform actor)
    {
        actors.Add(actor);
    }
    public void RemoveTransformFromActors(Transform actor)
    {
        actors.Remove(actor);
    }

    // explosion
    public void CheckForExplosionDistance(Transform explosionPoint, float maxDistance, float baseDamage40mm, float pushForce)
    {
        foreach (Transform actor in actors)
        {
            Debug.Log("distance: " + (explosionPoint.position - actor.position).magnitude);
            if((explosionPoint.position - actor.position).magnitude < maxDistance)
            {
                //within max distance
                DummyDamageHandler_TF tempHandler = actor.GetComponent<DummyDamageHandler_TF>();
                tempHandler.TakeExploDistanceDamage((actor.position - explosionPoint.position).magnitude, baseDamage40mm);
                if(tempHandler.health <= 0)
                {
                    RagdollActivate tempRagActive = actor.GetComponent<RagdollActivate>();
                    foreach (Rigidbody rbs in tempRagActive._rigidbodies)
                    {
                        Debug.Log("alert");
                        rbs.AddExplosionForce(pushForce, transform.position, 100);
                    }
                }
            }
        }
    }
    

}
