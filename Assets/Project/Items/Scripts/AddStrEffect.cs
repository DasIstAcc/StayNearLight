using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EquipmentItem;

public class AddStrEffect : EquippedEffect
{
    [Tooltip("Strength bonus amount")]
    public int amount;

    AttributeModifier modifier;

    private void OnEnable()
    {
        modifier = new AttributeModifier("Bonus strength", amount, AttributeModifier.Operation.ADDITION);
    }

    public override void Equipped(CharacterData user)
    {
        user.attributes.GetAttribute(Attributes.Strength).AddTransientModifier(modifier);
    }

    public override void Removed(CharacterData user)
    {
        user.attributes.GetAttribute(Attributes.Strength).RemoveModifier(modifier);
    }
}
