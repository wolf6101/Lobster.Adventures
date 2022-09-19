using System;
using System.Collections.Generic;

using Lobster.Adventures.Domain.Entities;

using PrivateObject = Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject;

namespace Lobster.Adventures.UnitTests.Domain.TestDataProviders
{
    public static class UserJourneyTestDataProvider
    {
        public static UserJourney GetUserJourney(Guid id)
        {
            var adventureId = new Guid("851235b5-9ee1-4985-8c3a-77414270c27c");
            var adventure = AdventureTestDataProvider.GetAdventure(adventureId);

            var journeyId = new Guid("57b90783-e28b-4015-b43e-5af715627e58");
            var userId = new Guid("30566f0d-32ac-4c73-8b1b-fbeeb9cc194d");
            var journey = new UserJourney(journeyId, adventureId, userId);

            var journeyWrapper = new PrivateObject(journey);
            journeyWrapper.SetProperty("Adventure", adventure);

            return journey;
        }
    }
}