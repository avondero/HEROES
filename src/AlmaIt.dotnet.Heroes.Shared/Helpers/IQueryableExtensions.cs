using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;

namespace AlmaIt.dotnet.Heroes.Shared.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrEmpty(sortBy))
            {
                return source;
            }

            var sortExpression = string.Empty;

            var listSortBy = sortBy.Split(',');
            foreach (var item in listSortBy)
            {
                sortExpression += AdjustDirection(item) + ",";
            }

            sortExpression = sortExpression.Substring(0, sortExpression.Length - 1);

            try
            {
                source = source.OrderBy(sortExpression);
            }
            catch (Exception ex)
            {
                // sortBy include field not part of the model
            }

            return source;
        }

        private static string AdjustDirection(string item)
        {
            if (!item.Contains(' '))
                return item; // no direction specified

            var field = item.Split(' ') [0];
            var direction = item.Split(' ') [1];

            switch (direction)
            {
                case "asc":
                case "ascending":
                    return field + " ascending";

                case "desc":
                case "descending":
                    return field + " descending";

                default:
                    return field;
            };
        }
    }
}
