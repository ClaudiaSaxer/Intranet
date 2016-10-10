using System.Linq;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    /// </summary>
    public class TestBll : ITestBll
    {
        #region Properties

        /// <summary>
        /// </summary>
        public IGenericRepository<Test> TestRepository { get; set; }


        #endregion

        /// <summary>
        ///     Add Test to DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on AddTest</param>
        public void AddTest( Test test ) => TestRepository.Add( test );

        /// <summary>
        ///     Gets Tests from DB
        /// </summary>
        public IQueryable<Test> AllTests()
            => TestRepository.GetAll();

        /// <summary>
        ///     Deletes Test from DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on RemoveTest</param>
        public void RemoveTest( Test test ) => TestRepository.Remove( test );

        /// <summary>
        ///     Update Car from DB
        /// </summary>
        /// <param name="test">todo: describe test parameter on UpdateTest</param>
        /// <param name="original">todo: describe original parameter on UpdateTest</param>
        public void UpdateTest( Test test, Test original )
        {
        }

    }
}