using Characters.AI.Goal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanUnit : CharacterUnit
{

    private bool Feared = false;
    public int fear = 0;
    [SerializeField]
    private int fearThreshold = 100;


    private bool Protected = false;

    private HumanFearGoal fearGoal;

    public override void Awake()
    {
        base.Awake();

        navAgent = GetComponent<NavMeshAgent>();
        goalSelector = new GoalSelector();
        targetSelector = new GoalSelector();
        sensing = new Sensing(this);

        fearGoal = new HumanFearGoal(this);

        ApplyGoals();
    }

    public override void Update()
    {
        base.Update();
        if (IsAlive())
        {
            AiStep();
            if (fear >= fearThreshold * 100) Feared = true;
            if (fear < 0) fear = 0;
            if (fear == 0) Feared = false;
        }
    }

    public void ApplyGoals()
    {
        goalSelector.AddGoal(4, fearGoal);
    }


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


    public bool IsFeared()
    {
        return Feared;
    }

    public bool IsProtected()
    {
        return Protected;
    }

    public bool CanBeSaved()
    {
        return IsFeared() && IsAlive() && !IsProtected();
    }


    public void Save()
    {
        Feared = false;
        Protected = true;
        Debug.Log("Human is saved");
    }


    private class HumanFearGoal : Goal // Unflagged goal ?(can't be raplaced)
    {
        HumanUnit m_owner;

        public HumanFearGoal(HumanUnit owner)
        {
            m_owner = owner;
        }

        public override bool CanUse()
        {
            return !m_owner.IsProtected();
        }

        public override void Tick()
        {
            base.Tick();

            Collider[] colliders = Physics.OverlapSphere(m_owner.transform.position, 10f, LayerMask.NameToLayer("MovingEntities"));

            foreach (Collider col in colliders)
            {
                if (col.GetComponent<MonsterUnit>() != null) m_owner.fear++;
            }
        }
    }
}

