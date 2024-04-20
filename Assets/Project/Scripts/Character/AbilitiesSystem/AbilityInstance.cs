using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AbilityInstance
{
    private Ability ability;

    public AbilityInstance(Ability ability)
    {
        this.ability = ability;
    }

    public Ability GetAbility()
    {
        return ability;
    }

}
