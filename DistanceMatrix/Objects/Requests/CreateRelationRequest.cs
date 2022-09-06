namespace DistanceMatrix.Objects.Requests
{
    public class CreateRelationRequest
    {
        public string FromAreaCode { get; set; }
        public string FromCorridor { get; set; }
        public long FromModule { get; set; }
        public string ToAreaCode { get; set; }
        public string ToCorridor { get; set; }
        public long ToModule { get; set; }
        public long Distance { get; set; }
    }
}