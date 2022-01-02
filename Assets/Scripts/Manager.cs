using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    [SerializeField] SpriteRenderer iconSprite;
    [SerializeField] SpriteRenderer maskRenderer;
    [SerializeField] Text levelLabel;

    [Space]
    [SerializeField] InputField summonerNameInput;
    [SerializeField] Dropdown regionDropdown;
    [SerializeField] Text errorMessage;

    [Space]
    [SerializeField] Text topChampsLabel;
    [SerializeField] TopChampIcon[] topChamps;
    [SerializeField] float maxOffset;
    [SerializeField] float topChampsSpeed;

    [Space]
    [SerializeField] RankBanner banner;

    [Space]
    [SerializeField] MatchItem[] matchLabels;
    [SerializeField] GameObject matchHistory;


    SummonerDTO player;
    ChampionDTO[] champs;
    ChampionMasteryDTO[] champsMastery;
    float startY;


    public void Start()
    {
        // Api stuff
        RiotApi.SetApiKey("RGAPI-0467abfa-db51-40e9-a467-65c443481d9a");
        RiotApi.SetRegion(regionDropdown.captionText.text);

        // Portrait
        champs = RiotApi.GetChampsInfo();
        RiotApi.DownloadChampionSprites(champs);
        Destroy(maskRenderer);

        startY = topChamps[0].transform.localPosition.y;

        HideElements();
    }

    public void OnRegionChange()
    {
        string newRegion = regionDropdown.options[regionDropdown.value].text;
        RiotApi.SetRegion(newRegion);
    }
    public void LoadSummoner()
    {
        if (LoadPlayerIcon())
        {
            LoadRankBanner();
            LoadTopChamps();
            LoadMatchHistory();
        }
    }

    public bool LoadPlayerIcon()
    {
        player = RiotApi.GetSummonerInfo(summonerNameInput.text);

        if (player == null)
        {
            HideElements();
            errorMessage.text = "Nie znaleziono gracza " + summonerNameInput.text;
            return false;
        }
        errorMessage.text = string.Empty;

        iconSprite.gameObject.SetActive(true);
        iconSprite.sprite = RiotApi.DownloadIconSprite(player.profileIconId);
        levelLabel.text = "Lv: " + player.summonerLevel.ToString();

        return true;
    }
    
    public void LoadRankBanner()
    {
        banner.gameObject.SetActive(true);

        var playerRank = RiotApi.GetEntries(player.id);

        if (playerRank == null)
        {
            banner.SetUnranked();
            return;
        }

        var rank = playerRank.rank;

        switch (playerRank.tier)
        {
            case "UNRANKED":
                banner.SetUnranked();
                break;
            case "IRON":
                banner.SetIron(rank);
                break;
            case "BRONZE":
                banner.SetBronze(rank);
                break;
            case "SILVER":
                banner.SetSilver(rank);
                break;
            case "GOLD":
                banner.SetGold(rank);
                break;
            case "PLATINUM":
                banner.SetPlat(rank);
                break;
            case "DIAMOND":
                banner.SetDiamond(rank);
                break;
            case "MASTER":
                banner.SetMaster();
                break;
            case "GRANDMASTER":
                banner.SetGrandmaster();
                break;
            case "CHALLENGER":
                banner.SetChallenger();
                break;
        }

        int wins = playerRank.wins;
        int losses = playerRank.losses;
        float winrate = (float)wins / (wins + losses);
    }

    public void LoadTopChamps()
    {
        var champsMastery = RiotApi.GetChampsMasteryInfo().ToList();
        champsMastery.OrderBy(o => o.championPoints);

        this.champsMastery = champsMastery.ToArray();

        float denominator = 1f / champsMastery[0].championPoints;

        for (int i = 0; i < topChamps.Length; i++)
        {
            topChamps[i].gameObject.SetActive(true);
            RiotApi.champsPortraits.TryGetValue(champsMastery[i].championId, out Sprite sprite);
            topChamps[i].score = champsMastery[i].championPoints;
            topChamps[i].spriteRenderer.sprite = sprite;
            StartCoroutine(MoveTopChampPortrait(topChamps[i], champsMastery[i].championPoints * denominator));
        }

        topChampsLabel.enabled = true;
    }
    IEnumerator MoveTopChampPortrait(TopChampIcon icon, float ratio)
    {
        // Set target
        float targetY = startY + (maxOffset * ratio);

        // Save coords
        float x = icon.transform.localPosition.x;
        float z = icon.transform.localPosition.z;
        icon.transform.localPosition = new Vector3(x, startY, z);

        // Set line renderer
        icon.lineRenderer.SetPosition(0, icon.transform.position - new Vector3(0, .5f, 0));
        icon.lineRenderer.SetPosition(1, icon.transform.position - new Vector3(0, .5f, 0));

        float width = .85f;
        icon.lineRenderer.startWidth = width;
        icon.lineRenderer.endWidth = width;

        // Get line renrerer colors
        Color[] colors = GetDominantColors(icon.spriteRenderer.sprite);
        icon.lineRenderer.startColor = colors[0];
        icon.lineRenderer.endColor = colors[1];

        float i = 0;
        while (i < 1)
        {
            icon.transform.localPosition = new Vector3(x, Mathf.Lerp(startY, targetY, (Mathf.Sqrt(i) + i) / 2), z);
            icon.lineRenderer.SetPosition(1, icon.transform.position);
            i += topChampsSpeed * Time.deltaTime;
            yield return null;
        }
        icon.transform.localPosition = new Vector3(x, targetY, z);
    }
    Color[] GetDominantColors(Sprite sprite)
    {
        Texture2D tex = sprite.texture;
        var colors = new Dictionary<Color, float>();

        int offset = 20;

        for (int r = 0; r < tex.width; r += offset)
        {
            for (int c = 0; c < tex.height; c += offset)
            {
                Color color = tex.GetPixel(r, c);
                color.r = (float)((int)(color.r * 10)) / 10;
                color.g = (float)((int)(color.g * 10)) / 10;
                color.b = (float)((int)(color.b * 10)) / 10;
                float avg = (color.r + color.g + color.b) / 3;

                if (colors.ContainsKey(color))
                {
                    colors[color] += 1f / avg;
                }
                else
                {
                    colors.Add(color, 1f / avg);
                }
            }
        }

        colors.OrderBy(o => o.Value);
        return new Color[] { colors.ElementAt(0).Key, colors.ElementAt(3).Key };
    }

    public void LoadMatchHistory()
    {
        int amount = matchLabels.Length;
        var matches = RiotApi.GetMatches(amount);

        for (int i = 0; i < amount; i++)
        {
            matchLabels[i].gameObject.SetActive(true);
            var m = matches[i];
            matchLabels[i].SetGame(m);
        }

        matchHistory.SetActive(true);
    }

    public void HideElements()
    {
        iconSprite.gameObject.SetActive(false);
        levelLabel.text = string.Empty;

        // Error message
        errorMessage.text = string.Empty;

        // Top 5 champs
        foreach (var icon in topChamps)
            icon.gameObject.SetActive(false);
        topChampsLabel.enabled = false;
        TopChampIcon.label = topChampsLabel;

        // Rank banner
        banner.gameObject.SetActive(false);

        // Match history
        foreach (var ml in matchLabels)
            ml.gameObject.SetActive(false);
        matchHistory.SetActive(false);

    }
}
