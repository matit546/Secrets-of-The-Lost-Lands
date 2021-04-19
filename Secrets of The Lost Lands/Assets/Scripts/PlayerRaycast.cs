using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public int layer = 1 << 7;
    public Camera camera;
    public bool enabled;
    public int messageNumber;
    public GameObject UIManager;
    public GameObject objectHolder;
    public List<Sprite> sprites;

    private CommunicationManager communicationManager;
    private bool isHolding = false;
    private List<Transform> objectParents = new List<Transform>();

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
    }

    private void Update()
    {
        if(enabled)
        {
            if (Physics.Raycast(this.transform.position, camera.transform.forward, out RaycastHit hit, 4f, layer))
            {
                foreach(Transform objectTransform in objectParents)
                {
                    if (hit.transform == objectTransform)
                    {
                        objectTransform.GetComponent<MeshRenderer>().material.color = Color.green;
                        if (!communicationManager.IsEnabled(messageNumber) && !isHolding)
                        {
                            communicationManager.EnableMessage(messageNumber);
                            communicationManager.ChangeText(messageNumber, "Podnieœ");
                            communicationManager.ChangeSprite(messageNumber, sprites[0]);
                        }

                        if (Input.GetKey(KeyCode.E) && !isHolding)
                        {
                            isHolding = true;
                            communicationManager.ChangeText(messageNumber, "Upuœæ");
                            communicationManager.ChangeSprite(messageNumber, sprites[1]);
                            objectTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            objectTransform.position = objectHolder.transform.position;
                            objectTransform.rotation = objectHolder.transform.rotation;
                            objectTransform.SetParent(objectHolder.transform);
                        }
                    }
                }
            }
            else
            {
                //parent.GetComponent<MeshRenderer>().material.color = Color.red;
                if (communicationManager.IsEnabled(messageNumber) && !isHolding)
                {
                    foreach (Transform objectTransform in objectParents)
                    {
                        objectTransform.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                    communicationManager.DisableMessage(messageNumber);
                }
            }

            if (Input.GetKey(KeyCode.F) && isHolding)
            {
                isHolding = false;
                communicationManager.ChangeText(messageNumber, "Podnieœ");
                communicationManager.ChangeSprite(messageNumber, sprites[0]);
                foreach (Transform objectTransform in objectParents)
                {
                    objectTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    objectTransform.SetParent(null);
                }
            }
        }
    }

    public void EnableRaycast(Transform parent)
    {
        Debug.Log($"Dodalem: {parent.name}");
        objectParents.Add(parent);
        enabled = true;
    }

    public void DisableRaycast(Transform parent)
    {
        Debug.Log($"Usunalem: {parent.name}");
        objectParents.Remove(parent);
        if(objectParents.Count == 0)
        {
            enabled = false;
        }
    }
}
