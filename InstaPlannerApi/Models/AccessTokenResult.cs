namespace InstaPlannerApi.Models
{
    public class AccessTokenResult
    {
        /// <summary>
        /// Retrieved AccesToken from Instagram user
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// Retrieved userId from Instagram user
        /// </summary>
        public string user_id { get; set; }
    }   
}