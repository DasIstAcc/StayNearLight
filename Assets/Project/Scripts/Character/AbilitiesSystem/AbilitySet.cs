using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AbilitySet
{
    private Dictionary<Ability, AbilityInstance> abilities;

    public AbilitySet()
    {
        abilities = new Dictionary<Ability, AbilityInstance>();

    }
}