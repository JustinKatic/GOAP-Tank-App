using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Tank : Agent
{
    public int ammoCount = 0;

    public bool shotFired;
    public float shootCooldown;
    private float shootCooldownTimer;

    public TankHealth tankHealth;

    public override void Start()
    {
        base.Start();
        agentPersonalState.AddPersonalState("CanShoot");
    }
    private void Update()
    {
        if (ammoCount <= 0)
        {
            agentPersonalState.AddPersonalState("NeedAmmo");
            agentPersonalState.RemoveState("HasAmmo");
        }

        if (shotFired)
        {
            shootCooldownTimer += Time.deltaTime;
            if (shootCooldownTimer >= shootCooldown)
            {
                agentPersonalState.AddPersonalState("CanShoot");
                shotFired = false;
                shootCooldownTimer = 0;
            }
        }

        if (tankHealth.currentHealth <= tankHealth.maxHealth -4)
        {
            agentPersonalState.AddPersonalState("HealthLow");
        }

        else if (agentPersonalState.HasState("HealthLow"))
            agentPersonalState.RemoveState("HealthLow");
    }



}
