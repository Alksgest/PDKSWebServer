using PdksBuisness.Dtos;

namespace PDKSWebServer.Messages
{
    public class CreateArticleRequestMessage
    {
        public ArticleDto Article { get; set; }
        public AuthToken Token { get; set; }
    }
}
