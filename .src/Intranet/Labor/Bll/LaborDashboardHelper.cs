using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborDashboard;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard helper
    /// </summary>
    public class LaborDashboardHelper : ServiceBase, ILaborDashboardHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardHelper) ) )
        {
        }

        #endregion

        /// <summary>
        /// Calculates the rwtype to get the more important of the before or actual
        /// </summary>
        /// <param name="before">the rwtype before</param>
        /// <param name="rwType">the rwtype at the moment</param>
        /// <returns>the rwtype that is more important</returns>
        public RwType CalcRwType(RwType before, RwType? rwType)
        {
            if (rwType == null)
                return before;
            if ((before == RwType.Worse) || (rwType == RwType.Worse))
                return RwType.Worse;
            return before == RwType.SomethingWorse ? RwType.SomethingWorse : rwType.Value;
        }

        /// <summary>
        /// Generates a Dashboard Info with the given param values
        /// </summary>
        /// <param name="key">the key for the info as a explaining text</param>
        /// <param name="value">the value for the info</param>
        /// <param name="rw">the rw type for the info</param>
        /// <returns></returns>
        private DashboardInfo ToDashboardInfo(String key, String value, RwType rw) => new DashboardInfo { InfoValue = value, InfoKey = key, RwType = rw };

        /// <summary>
        /// Generates Dashboard infos with the given testvalues
        /// </summary>
        /// <param name="testValues">the testvalues containing infos</param>
        /// <returns>a list of dashboard infos with the information from the given testvalues</returns>
        public List<DashboardInfo> ToDashboardInfos(IEnumerable<TestValue> testValues)
        {
            var infos = new List<DashboardInfo>();
            foreach (var testValue in testValues.Where(value => value.TestValueType == TestValueType.Average))
                infos.AddRange(testValue.ArticleTestType == ArticleType.BabyDiaper
                                    ? ToDashboardInfosBabyDiapers(testValue)
                                    : ToDashboardInfosIncontinencePad(testValue));

            return infos;
        }

        /// <summary>
        /// generates a list of dashboard infos which are from the testvalue of baby diaper
        /// </summary>
        /// <param name="testValue">the testvalues containing baby diapers test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        public List<DashboardInfo> ToDashboardInfosBabyDiapers(TestValue testValue)
        {
            var infos = new List<DashboardInfo>();
            switch (testValue.BabyDiaperTestValue.TestType)
            {
                case TestTypeBabyDiaper.Rewet:
                    infos.Add(ToDashboardInfo("Rewet - 140 ml",
                                                Math.Round(testValue.BabyDiaperTestValue.Rewet140Value, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.BabyDiaperTestValue.Rewet140Rw.GetValueOrDefault()));
                    return infos;

                case TestTypeBabyDiaper.RewetAndPenetrationTime:
                    infos.Add(ToDashboardInfo("Rewet - 210 ml",
                                                Math.Round(testValue.BabyDiaperTestValue.Rewet210Value, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue
                                                    .BabyDiaperTestValue.Rewet210Rw.GetValueOrDefault()));
                    infos.Add(ToDashboardInfo("Penetrationszeit - Zugabe 4",
                                                Math.Round(testValue.BabyDiaperTestValue.PenetrationTimeAdditionFourth, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.BabyDiaperTestValue.PenetrationRwType.GetValueOrDefault()));
                    return infos;

                case TestTypeBabyDiaper.Retention:
                    infos.Add(ToDashboardInfo("Retention - Nach Zentrifuge (g)",
                                                Math.Round(testValue.BabyDiaperTestValue.RetentionAfterZentrifugeValue, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.BabyDiaperTestValue.RetentionRw.GetValueOrDefault()));
                    return infos;
            }
            return infos;
        }

        /// <summary>
        /// generates a list of dashboard infos which are from the testvalue of  incontinenec pad
        /// </summary>
        /// <param name="testValue">the testvalues containing incontinence pod test value</param>
        /// <returns>a list of dashboard infos from the testvalue</returns>
        public List<DashboardInfo> ToDashboardInfosIncontinencePad(TestValue testValue)
        {
            var infos = new List<DashboardInfo>();

            switch (testValue.IncontinencePadTestValue.TestType)
            {
                case TestTypeIncontinencePad.Retention:

                    infos.Add(ToDashboardInfo("Retention",
                                                Math.Round(testValue.IncontinencePadTestValue.RetentionEndValue, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.RetentionRw));
                    return infos;

                case TestTypeIncontinencePad.RewetFree:
                    infos.Add(ToDashboardInfo("Rewet",
                                                Math.Round(testValue.IncontinencePadTestValue.RewetFreeDifference, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.RewetFreeRw));
                    return infos;
                case TestTypeIncontinencePad.AcquisitionTimeAndRewet:

                    infos.Add(ToDashboardInfo("Aquisitionszeit - Zugabe 1",
                                                Math.Round(testValue.IncontinencePadTestValue.AcquisitionTimeFirst, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeFirstRw));
                    infos.Add(ToDashboardInfo("Aquisitionszeit - Zugabe 2",
                                                Math.Round(testValue.IncontinencePadTestValue.AcquisitionTimeSecond, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeSecondRw));
                    infos.Add(ToDashboardInfo("Aquisitionszeit - Zugabe 3",
                                                Math.Round(testValue.IncontinencePadTestValue.AcquisitionTimeThird, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.AcquisitionTimeThirdRw));
                    infos.Add(ToDashboardInfo("Rewet nach Aquisition",
                                                Math.Round(testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeWeightDifference, 2)
                                                    .ToString(CultureInfo.InvariantCulture),
                                                testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw));
                    return infos;
            }

            return infos;
        }

        /// <summary>
        /// Generates a list od dashboard notes with the information from the given test values
        /// </summary>
        /// <param name="testValues">a list of testvalues containing attributes for the notes</param>
        /// <returns></returns>
        public List<DashboardNote> ToDashboardNote(IEnumerable<TestValue> testValues)
        {
            var notes = new List<DashboardNote>();

            foreach (var testValue in testValues.Where(testValue => testValue.TestValueNote.Count > 0))
                notes.AddRange(
                    testValue.TestValueNote.Select(
                                 testValueNote =>
                                         new DashboardNote { ErrorMessage = testValueNote.Error.Value, Message = testValueNote.Message, Code = testValueNote.Error.ErrorCode }));

            return notes;
        }

        /// <summary>
        /// collectes all rw data from the testvalues to a production order and calculates the most important one for the whole
        /// </summary>
        /// <param name="testValues">a list of testvalues</param>
        /// <returns>a rw type representing the whole lot of testvalues</returns>
        public RwType ToRwTypeAll(IEnumerable<TestValue> testValues)
        {
            var rwType = RwType.Ok;
            foreach (
                var testValue in
                testValues.Where(testValue => (testValue.TestValueType == TestValueType.Average) || (testValue.TestValueType == TestValueType.StandardDeviation)))
                if (testValue.ArticleTestType == ArticleType.BabyDiaper)
                {
                    rwType = CalcRwType(rwType, testValue?.BabyDiaperTestValue?.RetentionRw);
                    rwType = CalcRwType(rwType, testValue?.BabyDiaperTestValue?.PenetrationRwType);
                    rwType = CalcRwType(rwType, testValue?.BabyDiaperTestValue?.Rewet140Rw);
                    rwType = CalcRwType(rwType, testValue?.BabyDiaperTestValue?.Rewet210Rw);
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.RetentionRw);
                }
                else
                {
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeFirstRw);
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeSecondRw);
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.AcquisitionTimeThirdRw);
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.RewetAfterAcquisitionTimeRw);
                    rwType = CalcRwType(rwType, testValue?.IncontinencePadTestValue?.RewetFreeRw);
                }

            return rwType;
        }
    }
}