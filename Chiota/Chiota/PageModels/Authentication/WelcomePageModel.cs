﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Chiota.PageModels.Classes;
using Chiota.Pages.Authentication;
using Chiota.Pages.Help;
using Xamarin.Forms;

namespace Chiota.PageModels.Authentication
{
    public class WelcomePageModel : BasePageModel
    {
        #region Commands

        #region NewSeed

        public ICommand NewSeedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Show register page.
                    await PushAsync(new NewSeedPage());
                });
            }
        }

        #endregion

        #region SetSeed

        public ICommand SetSeedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PushAsync(new SetSeedPage());
                });
            }
        }

        #endregion

        #region SeedHelp

        public ICommand SeedHelpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PushAsync(new SeedHelpPage());
                });
            }
        }

        #endregion

        #endregion
    }
}
