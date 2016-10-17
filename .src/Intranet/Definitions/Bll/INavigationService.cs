﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.ViewModel;

namespace Intranet.Definition.Bll
{
    /// <summary>
    /// Interface representing the service for the navigation
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// All main models that the current User is allowed to see. 
        /// </summary>
        /// <returns>The ViewModel for the navigation</returns>
        NavigationViewModel GetNavicationViewModel();

   
    }
}
