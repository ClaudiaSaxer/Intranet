using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LaborContext>
    {
        #region Ctor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        protected override void Seed( LaborContext context )
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

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
            context.Articles.AddOrUpdate( a => a.ArticleId, article1 );

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
                /*TestValues = new List<TestValue>
                {
                    babyDiapersRetentionTestValue1
                }*/
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1
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
                TestSheetRefId = 1,
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
                TestType = TestTypeBabyDiaper.RewetAndPenetrationTime
            };

            babyDiapersRewetTestValue1.BabyDiaperTestValue = babyDiapersRewetTest1;
            //babyDiapersRewetTestValue1.BabyDiaperTestValueRefId = 1;

            //babyDiapersRewetTestValueAverage.BabyDiaperTestValueRefId = 2;
            babyDiapersRewetTestValueAverage.BabyDiaperTestValue = babyDiapersRewetTestAverage;
            //babyDiapersRewetTestValueStandardDeviation.BabyDiaperTestValueRefId = 3;
            babyDiapersRewetTestValueStandardDeviation.BabyDiaperTestValue = babyDiapersRewetTestStandardDeviation;

            //babyDiapersRetentionTestValueAverage.BabyDiaperTestValueRefId = 4;
            babyDiapersRetentionTestValueAverage.BabyDiaperTestValue = babyDiapersRetentionTestAverage;
            //babyDiapersRetentionTestValueStandardDeviation.BabyDiaperTestValueRefId = 5;
            babyDiapersRetentionTestValueStandardDeviation.BabyDiaperTestValue = babyDiapersRetentionTestStandardDeviation;

            //babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValueRefId = 6;
            babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValue = babyDiapersPenetrationTimeTestAverage;
            //babyDiapersPenetrationTimeTestValueStandardDeviation.BabyDiaperTestValueRefId = 7;
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

            //------------------Incontinence Pad -----------------
            var testSheet2 = new TestSheet
            {
                TestSheetId = 22,
                FaNr = "FA666",
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                ShiftType = ShiftType.Night,
                MachineNr = "M11",
                ProductName = "Cresta Extra",
                SizeName = "Inko Extra",
                ArticleType = ArticleType.IncontinencePad
            };

            var incontinencePadRewetTestValue1 = new TestValue
            {
                TestValueId = 21,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Single,
                TestSheetRefId = 1
            };
            var incontinencePadRetentionTestValueAverage = new TestValue
            {
                TestValueId = 22,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Average,
                TestSheetRefId = 1
            };
            var incontinencePadRetentionTestValueStandardDeviation = new TestValue
            {
                TestValueId = 23,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetRefId = 1
            };
            var incontinencePadRewetTestValueAverage = new TestValue
            {
                TestValueId = 24,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Average,
                TestSheetRefId = 1
            };
            var incontinencePadRewetTestValueStandardDeviation = new TestValue
            {
                TestValueId = 25,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetRefId = 1
            };
            var incontinencePadAcquisitionTimeTestValueAverage = new TestValue
            {
                TestValueId = 26,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Average,
                TestSheetRefId = 1
            };
            var incontinencePadAcquisitionTimeTestValueStandardDeviation = new TestValue
            {
                TestValueId = 27,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetRefId = 1
            };

            var incontinencePadRewetTest1 = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 21,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RewetFreeRw = RwType.Better,
                RewetFreeDifference = 1.0,
                RewetFreeDryValue = 2.0,
                RewetFreeWetValue = 45.0,
                TestType = TestTypeIncontinencePad.RewetFree
            };
            var incontinencePadRewetTestAverage = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 22,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RewetFreeRw = RwType.Better,
                RewetFreeDifference = 1.0,
                RewetFreeDryValue = 2.0,
                RewetFreeWetValue = 45.0,
                TestType = TestTypeIncontinencePad.RewetFree
            };
            var incontinencePadRewetTestStandardDeviation = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 23,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RewetFreeRw = RwType.Better,
                RewetFreeDifference = 1.0,
                RewetFreeDryValue = 2.0,
                RewetFreeWetValue = 45.0,
                TestType = TestTypeIncontinencePad.RewetFree
            };

            incontinencePadRewetTestValue1.IncontinencePadTestValue = incontinencePadRewetTest1;

            var testNote2 = new TestValueNote
            {
                Error = error2,
                Message = "Testnotiz"
            };
            var incontinencePadRetentionTestValue1 = new TestValue
            {
                TestValueId = 22,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Single,
                TestSheetRefId = 1,
                TestValueNote = new List<TestValueNote> { testNote2 }
            };
            var incontinencePadAcquisitionTimeTestValue1 = new TestValue
            {
                TestValueId = 32,
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                LastEditedDateTime = new DateTime( 2016, 11, 2, 1, 51, 0 ),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.IncontinencePad,
                TestValueType = TestValueType.Single,
                TestSheetRefId = 1,
                TestValueNote = new List<TestValueNote> { testNote2 }
            };
            testNote.TestValue = incontinencePadRetentionTestValue1;
            var incontinencePadRetentionTest1 = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 23,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RetentionRw = RwType.Better,
                RetentionWeight = 2.0,
                RetentionEndValue = 4.0,
                RetentionAbsorbtion = 2.0,
                RetentionWetValue = 12.0,
                RetentionAfterZentrifuge = 2.0,
                TestType = TestTypeIncontinencePad.Retention
            };
            incontinencePadRetentionTestValue1.IncontinencePadTestValue = incontinencePadRetentionTest1;

            var incontinencePadRetentionTestAverage = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 24,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RetentionRw = RwType.Better,
                RetentionWeight = 2.0,
                RetentionEndValue = 4.0,
                RetentionAbsorbtion = 2.0,
                RetentionWetValue = 12.0,
                RetentionAfterZentrifuge = 2.0,
                TestType = TestTypeIncontinencePad.Retention
            };

            var incontinencePadRetentionTestStandardDeviation = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 25,
                IncontinencePadTime = new TimeSpan( 1, 38, 0 ),
                RetentionRw = RwType.Better,
                RetentionWeight = 2.0,
                RetentionEndValue = 4.0,
                RetentionAbsorbtion = 2.0,
                RetentionWetValue = 12.0,
                RetentionAfterZentrifuge = 2.0,
                TestType = TestTypeIncontinencePad.Retention
            };
            var incontinencePadAcquisitionTimeTestAverage = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 26,
                AcquisitionTimeThirdRw = RwType.Better,
                AcquisitionTimeSecondRw = RwType.Better,
                AcquisitionTimeFirstRw = RwType.Better,
                AcquisitionTimeThird = 12,
                AcquisitionTimeFirst = 1,
                AcquisitionWeight = 12,
                AcquisitionTimeSecond = 12,
                RewetAfterAcquisitionTimeRw = RwType.Better,
                RewetAfterAcquisitionTimeDryWeight = 12,
                RewetAfterAcquisitionTimeWeightDifference = 12,
                RewetAfterAcquisitionTimeWetWeight = 14,
                IncontinencePadTime = new TimeSpan( 0, 0, 0 ),
                TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
            };
            var incontinencePadAcquisitionTimeTestStandardDeviation = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 27,
                AcquisitionTimeThirdRw = RwType.Better,
                AcquisitionTimeSecondRw = RwType.Better,
                AcquisitionTimeFirstRw = RwType.Better,
                AcquisitionTimeThird = 12,
                AcquisitionTimeFirst = 1,
                AcquisitionWeight = 12,
                AcquisitionTimeSecond = 12,
                RewetAfterAcquisitionTimeRw = RwType.Better,
                RewetAfterAcquisitionTimeDryWeight = 12,
                RewetAfterAcquisitionTimeWeightDifference = 12,
                RewetAfterAcquisitionTimeWetWeight = 14,
                IncontinencePadTime = new TimeSpan( 0, 0, 0 ),
                TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
            };
            var incontinencePadAcquisitionTimeTest1 = new IncontinencePadTestValue
            {
                IncontinencePadTestValueId = 28,
                AcquisitionTimeThirdRw = RwType.Better,
                AcquisitionTimeSecondRw = RwType.Better,
                AcquisitionTimeFirstRw = RwType.Better,
                AcquisitionTimeThird = 12,
                AcquisitionTimeFirst = 1,
                AcquisitionWeight = 12,
                AcquisitionTimeSecond = 12,
                RewetAfterAcquisitionTimeRw = RwType.Better,
                RewetAfterAcquisitionTimeDryWeight = 12,
                RewetAfterAcquisitionTimeWeightDifference = 12,
                RewetAfterAcquisitionTimeWetWeight = 14,
                IncontinencePadTime = new TimeSpan( 0, 0, 0 ),
                TestType = TestTypeIncontinencePad.AcquisitionTimeAndRewet
            };

            incontinencePadAcquisitionTimeTestValue1.IncontinencePadTestValue = incontinencePadAcquisitionTimeTest1;

            incontinencePadRewetTestValue1.IncontinencePadTestValue = incontinencePadRewetTest1;
            //babyDiapersRewetTestValue1.BabyDiaperTestValueRefId = 1;

            //babyDiapersRewetTestValueAverage.BabyDiaperTestValueRefId = 2;
            incontinencePadRewetTestValueAverage.IncontinencePadTestValue = incontinencePadRewetTestAverage;
            //babyDiapersRewetTestValueStandardDeviation.BabyDiaperTestValueRefId = 3;
            incontinencePadRewetTestValueStandardDeviation.IncontinencePadTestValue = incontinencePadRewetTestStandardDeviation;

            //babyDiapersRetentionTestValueAverage.BabyDiaperTestValueRefId = 4;
            incontinencePadRetentionTestValueAverage.IncontinencePadTestValue = incontinencePadRetentionTestAverage;
            //babyDiapersRetentionTestValueStandardDeviation.BabyDiaperTestValueRefId = 5;
            incontinencePadRetentionTestValueStandardDeviation.IncontinencePadTestValue = incontinencePadRetentionTestStandardDeviation;

            //babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValueRefId = 6;
            babyDiapersPenetrationTimeTestValueAverage.BabyDiaperTestValue = babyDiapersPenetrationTimeTestAverage;
            //babyDiapersPenetrationTimeTestValueStandardDeviation.BabyDiaperTestValueRefId = 7;
            incontinencePadAcquisitionTimeTestValueStandardDeviation.IncontinencePadTestValue = incontinencePadAcquisitionTimeTestStandardDeviation;

            testSheet2.TestValues = new List<TestValue>
            {
                incontinencePadRewetTestValue1,
                incontinencePadRetentionTestValue1,
                incontinencePadAcquisitionTimeTestValue1,
                incontinencePadRetentionTestValueAverage,
                incontinencePadRetentionTestValueStandardDeviation,
                incontinencePadRewetTestValueAverage,
                incontinencePadRewetTestValueStandardDeviation,
                incontinencePadAcquisitionTimeTestValueAverage,
                incontinencePadAcquisitionTimeTestValueStandardDeviation
            };
            context.TestSheets.AddOrUpdate( m => m.FaNr, testSheet2 );
            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadRewetTestValue1 );
            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadRewetTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadRewetTestValueStandardDeviation );

            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadRetentionTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadRetentionTestValueStandardDeviation );

            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadAcquisitionTimeTestValueAverage );
            context.TestValues.AddOrUpdate( m => m.TestValueId, incontinencePadAcquisitionTimeTestValueStandardDeviation );

            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadRewetTest1 );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadRewetTestStandardDeviation );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadRewetTestAverage );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadRetentionTestStandardDeviation );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadRetentionTestAverage );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadAcquisitionTimeTestStandardDeviation );
            context.IncontinencePadTestValues.AddOrUpdate( m => m.IncontinencePadTestValueId, incontinencePadAcquisitionTimeTestAverage );
        }
    }
}