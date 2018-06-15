using Assets.Support;
using Assets.Model;

using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class RelayForm
{
    /// <summary>
    /// 행동 분류
    /// </summary>
    [JsonProperty("pattern")]
    public Pattern Pattern;

    /// <summary>
    /// 움직인 플레이어의 색
    /// </summary>
    [JsonProperty("color")]
    public string Color;

    /// <summary>
    /// 시작 위치
    /// </summary>
    [JsonProperty("startLocation")]
    public Location StartLocation;

    /// <summary>
    /// 끝 위치
    /// </summary>
    [JsonProperty("endLocation")]
    public Location EndLocation;

    /// <summary>
    /// 캐슬링 시작 위치
    /// </summary>
    [JsonProperty("castlingStartLocation")]
    public Location CastlingStartLocation;

    /// <summary>
    /// 캐슬링 끝 위치
    /// </summary>
    [JsonProperty("castlingEndLocation")]
    public Location CastlingEndLocation;

    /// <summary>
    /// 프로모션으로 선택한 기물 종류
    /// </summary>
    [JsonProperty("promotionType")]
    public string PromotionType;

    /// <summary>
    /// 사용한 스킬의 레벨
    /// </summary>
    [JsonProperty("skillLevel")]
    public int SkillLevel;

    /// <summary>
    /// 턴이 종료되었는가?
    /// </summary>
    [JsonProperty("turnFinished")]
    public bool TurnFinished;
}
