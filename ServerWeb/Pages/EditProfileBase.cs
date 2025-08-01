using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectModels;
using ServerWeb.Services;
using System;
using System.Threading.Tasks;
using System.IO;
using BlazorInputFile;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ServerWeb.Pages
{
    public class EditProfileBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationstateTask { get; set; }
        public string Photo { get; set; }
        public Post Post { get; set; } = new Post();
        protected bool ShowConfirmation { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Edit Profile";
        public User User { get; set; }
        
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<User> ConfirmationChanged { get; set; }
       
        //public async Task ValidSubmit()
        //{
        //    await OnConfirmationChange(true);
        //    NavigationManager.NavigateTo("/profile");
        //}
        protected async Task OnConfirmationChange(bool value)
        {
            if(value==false)
            {
                NavigationManager.NavigateTo("/profile");
            }
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(User);
        }


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

            //image display

            if (User.ProfilePic != null)
            {
                Photo = Convert.ToBase64String(User.ProfilePic);

                User.ImageUrl = string.Format("data:image/png;base64,{0}", Photo);

            }
            Countries = (await CountryService.GetCountries()).ToList();

            CountryId =User.CountryId.ToString();

            CityId = User.CityID.ToString();
            Cities = (await CityService.GetCities(int.Parse(CountryId))).ToList();
        }
        public IFileListEntry files { get; set; }
       public  async Task HandleImageUpload(InputFileChangeEventArgs inputFileChangeEvent)
        {
            var file = inputFileChangeEvent.File;
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);
            User.ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
            User.ProfilePic = buffer;
            await UserService.UpdateUser(User);
        }

        public List<Country> Countries { get; set; } = new List<Country>();
        public List<City> Cities { get; set; } = new List<City>();
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public int id { get; set; }
        [Inject]
        public ICityService CityService { get; set; }
        [Inject]
        public ICountryService CountryService { get; set; }
        protected async void CountryChanged(string value)
        {
            if (value != null)
            {
                User.CountryId = int.Parse(value);
                CountryId = value;
                Cities.Clear();
                id = int.Parse(value);
                Cities = new List<City>();
                Cities = (await CityService.GetCities(id)).ToList();
                this.StateHasChanged();
            }
        }

        protected void CityChanged(string value)
        {
            User.CityID = int.Parse(value);
            User.CountryId = int.Parse(CountryId);
            CityId = value;
        }

    }

}
    
    

