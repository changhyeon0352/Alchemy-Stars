using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitHpBar : MonoBehaviour
{
    [SerializeField]
    Image hpImg;
	Unit unit;
	[Range(0f, 100f)]
	public float up=25;

    public void UpdateHpBar(int hp,int hpMax)
    {
        hpImg.fillAmount=(float)hp/(float)hpMax;
    }
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(unit.transform.position+ Vector3.up);
    }

    public void SetUnitInfo(Unit unit)
    {
        this.unit = unit;
    }
}
