using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;
    
    private void OnCollisionEnter2D(Collision2D collision){
        EvaluateCollision(collision);
        RecieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision){
        EvaluateCollision(collision);
        RecieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision){
        onGround = false;
        friction = 0f;
    }

    private void EvaluateCollision(Collision2D collision){
        for(int i = 0; i < collision.contactCount; i++){
            Vector2 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }

    private void RecieveFriction(Collision2D collision){
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

        friction = 0f;

        if(material != null) friction = material.friction;
    }

    public bool GetOnGround(){
        return onGround;
    }

    public float GetFriction(){
        return friction;
    }
}