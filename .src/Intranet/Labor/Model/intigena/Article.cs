using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model
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
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
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

        /// <summary>
        ///     Gets or sets the max value of the rewet 140ml test for the article
        /// </summary>
        public Double Rewet140Max { get; set; }

        /// <summary>
        ///     Gets or sets the max value of the rewet 210ml test for the article
        /// </summary>
        public Double Rewet210Max { get; set; }

        /// <summary>
        ///     Gets or sets the min value of the retention test for the article
        /// </summary>
        public Double MinRetention { get; set; }

        /// <summary>
        ///     Gets or sets the max value of the retention test for the article
        /// </summary>
        public Double MaxRetention { get; set; }

        /// <summary>
        ///     Gets or sets the max time of the 4th penetration test for the article
        /// </summary>
        public Double MaxPenetrationAfter4Time { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// Gets or sets the size name
        /// </summary>
        public String SizeName { get; set; }

        #endregion
    }
}