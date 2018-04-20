using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HeartIcon : MonoBehaviour
{
    public Animator _Anim;

    public void Awake()
    {
        _Anim = GetComponent<Animator>();
    }
    /// <summary>
    /// This function will be responsible for sending the value to the animator.
    /// </summary>
    /// <param name="_HeartValue"></param>
    public void SetHeartAnim(int _HeartValue)
    {
        _Anim.SetInteger("HeartHealth", _HeartValue);
    }
}