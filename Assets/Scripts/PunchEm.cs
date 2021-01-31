using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PunchEm : MonoBehaviour
{

    [SerializeField]
    private Attack[] possibleAttacks;

    private Dictionary<string, Attack> namedAttacks = new Dictionary<string, Attack>();

    private void Start() {
        foreach (Attack attack in possibleAttacks)
        {
            namedAttacks.Add(attack.attackName, attack);
        }
    }

    public bool DoAttack(string attackName, bool facingRight) {

        if(namedAttacks.TryGetValue(attackName, out Attack chosenAtk)) {
            
            Vector2 forward = facingRight ? Vector2.right : Vector2.left;
            Vector2 startPosition = chosenAtk.origin.position;
            
            Vector2 orthoDir = new Vector2(forward.y, -forward.x);
            orthoDir.Normalize();
            Vector2 cornerA = startPosition + orthoDir * chosenAtk.areaOfEffect;
            Vector2 cornerB = (startPosition + forward * chosenAtk.range) + orthoDir * -chosenAtk.areaOfEffect;

            Collider2D[] colliders = Physics2D.OverlapAreaAll(cornerA, cornerB);
            Debug.DrawLine(chosenAtk.origin.position, new Vector3(cornerB.x, cornerB.y, chosenAtk.origin.position.z), Color.red, 100f);
            Debug.DrawLine(chosenAtk.origin.position, new Vector3(cornerA.x, cornerA.y, chosenAtk.origin.position.z), Color.green, 100f);
            foreach (Collider2D collider in colliders)
            {
                if(collider.gameObject != gameObject &&
                 (  collider.gameObject.tag == "Player" || 
                    collider.gameObject.tag == "Enemy" || 
                    collider.gameObject.tag == "DamageObject"))
                {
                    ReceiveDamage receiveDamage = collider.gameObject.GetComponent<ReceiveDamage>();
                    if (receiveDamage != null) {
                        receiveDamage.receiveDamage(chosenAtk.damage);
                        return true;
                    }
                }
            };
            if(chosenAtk.soundClip != string.Empty) { AudioController.Instance.PlayOneshotClip(chosenAtk.soundClip); }
        } else {
            Debug.Log($"Attack with name {attackName} not found");
        }

        return false;
    }
}
