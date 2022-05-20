namespace Utilities_aspnet.Statistic.Dtos; 

public class AdminStatisticDto {
    public AdminStatisticDto() {
        VisitorDic = new Dictionary<DateTime, int>();
        UserAgentTodayDic = new Dictionary<string, int>();
        BotAgentTodayDic = new Dictionary<string, int>();

        UserAgentDic = new Dictionary<string, int>();
        BotAgentDic = new Dictionary<string, int>();

        TopUrlDic = new Dictionary<string, int>();
        TopUrlTodayDic = new Dictionary<string, int>();

        OsDic = new Dictionary<string, int>();
        OsDic = new Dictionary<string, int>();
        HoursDic = new Dictionary<byte, int>();
    }

    public int CountOfTodayVisitor { get; set; }
    public int CountOfTodayPageVisit { get; set; }
    public int CountOfTodayBot { get; set; }

    public int CountOfYesterdayVisitor { get; set; }
    public int CountOfYesterdayPageVisit { get; set; }
    public int CountOfYesterdayBot { get; set; }

    public int CountTotalComment { get; set; }
    public int CountNewComment { get; set; }


    public Dictionary<DateTime, int> VisitorDic { get; set; }
    public Dictionary<string, int> UserAgentTodayDic { get; set; }
    public Dictionary<string, int> BotAgentTodayDic { get; set; }

    public Dictionary<string, int> UserAgentDic { get; set; }
    public Dictionary<string, int> BotAgentDic { get; set; }
    public Dictionary<string, int> TopUrlDic { get; set; }
    public Dictionary<string, int> TopUrlTodayDic { get; set; }

    public Dictionary<string, int> OsDic { get; set; }
    public Dictionary<string, int> OsDicToday { get; set; }

    public Dictionary<byte, int> HoursDic { get; set; }


    public int UserCount { get; set; }
}