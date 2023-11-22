using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        private Health _health;

        [SerializeField] private GameObject _attackEffect;
        [SerializeField] private GameObject _damageEffect;
        [SerializeField] private PlaySound _soundEffect;
        
        [SerializeField]
        private float _speed = 2f;

        [SerializeField]
        private Transform _anchor;

        public bool IsAlive => _health.IsAlive;

        public Transform Anchor => _anchor;

        private void OnValidate()
        {
            _soundEffect ??= FindObjectOfType<PlaySound>();
        }


        private void Start()
        {
            _health.OnDeath += OnHealthDeath;
        }

        private void OnDestroy()
        {
            _health.OnDeath -= OnHealthDeath;
        }

        private void OnHealthDeath()
        {
            Debug.Log($"{GetType().Name}.OnHealthDeath:");
            if (_soundEffect) _soundEffect.PlaySoundEffect("Die");
            _animator.SetTrigger("Die");
        }


        public IEnumerator Attack(Character attackedCharacter)
        {
            Debug.Log($"{GetType().Name}.Attack: gameObject.name = {gameObject.name} => {attackedCharacter.gameObject.name}");

            if (_attackEffect) _attackEffect.GetComponent<Animation>().Play();

            if (_weapon.Type == WeaponType.Gun)
            {
                if (_soundEffect) _soundEffect.PlaySoundEffect("PistolShoot");
            }

            var originalPosition = transform.position;
            if (_weapon.Type == WeaponType.Bat)
            {
                yield return MoveTo(attackedCharacter.Anchor.position);
                if (_soundEffect) _soundEffect.PlaySoundEffect("BaseballBatHit");
            }

            var animationName = WeaponHelpers.GetAnimationNameFor(_weapon.Type);
            _animator.SetTrigger(animationName);

            yield return new WaitForSeconds(2f);

            attackedCharacter.TakeDamage(_weapon.Damage);

            if (_weapon.Type == WeaponType.Bat)
            {
                yield return MoveTo(originalPosition);
            }
        }

        private IEnumerator MoveTo(Vector3 position)
        {
            _animator.SetFloat("Speed", _speed);

            var step = _speed * Time.deltaTime;
            float distance;
            do
            {
                distance = Vector3.Distance(transform.position, position);
                transform.position = Vector3.MoveTowards(transform.position, position, step);
                yield return null;
            } while (distance > 0.5f);

            _animator.SetFloat("Speed", 0f);
        }


        public void TakeDamage(int damage)
        {
            if (_damageEffect)
            {
                foreach (var effect in _damageEffect.GetComponentsInChildren<ParticleSystem>())
                {
                    effect.Play();
                }
            }

            if (_soundEffect) _soundEffect.PlaySoundEffect("Hit");

            _health.TakeDamage(damage);
        }
    }
}