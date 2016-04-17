using UnityEngine;

public static class AnimatoHash
{
    public static int FULLBODY_IDLE = Animator.StringToHash("FullBody.Idle");
    public static int FULLBODY_RUN = Animator.StringToHash("FullBody.Run");

    public static int FULLBODY_ATTACK = Animator.StringToHash("FullBody.Attack");
    public static int FULLBODY_DASH = Animator.StringToHash("FullBody.Dash");

    public static int FULLBODY_KNOCKBACK = Animator.StringToHash("FullBody.Knockback");
    public static int FULLBODY_DAMAGE = Animator.StringToHash("FullBody.Damage");
    public static int FULLBODY_DEATH = Animator.StringToHash("FullBody.Death");

    public static int STATE_MOVEMENT_IDLE = Animator.StringToHash("MovementLayer.Idle");
    public static int STATE_MOVEMENT_RUN = Animator.StringToHash("MovementLayer.Run");

    public static int STATE_ACTION_NONE = Animator.StringToHash("ActionLayer.None");
    public static int STATE_ACTION_ATTACK = Animator.StringToHash("ActionLayer.Attack");
}