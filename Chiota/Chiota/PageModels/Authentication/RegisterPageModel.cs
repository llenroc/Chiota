﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Chiota.Exceptions;
using Chiota.Extensions;
using Chiota.PageModels.Classes;
using Chiota.Pages.BackUp;
using Chiota.Popups.PopupModels;
using Chiota.Popups.PopupPageModels;
using Chiota.Popups.PopupPages;
using Xamarin.Forms;

namespace Chiota.PageModels.Authentication
{
    public class RegisterPageModel : BasePageModel
    {
        #region Attributes

        private string _name;
        private string _password;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion

        #region ViewIsAppearing

        protected override void ViewIsAppearing()
        {
            base.ViewIsAppearing();

            //Clear the user inputs.
            Name = "";
            Password = "";
        }

        #endregion

        #region Commands

        public ICommand ContinueCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password))
                    {
                        //Here maybe insert password validation for a stronger password. TODO

                        //Show popup to confirm password.
                        var dialog = new DialogPopupModel()
                        {
                            Title = "Please confirm your password",
                            IsPassword = true,
                            Placeholder = "Password"
                        };
                        var result = await DisplayPopupAsync<DialogPopupPageModel, DialogPopupModel>(new DialogPopupPage(), dialog);

                        if (result.ResultText != Password)
                        {
                            //Show popup alert.
                            await new AuthFailedPasswordConfirmationException(new ExcInfo()).ShowAlertAsync();
                            return;
                        }

                        //Show the back up page.
                        await PushAsync(new BackUpPage());
                        return;
                    }

                    //Show popup alert.
                    await new MissingUserInputException(new ExcInfo(), "name or password").ShowAlertAsync();
                });
            }
        }

        #endregion
    }
}