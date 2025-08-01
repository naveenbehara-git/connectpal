using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using ProjectModels;
using ServerWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServerWeb.Pages
{
    public class AddPostBase : ComponentBase
    {
        
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationstateTask { get; set; }
        
        public Post Post { get; set; } = new Post();
        [Parameter]
        public string id { get; set; }
        public int postid { get; set; }

        public User User { get; set; } = new User();
        [Inject]
        public IPostService PostService { get; set; }
        public string UserEmail { get; set; }
        public string Username { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {   

            var authenticationstate = await AuthenticationstateTask;
            if (authenticationstate.User.Identity.IsAuthenticated)
            {
                
                UserEmail = authenticationstate.User.Identity.Name;
            }
            string[] user = UserEmail.Split('@');
            Username = user[0].ToString();
            User = await UserService.Search(Username);

            if (id != null)
            {
                postid = int.Parse(id);
                Post = await PostService.Search(postid);
            }
            //else
            //{
            //    Post = null;
            //}
        }
        public async Task HandleImageUpload(InputFileChangeEventArgs inputFileChangeEvent)
        {
            var file = inputFileChangeEvent.File;
            var buffer = new byte[file.Size];
            await file.OpenReadStream(1999900).ReadAsync(buffer);
            Post.ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
            Post.Image = buffer;
            Post.UserId = User.UserId;
        }
        
        protected async Task HandleValidSubmit()
        {


            var result = Post;

            if (Post.PostId != 0)
            {
                
            }
            else
            {
                result = await PostService.AddPost(Post);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/posts");
            }
        }

        public async void Delete_Click()
        {
            await PostService.DeletePost(Post.PostId);
            NavigationManager.NavigateTo("/profile");
        }

        public async void Update_Post()
        {
            Post result = null; 
            result=await PostService.UpdatePost(Post);
            if (result != null)
            {
                NavigationManager.NavigateTo("/profile");
            }
            
           
        }
    }
}
