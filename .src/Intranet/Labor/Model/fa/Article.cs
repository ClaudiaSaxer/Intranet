using System;
using System.Collections.Generic;

namespace Intranet.Labor.Model.fa
{
    /// <summary>
    ///     The class representing an article.
    /// </summary>
    public class Article
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the article
        /// </summary>
        /// <value>the id of the article</value>
        public Int32 ArticleId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the article
        /// </summary>
        /// <value>the number of the article</value>
        [UniqueKey]
        public String ArticleNr { get; set; }

        /// <summary>
        ///     Gets or sets the name of the article
        /// </summary>
        /// <value>the name of the article</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the type of the article <see cref="ArticleType" />
        /// </summary>
        /// <value>the type of the article</value>
        public ArticleType ArticleType { get; set; }

        /// <summary>
        ///     Gets or sets the production orders of the article <see cref="ProductionOrder" />
        /// </summary>
        /// <value>the production orders of the article </value>
        public ICollection<ProductionOrder> ProductionOrders { get; set; }

        #endregion
    }
}