using Assets.Character.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHandler : MonoBehaviour
{

    public static UnityEvent<CharacterHandler> OnPlayerDiedEvent = new UnityEvent<CharacterHandler>();
    public static UnityEvent<CharacterHandler> OnPlayerSpawendEvent = new UnityEvent<CharacterHandler>();
    private List<MovementModifier> modifiers = new List<MovementModifier>();
   [SerializeField] private ReadOnlyCollection<MovementModifier> modifiersList;
   // Start is called before the first frame update
   private Rigidbody2D m_Rigidbody2D;


    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        modifiersList = modifiers.AsReadOnly();
    }

    public void AddModifier(MovementModifier mod)
    {
        modifiers.Add(mod);
    }

    public void RemoveModifier(MovementModifier mod)
    {
        modifiers.Remove(mod);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetSpeed = Vector2.zero;
        foreach (MovementModifier mod in modifiers)
        {
            targetSpeed += mod.Value;
        }
         
        m_Rigidbody2D.velocity = targetSpeed;
    }
}
