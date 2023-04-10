using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace CommandSystem
{
    public class SpinCommand : BaseCommand
    {
        private float m_TargetSpinAngle;
        private float m_SpinnedAngle;


        private void Update()
        {
            if (!isActive) return;
            
            PerformSpinning();
        }


        public override void Execute(CommandArgs args, Action onCompleted)
        {
            const float targetSpinAngle = 360f;
            m_TargetSpinAngle = targetSpinAngle;
            m_SpinnedAngle = 0f;
            StartCommand(onCompleted);
        }

        public override IEnumerator<(GridPosition, GridVisual.State, CommandStatus)> GetAllGridPositionStates()
        {
            if (!HasEnoughCommandPoint())
            {
                yield return (Unit.GridPosition, GridVisual.State.Red, CommandStatus.NotEnoughCommandPoint);
                yield break;
            }
            
            yield return (Unit.GridPosition, GridVisual.State.Green, CommandStatus.Ok);
        }

        public override string GetName()
        {
            const string commandName = "Spin";
            return commandName;
        }

        public override int GetRequiredCommandPoint()
        {
            return 2;
        }


        private void PerformSpinning()
        {
            var hasReachedTargetSpinAngle = m_SpinnedAngle >= m_TargetSpinAngle;
            
            if (hasReachedTargetSpinAngle)
            {
                CompleteCommand();
                return;
            }

            const float spinSpeed = 360f;
            var deltaSpinAngle = Time.deltaTime * spinSpeed;
            transform.eulerAngles += Vector3.up * deltaSpinAngle;
            m_SpinnedAngle += deltaSpinAngle;
        }
    }
}