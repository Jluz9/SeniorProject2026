using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerPos;

    [HideInInspector] public ItemInteract interact1;
    public GameObject FindClosestInteractObject(ItemInteract interact1)
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("InteractObject");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = playerPos.position;
        foreach (GameObject obj in objs)
        {
            Vector3 diff = obj.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = obj;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    // Update is called once per frame
    void Update()
    {
        FindClosestInteractObject(interact1);
        
        if (interact1 != null)
        {
            interact1.enabled = true;
        }
    }
}
