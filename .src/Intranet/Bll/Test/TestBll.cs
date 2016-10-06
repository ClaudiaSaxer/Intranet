using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Intranet.Common;
using Intranet.Dal;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    /// </summary>
    public class TestBll : ITestBll
    {
        #region Fields

        private readonly ILoggerFactory _loggerFactory;

        #endregion

        #region Properties

        private GenericRepository<IntranetContext, Test> TestRepository { get; set; }
        private IDatabaseFactory<IntranetContext> DbFactory { get; set; }
        private IDbCommit<IntranetContext> DbCommit { get; set; }

        #endregion

        /// <summary>
        ///     Add Test to DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on AddTest</param>
        public void AddTest( Test test )
        {
            /*using ( var factory = new DbFactory<IntranetContext>( _loggerFactory ) )
                using ( var repo = new TestRepository( factory, _loggerFactory ) )
                    using ( var dbCommit = new DbCommit<IntranetContext>( factory, _loggerFactory ) )
                    {
                        repo.DbCommit = dbCommit;
                        repo.Add( test );
                        try
                        {
                            repo.SaveChanges();
                        }
                        catch ( DBConcurrencyException )
                        {
                            HandleDbConcurrencyException( new DbFactory<IntranetContext>( _loggerFactory ).GetDb(), test );
                        }
                    }*/

            using ( DbFactory )
                using ( TestRepository )
                    using ( DbCommit )
                    {
                        TestRepository.DbCommit = DbCommit;
                        TestRepository.Add( test );
                        try
                        {
                            TestRepository.SaveChanges();
                        }
                        catch ( DBConcurrencyException )
                        {
                            HandleDbConcurrencyException( DbFactory.GetDb(), test );
                        }
                    }
        }

        /// <summary>
        ///     Gets Tests from DB
        /// </summary>
        public IEnumerable<Test> AllTests()
        {
            using ( TestRepository )

                return TestRepository.GetAll()
                                     .AsNoTracking()
                                     .ToList();

            /*  using ( var repo = new TestRepository( new DbFactory<IntranetContext>( _loggerFactory ), _loggerFactory ) )
                  return repo.GetAll()
                             .AsNoTracking()
                             .ToList();*/
        }

        /// <summary>
        ///     Deletes Test from DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on RemoveTest</param>
        public void RemoveTest( Test test )
        {
            /*   using ( var factory = new DbFactory<IntranetContext>( _loggerFactory ) )
                   using ( var repo = new TestRepository( factory, _loggerFactory ) )
                       using ( var dbCommit = new DbCommit<IntranetContext>( factory, _loggerFactory ) )

                       {
                           repo.DbCommit = dbCommit;
                           test = repo.Attach( test );
                           repo.Remove( test );
                           try
                           {
                               repo.SaveChanges();
                           }
                           catch ( DBConcurrencyException )
                           {
                               HandleDbConcurrencyException( new DbFactory<IntranetContext>( _loggerFactory ).GetDb(), test );
                           }
                       }*/

            using ( DbFactory )
                using ( TestRepository )
                    using ( DbCommit )
                    {
                        TestRepository.DbCommit = DbCommit;
                        test = TestRepository.Attach( test );
                        TestRepository.Remove( test );
                        try
                        {
                            TestRepository.SaveChanges();
                        }
                        catch ( DBConcurrencyException )
                        {
                            HandleDbConcurrencyException( DbFactory.GetDb(), test );
                        }
                    }
        }

        /// <summary>
        ///     Update Car from DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on UpdateTest</param>
        /// <param name="original">todo: describe original parameter on UpdateTest</param>
        public void UpdateTest( Test test, Test original )
        {
            /*  using ( var factory = new DbFactory<IntranetContext>( _loggerFactory ) )
                  using ( var repo = new TestRepository( factory, _loggerFactory ) )
                      using ( var dbCommit = new DbCommit<IntranetContext>( factory, _loggerFactory ) )
                      {
                          repo.DbCommit = dbCommit;
                          test = repo.Attach( test );
                          repo.SetModified( test );
                          try
                          {
                              repo.SaveChanges();
                          }
                          catch ( DBConcurrencyException )
                          {
                              HandleDbConcurrencyException( new DbFactory<IntranetContext>( _loggerFactory ).GetDb(), original );
                          }
                      }*/
            using ( DbFactory )
                using ( TestRepository )
                    using ( DbCommit )
                    {
                        TestRepository.DbCommit = DbCommit;
                        test = TestRepository.Attach( test );
                        TestRepository.SetModified( test );
                        try
                        {
                            TestRepository.SaveChanges();
                        }
                        catch ( DBConcurrencyException )
                        {
                            HandleDbConcurrencyException( DbFactory.GetDb(), original );
                        }
                    }
        }

        private static void HandleDbConcurrencyException<T>( IntranetContext context, T original ) where T : class
        {
            var databaseValue = context.Entry( original )
                                       .GetDatabaseValues();
            context.Entry( original )
                   .CurrentValues.SetValues( databaseValue );
            throw new LocalOptimisticConcurrencyException<T>( $"Update {typeof(T).Name}: Concurrency-Fehler", original );
        }
    }
}