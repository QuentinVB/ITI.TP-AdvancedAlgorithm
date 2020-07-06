namespace ITI.TP_UserBasedRecommendation
{
    public interface IData
    {
        int Rate { get; set; }
        User User { get; set; }
        Movie Movie { get; set; }
    }
}