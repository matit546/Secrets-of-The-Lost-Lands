using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public int objectLayer;
    public int crateLayer = 1 << 10;
    public int leverLayer = 1 << 8;
    public Camera camera;
    public bool enabled;
    public int messageNumber;
    public GameObject UIManager;
    public GameObject objectHolder;
    private Transform objectLocation;
    public List<Sprite> sprites;
    public PuzzleSovledScript puzzle;

    private CommunicationManager communicationManager;
    private bool isHolding = false;
    private List<Transform> objectsInTriggers = new List<Transform>();

    private bool didUse = false;
    private bool isRaycastPointingAt = false;
    private int i = 0;

    private void Start()
    {
        communicationManager = UIManager.GetComponent<CommunicationManager>();
        objectLayer = LayerMask.GetMask("Object");
    }

    private void Update()
    {
        if(enabled)
        {
            Debug.Log($"Layer: {objectLayer}");
            Debug.Log($"Layer: {crateLayer}");
            foreach (Transform items in objectsInTriggers)
            {
                Debug.Log($"Layer[{i}]: {items.gameObject.layer}");
                i++;
            }
            i = 0;
            Debug.DrawRay(this.transform.position, camera.transform.forward, Color.blue, 10);
            if (Physics.Raycast(this.transform.position, camera.transform.forward, out RaycastHit hit, 10f, crateLayer))
            {
                foreach (Transform objectTransform in objectsInTriggers)
                {
                    if (hit.transform == objectTransform)
                    {
                        Debug.Log($"Hit: {objectTransform}");
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
            else if(Physics.Raycast(this.transform.position, camera.transform.forward, out RaycastHit hit2, 10f, leverLayer))
            {
                foreach (Transform objectTransform in objectsInTriggers)
                {
                    if (hit2.transform == objectTransform)
                    {
                        if (!communicationManager.IsEnabled(messageNumber))
                        {
                            communicationManager.EnableMessage(messageNumber);
                            communicationManager.ChangeText(messageNumber, "U¿yj");
                            communicationManager.ChangeSprite(messageNumber, sprites[0]);
                        }

                        if (Input.GetKeyDown(KeyCode.E) && !didUse)
                        {
                            didUse = true;
                            
                            if (objectTransform.GetComponent<LeverPosition>().pulled)
                            {
                                objectTransform.GetComponent<LeverPosition>().pulled = false;
                                objectTransform.Rotate(-60f, 0, 0, Space.Self);
                            }
                            else
                            {
                                objectTransform.GetComponent<LeverPosition>().pulled = true;
                                objectTransform.Rotate(60f, 0, 0, Space.Self);
                            }

                            if(objectTransform.parent.GetChild(2).GetComponent<OpenDoors>())
                            {
                                objectTransform.parent.GetChild(2).GetComponent<OpenDoors>().OpenCloseDoor();
                            }
                        }
                        else if(didUse == true)
                        {
                            didUse = false;
                        }
                    }
                }
            }
            else if(Physics.Raycast(this.transform.position, camera.transform.forward, out RaycastHit hit3, 10f, objectLayer))
            {
                Debug.DrawRay(this.transform.position, camera.transform.forward, Color.red, 10);
                foreach (Transform objectTransform in objectsInTriggers)
                {
                    if (hit3.transform == objectTransform)
                    {
                        Debug.Log($"Hit: {objectTransform}");
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
                            objectTransform.position = objectHolder.transform.position;
                            objectTransform.SetParent(objectHolder.transform);

                        }
                    }
                }
            }
            else
            {
                if (communicationManager.IsEnabled(messageNumber) && !isHolding)
                {
                    communicationManager.DisableMessage(messageNumber);
                }
            }
            if (Input.GetKey(KeyCode.F) && isHolding)
            {
                isHolding = false;
                foreach (Transform objectTransform in objectsInTriggers)
                {
                    objectTransform.SetParent(null);
                    Debug.Log(objectTransform.gameObject.layer);
                    if (objectTransform.gameObject.layer != 12)
                    {
                        if (objectTransform.GetComponent<Rigidbody>())
                        {
                            objectTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        }
                    }
                    else
                    {
                        if (objectLocation != null)
                        {
                            objectTransform.transform.position = objectLocation.transform.position;
                            objectTransform.SetParent(objectLocation);
                            puzzle.CheckItem();
                        }
                    }
                }

            }
       
        }
    }

    public void EnableRaycast(Transform parent)
    {
        Debug.Log($"Dodalem: {parent.name}");
        objectsInTriggers.Add(parent);
        enabled = true;
    }

    public void DisableRaycast(Transform parent)
    {
        Debug.Log($"Usunalem: {parent.name}");
        objectsInTriggers.Remove(parent);
        if(objectsInTriggers.Count == 0)
        {
            enabled = false;
            communicationManager.DisableMessage(messageNumber);
        }
    }

    public void EnablePutDown(Transform loc)
    {
        Debug.Log($"Dodalem: loc");
        objectLocation = loc;
        if (objectLocation.childCount == 0)
        { 
            enabled = true;
            communicationManager.ChangeText(messageNumber, "Po³ó¿");
            communicationManager.ChangeSprite(messageNumber, sprites[1]);
        }
    }

    public void DisablePutDown(Transform loc)
    {
        Debug.Log($"Usunalem: loc");
        objectLocation = null;
        if (objectsInTriggers.Count == 0)
        {
            enabled = false;
        }
        communicationManager.ChangeText(messageNumber, "Upuœæ");
        communicationManager.ChangeSprite(messageNumber, sprites[1]);
    }
}
