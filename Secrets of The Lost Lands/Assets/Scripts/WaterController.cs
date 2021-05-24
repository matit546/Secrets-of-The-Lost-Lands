using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public List<Transform> transforms;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            List<float> distances = new List<float>();
            GameObject player = other.gameObject;
            foreach (Transform transform in transforms)
            {
                distances.Add(Vector3.Distance(other.transform.position, transform.position));
            }

            float closestDistance = distances.Min();
            Transform closestBonfire = transforms[distances.IndexOf(closestDistance)];

            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<Transform>().position = closestBonfire.position;
            player.GetComponent<CharacterController>().enabled = true;
        }

    }
}
