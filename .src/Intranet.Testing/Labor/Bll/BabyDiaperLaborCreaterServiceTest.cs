using System;
using System.Collections.Generic;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing Tests for BabyDiaperLaborCreatorService
    /// </summary>
    public class BabyDiaperLaborCreaterServiceTest
    {
        /// <summary>
        ///     Test GetLaborCreatorViewModel
        /// </summary>
        [Fact]
        public void GetBabyDiaperLaborCreatorViewModelTest()
        {
            var serviceHelperMoq = MockHelperLaborCreatorServiceHelper.GetLaborCreatorServiceHelper();

            const ShiftType shiftType = ShiftType.Late;
            const String faNr = "Fa666";
            const String productName = "Beast";
            const String sizeName = "Huge";
            var createdDateTime = new DateTime( 2016, 1, 1 );

            var laborCreatorBllMoq = MockHelperBll.GetBabyDiaperLaborCreatorBll(
                new TestSheet
                {
                    ShiftType = shiftType,
                    FaNr = faNr,
                    ProductName =productName ,
                    SizeName = sizeName,
                    CreatedDateTime = createdDateTime,
                    TestValues = new List<TestValue>(),
                    TestSheetId = 1
                    
                } );

            var target = new BabyDiaperLaborCreatorService( new NLogLoggerFactory() )
            {
                Helper = serviceHelperMoq,
                BabyDiaperLaborCreatorBll = laborCreatorBllMoq
            };

            var actual = target.GetLaborCreatorViewModel( 1 );
            actual.TestSheetId.Should()
                  .Be( 1 );
            actual.Producer.Should()
                  .Be( "Intigena" );
            actual.Shift.Should()
                  .Be( shiftType.ToFriendlyString() );
            actual.FaNr.Should()
                  .Be( faNr );
            actual.ProductName.Should()
                  .Be( productName );
            actual.SizeName.Should()
                  .Be( sizeName );
            actual.CreatedDate.Should()
                  .Be( "01.01.2016" );
            actual.Rewets.Should()
                  .NotBeNull( "because it is initialized" );
            actual.RewetAverage.Should()
                 .NotBeNull("because it is initialized");
            actual.RetentionStandardDeviation.Should()
                 .NotBeNull("because it is initialized");
            actual.Retentions.Should()
                 .NotBeNull("because it is initialized");
            actual.RewetAverage.Should()
                 .NotBeNull("because it is initialized");
            actual.RetentionStandardDeviation.Should()
                 .NotBeNull("because it is initialized");
            actual.PenetrationTimes.Should()
                 .NotBeNull("because it is initialized");
            actual.PenetrationTimeAverage.Should()
                 .NotBeNull("because it is initialized");
            actual.PenetrationTimeStandardDeviation.Should()
                 .NotBeNull("because it is initialized");

        }
    }
}