﻿using Datalist;
using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Resources;
using MvcTemplate.Tests.Objects;
using NSubstitute;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Xunit;
using Xunit.Extensions;

namespace MvcTemplate.Tests.Unit.Components.Datalists
{
    public class BaseDatalistTests : IDisposable
    {
        private BaseDatalistProxy<Role, RoleView> datalist;
        private IUnitOfWork unitOfWork;

        public BaseDatalistTests()
        {
            HttpContext.Current = HttpContextFactory.CreateHttpContext();
            datalist = new BaseDatalistProxy<Role, RoleView>();
            unitOfWork = Substitute.For<IUnitOfWork>();
        }
        public void Dispose()
        {
            HttpContext.Current = null;
        }

        #region Constructor: BaseDatalist()

        [Fact]
        public void BaseDatalist_SetsDialogTitle()
        {
            datalist = new BaseDatalistProxy<Role, RoleView>();

            String expected = ResourceProvider.GetDatalistTitle<Role>();
            String actual = datalist.DialogTitle;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BaseDatalist_SetsDatalistUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            datalist = new BaseDatalistProxy<Role, RoleView>();
            UrlHelper url = new UrlHelper(request.RequestContext);

            String expected = url.Action(typeof(Role).Name, AbstractDatalist.Prefix, new { area = "" });
            String actual = datalist.DatalistUrl;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Constructor: BaseDatalist(IUnitOfWork unitOfWork)

        [Fact]
        public void BaseDatalist_SetsUnitOfWork()
        {
            datalist = new BaseDatalistProxy<Role, RoleView>(unitOfWork);

            IUnitOfWork actual = datalist.BaseUnitOfWork;
            IUnitOfWork expected = unitOfWork;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Method: GetColumnHeader(PropertyInfo property)

        [Fact]
        public void GetColumnHeader_ReturnsPropertyTitle()
        {
            String actual = datalist.BaseGetColumnHeader(typeof(RoleView).GetProperty("Name"));
            String expected = ResourceProvider.GetPropertyTitle(typeof(RoleView), "Name");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetColumnHeader_ReturnsRelationPropertyTitle()
        {
            PropertyInfo property = typeof(AllTypesView).GetProperty("Child");

            String actual = datalist.BaseGetColumnHeader(property);
            String expected = "";

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Method: GetColumnCssClass(PropertyInfo property)

        [Theory]
        [InlineData("EnumField", "text-cell")]
        [InlineData("SByteField", "number-cell")]
        [InlineData("ByteField", "number-cell")]
        [InlineData("Int16Field", "number-cell")]
        [InlineData("UInt16Field", "number-cell")]
        [InlineData("Int32Field", "number-cell")]
        [InlineData("UInt32Field", "number-cell")]
        [InlineData("Int64Field", "number-cell")]
        [InlineData("UInt64Field", "number-cell")]
        [InlineData("SingleField", "number-cell")]
        [InlineData("DoubleField", "number-cell")]
        [InlineData("DecimalField", "number-cell")]
        [InlineData("DateTimeField", "date-cell")]

        [InlineData("NullableEnumField", "text-cell")]
        [InlineData("NullableSByteField", "number-cell")]
        [InlineData("NullableByteField", "number-cell")]
        [InlineData("NullableInt16Field", "number-cell")]
        [InlineData("NullableUInt16Field", "number-cell")]
        [InlineData("NullableInt32Field", "number-cell")]
        [InlineData("NullableUInt32Field", "number-cell")]
        [InlineData("NullableInt64Field", "number-cell")]
        [InlineData("NullableUInt64Field", "number-cell")]
        [InlineData("NullableSingleField", "number-cell")]
        [InlineData("NullableDoubleField", "number-cell")]
        [InlineData("NullableDecimalField", "number-cell")]
        [InlineData("NullableDateTimeField", "date-cell")]

        [InlineData("StringField", "text-cell")]
        [InlineData("Child", "text-cell")]
        public void GetColumnCssClass_ReturnsCssClassForPropertyType(String propertyName, String expected)
        {
            PropertyInfo property = typeof(AllTypesView).GetProperty(propertyName);

            String actual = datalist.BaseGetColumnCssClass(property);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Method: GetModels()

        [Fact]
        public void GetModels_ReturnsModelsFromUnitOfWork()
        {
            unitOfWork.Select<Role>().To<RoleView>().Returns(Enumerable.Empty<RoleView>().AsQueryable());
            datalist = new BaseDatalistProxy<Role, RoleView>(unitOfWork);

            IQueryable expected = unitOfWork.Select<Role>().To<RoleView>();
            IQueryable actual = datalist.BaseGetModels();

            Assert.Same(expected, actual);
        }

        #endregion
    }
}
