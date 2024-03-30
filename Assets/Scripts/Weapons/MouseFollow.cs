using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        /*if(ActiveWeapon.Instance.curWeapon is Staff) */FollowMouse();
        //if(ActiveWeapon.Instance.curWeapon is Bow) FollowMouseBow();
    }
    private void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = transform.position - mousePos;
        transform.right = -dir;
        //if(dir.x > 0 && ActiveWeapon.Instance.curWeapon is Staff)
        //{
        //    ActiveWeapon.Instance.transform.localRotation = Quaternion.Euler(180, 0 , ActiveWeapon.Instance.transform.localRotation.z);
        //}
        //else
        //{
        //    ActiveWeapon.Instance.transform.localRotation = Quaternion.Euler(0, 0, ActiveWeapon.Instance.transform.localRotation.z);
        //}
    }
    private void FollowMouseBow()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if(transform.localEulerAngles.z < 235f && transform.localEulerAngles.z > 70)
        {
            ActiveWeapon.Instance.curWeapon.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            ActiveWeapon.Instance.curWeapon.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
