using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashWeaponPrefab : WeaponBaseClass
{
    Rigidbody rigidbody;

    Vector3 WeaponSpeed = new Vector3(0.0f, 0.0f, 0.0f);

    bool Changetremble;

    bool Stoptremble;

    float changeTrembleNum;

    private void Update()
    {
        changeTrembleNum += Time.deltaTime;

        if (changeTrembleNum >= 0.03f)
        {
            _trembleWeapon();

            changeTrembleNum = 0.0f;
        }
    }

    public override void _MoveWeapon(float Speed)
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        WeaponSpeed.y = Speed;

        StartCoroutine(_Delay());
    }

    IEnumerator _Delay()
    {
        yield return new WaitForSeconds(2.0f);

        Stoptremble = true;
        rigidbody.velocity = WeaponSpeed;

    }

    void _trembleWeapon()
    {
        if (Stoptremble) return;

        Vector3 objectPos = this.gameObject.transform.position;

        if (Changetremble)
        {
            objectPos.x -= 0.1f;
            Changetremble = false;
        }
        else
        {
            objectPos.x += 0.1f;
            Changetremble = true;
        }

        this.gameObject.transform.position = objectPos;

    }

}
