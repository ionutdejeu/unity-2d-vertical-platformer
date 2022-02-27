using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class HungerSystem : MonoBehaviour
{
    [SerializeField] int totalHungar = 100;
    [SerializeField] float hungarDecrement = 10;

    [SerializeField] public AnimationCurve decreaseCurve;
    [SerializeField] float currentHungarLeft = 10;
    [SerializeField] float hungerUpdateRate = 1f;
    float timePassedSinceStarted = 0;

    [SerializeField] UnityEvent onHungerDepleted = new UnityEvent();
    List<PickupObject> collectablesInLevel = new List<PickupObject>();

    [SerializeField] HungarUiView view;
    
    private void handlItemPickedup(GenericPickupObjectPayload<int> payload)
    {
        currentHungarLeft = Mathf.Min(totalHungar,payload.payload+currentHungarLeft);
        view.SetHungarValue((int)currentHungarLeft);
    }
    // Use this for initialization
    void Start()
    {
        currentHungarLeft = totalHungar;
        timePassedSinceStarted = 0;
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Food");
        foreach(GameObject ob in objs)
        {
            PickupObject o = ob.GetComponent<PickupObject>();
            o.onTriggerEvent.AddListener(this.handlItemPickedup);
            collectablesInLevel.Add(o);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        timePassedSinceStarted += Time.deltaTime;
        if (currentHungarLeft <= 0)
        {
            // emit event
            onHungerDepleted.Invoke();
            // stop the process
            this.enabled =false;
            return;
        }
        if (timePassedSinceStarted > hungerUpdateRate)
        {
            timePassedSinceStarted -= hungerUpdateRate;
            // decrement hunger based on curve value;
            float decrementFactor = decreaseCurve.Evaluate(currentHungarLeft / totalHungar);
            currentHungarLeft -= hungarDecrement * decrementFactor;
            view.SetHungarValue((int)currentHungarLeft);

        }
        
    }
}
