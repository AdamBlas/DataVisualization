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
    [SerializeField] OnMouseHoverInfo hoverInfo;


    public void SetUnranked()
    {
        trimRenderer.sprite = unrankedTrim;
        hoverInfo.text = "Rank:\nUnranked";
    }
    public void SetIron(string div)
    {
        trimRenderer.sprite = ironTrim;
        hoverInfo.text = "Rank:\nIron " + div;
    }
    public void SetBronze(string div)
    {
        trimRenderer.sprite = bronzeTrim;
        hoverInfo.text = "Rank:\nBronze " + div;
    }
    public void SetSilver(string div)
    {
        trimRenderer.sprite = silverTrim;
        hoverInfo.text = "Rank:\nSilver " + div;
    }
    public void SetGold(string div)
    {
        trimRenderer.sprite = goldTrim;
        hoverInfo.text = "Rank:\nGold " + div;
    }
    public void SetPlat(string div)
    {
        trimRenderer.sprite = platTrim;
        hoverInfo.text = "Rank:\nPlatinum " + div;
    }
    public void SetDiamond(string div)
    {
        trimRenderer.sprite = diamondTrim;
        hoverInfo.text = "Rank:\nDiamond " + div;
    }
    public void SetMaster()
    {
        trimRenderer.sprite = masterTrim;
        hoverInfo.text = "Rank:\nMaster";
    }
    public void SetGrandmaster()
    {
        trimRenderer.sprite = grandmasterTrim;
        hoverInfo.text = "Rank:\nGrandmaster";
    }
    public void SetChallenger()
    {
        trimRenderer.sprite = challengerTrim;
        hoverInfo.text = "Rank:\nChallenger";
    }
}
