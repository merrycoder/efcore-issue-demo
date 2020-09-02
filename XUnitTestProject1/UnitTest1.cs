using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFGetStarted;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void core_issue()
        {
            /*
             * Demonstrates the core issue: And() and Or() are extension methods
             * used to chain expression to an expression tree in the IS24 search.
             * (https://github.xmedia.ch/Immoscout24/BE-2/blob/da8f478c29aef64028de476e20b0a10215c133db/Src/Shared/Scout24.IS24.Shared.Services/Search/Internal/SearchDataStoreProviders/EFLinqDataStoreSearchProvider.cs#L70 ff).
             * This works on one level (with line 32 outcommented), but not with nested expressions.
             * The issue is tracked in https://github.com/dotnet/efcore/issues/19511 and solved
             * for .NET 5.0 (tested with 5.0.0-preview.2.20120.8).
             */
            using (var db = new PropertyContextInMemory())
            {
                Expression<Func<Property, bool>> expr1 = p => p.IsOnline; // some basic expression

                Expression<Func<Property, bool>> expr2 = p => p.CategoryId == 1; // another expression chained later.

                // here lies the problem: an nested And in expr2
                // outcomment the following line to make the test succeed.
                expr2 = expr2.Or(p => p.PriceTypeId == 2);

                // chain expr1 And expr2: works with no problem, when line 32 is outcommented.
                expr1 = expr1.And(expr2);

                // fails with "The LINQ expression .. could not be translated."
                var result = db.Properties.Where(expr1).ToList();


                /*
                 * I'm aware that this code could easily be replaced by the following:
                 */

                expr1 = p => p.IsOnline && (p.CategoryId == 1 || p.PriceTypeId == 2);

                /*
                 * but this removes the possibility to build the expression tree step by step
                 * and concatenate the parts of it
                 * as done in https://github.xmedia.ch/Immoscout24/BE-2/blob/da8f478c29aef64028de476e20b0a10215c133db/Src/Shared/Scout24.IS24.Shared.Services/Search/Internal/SearchDataStoreProviders/EFLinqDataStoreSearchProvider.cs#L70
                 */

                /*
                 * but IMO the code in https://github.xmedia.ch/Immoscout24/BE-2/blob/da8f478c29aef64028de476e20b0a10215c133db/Src/Shared/Scout24.IS24.Shared.Services/Search/Internal/SearchDataStoreProviders/EFLinqDataStoreSearchProvider.cs#L70
                 * cannot easily be rewritten in the form above and rewriting the search code carries
                 * a major risk and should be avoided IMO. So it would be best the find a solution
                 * without the need to rewrite much of the search code.
                 */
            }
        }
    }
}
