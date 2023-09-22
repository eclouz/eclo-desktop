using Dtos.Auth;
using Dtos.Comment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer.Comments.Concrete;

public class CommentService : ICommentService
{
    public async Task<bool> CreateComment(CommentDto commentDto, string token)
    {
        using (var client = new HttpClient())
        {
            //API.CREATE_Comment
            var request = new HttpRequestMessage(HttpMethod.Post, API.BASE_URL + "user/product/comments");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();

            content.Add(new StringContent($"{commentDto.ProductId}"), "ProductId");
            content.Add(new StringContent($"{commentDto.UserId}"), "UserId");
            content.Add(new StringContent(commentDto.Comment), "Comment");
            content.Add(new StringContent($"{commentDto.IsEdited}"), "IsEdited");

            request.Content = content;

            //Send Request
            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseRead = await response.Content.ReadAsStringAsync();
                //var responsJson = JsonConvert.DeserializeObject<RegisterCheckDto>(responseRead);

                return  true;
            }

            return false;
        }
    }
}
