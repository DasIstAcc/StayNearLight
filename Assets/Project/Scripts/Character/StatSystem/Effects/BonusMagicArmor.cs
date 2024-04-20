using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BonusMagicArmor : Effect
{
    private int amount;

    public BonusMagicArmor(int amount)
    {
        this.amount = amount;
    }

    public override void OnApplication()
    {
        host.ModifyAttribute(Attributes.MagicArmor, new AttributeModifier("bonus_magic_armor_effect", amount, AttributeModifier.Operation.ADDITION));
    }

    public override void OnRemoval()
    {
        host.ModifyAttribute(Attributes.MagicArmor, new AttributeModifier("bonus_magic_armor_effect", -amount, AttributeModifier.Operation.ADDITION));
    }
}