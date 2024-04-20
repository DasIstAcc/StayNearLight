using Characters.AI.Goal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : CharacterUnit
{

    public override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        goalSelector = new GoalSelector();
        targetSelector = new GoalSelector();
        sensing = new Sensing(this);
    }

    public override void Update()
    {
        base.Update();
        if (IsAlive()) AiStep();
    }

    //private bool shake = true;

    public override void Death()
    {
        base.Death();
        navAgent.isStopped = true;
    }

    protected void AiStep()
    {
        noActionTime++;
        
        if (noActionTime % 30 == 0)
        {
            goalSelector.Tick();
            targetSelector.Tick();
            sensing.Tick();
        }
        else
        {
            goalSelector.TickRunningGoals(false);
            targetSelector.TickRunningGoals(false);
            
        }
    }

    
}
