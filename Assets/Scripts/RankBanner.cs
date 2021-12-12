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
    [SerializeField] Text text;


    public void SetUnranked()
    {
        trimRenderer.sprite = unrankedTrim;
        text.text = "Rank: None";
    }
    public void SetIron(string div)
    {
        trimRenderer.sprite = ironTrim;
        text.text = "Rank: Iron " + div;
    }
    public void SetBronze(string div)
    {
        trimRenderer.sprite = bronzeTrim;
        text.text = "Rank: Bronze " + div;
    }
    public void SetSilver(string div)
    {
        trimRenderer.sprite = silverTrim;
        text.text = "Rank: Silver " + div;
    }
    public void SetGold(string div)
    {
        trimRenderer.sprite = goldTrim;
        text.text = "Rank: Gold " + div;
    }
    public void SetPlat(string div)
    {
        trimRenderer.sprite = platTrim;
        text.text = "Rank: Platinum " + div;
    }
    public void SetDiamond(string div)
    {
        trimRenderer.sprite = diamondTrim;
        text.text = "Rank: Diamond " + div;
    }
    public void SetMaster()
    {
        trimRenderer.sprite = masterTrim;
        text.text = "Rank: Master";
    }
    public void SetGrandmaster()
    {
        trimRenderer.sprite = grandmasterTrim;
        text.text = "Rank: Grandmaster";
    }
    public void SetChallenger()
    {
        trimRenderer.sprite = challengerTrim;
        text.text = "Rank: Challenger";
    }
}
