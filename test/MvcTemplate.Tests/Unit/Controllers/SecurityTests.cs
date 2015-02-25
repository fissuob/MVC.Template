﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Xunit;

namespace MvcTemplate.Tests.Unit.Controllers
{
    public class SecurityTests
    {
        #region ValidateAntiForgeryToken

        [Fact]
        public void AllControllerPostMethods_HasValidateAntiForgeryToken()
        {
            IEnumerable<MethodInfo> postMethods = Assembly
                .Load("MvcTemplate.Controllers")
                .GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.GetCustomAttribute<HttpPostAttribute>() != null);

            foreach (MethodInfo method in postMethods)
                Assert.NotNull(method.GetCustomAttribute<ValidateAntiForgeryTokenAttribute>());
        }

        #endregion
    }
}
