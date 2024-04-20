using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Effect
{
    protected int duration;
    protected CharacterUnit host;
    protected CharacterUnit sender;


    public abstract void OnApplication();
    public virtual void OnRemoval() { }


}