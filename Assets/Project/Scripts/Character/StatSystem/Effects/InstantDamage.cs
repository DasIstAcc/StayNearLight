using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InstantDamage : Effect
{
    private int damage_amount;

    public InstantDamage(int amount)
    {
        damage_amount = amount;
    }

    public override void OnApplication()
    {
        host.TakeHit(sender, DamageSource.MAGICAL, damage_amount);
    }
}