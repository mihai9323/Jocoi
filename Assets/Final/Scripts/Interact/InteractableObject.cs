/* Code by Mihai-Ovidiu ANTON 10/24/2014
 */

using UnityEngine;
using System.Collections;

public abstract class InteractableObject : MonoBehaviour {

    public abstract void StartLMB();
    public abstract void StartRMB();
    public abstract void StopLMB();
    public abstract void StopRMB();
    public abstract void StopAllInteractions();

}
