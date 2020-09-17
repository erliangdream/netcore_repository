using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Trials.Kevin.XUnitOfTest
{
    [CollectionDefinition("SaleOrderDataCollection")]
    public class SaleOrderData : ICollectionFixture<SaleOrderFixture>
    {

    }
}
