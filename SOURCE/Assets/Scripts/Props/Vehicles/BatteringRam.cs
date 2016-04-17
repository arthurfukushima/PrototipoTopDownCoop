using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BatteringRam : BaseActor
{
    private PhysicsController physicsController;
    private List<BaseActor> onInteractionAreaActors = new List<BaseActor>();

    public int coinsNeeded = 50;
    public int coinsCollected = 0;

    public float movementSpeed = 3.0f;
    public float movementMaxSpeed = 8.0f;

    [Header("UI")]
    public PayTributePanel tributesPanel;

#region PROPERTIES
    public PhysicsController _PhysicsController {
        get {
            if(physicsController == null)
                physicsController = GetComponent<PhysicsController>();

            return physicsController;
        }
    }
#endregion

#region OVERRIDE
    protected override void Start()
    {
        base.Start();

        _PhysicsController.CachedRigidbody.centerOfMass -= new Vector3(0.0f, 0.5f, 0.0f);

        tributesPanel.tributesCollected = coinsCollected;
        tributesPanel.tributesNeeded = coinsNeeded;

        tributesPanel.UpdateTributesText();
    }

    public override void ReceiveDamage(BaseActor pBully, int pDamage)
    {
        pDamage = 0;
        base.ReceiveDamage(pBully, pDamage);
    }


#endregion

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(onInteractionAreaActors.Count > 0 && InventoryManager.Instance.coinsCount >= 10)
            {
                coinsCollected += 10;
                tributesPanel.PayTribute(10);
                InventoryManager.Instance.AddCoins(-10);
            }
        }
    }

    private void FixedUpdate()
    {
        if(coinsCollected >= coinsNeeded)
        {
            Vector3 velocity = new Vector3(-movementSpeed, 0.0f, 0.0f);
            velocity = transform.TransformVector(velocity);

            _PhysicsController.Velocity = velocity;

            movementSpeed = Mathf.MoveTowards(movementSpeed, movementMaxSpeed, Time.deltaTime * 5.0f);
        }
    }

    private void OnTriggerEnter(Collider pCollider)
    {
        BaseCharacter character = pCollider.GetComponent<BaseCharacter>();

        if(character != null)
        {
            if(!onInteractionAreaActors.Contains(character))
                onInteractionAreaActors.Add(character);

            tributesPanel.Show();
        }
    }

    private void OnTriggerExit(Collider pCollider)
    {
        BaseCharacter character = pCollider.GetComponent<BaseCharacter>();

        if(character != null)
        {
            if(onInteractionAreaActors.Contains(character))
                onInteractionAreaActors.Remove(character);

            tributesPanel.Hide();
        }
    }
}
