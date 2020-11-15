using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Sol_Demo.Components
{
    public partial class ProtectedBrowserStorageDemo
    {
        #region Declaration

        private const string _key = "SecureCounter";

        #endregion Declaration

        #region Public Property

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        #endregion Public Property

        #region Private Property

        private int CurrentCount { get; set; } = 0;

        #endregion Private Property

        #region Private Method

        private async Task GetLocalStorageDataAsync()
        {
            try
            {
                var result = await LocalStorage.GetAsync<int>(key: _key);
                if (result.Success)
                {
                    CurrentCount = result.Value;
                    base.StateHasChanged();
                }
            }
            catch
            {
                CurrentCount = 0;
            }
        }

        #endregion Private Method

        #region Ui Events

        public async Task OnIncrement()
        {
            CurrentCount++;
            await LocalStorage.SetAsync(key: _key, value: CurrentCount);
        }

        #endregion Ui Events

        #region Protected Member

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetLocalStorageDataAsync();
            }
        }

        #endregion Protected Member
    }
}