using System.Collections.Generic;
using UnityEngine;

public class AbilityTarget
{
    public List<CharacterUnit> targetedObjects = new List<CharacterUnit>();
    public List<Vector3> targetedPoints = new List<Vector3>();


    #region Builders
    public AbilityTarget()
    {

    }

    public AbilityTarget(CharacterUnit target)
    {
        targetedObjects.Add(target);
    }

    public AbilityTarget(Vector3 target)
    {
        targetedPoints.Add(target);
    }

    public AbilityTarget(List<CharacterUnit> targets)
    {
        targetedObjects.AddRange(targets);
    }

    public AbilityTarget(List<Vector3> targets)
    {
        targetedPoints.AddRange(targets);
    }

    public AbilityTarget(CharacterUnit[] targets)
    {
        targetedObjects.AddRange(targets);
    }

    public AbilityTarget(HashSet<CharacterUnit> targets)
    {
        targetedObjects.AddRange(targets);
    }

    public AbilityTarget(Vector3[] targets)
    {
        targetedPoints.AddRange(targets);
    }

    public AbilityTarget(HashSet<Vector3> targets)
    {
        targetedPoints.AddRange(targets);
    }
    #endregion

    public CharacterUnit GetTargetCharacter(bool remove)
    {
        CharacterUnit result = null;

        if (targetedObjects.Count > 0)
        {
            result = targetedObjects[0];
            if (remove)
                targetedObjects.RemoveAt(0);
        }

        return result;
    }

    public Vector3? GetTargetPoint(bool remove)
    {
        Vector3? result = null;

        if (targetedObjects.Count > 0)
        {
            result = targetedPoints[0];
            if (remove)
                targetedObjects.RemoveAt(0);
        }

        return result;
    }

    public List<CharacterUnit> GetAllTargetCharacters(bool remove)
    {
        List<CharacterUnit> result = new List<CharacterUnit>();
        result.AddRange(targetedObjects);
        if (remove)
        {
            targetedObjects.Clear();
        }

        return result;
    }

    public List<Vector3> GetAllTargetPoints(bool remove)
    {
        List<Vector3> result = new List<Vector3>();
        result.AddRange(targetedPoints);
        if (remove)
        {
            targetedPoints.Clear();
        }

        return result;
    }
}