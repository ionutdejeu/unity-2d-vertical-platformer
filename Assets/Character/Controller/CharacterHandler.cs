using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHandler : MonoBehaviour
{

    public static UnityEvent<CharacterHandler> OnPlayerDiedEvent = new UnityEvent<CharacterHandler>();
    public static UnityEvent<CharacterHandler> OnPlayerSpawendEvent = new UnityEvent<CharacterHandler>();
    private List<IMovementModifier> modifiers = new List<IMovementModifier>();
    // Start is called before the first frame update

    
    void Start()
    {
                   
    }

    public void AddModifier(IMovementModifier mod)
    {
        modifiers.Add(mod);
    }

    public void RemoveModifier(IMovementModifier mod)
    {
        modifiers.Remove(mod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
