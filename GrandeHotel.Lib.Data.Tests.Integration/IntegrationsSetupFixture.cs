using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Tests.Integration
{
    [SetUpFixture]
    public class IntegrationsSetupFixture : IntegrationsTestBase
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            ClearDatabase();
            MigrateDatabase();
        }
    }
}
