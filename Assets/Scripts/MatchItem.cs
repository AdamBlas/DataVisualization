using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour
{
    [SerializeField] GameObject gfxLayer;
    [SerializeField] GameObject preciseLayer;
    [Space]

    [SerializeField] SpriteRenderer champIcon;
    [SerializeField] SpriteRenderer gameStatusSprite;
    [SerializeField] Text gameStatusText;
    [SerializeField] Sprite winMark;
    [SerializeField] Sprite loseMark;
    [Space]

    [SerializeField] LineRenderer playerKDA;
    [SerializeField] LineRenderer playerMGV;
    [Space]

    [SerializeField] Text playerKills;
    [SerializeField] Text playerDeaths;
    [SerializeField] Text playerAssists;
    [SerializeField] Text playerMinions;
    [SerializeField] Text playerGold;
    [SerializeField] Text playerVisionScore;
    [SerializeField] Text playerMinionsPerMin;
    [SerializeField] Text playerGoldPerMin;
    [SerializeField] Text playerVisScorePerMin;
    [SerializeField] Text gameLength;


    static readonly float killX = -0.86602540378f;
    static readonly float killY = 0.5f;
    static readonly float assistX = 0f;
    static readonly float assistY = -1f;
    static readonly float deathX = 0.86602540378f;
    static readonly float deathY = 0.5f;

    Color redColor = new Color(0.5f, 0, 0);
    Color greenColor = new Color(0, 0.5f, 0);

    ParticipantDTO player = null;

    public void Start()
    {
        OnMouseExit();
    }

    public void SetGame(MatchInfo info)
    {
        // Get match length values to mm:ss format
        long matchLengthSec = info.gameDuration;
        long matchLengthMin = matchLengthSec / 60;
        matchLengthSec -= (matchLengthMin * 60);
        float matchLengthMinFloat = (float)info.gameDuration / 60;

        // Get game stats
        int winTeamKills = 0;
        int winTeamAssists = 0;
        int winTeamDeaths = 0;
        int winTeamMinions = 0;
        int winTeamGold = 0;
        int winTeamVisionScore = 0;

        int loseTeamKills = 0;
        int loseTeamAssists = 0;
        int loseTeamDeaths = 0;
        int loseTeamMinions = 0;
        int loseTeamGold = 0;
        int loseTeamVisionScore = 0;

        foreach  (var p in info.participants)
        {
            if (p.puuid.Equals(RiotApi.puuid))
                player = p;

            if (p.win)
            {
                winTeamKills += p.kills;
                winTeamAssists += p.assists;
                winTeamDeaths += p.deaths;
                winTeamMinions += p.totalMinionsKilled;
                winTeamGold += p.goldEarned;
                winTeamVisionScore += p.visionScore;
            }
            else
            {
                loseTeamKills += p.kills;
                loseTeamAssists += p.assists;
                loseTeamDeaths += p.deaths;
                loseTeamMinions += p.totalMinionsKilled;
                loseTeamGold += p.goldEarned;
                loseTeamVisionScore += p.visionScore;
            }
        }


        // Get player team stats
        int playerTeamKills;
        int playerTeamAssists;
        int playerTeamDeaths;
        int playerTeamMinions;
        int playerTeamGold;
        int playerTeamVisionScore;

        if (player.win)
        {
            playerTeamKills = winTeamKills;
            playerTeamAssists = winTeamAssists;
            playerTeamDeaths = winTeamDeaths;
            playerTeamMinions = winTeamMinions;
            playerTeamGold = winTeamGold;
            playerTeamVisionScore = winTeamVisionScore;
        }
        else
        {
            playerTeamKills = loseTeamKills;
            playerTeamAssists = loseTeamAssists;
            playerTeamDeaths = loseTeamDeaths;
            playerTeamMinions = loseTeamMinions;
            playerTeamGold = loseTeamGold;
            playerTeamVisionScore = loseTeamVisionScore;
        }


        // Set champ portrait
        RiotApi.champsPortraits.TryGetValue(player.championId, out Sprite sprite);
        champIcon.sprite = sprite;

        // Set win/lose GFX
        if (player.win)
        {
            gameStatusSprite.sprite = winMark;
            gameStatusText.text = "W\nI\nN";
            gameStatusText.color = new Color(0, 128, 0);
        }
        else
        {
            gameStatusSprite.sprite = loseMark;
            gameStatusText.text = "L\nO\nS\nE";
            gameStatusText.color = new Color(128, 0, 0);
        }


        // Get player ratios
        float avgKill = (winTeamKills + loseTeamKills) * 0.1f;
        float avgDeath = (winTeamDeaths + loseTeamDeaths) * 0.1f;
        float avgAssist = (winTeamAssists + loseTeamAssists) * 0.1f;
        float avgMinions = (winTeamMinions + loseTeamMinions) * 0.1f;
        float avgGold = (winTeamGold + loseTeamGold) * 0.1f;
        float avgVisScore = (winTeamVisionScore + loseTeamVisionScore) * 0.1f;

        float killRate = Mathf.Clamp(player.kills / avgKill, 0, 2);
        float deathRate = Mathf.Clamp(player.deaths / avgDeath, 0, 2);
        float assistRate = Mathf.Clamp(player.assists / avgAssist, 0, 2);
        float minionsRate = Mathf.Clamp(player.totalMinionsKilled / avgMinions, 0, 2);
        float goldRate = Mathf.Clamp(player.goldEarned / avgGold, 0, 2);
        float visScoreRate = Mathf.Clamp(player.visionScore / avgVisScore, 0, 2);


        // Set positions of KDA radial graph
        playerKDA.SetPosition(0, 0.5f * killRate * new Vector3(killX, killY, 0));
        playerKDA.SetPosition(1, 0.5f * deathRate * new Vector3(deathX, deathY, 0));
        playerKDA.SetPosition(2, 0.5f * assistRate * new Vector3(assistX, assistY, 0));

        // Set colors of KDA radial graph
        Gradient gradientKDA = new Gradient();
        GradientColorKey[] colorsKDA = new GradientColorKey[4];
        GradientAlphaKey[] alphasKDA = new GradientAlphaKey[1];

        float killsStep = playerTeamKills == 0 ? 0 : (float)player.kills / playerTeamKills;
        float deathsStep = playerTeamDeaths == 0 ? 0 :-((float)player.deaths / playerTeamDeaths) + 1;
        float assistsStep = playerTeamAssists == 0 ? 0 : (float)player.assists / playerTeamAssists;

        colorsKDA[0].color = Color.Lerp(redColor, greenColor, killsStep);
        colorsKDA[0].time = 0f;
        colorsKDA[1].color = Color.Lerp(redColor, greenColor, deathsStep);
        colorsKDA[1].time = 0.33f;
        colorsKDA[2].color = Color.Lerp(redColor, greenColor, assistsStep);
        colorsKDA[2].time = 0.66f;
        colorsKDA[3].color = colorsKDA[0].color;
        colorsKDA[3].time = 1f;

        alphasKDA[0].alpha = 1f;
        alphasKDA[0].time = 0f;

        gradientKDA.SetKeys(colorsKDA, alphasKDA);
        playerKDA.colorGradient = gradientKDA;


        // Set positions of MGV radial graph
        playerMGV.SetPosition(0, 0.5f * minionsRate * new Vector3(killX, killY, 0));
        playerMGV.SetPosition(1, 0.5f * goldRate * new Vector3(deathX, deathY, 0));
        playerMGV.SetPosition(2, 0.5f * visScoreRate * new Vector3(assistX, assistY, 0));

        // Set colors of MGV radial graph
        Gradient gradientMGV = new Gradient();
        GradientColorKey[] colorsMGV = new GradientColorKey[4];
        GradientAlphaKey[] alphasMGV = new GradientAlphaKey[1];

        colorsMGV[0].color = Color.Lerp(redColor, greenColor, (float)player.totalMinionsKilled / playerTeamKills);
        colorsMGV[0].time = 0f;
        colorsMGV[1].color = Color.Lerp(redColor, greenColor, (float)player.goldEarned / playerTeamDeaths);
        colorsMGV[1].time = 0.33f;
        colorsMGV[2].color = Color.Lerp(redColor, greenColor, (float)player.visionScore / playerTeamAssists);
        colorsMGV[2].time = 0.66f;
        colorsMGV[3].color = colorsMGV[0].color;
        colorsMGV[3].time = 1f;

        alphasMGV[0].alpha = 1f;
        alphasMGV[0].time = 0f;

        gradientMGV.SetKeys(colorsMGV, alphasMGV);
        playerMGV.colorGradient = gradientMGV;


        // Set precise data
        playerKills.text = player.kills.ToString();
        playerDeaths.text = player.deaths.ToString();
        playerAssists.text = player.assists.ToString();
        playerMinions.text = player.totalMinionsKilled.ToString();
        playerGold.text = player.goldEarned.ToString();
        playerVisionScore.text = player.visionScore.ToString();
        playerMinionsPerMin.text = (player.totalMinionsKilled / matchLengthMinFloat).ToString("0.0") + "\t\t/min";
        playerGoldPerMin.text = (player.goldEarned / matchLengthMinFloat).ToString("0.0") + "\t/min";
        playerVisScorePerMin.text = (player.visionScore / matchLengthMinFloat).ToString("0.0") + "\t\t/min";
        gameLength.text = matchLengthMin.ToString() + ":" + matchLengthSec.ToString("00");
    }

    public void OnMouseEnter()
    {
        gfxLayer.SetActive(false);
        preciseLayer.SetActive(true);
    }

    public void OnMouseExit()
    {
        gfxLayer.SetActive(true);
        preciseLayer.SetActive(false);
    }
}
