using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepTrigger : MonoBehaviour
{
    [SerializeField] private PlaySound _soundEffect;

    private void OnValidate()
    {
        _soundEffect ??= FindObjectOfType<PlaySound>();
    }

    public void Trigger()
    {
        _soundEffect.PlaySoundEffect("FootstepSound");
    }
}
