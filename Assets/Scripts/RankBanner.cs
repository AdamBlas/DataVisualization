using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankBanner : MonoBehaviour
{
    [SerializeField] Sprite unrankedTrim;
    [SerializeField] Sprite ironTrim;
    [SerializeField] Sprite bronzeTrim;
    [SerializeField] Sprite silverTrim;
    [SerializeField] Sprite goldTrim;
    [SerializeField] Sprite platTrim;
    [SerializeField] Sprite diamondTrim;
    [SerializeField] Sprite masterTrim;
    [SerializeField] Sprite grandmasterTrim;
    [SerializeField] Sprite challengerTrim;

    [SerializeField] SpriteRenderer trimRenderer;


    public void SetUnranked()
    {
        trimRenderer.sprite = unrankedTrim;
    }
    public void SetIron(string div)
    {
        trimRenderer.sprite = ironTrim;
    }
    public void SetBronze(string div)
    {
        trimRenderer.sprite = bronzeTrim;
    }
    public void SetSilver(string div)
    {
        trimRenderer.sprite = silverTrim;
    }
    public void SetGold(string div)
    {
        trimRenderer.sprite = goldTrim;
    }
    public void SetPlat(string div)
    {
        trimRenderer.sprite = platTrim;
    }
    public void SetDiamond(string div)
    {
        trimRenderer.sprite = diamondTrim;
    }
    public void SetMaster()
    {
        trimRenderer.sprite = masterTrim;
    }
    public void SetGrandmaster()
    {
        trimRenderer.sprite = grandmasterTrim;
    }
    public void SetChallenger()
    {
        trimRenderer.sprite = challengerTrim;
    }
}
