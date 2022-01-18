using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer champIcon;
    [SerializeField] SpriteRenderer glowSprite;
    [SerializeField] SpriteRenderer winLoseSprite;
    [SerializeField] Color winColor;
    [SerializeField] Color loseColor;
    [Space]

    [SerializeField] LineRenderer playerStats;
    [SerializeField] LineRenderer avgStats;
    [SerializeField] LineRenderer maxStats;
    [SerializeField] float size;
    [Space]

    [SerializeField] Text playerKills;
    [SerializeField] Text playerDeaths;
    [SerializeField] Text playerAssists;
    [SerializeField] Text playerMinions;
    [SerializeField] Text playerGold;
    [SerializeField] Text playerVisionScore;

    [SerializeField] Text killRating;
    [SerializeField] Text deathRating;
    [SerializeField] Text assistsRating;
    [SerializeField] Text minionsRating;
    [SerializeField] Text goldRating;
    [SerializeField] Text visionScoreRating;
    [Space]

    static readonly float deathX = 0.5f;
    static readonly float deathY = 0.866f;
    static readonly float assistX = 1.0f;
    static readonly float assistY = 0;
    static readonly float minionsX = 0.5f;
    static readonly float minionsY = -0.866f;
    static readonly float goldX = -0.5f;
    static readonly float goldY = -0.866f;
    static readonly float visScoreX = -1.0f;
    static readonly float visScoreY = 0.0f;
    static readonly float killX = -0.5f;
    static readonly float killY = 0.866f;

    public float kills { get; private set; }
    public float deaths { get; private set; }
    public float assists { get; private set; }
    public float minions { get; private set; }
    public float gold { get; private set; }
    public float visionScore { get; private set; }
    public float killRate { get; private set; }
    public float deathRate { get; private set; }
    public float assistRate { get; private set; }
    public float minionRate { get; private set; }
    public float goldRate { get; private set; }
    public float visionScoreRate { get; private set; }


    ParticipantDTO player = null;

    public void Start()
    {

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
                winTeamMinions += p.totalMinionsKilled + p.neutralMinionsKilled;
                winTeamGold += p.goldEarned;
                winTeamVisionScore += p.visionScore;
            }
            else
            {
                loseTeamKills += p.kills;
                loseTeamAssists += p.assists;
                loseTeamDeaths += p.deaths;
                loseTeamMinions += p.totalMinionsKilled + p.neutralMinionsKilled;
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
            winLoseSprite.color = winColor;
        }
        else
        {
            winLoseSprite.color = loseColor;
        }



        // Get player ratios
        float avgKill = (winTeamKills + loseTeamKills) * 0.1f;
        float avgDeath = (winTeamDeaths + loseTeamDeaths) * 0.1f;
        float avgAssist = (winTeamAssists + loseTeamAssists) * 0.1f;
        float avgMinions = (winTeamMinions + loseTeamMinions) * 0.1f;
        float avgGold = (winTeamGold + loseTeamGold) * 0.1f;
        float avgVisScore = (winTeamVisionScore + loseTeamVisionScore) * 0.1f;

        avgKill = avgKill == 0 ? 1 : avgKill;
        avgDeath = avgDeath == 0 ? 1 : avgDeath;
        avgAssist = avgAssist == 0 ? 1 : avgAssist;
        avgMinions = avgMinions == 0 ? 1 : avgMinions;
        avgGold = avgGold == 0 ? 1 : avgGold;
        avgVisScore = avgVisScore == 0 ? 1 : avgVisScore;


        // Save player stats
        kills = player.kills;
        deaths = player.deaths;
        assists = player.assists;
        minions = player.neutralMinionsKilled + player.totalMinionsKilled;
        gold = player.goldEarned;
        visionScore = player.visionScore;

        killRate = Mathf.Clamp(player.kills / avgKill, 0, 2);
        deathRate = Mathf.Clamp(player.deaths / avgDeath, 0, 2);
        assistRate = Mathf.Clamp(player.assists / avgAssist, 0, 2);
        minionRate = Mathf.Clamp((player.totalMinionsKilled + player.neutralMinionsKilled) / avgMinions, 0, 2);
        goldRate = Mathf.Clamp(player.goldEarned / avgGold, 0, 2);
        visionScoreRate = Mathf.Clamp(player.visionScore / avgVisScore, 0, 2);



        // Reverse death rate, since it's the only statistic where less means better
        deathRate = -deathRate + 2;

        float killsStep = playerTeamKills == 0 ? 0 : (float)player.kills * 3 / playerTeamKills;
        float deathsStep = playerTeamDeaths == 0 ? 0 : (float)player.deaths * 3 / playerTeamDeaths;
        float assistsStep = playerTeamAssists == 0 ? 0 : (float)player.assists * 3 / playerTeamAssists;
        float minionsStep = playerTeamMinions == 0 ? 0 : (float)(player.totalMinionsKilled * player.neutralMinionsKilled) * 3 / playerTeamMinions;
        float goldStep = playerTeamGold == 0 ? 0 : (float)player.goldEarned * 3 / playerTeamGold;
        float visScoreStep = playerTeamVisionScore == 0 ? 0 : (float)player.visionScore * 3 / playerTeamVisionScore;

        // Set positions of KDA radial graph
        playerStats.SetPosition(0, size * .5f * killRate * new Vector3(killX, killY, 0));
        playerStats.SetPosition(1, size * .5f * deathRate * new Vector3(deathX, deathY, 0));
        playerStats.SetPosition(2, size * .5f * assistRate * new Vector3(assistX, assistY, 0));
        playerStats.SetPosition(3, size * .5f * minionRate * new Vector3(minionsX, minionsY, 0));
        playerStats.SetPosition(4, size * .5f * goldRate * new Vector3(goldX, goldY, 0));
        playerStats.SetPosition(5, size * .5f * visionScoreRate * new Vector3(visScoreX, visScoreY, 0));

        //avgStats.SetPosition(0, size * .5f * new Vector3(killX, killY, 0));
        //avgStats.SetPosition(1, size * .5f * new Vector3(deathX, deathY, 0));
        //avgStats.SetPosition(2, size * .5f * new Vector3(assistX, assistY, 0));
        //avgStats.SetPosition(3, size * .5f * new Vector3(minionsX, minionsY, 0));
        //avgStats.SetPosition(4, size * .5f * new Vector3(goldX, goldY, 0));
        //avgStats.SetPosition(5, size * .5f * new Vector3(visScoreX, visScoreY, 0));

        //maxStats.SetPosition(0, size * new Vector3(killX, killY, 0));
        //maxStats.SetPosition(1, size * new Vector3(deathX, deathY, 0));
        //maxStats.SetPosition(2, size * new Vector3(assistX, assistY, 0));
        //maxStats.SetPosition(3, size * new Vector3(minionsX, minionsY, 0));
        //maxStats.SetPosition(4, size * new Vector3(goldX, goldY, 0));
        //maxStats.SetPosition(5, size * new Vector3(visScoreX, visScoreY, 0));



        //Gradient gradient = new Gradient();
        //GradientColorKey[] colors = new GradientColorKey[7];
        //GradientAlphaKey[] alphas = new GradientAlphaKey[1];

        //colors[0].color = Color.Lerp(redColor, greenColor, killsStep);
        //colors[1].color = Color.Lerp(greenColor, redColor, deathsStep);
        //colors[2].color = Color.Lerp(redColor, greenColor, assistsStep);
        //colors[3].color = Color.Lerp(redColor, greenColor, minionsStep);
        //colors[4].color = Color.Lerp(redColor, greenColor, goldStep);
        //colors[5].color = Color.Lerp(redColor, greenColor, visScoreStep);
        //colors[6].color = colors[0].color;

        //for (int i = 0; i < colors.Length; i++)
        //{
        //    colors[i].time = (float)i / colors.Length;
        //}

        //alphas[0].alpha = 1f;
        //alphas[0].time = 0f;

        //gradient.SetKeys(colors, alphas);
        //playerStats.colorGradient = gradient;



        // Set data
        playerKills.text = player.kills.ToString();
        playerDeaths.text = player.deaths.ToString();
        playerAssists.text = player.assists.ToString();
        playerMinions.text = (player.totalMinionsKilled + player.neutralMinionsKilled).ToString();
        playerGold.text = player.goldEarned.ToString();
        playerVisionScore.text = player.visionScore.ToString();

        killRating.text = RateStatistic(killRate);
        deathRating.text = RateStatistic(deathRate);
        assistsRating.text = RateStatistic(assistRate);
        minionsRating.text = RateStatistic(minionRate);
        goldRating.text = RateStatistic(goldRate);
        visionScoreRating.text = RateStatistic(visionScoreRate);
    }

    public string RateStatistic(float rate)
    {
        int score = (int)(rate * 7.5f);
        string[] scores = { "D-", "D", "D+",
                            "C-", "C", "C+",
                            "B-", "B", "B+",
                            "A-", "A", "A+",
                            "S-", "S", "S+", "S+" };


        return scores[score];
    }
}
