#region Usings

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LaborContext>
    {
        #region Ctor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        #endregion

        protected override void Seed( LaborContext context )
        {
           var error1 = new Error
            {
                ErrorCode = "080",
                Value = "Fixtape fehlt"
            };
            var error2 = new Error
            {
                ErrorCode = "411",
                Value = "Saugkissen hinten zu kurz"
            };
            var error3 = new Error
            {
                ErrorCode = "023",
                Value = "Zu wenig Inhalt"
            };
            var error4 = new Error
            {
                ErrorCode = "802",
                Value = "Linke Seite reisst auf"
            };
            context.Errors.AddOrUpdate( e => e.ErrorId, error1, error2, error3, error4 );

            var machine1 = new Machine
            {
                MachineNr = "M10"
            };
            var machine2 = new Machine
            {
                MachineNr = "M11"
            };
            var machine3 = new Machine
            {
                MachineNr = "M49"
            };
            context.Machines.AddOrUpdate( m => m.MachineId, machine1, machine2, machine3 );

            var article1 = new Article
            {
                ArticleNr = "10401",
                Name = "Babydream Maxi-Plus",
                ArticleType = ArticleType.BabyDiaper,
                Rewet140Max = 0.4,
                Rewet210Max = 0.5,
                MinRetention = 350,
                MaxRetention = 380,
                MaxPenetrationAfter4Time = 250
            };
            var article2 = new Article
            {
                ArticleNr = "10412",
                Name = "Inko Extra",
                ArticleType = ArticleType.IncontinencePad,
                SizeName = "Inko  Extra"
            };

            context.Articles.AddOrUpdate( a => a.ArticleId, article1 );
            context.Articles.AddOrUpdate( a => a.ArticleId, article2 );

            var productionOrderComponent1 = new ProductionOrderComponent
            {
                SAP = 32.7,
                PillowRetentWithoutSAP = 31.2,
                PillowWeightWithoutSAP = 26.0,
                CelluloseRetention = 1.2,
                ComponentType = "EKX",
                ComponentNr = "EN67"
            };

            var productionOrder1 = new ProductionOrder
            {
                FaNr = "FA123456",
                StartDateTime = new DateTime( 2016, 1, 1 ),
                EndDateTime = new DateTime( 2018, 1, 1 ),
                Machine = machine2,
                Component = productionOrderComponent1,
                Article = article1
            };
 
            productionOrderComponent1.ProductionOrder = productionOrder1;

            context.ProductionOrderComponent.AddOrUpdate( p => p.ProductionOrderComponentId, productionOrderComponent1 );
            context.ProductionOrders.AddOrUpdate( p => p.FaId, productionOrder1 );

            ////////////////////////////////////////////////////

            var inkoArticle1 = new Article
            {
                ArticleNr = "10501",
                Name = "Cresta Extra Inko Extra",
                ArticleType = ArticleType.IncontinencePad,
                MaxInkoRewet = 0.5,
                MinInkoRetention = 180,
                MaxHyTec1 = 20,
                MaxHyTec2 = 60,
                MaxHyTec3 = 85,
                MaxInkoRewetAfterAquisition = 2
            };
            var productionOrder2 = new ProductionOrder
            {
                FaNr = "FA654321",
                StartDateTime = new DateTime( 2016, 1, 1 ),
                EndDateTime = new DateTime( 2018, 1, 1 ),
                Machine = machine3,
                Article = inkoArticle1
            };
            context.Articles.AddOrUpdate( a => a.ArticleId, inkoArticle1 );
            context.ProductionOrders.AddOrUpdate( p => p.FaId, productionOrder2 );

            ////////////////////////////////////////////////////
            var shift1 = new ShiftSchedule
            {
                Name = "Mo Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Sunday,
                EndDay = DayOfWeek.Monday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };
            var shift2 = new ShiftSchedule
            {
                Name = "Mo Morgen",
                ShiftType = ShiftType.Morning,
                StartDay = DayOfWeek.Monday,
                EndDay = DayOfWeek.Monday,
                StartTime = new TimeSpan( 05, 00, 00 ),
                EndTime = new TimeSpan( 13, 59, 59 )
            };
            var shift3 = new ShiftSchedule
            {
                Name = "Mo Sp�t",
                ShiftType = ShiftType.Late,
                StartDay = DayOfWeek.Monday,
                EndDay = DayOfWeek.Monday,
                StartTime = new TimeSpan( 14, 00, 00 ),
                EndTime = new TimeSpan( 22, 59, 59 )
            };
            var shift4 = new ShiftSchedule
            {
                Name = "Di Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Monday,
                EndDay = DayOfWeek.Tuesday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };
            var shift5 = new ShiftSchedule
            {
                Name = "Di Morgen",
                ShiftType = ShiftType.Morning,
                StartDay = DayOfWeek.Tuesday,
                EndDay = DayOfWeek.Tuesday,
                StartTime = new TimeSpan( 05, 00, 00 ),
                EndTime = new TimeSpan( 13, 59, 59 )
            };
            var shift6 = new ShiftSchedule
            {
                Name = "Di Sp�t",
                ShiftType = ShiftType.Late,
                StartDay = DayOfWeek.Tuesday,
                EndDay = DayOfWeek.Tuesday,
                StartTime = new TimeSpan( 14, 00, 00 ),
                EndTime = new TimeSpan( 22, 59, 59 )
            };
            var shift7 = new ShiftSchedule
            {
                Name = "Mi Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Tuesday,
                EndDay = DayOfWeek.Wednesday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };
            var shift8 = new ShiftSchedule
            {
                Name = "Mi Morgen",
                ShiftType = ShiftType.Morning,
                StartDay = DayOfWeek.Wednesday,
                EndDay = DayOfWeek.Wednesday,
                StartTime = new TimeSpan( 05, 00, 00 ),
                EndTime = new TimeSpan( 13, 59, 59 )
            };
            var shift9 = new ShiftSchedule
            {
                Name = "Mi Sp�t",
                ShiftType = ShiftType.Late,
                StartDay = DayOfWeek.Wednesday,
                EndDay = DayOfWeek.Wednesday,
                StartTime = new TimeSpan( 14, 00, 00 ),
                EndTime = new TimeSpan( 22, 59, 59 )
            };
            var shift10 = new ShiftSchedule
            {
                Name = "Do Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Wednesday,
                EndDay = DayOfWeek.Thursday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };
            var shift11 = new ShiftSchedule
            {
                Name = "Do Morgen",
                ShiftType = ShiftType.Morning,
                StartDay = DayOfWeek.Thursday,
                EndDay = DayOfWeek.Thursday,
                StartTime = new TimeSpan( 05, 00, 00 ),
                EndTime = new TimeSpan( 13, 59, 59 )
            };
            var shift12 = new ShiftSchedule
            {
                Name = "Do Sp�t",
                ShiftType = ShiftType.Late,
                StartDay = DayOfWeek.Thursday,
                EndDay = DayOfWeek.Thursday,
                StartTime = new TimeSpan( 14, 00, 00 ),
                EndTime = new TimeSpan( 22, 59, 59 )
            };
            var shift13 = new ShiftSchedule
            {
                Name = "Fr Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Thursday,
                EndDay = DayOfWeek.Friday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };
            var shift14 = new ShiftSchedule
            {
                Name = "Fr Morgen",
                ShiftType = ShiftType.Morning,
                StartDay = DayOfWeek.Friday,
                EndDay = DayOfWeek.Friday,
                StartTime = new TimeSpan( 05, 00, 00 ),
                EndTime = new TimeSpan( 13, 59, 59 )
            };
            var shift15 = new ShiftSchedule
            {
                Name = "Fr Sp�t",
                ShiftType = ShiftType.Late,
                StartDay = DayOfWeek.Friday,
                EndDay = DayOfWeek.Friday,
                StartTime = new TimeSpan( 14, 00, 00 ),
                EndTime = new TimeSpan( 22, 59, 59 )
            };
            var shift16 = new ShiftSchedule
            {
                Name = "Sa Nacht",
                ShiftType = ShiftType.Night,
                StartDay = DayOfWeek.Friday,
                EndDay = DayOfWeek.Saturday,
                StartTime = new TimeSpan( 22, 00, 00 ),
                EndTime = new TimeSpan( 04, 59, 59 )
            };

            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift1 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift2 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift3 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift4 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift5 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift6 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift7 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift8 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift9 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift10 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift11 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift12 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift13 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift14 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift15 );
            context.ShiftSchedules.AddOrUpdate( s => s.Name, shift16 );
            ////////////////////////////////////////////////////

            var testSheet = new TestSheet
            {
                TestSheetId = 1,
                FaNr = "FA123456",
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                ShiftType = ShiftType.Night,
                MachineNr = "M11",
                SAPType = "EKX",
                SAPNr = "EN67",
                ProductName = "Babydream",
                SizeName = "Maxi-Plus",
                ArticleType = ArticleType.BabyDiaper
            };

            var babyDiapersRewetTestValue1 = new TestValue
            {
                TestValueId = 1,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Single,
                TestSheetId = 1
            };
            var babyDiapersRetentionTestValueAverage = new TestValue
            {
                TestValueId = 2,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Average,
                TestSheetId = 1
            };
            var babyDiapersRetentionTestValueStandardDeviation = new TestValue
            {
                TestValueId = 3,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetId = 1
            };
            var babyDiapersRewetTestValueAverage = new TestValue
            {
                TestValueId = 4,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Average,
                TestSheetId = 1
            };
            var babyDiapersRewetTestValueStandardDeviation = new TestValue
            {
                TestValueId = 5,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetId = 1
            };
            var babyDiapersPenetrationTimeTestValueAverage = new TestValue
            {
                TestValueId = 6,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Average,
                TestSheetId = 1
            };
            var babyDiapersPenetrationTimeTestValueStandardDeviation = new TestValue
            {
                TestValueId = 7,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetId = 1
            };

            var babyDiapersRewetTest1 = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 1,
                DiaperCreatedTime = new TimeSpan( 1, 38, 0 ),
                WeightDiaperDry = 32.9,
                Rewet140Value = 0.1,
                Rewet210Value = 0.18,
                StrikeTroughValue = 0.28,
                DistributionOfTheStrikeTrough = 240,
                Rewet140Rw = RwType.Ok,
                Rewet210Rw = RwType.Ok,
                TestType = TestTypeBabyDiaper.Rewet
            };
            var babyDiapersRewetTestAverage = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 2,
                DiaperCreatedTime = new TimeSpan( 1, 38, 0 ),
                WeightDiaperDry = 32.9,
                Rewet140Value = 0.1,
                Rewet210Value = 0.18,
                StrikeTroughValue = 0.28,
                DistributionOfTheStrikeTrough = 240,
                Rewet140Rw = RwType.Ok,
                Rewet210Rw = RwType.Ok,
                TestType = TestTypeBabyDiaper.Rewet
            };
            var babyDiapersRewetTestStandardDeviation = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 3,
                DiaperCreatedTime = new TimeSpan( 1, 38, 0 ),
                WeightDiaperDry = 32.9,
                Rewet140Value = 0.1,
                Rewet210Value = 0.18,
                StrikeTroughValue = 0.28,
                DistributionOfTheStrikeTrough = 240,
                Rewet140Rw = RwType.Ok,
                Rewet210Rw = RwType.Ok,
                TestType = TestTypeBabyDiaper.Rewet
            };

            babyDiapersRewetTestValue1.BabyDiaperTestValue = babyDiapersRewetTest1;

            var testNote = new TestValueNote
            {
                Error = error2,
                Message = "Testnotiz"
            };
            var babyDiapersRetentionTestValue1 = new TestValue
            {
                TestValueId = 2,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Single,
                TestSheetId = 1,
                TestValueNote = new List<TestValueNote> { testNote }
            };
            testNote.TestValue = babyDiapersRetentionTestValue1;
            var babyDiapersRetentionTest1 = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 2,
                DiaperCreatedTime = new TimeSpan( 1, 38, 0 ),
                WeightDiaperDry = 33.0,
                RetentionWetWeight = 414.0,
                RetentionAfterZentrifugeValue = 381.0,
                RetentionAfterZentrifugePercent = 1155,
                RetentionRw = RwType.Better,
                SapType = "EKX",
                SapNr = "EN67",
                SapGHoewiValue = 10.70,
                TestType = TestTypeBabyDiaper.Retention
            };
            babyDiapersRetentionTestValue1.BabyDiaperTestValue = babyDiapersRetentionTest1;

            var babyDiapersRetentionTestAverage = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 4,
                DiaperCreatedTime = new TimeSpan( 0, 0, 0 ),
                WeightDiaperDry = 0,
                RetentionRw = RwType.Ok,
                RetentionAfterZentrifugeValue = 0,
                RetentionWetWeight = 0,
                RetentionAfterZentrifugePercent = 0,
                TestType = TestTypeBabyDiaper.Retention
            };
            var babyDiapersRetentionTestStandardDeviation = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 5,
                DiaperCreatedTime = new TimeSpan( 0, 0, 0 ),
                WeightDiaperDry = 0,
                RetentionRw = RwType.Ok,
                RetentionAfterZentrifugeValue = 0,
                RetentionWetWeight = 0,
                RetentionAfterZentrifugePercent = 0,
                TestType = TestTypeBabyDiaper.Retention
            };
            var babyDiapersPenetrationTimeTestAverage = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 6,
                DiaperCreatedTime = new TimeSpan( 0, 0, 0 ),
                WeightDiaperDry = 0,
                PenetrationTimeAdditionThird = 0,
                PenetrationTimeAdditionSecond = 0,
                PenetrationTimeAdditionFourth = 0,
                PenetrationTimeAdditionFirst = 0,
                RetentionAfterZentrifugePercent = 0,
                PenetrationRwType = RwType.Ok,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
            };
            var babyDiapersPenetrationTimeTestStandardDeviation = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 7,
                DiaperCreatedTime = new TimeSpan( 0, 0, 0 ),
                PenetrationTimeAdditionThird = 0,
                PenetrationTimeAdditionSecond = 0,
                PenetrationTimeAdditionFourth = 0,
                PenetrationTimeAdditionFirst = 0,
                RetentionAfterZentrifugePercent = 0,
                WeightDiaperDry = 0,
                PenetrationRwType = RwType.Ok,
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
            };

            babyDiapersRewetTestValue1.BabyDiaperTestValue = babyDiapersRewetTest1;

            babyDiapersRewetTestValueAverage.BabyDiaperTestValue = babyDiapersRewetTestAverage;
            babyDiapersRewetTestValueStandardDeviation.BabyDiaperTestValue = babyDiapersRewetTestStandardDeviation;

            babyDiapersRetentionTestValueAverage.BabyDiaperTestValue = babyDiapersRetentionTestAverage;
            babyDiapersRetentionTestValueStandardDeviation.BabyDiaperTestValue = babyDiapersRetentionTestStandardDeviation;

            babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValue = babyDiapersPenetrationTimeTestAverage;
            babyDiapersPenetrationTimeTestValueStandardDeviation.BabyDiaperTestValue = babyDiapersPenetrationTimeTestStandardDeviation;

            testSheet.TestValues = new List<TestValue>
            {
                babyDiapersRewetTestValue1,
                babyDiapersRetentionTestValue1,
                babyDiapersRetentionTestValueAverage,
                babyDiapersRetentionTestValueStandardDeviation,
                babyDiapersRewetTestValueAverage,
                babyDiapersRewetTestValueStandardDeviation,
                babyDiapersPenetrationTimeTestValueAverage,
                babyDiapersPenetrationTimeTestValueStandardDeviation
            };
            context.TestSheets.AddOrUpdate( m => m.FaNr, testSheet );
            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersRewetTestValue1 );
            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersRewetTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersRewetTestValueStandardDeviation );

            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersRetentionTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersRetentionTestValueStandardDeviation );

            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersPenetrationTimeTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, babyDiapersPenetrationTimeTestValueStandardDeviation );

            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersRewetTest1 );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersRewetTestStandardDeviation );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersRewetTestAverage );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersRetentionTestStandardDeviation );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersRetentionTestAverage );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersPenetrationTimeTestStandardDeviation );
            context.BabyDiaperTestValues.AddOrUpdate( m => m.BabyDiaperTestValueId, babyDiapersPenetrationTimeTestAverage );
            
            babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValue = babyDiapersPenetrationTimeTestAverage;
          
        }
    }
}