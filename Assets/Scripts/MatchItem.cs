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

    [SerializeField] Image champIcon;
    [SerializeField] Image gameStatusSprite;
    [SerializeField] Text gameStatusText;
    [SerializeField] Sprite winMark;
    [SerializeField] Sprite loseMark;
    [Space]

    [SerializeField] LineRenderer playerStats;
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
    [Space]

    /*
    [SerializeField] SpriteRenderer lane;
    [SerializeField] Sprite topIcon;
    [SerializeField] Sprite jglIcon;
    [SerializeField] Sprite midIcon;
    [SerializeField] Sprite botIcon;
    [SerializeField] Sprite suppIcon;
    [Space]

    [SerializeField] SpriteRenderer role;
    [SerializeField] Sprite tankIcon;
    [SerializeField] Sprite warriorIcon;
    [SerializeField] Sprite assassinIcon;
    [SerializeField] Sprite mageIcon;
    [SerializeField] Sprite marksmanIcon;
    [SerializeField] Sprite supportIcon;
    [Space]
    */


    static readonly float killX = Mathf.Sin(330);
    static readonly float killY = Mathf.Cos(330);
    static readonly float deathX = Mathf.Sin(30);
    static readonly float deathY = Mathf.Cos(30);
    static readonly float assistX = Mathf.Sin(90);
    static readonly float assistY = Mathf.Cos(90);
    static readonly float minionsX = Mathf.Sin(150);
    static readonly float minionsY = Mathf.Cos(150);
    static readonly float goldX = Mathf.Sin(210);
    static readonly float goldY = Mathf.Cos(210);
    static readonly float visScoreX = Mathf.Sin(270);
    static readonly float visScoreY = Mathf.Cos(270);

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

        // Reverse death rate, since it's the only statistic where less means better
        deathRate = -deathRate + 2;

        float killsStep = playerTeamKills == 0 ? 0 : (float)player.kills / playerTeamKills;
        float deathsStep = playerTeamDeaths == 0 ? 0 : (float)player.deaths / playerTeamDeaths;
        float assistsStep = playerTeamAssists == 0 ? 0 : (float)player.assists / playerTeamAssists;
        float minionsStep = playerTeamMinions == 0 ? 0 : (float)player.totalMinionsKilled / playerTeamMinions;
        float goldStep = playerTeamGold == 0 ? 0 : (float)player.goldEarned / playerTeamGold;
        float visScoreStep = playerTeamVisionScore == 0 ? 0 : (float)player.visionScore / playerTeamVisionScore;

        // Set positions of KDA radial graph
        /*
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
        */
        playerStats.SetPosition(0, .5f * killRate * new Vector3(killX, killY, 0));
        playerStats.SetPosition(1, .5f * deathRate * new Vector3(deathX, deathY, 0));
        playerStats.SetPosition(2, .5f * assistRate * new Vector3(assistX, assistY, 0));
        playerStats.SetPosition(3, .5f * minionsRate * new Vector3(minionsX, minionsY, 0));
        playerStats.SetPosition(4, .5f * goldRate * new Vector3(goldX, goldY, 0));
        playerStats.SetPosition(5, .5f * visScoreRate * new Vector3(visScoreX, visScoreY, 0));

        Gradient gradient = new Gradient();
        GradientColorKey[] colors = new GradientColorKey[7];
        GradientAlphaKey[] alphas = new GradientAlphaKey[1];

        colors[0].color = Color.Lerp(redColor, greenColor, killsStep);
        colors[1].color = Color.Lerp(redColor, greenColor, deathsStep);
        colors[2].color = Color.Lerp(redColor, greenColor, assistsStep);
        colors[3].color = Color.Lerp(redColor, greenColor, minionsStep);
        colors[4].color = Color.Lerp(redColor, greenColor, goldStep);
        colors[5].color = Color.Lerp(redColor, greenColor, visScoreStep);
        colors[6].color = colors[0].color;

        colors[0].time = 0f;
        colors[1].time = 0.167f;
        colors[2].time = 0.334f;
        colors[3].time = 0.5f;
        colors[4].time = 0.667f;
        colors[5].time = 0.834f;
        colors[6].time = 1f;

        alphas[0].alpha = 1f;
        alphas[0].time = 0f;

        gradient.SetKeys(colors, alphas);
        playerStats.colorGradient = gradient;



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
