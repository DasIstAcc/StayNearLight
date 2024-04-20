using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DetectionSystem
{

    public static bool IsVisibleUnit<T>(T unit, Transform from, float visionAngle, float visionDistance, LayerMask mask) where T : CharacterUnit
    {
        bool result = false;

        if (unit != null)
        {
            foreach (Transform point in unit.visiblePoints)
            {
                if (IsVisibleObject(from, point.position, unit.gameObject, visionAngle, visionDistance, mask))
                {
                    result = true; break;
                }
            }
        }

        return result;
    }

    internal static bool IsVisibleObject(Transform from, Vector3 point, GameObject target, float angle, float distance, LayerMask mask)
    {
        bool result = false;

        if (IsAvaliablePoint(from, point, angle, distance))
        {
            
            Vector3 direction = (point - from.position);
            Ray ray = new Ray(from.position, direction);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, mask.value))
            {
                if (hit.collider.gameObject == target)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    internal static bool IsAvaliablePoint(Transform from, Vector3 point, float angle, float distance)
    {
        bool result = false;

        if (from != null && Vector3.Distance(from.position, point) <= distance)
        {
            Vector3 direction = (point - from.position);
            float dot = Vector3.Dot(from.forward, direction.normalized);
            if (dot < 1)
            {
                float angleRadians = MathF.Acos(dot);
                float angleDeg = angleRadians * MathF.PI;
                result = (angleDeg <= angle);
            }
            else
            {
                result = true;
            }
        }

        return result;
    }
}