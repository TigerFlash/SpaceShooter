using TMPro;
using UnityEngine;

public class EndOfAnim : MonoBehaviour
{
    public Animator animator;
void End()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool(0, true);
    }
}
