using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectModels;
using ServerWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServerWeb.Pages
{
    public class PostBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationstateTask { get; set; }
       
        public User User { get; set; }
        public User PostedUser { get; set; }
        public Post Post { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string UserEmail { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public List<Post> Posts { get; set; }
        public string UserImageIcon { get; set; }
        public string Photo { get; set; }
        public string Image { get; set; }
        public string PostedUserName { get; set; }
        public User PageUser { get; set; } = new User();
        public string NumberReminder { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var authenticationstate = await authenticationstateTask;
            if (authenticationstate.User.Identity.IsAuthenticated)
            {
                UserEmail = authenticationstate.User.Identity.Name;
            }
            string[] user = UserEmail.Split('@');
            Username = user[0].ToString();
            PageUser= await UserService.Search(Username);
            if(PageUser.Friends==null || PageUser.Requests==null)
            {
                PageUser.Requests += PageUser.UserId;
                PageUser.Friends += PageUser.UserId;
                await UserService.UpdateUser(PageUser);
            }
            if(PageUser.MobileNumber==0)
            {
                NumberReminder = "Please add Mobilenumber * Mandatory *";
            }
            Posts = (await PostService.GetPosts()).ToList();
            Posts.Reverse();
            
        }
        public User Searchresult { get; set; } = new User();
        public string SearchName { get; set; }
        public string SearchUserImage { get; set; }
        public string  ErrorMessage { get; set; }
        public string AddVisibility { get; set; }
        public User emptyUser { get; set; } = new User();
        public async Task Search()
        {
            
            if (SearchName != null)
            {
                
                var result= await UserService.Search(SearchName);
                {
                    if(result==null)
                    {
                        Searchresult = emptyUser;
                        ErrorMessage = "User Not Found/Enter Valid User Name";
                    }
                    else
                    {
                        
                        Searchresult = result;
                        if(Searchresult.Friends!=null)
                        {
                            string[] friendsList = Searchresult.Friends.Split(",");
                            string[] RequestList = Searchresult.Requests.Split(","); 
                            if (friendsList.Contains((PageUser.UserId).ToString()) ) 
                            {

                                AddVisibility = "hidden";

                            }
                            else
                            {
                                if (RequestList.Contains((PageUser.UserId).ToString()))
                                {
                                    AddVisibility = "hidden";
                                }
                                else
                                {
                                    AddVisibility = "visible";
                                }
                            }
                        }
                        else
                        {
                            ErrorMessage = "No friends";
                        }

                        if (Searchresult.ProfilePic != null)
                        {
                            SearchUserImage = Convert.ToBase64String(Searchresult.ProfilePic);
                            Searchresult.ImageUrl = string.Format("data:image/png;base64,{0}", SearchUserImage);
                        }
                        
                    }
                }
               


            }

            
        }
       
        public async void FriendRequest()
        {
            Searchresult.Requests +=","+ PageUser.UserId;
            var result = await UserService.UpdateUser(Searchresult);

            //PageUser.Friends += "," + Searchresult.UserId;
            //var result1 = await UserService.UpdateUser(PageUser);

            if(result!=null )
            {

                //NavigationManager.NavigateTo("/posts");
                //NavigationManager.NavigateTo("/profile");
                await Search();
                StateHasChanged();
                
                
                //AddVisibility = "hidden";
            }
        }
    }
}
//Searchresult = await UserService.Search(SearchName);
//if(Searchresult!=null)
//{
//    if (Searchresult.UserId != 0)
//    {
//        if (Searchresult.ProfilePic != null)
//        {
//            SearchUserImage = Convert.ToBase64String(Searchresult.ProfilePic);
//            Searchresult.ImageUrl = string.Format("data:image/png;base64,{0}", SearchUserImage);
//        }

//    }
//}
//else
//{
//    ErrorMessage = "User Not Found";
//}