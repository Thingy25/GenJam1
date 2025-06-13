using System.Collections;
using UnityEngine;

public class MovableObject: MonoBehaviour, IPuzzleInteractable
{
    [SerializeField]
    private float distanceToMove;

    private float newPosition;

    void Start()
    {
        newPosition = transform.localPosition.y;
        //StartCoroutine(MoveVertically());
        //Debug.Log(newPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPuzzleInteractable.CallInteraction()
    {
        StartCoroutine(MoveVertically());
    }

    IEnumerator MoveVertically()
    {
        while (transform.localPosition.y < distanceToMove)
        {
            yield return new WaitForSeconds(0.03f);
            newPosition += 0.15f;
            transform.localPosition = new Vector3(transform.localPosition.x, newPosition, transform.localPosition.z);
            //Debug.Log(newPosition);
        }
    }
}
