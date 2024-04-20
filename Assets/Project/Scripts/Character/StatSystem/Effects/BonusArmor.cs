using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BonusArmor : Effect
{
    private int amount;

    public BonusArmor(int amount)
    {
        this.amount = amount;
    }

    public override void OnApplication()
    {
        host.ModifyAttribute(Attributes.Armor, new AttributeModifier("bonus_armor_effect", amount, AttributeModifier.Operation.ADDITION));
    }

    public override void OnRemoval()
    {
        host.ModifyAttribute(Attributes.Armor, new AttributeModifier("bonus_armor_effect", -amount, AttributeModifier.Operation.ADDITION));
    }
}