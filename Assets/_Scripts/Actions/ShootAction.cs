using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Unit;
using UnityEngine;

public class ShootAction : BaseAction
{
    private enum State
    {
        Aiming,
        Shooting,
        CoolOff,
    }

    private int maxShootDistance = 7;
    private State state;
    private float stateTimer;
    private Unit targerUnit;
    private bool canShootBullet;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;


        switch (state)
        {
            case State.Aiming:
                Vector3 aimDirection = (targerUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, rotateSpeed * Time.deltaTime);
                break;
            
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            
            case State.CoolOff:
                break;
        }
        
        
        if (stateTimer <= 0f)
        {
            NextState();
        }
        
    }

    private void Shoot()
    {
        targerUnit.Damage();
    }


    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                if (stateTimer <= 0f)
                {
                    state = State.Shooting;
                    float shootingStateTime = 0.1f;
                    stateTimer = shootingStateTime;
                }
                break;
            
            
            case State.Shooting:
                if (stateTimer <= 0f)
                {
                    state = State.CoolOff;
                    float coolOffStateTime = 0.1f;
                    stateTimer = coolOffStateTime;
                }
                break;
            
            
            case State.CoolOff:
                if (stateTimer <= 0f)
                {
                    ActionComplete();
                }
                break;
        }
    }
    
    
    public override string GetActionName()
    {
        return "Shoot";
    }

    
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targerUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;
    }

    
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))  
                {
                    // Grid position is empty, no Unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    // Both Units on same "team"
                    continue;
                }
                    
                validGridPositionList.Add(testGridPosition);
            }
        }
            
        return validGridPositionList;
    }
}
