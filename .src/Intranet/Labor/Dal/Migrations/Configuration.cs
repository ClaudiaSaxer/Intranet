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
            context.Machines.AddOrUpdate( m => m.MachineId, machine1, machine2, machine3);

            var article1 = new Article
            {
                ArticleNr = "10401",
                Name = "Babydream Maxi-Plus",
                ArticleType = ArticleType.BabyDiaper
            };
            context.Articles.AddOrUpdate( a => a.ArticleId, article1 );

            var productionOrderComponent1 = new ProductionOrderComponent
            {
                SAP = 32.7,
                WithoutSAP = 31.2,
                ComponentType = "EKX",
                ComponentNr = "EN67"
            };

            var productionOrder1 = new ProductionOrder
            {
                FaNr = "FA123456",
                StartDateTime = new DateTime(2016,1,1),
                EndDateTime = new DateTime(2018,1,1),
                Machine = machine2,
                Component = productionOrderComponent1,
                Article = article1
            };
            productionOrderComponent1.ProductionOrder = productionOrder1;

            context.ProductionOrderComponent.AddOrUpdate( p => p.ProductionOrderComponentId, productionOrderComponent1 );
            context.ProductionOrders.AddOrUpdate( p => p.FaId, productionOrder1 );

            ////////////////////////////////////////////////////

            var testSheet = new TestSheet
            {
                TestSheetId = 1,
                FaNr = "FA123456",
                CreatedDateTime = new DateTime( 2016, 11, 2, 1, 50, 0 ),
                ShiftType = ShiftType.Night,
                MachineNr = "11",
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
                CreatedDateTime = new DateTime(2016, 11, 2,1,50,0),
                LastEditedDateTime = new DateTime(2016, 11, 2,1,50,0),
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
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Average,
                TestSheetRefId = 1
            };
            var babyDiapersRewetTestValueStandardDeviation = new TestValue
            {
                TestValueId = 5,
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.StandardDeviation,
                TestSheetRefId = 1
            };
            var babyDiapersPenetrationTimeTestValueAverage = new TestValue
            {
                TestValueId = 6,
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Average,
                TestSheetRefId = 1
            };
            var babyDiapersPenetrationTimeTestValueStandardDeviation = new TestValue
            {
                TestValueId = 7,
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 50, 0),
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
                DiaperCreatedTime = new TimeSpan(1, 38, 0),
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
                DiaperCreatedTime = new TimeSpan(1, 38, 0),
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

            var babyDiapersRetentionTestValue1 = new TestValue
            {
                TestValueId = 2,
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 51, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 51, 0),
                DayInYearOfArticleCreation = 307,
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Single,
                TestSheetRefId = 1
            };
            var babyDiapersRetentionTest1 = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 2,
                DiaperCreatedTime = new TimeSpan(1, 38, 0),
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
                DiaperCreatedTime = new TimeSpan(0, 0, 0),
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
                DiaperCreatedTime = new TimeSpan(0, 0, 0),
                WeightDiaperDry = 0,
                RetentionRw = RwType.Ok,
                RetentionAfterZentrifugeValue = 0,
                RetentionWetWeight = 0,
                RetentionAfterZentrifugePercent = 0,
                TestType = TestTypeBabyDiaper.Retention
            };
            var babyDiapersPenetrationTimeTestAverage = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId =6,
                DiaperCreatedTime = new TimeSpan(0, 0, 0),
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
                DiaperCreatedTime = new TimeSpan(0, 0, 0),
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

            context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersRetentionTestValueAverage);
            context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersRetentionTestValueStandardDeviation);

            context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersPenetrationTimeTestValueAverage);
            context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersPenetrationTimeTestValueStandardDeviation);

            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRewetTest1 );
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRewetTestStandardDeviation);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRewetTestAverage);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRetentionTestStandardDeviation);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRetentionTestAverage);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersPenetrationTimeTestStandardDeviation);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersPenetrationTimeTestAverage);
        }
    }
}