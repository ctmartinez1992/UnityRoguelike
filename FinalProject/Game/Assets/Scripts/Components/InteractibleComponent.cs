using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleComponent : MonoBehaviour {

    public string componentName;

    public abstract void Use();                 //Using something is similar to walk into, but can yield more options.
    public abstract void Interact();            //Interacting with something will often reveal details on said thing.
    public abstract void WalkInto();            //If you walk into something, you will trigger a certain action.
}
