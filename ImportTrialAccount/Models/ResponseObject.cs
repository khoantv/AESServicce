namespace ImportTrialAccount.Models
{
    public class ResponseObject<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public dynamic errors { get; set; }
    }
}
