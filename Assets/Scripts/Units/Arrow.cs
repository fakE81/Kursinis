using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Transform target;
    public GameObject impactEffect;
    public float speed = 50f;
    public float damage = 10f;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // distance length 
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized*distanceThisFrame,Space.World);
    }

    void HitTarget()
    {
        // Hit effects.
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }

    public void setDamage(float damage){
        this.damage = damage;
    }
}
