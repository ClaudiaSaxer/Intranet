using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Error = Intranet.Labor.Model.Error;

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
                ErrorId = 80,
                Value = "Fixtape fehlt"
            };
            var error2 = new Error
            {
                ErrorId = 411,
                Value = "Saugkissen hinten zu kurz"
            };
            var error3 = new Error
            {
                ErrorId = 23,
                Value = "Zu wenig Inhalt"
            };
            var error4 = new Error
            {
                ErrorId = 802,
                Value = "Linke Seite reisst auf"
            };
            context.Errors.AddOrUpdate(e => e.ErrorId,error1,error2,error3,error4);

            var testSheet = new TestSheet
            {
                TestSheetId = 1,
                FaNr = "FA123456",
                CreatedDateTime = new DateTime(2016,11,2,1,50,0),
                ShiftType = ShiftType.Night,
                MachineNr = "11",
                SAPType = "EKX",
                SAPNr = "EN67",
                ProductName = "Babydream",
                SizeName = "Maxi-Plus",
                ArticleType = ArticleType.BabyDiaper,
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
                CreatedPerson = "Hans",
                LastEditedPerson = "Hans",
                ArticleTestType = ArticleType.BabyDiaper,
                TestValueType = TestValueType.Single,
                TestSheetRefId = 1
            };
            var babyDiapersRewetTest1 = new BabyDiaperTestValue
            {
                BabyDiaperTestValueId = 1,
                DiaperCreatedTime = new TimeSpan( 1, 38, 0 ),
                WeightDiaperDry = 32.9,
                Revert140Value = 0.1,
                Revet210Value = 0.18,
                StrikeTroughValue = 0.28,
                DistributionOfTheStrikeTrough = 240,
                Revet140Rw = RwType.Ok,
                Revet210Rw = RwType.Ok,
                TestType = TestTypeBabyDiaper.Rewet
            };
            babyDiapersRewetTestValue1.BabyDiaperTestValue = babyDiapersRewetTest1;
            babyDiapersRewetTestValue1.BabyDiaperTestValueRefId = 1;

            var babyDiapersRetentionTestValue1 = new TestValue
            {
                TestValueId = 2,
                CreatedDateTime = new DateTime(2016, 11, 2, 1, 51, 0),
                LastEditedDateTime = new DateTime(2016, 11, 2, 1, 51, 0),
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
            babyDiapersRetentionTestValue1.BabyDiaperTestValueRefId = 2;


            testSheet.TestValues = new List<TestValue>
            {
                babyDiapersRewetTestValue1,
                babyDiapersRetentionTestValue1
            };
            //babyDiapersRetentionTestValue1.TestSheet = testSheet;*/

            //context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRetentionTest1);
            //context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersRetentionTestValue1);
            context.TestSheets.AddOrUpdate(m => m.FaNr, testSheet);
            context.TestValues.AddOrUpdate(m => m.TestValueId, babyDiapersRewetTestValue1, babyDiapersRetentionTestValue1);
            context.BabyDiaperTestValues.AddOrUpdate(m => m.BabyDiaperTestValueId, babyDiapersRewetTest1, babyDiapersRetentionTest1);
        }
    }
}