using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborDashboard;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Interaface representing Labor Dashboard Helper
    /// </summary>
    public interface ILaborDashboardHelper
    {

        /// <summary>
        /// Calculates the rwtype to get the more important of the before or actual
        /// </summary>
        /// <param name="before">the rwtype before</param>
        /// <param name="rwType">the rwtype at the moment</param>
        /// <returns>the rwtype that is more important</returns>
        RwType CalcRwType( RwType before, RwType? rwType );

        /// <summary>
        /// Generates Dashboard infos with the given testvalues
        /// </summary>
        /// <param name="testValues">the testvalues containing infos</param>
        /// <returns>a list of dashboard infos with the information from the given testvalues</returns>
        List<DashboardInfo> ToDashboardInfos( IEnumerable<TestValue> testValues );

        /// <summary>
        /// generates a list of dashboard infos which are from the testvalue of baby diaper
        /// </summary>
        /// <param name="testValue">the testvalues containing baby diapers test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        List<DashboardInfo> ToDashboardInfosBabyDiapers( TestValue testValue );

        /// <summary>
        /// generates a list of dashboard infos which are from the testvalue of  incontinenec pad
        /// </summary>
        /// <param name="testValue">the testvalues containing incontinence pod test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        List<DashboardInfo> ToDashboardInfosIncontinencePad( TestValue testValue );
        /// <summary>
        /// Generates a list od dashboard notes with the information from the given test values
        /// </summary>
        /// <param name="testValues">a list of testvalues containing attributes for the notes</param>
        /// <returns></returns>
        List<DashboardNote> ToDashboardNote( IEnumerable<TestValue> testValues );

        /// <summary>
        /// collectes all rw data from the testvalues to a production order and calculates the most important one for the whole
        /// </summary>
        /// <param name="testValues">a list of testvalues</param>
        /// <returns>a rw type representing the whole lot of testvalues</returns>
        RwType ToRwTypeAll( IEnumerable<TestValue> testValues );
    }
}