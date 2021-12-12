public abstract class DTO { }

public class ChampionDTO : DTO
{
    public long id { get; set; }
    public string name { get; set; }
}

public class ChampionMasteryDTO : DTO
{
    public bool chestGranted { get; set; }
    public long championId { get; set; }
    public long lastPlayTime { get; set; }
    public int championPoints { get; set; }
}

public class SummonerDTO : DTO
{
    public string id { get; set; }
    public string puuid { get; set; }
    public int profileIconId { get; set; }
    public long summonerLevel { get; set; }
}

public class LeagueEntryDTO : DTO
{
    public string leagueId { get; set; }
    public string summonerId { get; set; }
    public string summonerName { get; set; }
    public string tier { get; set; }
    public string rank { get; set; }
    public int wins { get; set; }
    public int losses { get; set; }
}

public class MatchDTO : DTO
{
    public InfoDto info { get; set; }
}

public class InfoDto : DTO
{
    public long gameCreation { get; set; }
    public long gameDuration { get; set; }
    public ParticipantDTO[] participants { get; set; }
}

public class ParticipantDTO : DTO
{
    public int assists { get; set; }
    public int deaths { get; set; }
    public int goldEarned { get; set; }
    public int kills { get; set; }
    public int championId { get; set; }
    public int totalMinionsKilled { get; set; }
    public string puuid { get; set; }
    public int visionScore { get; set; }
    public bool win { get; set; }
}

public class MatchInfo
{
    public long gameCreation { get; set; }
    public long gameDuration { get; set; }
    public ParticipantDTO[] participants { get; set; }
}