using DG.Tweening;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts
{
    public class CharacterSimpleAnimator : MonoBehaviour {
        public SpriteRenderer SpriteRenderer;
        public Transform Bullet;

        public float DiedTime;
        public float BulletShootTime;
        public float UltTime;

        public void DiedAnimation() =>
            SpriteRenderer.DOFade(0f, DiedTime).OnComplete(() => Destroy(gameObject));

        public void SimpleShootAnimation(Vector3 targetPosition) {
            Vector3 startPosition = Bullet.transform.position;

            Bullet.gameObject.SetActive(true);
            Bullet.DOMove(targetPosition, BulletShootTime).OnComplete(()=> {
                Bullet.position = startPosition;
                Bullet.gameObject.SetActive(false);
            });
        }

        public void SetColor() =>
            SpriteRenderer.color = Random.ColorHSV();
    }
}
