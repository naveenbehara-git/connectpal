using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectModels;
using ServerWeb.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace ServerWeb.Pages
{
    public class ProfileBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationstateTask { get; set; }
        public User User { get; set; } = new User();
        public int postid { get; set; }
        public string Photo { get; set; }
        public string DeleteId { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IPostService PostService { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public string UserEmail { get; set; }
        public string Username { get; set; }
        public string[] FriendsString { get; set; }
        public string[] RequestsString { get; set; }
        public List<User> PersonList { get; set; } = new List<User>();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationstate = await authenticationstateTask;
            if (authenticationstate.User.Identity.IsAuthenticated)
            {
                UserEmail = authenticationstate.User.Identity.Name;
            }

            string[] user = UserEmail.Split('@');
            Username = user[0].ToString();
            User = await UserService.Search(Username);
            Posts = null;
            Posts = (await PostService.GetUserPosts(User.UserId)).ToList();
            
            
            //image
            if (User.ProfilePic != null)
            {
                Photo = Convert.ToBase64String(User.ProfilePic);

                User.ImageUrl = string.Format("data:image/png;base64,{0}", Photo);

            }
            if(User.Friends!=null)
            {
                FriendsString = User.Friends.Split(",");
                //FriendsString = FriendsString.Skip(0).ToArray();
                foreach (var id in FriendsString.Skip(1))
                {
                    var result = await UserService.GetUser(int.Parse(id));
                    PersonList.Add(result);
                }
            }
            
            if(User.Requests!=null)
            {
                RequestsString = User.Requests.Split(",");
                //RequestsString = RequestsString.Skip(0).ToArray();
                foreach (var id in RequestsString.Skip(1))
                {
                    var result = await UserService.GetUser(int.Parse(id));
                    if (result != null)
                    {
                        RequestsList.Add(result);
                    }
                }
            }
            

        }
        public List<User> RequestsList { get; set; } = new List<User>();
        public bool Show { get; set; }
        protected  void Show_Requests()
        {
            Show = true;
        }
        protected async void Requests_Update()
        {
            Show = false;
            var result = await UserService.UpdateUser(User);
            if(result!=null)
            {
                NavigationManager.NavigateTo("/profile");
            }

        }
        protected EditProfileBase EditConfirmation { get; set; }
       
        protected void Edit_Click()
        {
            EditConfirmation.Show();
        }
        protected async Task ConfirmEdit_Click(User user)
        {
            if (user != null)
            {

                var result = await UserService.UpdateUser(user);
                if (result != null)
                {
                    NavigationManager.NavigateTo("/profile");
                    await OnInitializedAsync();
                }

            }
        }

        public async Task Delete_Click()
        {
            if (postid != 0)
            {
                await PostService.DeletePost(postid);

                NavigationManager.NavigateTo("/profile");

            }
        }

        [Parameter]
        public EventCallback<User> ConfirmationChanged { get; set; }
        public List<string> RequestRenewal { get; set; }
        protected async Task OnConfirmationChange(User user)
        {
            if(user!=null)
            {
                User.Friends += "," + user.UserId;
                RequestsString = User.Requests.Split(",").ToArray();
                RequestRenewal = RequestsString.ToList();
                RequestRenewal.Remove((user.UserId).ToString());
                User.Requests = (User.UserId).ToString();
                foreach (var i in RequestRenewal.Skip(1))
                {
                    
                    User.Requests += ","+(i).ToString();
                    
                    

                }


                var result=await UserService.UpdateUser(User);
                if(result!=null)
                {
                    FriendsString = null;
                    await OnInitializedAsync();
                    Show = false;
                    
                    StateHasChanged();
                }
            }
            
            

        }

    }  
      
}

