using UnityEngine;
using System.Collections;

public class BaseAI : BaseCharacter 
{
    private CharacterBlackboard blackboard;

    public CharacterBlackboard _Blackboard {
        get {
            if(blackboard == null)
                blackboard = GetComponent<CharacterBlackboard>();

            return blackboard;
        }
    }
}
