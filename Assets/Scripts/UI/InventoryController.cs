using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : Singleton<InventoryController>
{
    private WeaponInfo curWeapon;
    private int curIndex;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        EquipStartingWeapon();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !ActiveWeapon.Instance.IsAttacking)
        {
            curIndex = (int)KeyCode.Alpha1 - 49;
            ActiveSlot();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !ActiveWeapon.Instance.IsAttacking)
        {
            curIndex = (int)KeyCode.Alpha2 - 49;
            ActiveSlot();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !ActiveWeapon.Instance.IsAttacking)
        {
            curIndex = (int)KeyCode.Alpha3 - 49;
            ActiveSlot();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !ActiveWeapon.Instance.IsAttacking)
        {
            curIndex = (int)KeyCode.Alpha4 - 49;
            ActiveSlot();
        }
    }
    private void ActiveSlot()
    {
        foreach (Transform trans in this.transform)
        {
            trans.Find("Active").gameObject.SetActive(false);
        }
        gameObject.transform.GetChild(curIndex).Find("Active").gameObject.SetActive(true);
        ChangeWeapon();
    }
    private void ChangeWeapon()
    {
        if(ActiveWeapon.Instance.curWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.curWeapon.gameObject);
        }
        if (transform.GetChild(curIndex).GetComponent<InventorySlot>().GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.SetWeapon(null);
            return;
        }
        curWeapon = transform.GetChild(curIndex).GetComponent<InventorySlot>().GetWeaponInfo();// get weapon info
        GameObject wp = Instantiate(curWeapon.weapon, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        wp.transform.parent = ActiveWeapon.Instance.transform;
        //create the weapon
        ActiveWeapon.Instance.SetWeapon(wp.GetComponent<MonoBehaviour>());
        //set the weapon into active weapon
    }
    public void EquipStartingWeapon()
    {
        curIndex = 0;// default index 0 = sword
        ActiveSlot();
    }
    public GameObject GetWeapon()
    {
        return curWeapon.weapon;
    }
}
